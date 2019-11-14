using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFeatureGeneration : MonoBehaviour
{
    [SerializeField] GameObject pinetree;
    [SerializeField] GameObject fireflyExample;

    private List<GameObject[,,]> worldData;
    List<Vector3> treePositions = new List<Vector3>();
    List<Vector3> fireflyPositions = new List<Vector3>();
    List<GameObject> trees = new List<GameObject>();
    List<GameObject> fireflies = new List<GameObject>();
    float minX, maxX, minZ, maxZ, minY;

    void Start()
    {
        worldData = new List<GameObject[,,]>();
    }

    public void beginGeneration(List<GameObject[,,]> wd)
    {
        worldData = wd;
        minX = worldData[0][0, 0, 0].transform.position.x;
        minY = worldData[0][0, 0, 0].transform.position.y;
        minZ = worldData[0][0, 0, 0].transform.position.z;
        maxX = minX;
        maxZ = minZ;

        foreach (GameObject[,,] cubeList in worldData)
        {
            foreach (GameObject cube in cubeList)
            {
                if (cube != null)
                {
                    if (cube.transform.position.x < minX)
                    {
                        minX = cube.transform.position.x;
                    }
                    if (cube.transform.position.x > maxX)
                    {
                        maxX = cube.transform.position.x;
                    }
                    if (cube.transform.position.z < minZ)
                    {
                        minZ = cube.transform.position.z;
                    }
                    if (cube.transform.position.z > maxZ)
                    {
                        maxZ = cube.transform.position.z;
                    }
                    if (cube.transform.position.y < minY)
                    {
                        minY = cube.transform.position.y;
                    }
                }
            }
        }

        generateTrees();
        applyTextures();
        generateFireflies();
        centerMap();
    }

    private void centerMap()
    {
        foreach (GameObject[,,] cubeList in worldData)
        {
            foreach (GameObject cube in cubeList)
            {
                if (cube != null)
                {
                    cube.transform.position = new Vector3(cube.transform.position.x - (maxX / 2), cube.transform.position.y, cube.transform.position.z - (maxX / 2));
                }
            }
        }

        foreach (GameObject tree in trees)
        {
            tree.transform.position = new Vector3(tree.transform.position.x - (maxX / 2), tree.transform.position.y, tree.transform.position.z - (maxX / 2));
        }

        foreach (GameObject firefly in fireflies)
        {
            firefly.transform.position = new Vector3(firefly.transform.position.x - (maxX / 2), firefly.transform.position.y, firefly.transform.position.z - (maxX / 2));
        }
    }

    private void generateTrees()
    {
        int treeNum = 400;
        int newPosAttempt;
        float randomX;
        float randomZ;

        while (treeNum > 0)
        {
            randomX = Mathf.Floor(Random.Range(minX, maxX)) + 0.05f;
            randomZ = Mathf.Floor(Random.Range(minZ, maxZ)) + 0.05f;
            newPosAttempt = 0;

            for (int i = 0; i < treePositions.Count; i++)
            {
                RaycastHit hit;
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                if (hit.collider == null || Physics.CheckSphere(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 1.8f, randomZ), 1.2f) || Vector3.Distance(new Vector3(randomX, 0.5f, randomZ), treePositions[i]) < 5f)
                {
                    randomX = Mathf.Floor(Random.Range(minX, maxX)) + 0.05f;
                    randomZ = Mathf.Floor(Random.Range(minZ, maxZ)) + 0.05f;
                    i = 0;
                    newPosAttempt++;
                }
                if (newPosAttempt == 100)
                {
                    break;
                }
            }
            if (newPosAttempt != 100)
            {
                RaycastHit hit;
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                if (hit.collider != null)
                {
                    treePositions.Add(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 0.4f, randomZ));
                }
            }
            treeNum--;
        }

        GameObject tree;
        foreach (Vector3 pos in treePositions)
        {
            tree = Instantiate(pinetree, pos, new Quaternion(0f, 0f, 0f, 0f));
            trees.Add(tree);
        }
    }

    private void applyTextures()
    {
        foreach (GameObject[,,] cubeList in worldData)
        {
            foreach (GameObject cube in cubeList)
            {
                if (cube != null)
                {
                    cube.GetComponent<MeshRenderer>().material = (Material)Resources.Load("ZoneMaterial", typeof(Material));

                    Mesh mesh = cube.GetComponent<MeshFilter>().mesh;
                    if (mesh.vertices.Length > 0)
                    {
                        Vector2[] UVs = new Vector2[mesh.vertices.Length];
                        // Front
                        UVs[0] = new Vector2(0.0f, 0.0f);
                        UVs[1] = new Vector2(0.333f, 0.0f);
                        UVs[2] = new Vector2(0.0f, 0.333f);
                        UVs[3] = new Vector2(0.333f, 0.333f);
                        // Top
                        UVs[4] = new Vector2(0.334f, 0.333f);
                        UVs[5] = new Vector2(0.666f, 0.333f);
                        UVs[8] = new Vector2(0.334f, 0.0f);
                        UVs[9] = new Vector2(0.666f, 0.0f);
                        // Back
                        UVs[6] = new Vector2(1.0f, 0.0f);
                        UVs[7] = new Vector2(0.667f, 0.0f);
                        UVs[10] = new Vector2(1.0f, 0.333f);
                        UVs[11] = new Vector2(0.667f, 0.333f);
                        // Bottom
                        UVs[12] = new Vector2(0.0f, 0.334f);
                        UVs[13] = new Vector2(0.0f, 0.666f);
                        UVs[14] = new Vector2(0.333f, 0.666f);
                        UVs[15] = new Vector2(0.333f, 0.334f);
                        // Left
                        UVs[16] = new Vector2(0.334f, 0.334f);
                        UVs[17] = new Vector2(0.334f, 0.666f);
                        UVs[18] = new Vector2(0.666f, 0.666f);
                        UVs[19] = new Vector2(0.666f, 0.334f);
                        // Right        
                        UVs[20] = new Vector2(0.667f, 0.334f);
                        UVs[21] = new Vector2(0.667f, 0.666f);
                        UVs[22] = new Vector2(1.0f, 0.666f);
                        UVs[23] = new Vector2(1.0f, 0.334f);
                        mesh.uv = UVs;
                    }
                }
            }
        }
    }

    private void generateFireflies()
    {
        int fireflyNum1 = 50;
        int fireflyNum2 = 200;
        int fireflyNum3 = 600;
        int newPosAttempt;
        float zoneWidth = (maxX - minX) / 7;
        float randomX;
        float randomZ;

        while (fireflyNum3 > 0)
        {
            randomX = Mathf.Floor(Random.Range(minX, maxX));
            randomZ = Mathf.Floor(Random.Range(minZ, maxZ));

            newPosAttempt = 0;

            for (int i = 0; i < fireflyPositions.Count; i++)
            {
                RaycastHit hit;
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                if (randomX > zoneWidth && randomX < (zoneWidth * 6) && randomZ > zoneWidth && randomZ < (zoneWidth * 6))
                {
                    randomX = Mathf.Floor(Random.Range(minX, maxX));
                    randomZ = Mathf.Floor(Random.Range(minZ, maxZ));

                    i = 0;
                    newPosAttempt++;
                }
                else if (hit.collider == null || Physics.CheckSphere(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 1.2f, randomZ), 0.6f) || Vector3.Distance(new Vector3(randomX, 1.2f, randomZ), fireflyPositions[i]) < 2f)
                {
                    randomX = Mathf.Floor(Random.Range(minX, maxX));
                    randomZ = Mathf.Floor(Random.Range(minZ, maxZ));

                    i = 0;
                    newPosAttempt++;
                }

                if (newPosAttempt == 100)
                {
                    break;
                }
            }
            if (newPosAttempt < 100)
            {
                RaycastHit hit;
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                if (hit.collider != null)
                {
                    fireflyPositions.Add(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 1.2f, randomZ));
                }
            }
            fireflyNum3--;
        }

        while (fireflyNum2 > 0)
        {
            randomX = Mathf.Floor(Random.Range(minX + zoneWidth, maxX - zoneWidth));
            randomZ = Mathf.Floor(Random.Range(minZ + zoneWidth, maxZ - zoneWidth));

            newPosAttempt = 0;

            for (int i = 0; i < fireflyPositions.Count; i++)
            {
                RaycastHit hit;
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                if (randomX > (zoneWidth * 2) && randomX < (zoneWidth * 5) && randomZ > (zoneWidth * 2) && randomZ < (zoneWidth * 5))
                {
                    randomX = Mathf.Floor(Random.Range(minX + zoneWidth, maxX - zoneWidth));
                    randomZ = Mathf.Floor(Random.Range(minZ + zoneWidth, maxZ - zoneWidth));

                    i = 0;
                    newPosAttempt++;
                }
                else if (hit.collider == null || Physics.CheckSphere(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 1.2f, randomZ), 0.6f) || Vector3.Distance(new Vector3(randomX, 1.2f, randomZ), fireflyPositions[i]) < 2f)
                {
                    randomX = Mathf.Floor(Random.Range(minX + zoneWidth, maxX - zoneWidth));
                    randomZ = Mathf.Floor(Random.Range(minZ + zoneWidth, maxZ - zoneWidth));

                    i = 0;
                    newPosAttempt++;
                }

                if (newPosAttempt == 100)
                {
                    break;
                }
            }
            if (newPosAttempt < 100)
            {
                RaycastHit hit;
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                if (hit.collider != null)
                {
                    fireflyPositions.Add(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 1.2f, randomZ));
                }
            }
            fireflyNum2--;
        }

        while (fireflyNum1 > 0)
        {
            randomX = Mathf.Floor(Random.Range(minX + (zoneWidth * 2), maxX - (zoneWidth * 2)));
            randomZ = Mathf.Floor(Random.Range(minZ + (zoneWidth * 2), maxZ - (zoneWidth * 2)));

            newPosAttempt = 0;

            for (int i = 0; i < fireflyPositions.Count; i++)
            {
                RaycastHit hit;
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                if (randomX > (zoneWidth * 3) && randomX < (zoneWidth * 4) && randomZ > (zoneWidth * 3) && randomZ < (zoneWidth * 4))
                {
                    randomX = Mathf.Floor(Random.Range(minX + (zoneWidth * 2), maxX - (zoneWidth * 2)));
                    randomZ = Mathf.Floor(Random.Range(minZ + (zoneWidth * 2), maxZ - (zoneWidth * 2)));

                    i = 0;
                    newPosAttempt++;
                }
                else if (hit.collider == null || Physics.CheckSphere(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 1.2f, randomZ), 0.6f) || Vector3.Distance(new Vector3(randomX, 1.2f, randomZ), fireflyPositions[i]) < 2f)
                {
                    randomX = Mathf.Floor(Random.Range(minX + (zoneWidth * 2), maxX - (zoneWidth * 2)));
                    randomZ = Mathf.Floor(Random.Range(minZ + (zoneWidth * 2), maxZ - (zoneWidth * 2)));

                    i = 0;
                    newPosAttempt++;
                }

                if (newPosAttempt == 100)
                {
                    break;
                }
            }
            if (newPosAttempt < 100)
            {
                RaycastHit hit;
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                if (hit.collider != null)
                {
                    fireflyPositions.Add(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 1.2f, randomZ));
                }
            }
            fireflyNum1--;
        }

        GameObject firefly;
        for (int i = 0; i < fireflyPositions.Count; i++)
        {
            firefly = Instantiate(fireflyExample, fireflyPositions[i], new Quaternion(0f, 0f, 0f, 0f));
            firefly.name = "Firefly " + i;
            fireflies.Add(firefly);
            //firefly.GetComponent<Animator>().SetFloat("Offset", Random.value * 1.5f);
            //firefly.GetComponent<Animator>().speed = Random.value * 1.5f + 0.1f;
        }
    }
}
