using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneFeaturePlanning : MonoBehaviour
{
    /*
     * !!!!CONTROLS!!!!
     * Spacebar: regenerate the world
     * Up arrow key: increase ringSize by one
     * Down arrow key: decrease ringSize by one
     */

    //change the ringSize variable here to change the thickness of a single zone's ring
    int ringSize = 5;
    int mapSize;

    string[,] map;
    List<GameObject> cubes = new List<GameObject>();
    string[] zone1Tiles = { "1-1", "1-2", "1-3", "1-4"};
    string[] zone2Tiles = { "2-1", "2-2", "2-3", "2-4" };
    string[] zone3Tiles = { "3-1", "3-2", "3-3", "3-4" };

    //alters the map 2D array to include the city and the city zone at the center of the map
    private void generateCity()
    {
        for (int i = (map.GetLength(0) / 2) - 2; i < (map.GetLength(0) / 2) + 3; i++)
        {
            for (int q = (map.GetLength(0) / 2) - 2; q < (map.GetLength(0) / 2) + 3; q++)
            {
                map[i, q] = "0-0";
            }
        }
        map[map.GetLength(0) / 2, map.GetLength(0) / 2] = "C-C";
    }

    //generates three rings around the city as well as a single ring around the entire world for the wal
    private void generateWorld()
    {
        //determine map size necessary for inputted ring size
        mapSize = (ringSize * 6) + 7;
        map = new string[mapSize, mapSize];

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int q = 0; q < map.GetLength(0); q++)
            {
                if (i == 0 || i == mapSize - 1 || q == 0 || q == mapSize - 1)
                {
                    map[i, q] = "5-5";
                } else if (i < ringSize + 1 || q < ringSize + 1 || i > (mapSize - ringSize - 2) || q > (mapSize - ringSize - 2))
                {
                    map[i, q] = zone3Tiles[(int)Random.Range(0, 3)];
                } else if ((i > ringSize && i <= ringSize * 2 && q > ringSize && q < mapSize - ringSize - 2) || (i > mapSize - (ringSize * 2) - 2 && i <= mapSize - ringSize - 2 && q > ringSize && q < mapSize - ringSize - 1) || (q > ringSize && q <= ringSize * 2 && i > ringSize && i < mapSize - ringSize - 2) || (q > mapSize - (ringSize * 2) - 2 && q <= mapSize - ringSize - 2 && i > ringSize && i < mapSize - ringSize - 1))
                {
                    map[i, q] = zone2Tiles[(int)Random.Range(0, 3)];
                } else
                {
                    map[i, q] = zone1Tiles[(int)Random.Range(0, 3)];
                }
            }
        }
    }

    //using the map array, spawn in a bunch of cubes with varying color per zone and per zone tile type
    public void layoutWorld()
    {
        cubes = new List<GameObject>();
        generateWorld();
        generateCity();

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int q = 0; q < map.GetLength(0); q++)
            {
                GameObject mapPiece = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubes.Add(mapPiece);
                mapPiece.transform.localScale = new Vector3(5, 5, 5);
                mapPiece.transform.position = new Vector3(5 * i, -2.5f, 5 * q);
                if (map[i, q].Substring(0, 1).Equals("3"))
                {
                    if(map[i, q].Substring(2, 1).Equals("1"))
                    {
                        mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0.437f, 0.337f, 0.906f);
                    }
                    else if (map[i, q].Substring(2, 1).Equals("2"))
                    {
                        mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0.216f, 0.070f, 0.642f);
                    }
                    else
                    {
                        mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0.127f, 0.008f, 0.358f);
                    }
                }
                else if (map[i, q].Substring(0, 1).Equals("2"))
                {
                    if (map[i, q].Substring(2, 1).Equals("1"))
                    {
                        mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0.724f, 0.454f, 0.934f);
                    }
                    else if (map[i, q].Substring(2, 1).Equals("2"))
                    {
                        mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0.431f, 0.123f, 0.670f);
                    }
                    else
                    {
                        mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0.196f, 0.023f, 0.330f);
                    }
                }
                else if (map[i, q].Substring(0, 1).Equals("1"))
                {
                    if (map[i, q].Substring(2, 1).Equals("1"))
                    {
                        mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0.848f, 0.405f, 0.870f);
                    }
                    else if (map[i, q].Substring(2, 1).Equals("2"))
                    {
                        mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0.669f, 0.092f, 0.585f);
                    }
                    else
                    {
                        mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0.406f, 0.013f, 0.309f);
                    }
                }
                else if (map[i, q].Substring(0, 1).Equals("0"))
                {
                    mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                }
                else if (map[i, q].Substring(0, 1).Equals("C"))
                {
                    mapPiece.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                }
                else if (map[i, q].Substring(0, 1).Equals("5"))
                {
                    mapPiece.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.2450981f, 0);
                    mapPiece.transform.localScale = new Vector3(5, 20, 5);
                }
            }
        }

        foreach (GameObject go in cubes){
            go.transform.Translate(new Vector3(-2.5f * map.GetLength(0), 0, -2.5f * map.GetLength(0)));
        }

    }

    public void Start()
    {
        layoutWorld();
    }

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            foreach (GameObject g in cubes)
            {
                Destroy(g);
            }
            layoutWorld();
        }

        if (Input.GetKeyDown("up"))
        {
            ringSize++;
            foreach (GameObject g in cubes)
            {
                Destroy(g);
            }
            layoutWorld();
        }

        if (Input.GetKeyDown("down"))
        {
            ringSize--;
            foreach (GameObject g in cubes)
            {
                Destroy(g);
            }
            layoutWorld();
        }
    }
}
