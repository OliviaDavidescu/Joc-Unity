using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.AI;


public class MazeGenerator : MonoBehaviour
{

    [SerializeField] MazeNode nodePrefab;
    [SerializeField] Vector2Int mazeSize;
    [SerializeField] float nodeSize;
    [SerializeField] float velocity = 50;
    [SerializeField] List<NavMeshSurface> surfaces = new List<NavMeshSurface>();
    public static bool AI;

    private void Start()
    {
        //AI = MainMenu.AI;

        GenerateMazeInstant(mazeSize);
        //StartCoroutine(GenerateMaze(mazeSize));

        if (AI)
        {
            Rigidbody rb = SphereGenerator.Sphere.GetComponent<Rigidbody>();

            // Frezee sphere
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rb.velocity = Vector3.zero;

            // Deactivate gravity
            rb.useGravity = false;

            // Stop rotating

            // Bake
            foreach (var surface in surfaces)
            {
                surface.BuildNavMesh();
            }
        }
    }
    public Vector3 getCorner()
    {
        Vector3 corner;
        if (mazeSize.x % 2 == 0)
            corner = new Vector3(-mazeSize.x / 2, 1, -mazeSize.y / 2);

        else corner = new Vector3(-0.5f - mazeSize.x / 2, 1, -0.5f - mazeSize.y / 2);
        return corner;
    }

    public Vector3 getVictoryCorner()
    {
        Vector3 corner;
        if (mazeSize.x % 2 == 0)
            corner = new Vector3(mazeSize.x / 2, 1, mazeSize.y / 2);

        else corner = new Vector3(0.5f + mazeSize.x / 2, 1, 0.5f + mazeSize.y / 2);
        return corner;
    }

    private void Update()
    {
        if (AI)
        {
            // Stick to the ball
            
        }
        else
        {
            float zRotation = this.transform.eulerAngles.z;
            float xRotation = this.transform.eulerAngles.x;
            float yRotation = this.transform.eulerAngles.y;

            if ((int)zRotation == 35 || (int)zRotation == 36)
            {
                zRotation = 34.9f;
                transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
            }

            if ((int)zRotation == 325 || (int)zRotation == 324)
            {
                zRotation = 326f;
                transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
            }

            if ((int)xRotation == 35 || (int)xRotation==36)
            {
                xRotation = 34.9f;
                transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
            }

            if ((int)xRotation == 325 || (int)xRotation==324)
            {
                xRotation = 326f;
                transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
            }

            if (Input.GetKey(KeyCode.LeftArrow) && (zRotation < 35 || zRotation > 325))
            {

                transform.Rotate(Vector3.forward * velocity * Time.deltaTime);

            }

            if (Input.GetKey(KeyCode.RightArrow) && (zRotation < 35 || zRotation > 325))
            {
                transform.Rotate(-Vector3.forward * velocity * Time.deltaTime);

            }

            if (Input.GetKey(KeyCode.UpArrow) && (xRotation < 35 || xRotation > 325))
            {
                //transform.Rotate(Vector3.right * velocity * Time.deltaTime);
                transform.rotation = Quaternion.Euler(xRotation + velocity *
                    Time.deltaTime, yRotation, zRotation);

            }

            if (Input.GetKey(KeyCode.DownArrow) && (xRotation <= 35 || xRotation >= 325))
            {
                //transform.Rotate(-Vector3.right * velocity * Time.deltaTime);
                transform.rotation = Quaternion.Euler(xRotation + -velocity *
                    Time.deltaTime, yRotation, zRotation);
            }
        }
    }

    void GenerateMazeInstant(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        // Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f));
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);

                if (AI) { surfaces.Add(newNode.surface); }
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // Choose starting node
        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
        currentPath[0].SetState(NodeState.Current);

        while (completedNodes.Count < nodes.Count)
        {
            // Check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                // Check node to the right of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if (currentNodeX > 0)
            {
                // Check node to the left of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if (currentNodeY < size.y - 1)
            {
                // Check node above the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if (currentNodeY > 0)
            {
                // Check node below the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // Choose next node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
                chosenNode.SetState(NodeState.Current);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath[currentPath.Count - 1].SetState(NodeState.Completed);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
        nodes.Last().SetState(NodeState.Victory);
        nodes.Last().AddComponent<BoxCollider>();   
    }

    IEnumerator GenerateMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        // Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f));
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);

                yield return null;
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // Choose starting node
        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
        currentPath[0].SetState(NodeState.Current);

        while (completedNodes.Count < nodes.Count)
        {
            // Check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                // Check node to the right of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if (currentNodeX > 0)
            {
                // Check node to the left of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if (currentNodeY < size.y - 1)
            {
                // Check node above the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if (currentNodeY > 0)
            {
                // Check node below the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // Choose next node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
                chosenNode.SetState(NodeState.Current);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath[currentPath.Count - 1].SetState(NodeState.Completed);
                currentPath.RemoveAt(currentPath.Count - 1);
               

            }

            yield return new WaitForSeconds(0.05f);
        }
    }
   
}