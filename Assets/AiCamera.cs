using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCamera : MonoBehaviour
{
    Transform target;
    int distance = 2;

    void Start()
    {
        if (MazeGenerator.AI)
        {
            transform.position = new Vector3(0, distance, 0);
            transform.rotation = Quaternion.Euler(90, 0, 0);
            target = SphereGenerator.Sphere.transform;
        }
    }

    void Update()
    {
        if (MazeGenerator.AI)
        {
            if(target != null) { transform.position = new Vector3(target.position.x, distance, target.position.z); }
        }
    }
}
