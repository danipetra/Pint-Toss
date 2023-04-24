using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TrajectorySimulator : MonoBehaviour
{
    private Scene simulationScene;
    private PhysicsScene physicsScene;
    private List<Transform> collidingObjects;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int maxPhysicsFrameIterations = 30;

    private void Start() {
        InitCollidingObjects();
        CreatePhysicsSimScene();
    }

    private void InitCollidingObjects(){
        collidingObjects.Add(GameObject.FindGameObjectWithTag("Backboard").transform);
        collidingObjects.Add(GameObject.FindGameObjectWithTag("Floor").transform);
        collidingObjects.Add(GameObject.FindGameObjectWithTag("Objective").transform);
    }

    private GameObject InstantiateGhostObject(Transform objectTransform, Vector3 position, Quaternion rotation){
            GameObject ghost = Instantiate(objectTransform.gameObject, position, rotation);
            ghost.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghost, simulationScene);
            return ghost;
    }

    private void CreatePhysicsSimScene(){
        simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        physicsScene = simulationScene.GetPhysicsScene();
        foreach(Transform objT in collidingObjects){
            InstantiateGhostObject(objT, objT.position, objT.rotation);
        }
    }

    public void SimulateTrajectory(GameObject throwable, Vector3 pos, float swipeIntensity){
        
        var ghost = InstantiateGhostObject(throwable.transform, throwable.transform.position, throwable.transform.rotation);
        //simulate the throw and the trajectory of the throwable
        ghost.GetComponent<Pint>().GetThrower().GetComponent<Player>().ThrowPint(swipeIntensity);
        //render the line to show the trajectory
        lineRenderer.positionCount = maxPhysicsFrameIterations;
        for(int i  = 0; i < maxPhysicsFrameIterations; i++){
                                    //Maybe you need to cange it to deltaTime
            physicsScene.Simulate(Time.fixedDeltaTime);
            lineRenderer.SetPosition(i, ghost.transform.position);
        }
    
    }

    

    
}
