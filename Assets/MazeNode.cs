using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NodeState
{
    Available,
    Current,
    Completed,
    Victory
}

public class MazeNode : MonoBehaviour
{
    //[SerializeField] Material VictoryFloorMat ;
    [SerializeField] GameObject[] walls;
    [SerializeField] MeshRenderer floor;
    [SerializeField] public NavMeshSurface surface;
    bool triggersVictory = false;
    bool isOver = false;
    public bool TriggersVictory()
    {
        return triggersVictory;
    }

    public bool gameEnd()
    {
        return isOver;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colziiune nod");
        if(triggersVictory)
        {
            Debug.Log("coliziune nod victorie");

            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("coliziune sfera");
                Destroy(collision.gameObject);
                
                isOver = true;

                /*GameObject otherObject = GameObject.Find("Maze Generator");
                
                otherObject.transform.rotation = Quaternion.Euler(0, 0, 0);

                MazeGenerator mazga = otherObject.GetComponent<MazeGenerator>();
                Vector3 spherePoz = mazga.getCorner();
                // Generate a new sphere at the specified position
                GameObject sphere = SphereScript.CreateSphere(spherePoz, new Vector3(0.5f,0.5f,0.5f));
                sphere.tag = "Player";*/
            }
        }
    }

    public void RemoveWall(int wallToRemove)
    {
        //walls[wallToRemove].gameObject.SetActive(false);
        Destroy(walls[wallToRemove]);
    }
    public void SetState (NodeState state)
    {
        switch(state)
        {
            case NodeState.Available:
                floor.material.color=Color.white;
                break;

            case NodeState.Current:
                floor.material.color=Color.yellow;
                break;

            case NodeState.Completed:
                floor.material.color = Color.blue;
                break;
            case NodeState.Victory:
                //floor.material.color=Color.green;
                //floor.GetComponent<Renderer>().material = VictoryFloorMat;
                this.triggersVictory = true;
                this.tag = "Finish";
                break;



        }
    }

}
