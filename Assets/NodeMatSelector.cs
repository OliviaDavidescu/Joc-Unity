using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMatSelector: MonoBehaviour
{
    [SerializeField] List<Material> mazeNodeFloorMats;
    [SerializeField] List<Material> mazeNodeWallMats;
    [SerializeField] Material victoryNodeMat;
    GameObject floor;
    // de adaugat lista de pereti si index pt material pereti
    public static int mazeFloorMatIndex =0;


    // Start is called before the first frame update
    void Start()
    {
        MazeNode nod = GetComponentInChildren<MazeNode>();
        floor = transform.GetChild(0).gameObject; // primul copil e podeaua
        floor.GetComponent<Renderer>().material = mazeNodeFloorMats[mazeFloorMatIndex];  
        if (nod.TriggersVictory())
        {
            floor.GetComponent<Renderer>().material = victoryNodeMat; 
        }
    }

    
}
