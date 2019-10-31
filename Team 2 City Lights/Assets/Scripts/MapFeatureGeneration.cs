using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFeatureGeneration : MonoBehaviour
{
    [SerializeField] GameObject pinetree;

	private List<GameObject[,,]> worldData;
    List<Vector3> treePositions = new List<Vector3>();
    float minX, maxX, minZ, maxZ, minY;

	void Start()
    {
		worldData = new List<GameObject[,,]>();
	}

	public void beginGeneration(List<GameObject[,,]>  wd)
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
                Physics.Raycast(new Vector3(randomX, 10f, randomZ), -Vector3.up, out hit);
                if (hit.collider == null || Vector3.Distance(new Vector3(randomX, 0.5f, randomZ), treePositions[i]) < 5f)
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
                treePositions.Add(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 0.4f, randomZ));
            }
            treeNum--;
        }

        foreach (Vector3 pos in treePositions)
        {
            if (!Physics.CheckSphere(new Vector3(pos.x, pos.y + 1.4f, pos.z), 1.2f))
            {
                Instantiate(pinetree, pos, new Quaternion(0f, 0f, 0f, 0f));
            }
        }
    }

    void Update()
    {
        
    }
}
