using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TrajectorySimulator : MonoBehaviour
{
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private List<Transform> _collidingObjects;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _maxPhysicsFrameIterations = 30;

    private void Start() 
    {
        InitCollidingObjects();
        CreatePhysicsSimScene();
    }
    
    /**/
    private void InitCollidingObjects(){
        _collidingObjects.Add(GameObject.FindGameObjectWithTag("Backboard").transform);
        _collidingObjects.Add(GameObject.FindGameObjectWithTag("Floor").transform);
        _collidingObjects.Add(GameObject.FindGameObjectWithTag("Objective").transform);
    }

    /**/
    private GameObject InstantiateGhostObject(Transform objectTransform, Vector3 position, Quaternion rotation){
            GameObject ghost = Instantiate(objectTransform.gameObject, position, rotation);
            ghost.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghost, _simulationScene);
            return ghost;
    }

    /* Creates an invisible scene used to simulate the throwable object's trajectory */
    private void CreatePhysicsSimScene(){
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();
        foreach(Transform objT in _collidingObjects){
            InstantiateGhostObject(objT, objT.position, objT.rotation);
        }
    }

    /* Uses a line renderer to show the throwable trajectory using the invisible scene created at the start */
    public void SimulateTrajectory(GameObject throwable, Vector3 pos, float swipeIntensity){
        
        var ghost = InstantiateGhostObject(throwable.transform, throwable.transform.position, throwable.transform.rotation);
        //simulate the throw and the trajectory of the throwable using the ghost object
        ghost.GetComponent<Pint>().GetThrower().GetComponent<Player>().ThrowPint(swipeIntensity);
        //render the line to show the trajectory
        _lineRenderer.positionCount = _maxPhysicsFrameIterations;
        for(int i  = 0; i < _maxPhysicsFrameIterations; i++){
                                    //Maybe you need to cange it to deltaTime
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _lineRenderer.SetPosition(i, ghost.transform.position);
        }
        Destroy(ghost);
    }
}
