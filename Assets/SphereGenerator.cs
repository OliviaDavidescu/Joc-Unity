using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SphereGenerator : MonoBehaviour
{   
    [SerializeField] List<Material> sphereMats;
    [SerializeField] GameObject spherePrefab;

    public static int sphereMatIndex = 0;
    public static GameObject Sphere;

    void Start()
    {
        GameObject mazeGenGameObj = GameObject.Find("Maze Generator"); //alternativ se putea folosi static function
        MazeGenerator mazeGenObj = mazeGenGameObj.GetComponent<MazeGenerator>();
        Vector3 spherePoz = mazeGenObj.getCorner();

        Sphere = Instantiate(spherePrefab, spherePoz, Quaternion.identity, transform); // se creaza sfera 
        Sphere.GetComponent<Renderer>().material = sphereMats[sphereMatIndex]; // se selecteaza materialul
        Sphere.tag = "Player";
    }

    void Update()
    {
/*        Rigidbody rb = Sphere.GetComponent<Rigidbody>();
        if (Input.GetKeyDown(KeyCode.H))
            rb.useGravity = false;*/
    }

}
