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

    [SerializeField] GameObject playerCharacter;
    [SerializeField] GameObject shadeExample;



    //change the ringSize variable here to change the thickness of a single zone's ring
    //chnage the tileSize variable here to change the size of each tile
    int ringSize = 20;
    int tileSize = 3;


    int mapSize;
    string ringLocation = "0";
    bool spawningShade = true;

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
                mapPiece.name = map[i, q];
                mapPiece.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
                mapPiece.transform.position = new Vector3(tileSize * i, -0.5f * tileSize, tileSize * q);
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
                        Sprite sprite = (Sprite)Resources.LoadAll("Sprites/Zone1Tiles")[5];
                        Texture2D tex =  new Texture2D((int)sprite.textureRect.width, (int)sprite.textureRect.height);
                        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);
                        tex.SetPixels(pixels);
                        tex.Apply();
                        mapPiece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex);
                    }
                    else if (map[i, q].Substring(2, 1).Equals("2"))
                    {
                        Sprite sprite = (Sprite)Resources.LoadAll("Sprites/Zone1Tiles")[14];
                        Texture2D tex = new Texture2D((int)sprite.textureRect.width, (int)sprite.textureRect.height);
                        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);
                        tex.SetPixels(pixels);
                        tex.Apply();
                        mapPiece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex);
                    }
                    else
                    {
                        Sprite sprite = (Sprite)Resources.LoadAll("Sprites/Zone1Tiles")[23];
                        Texture2D tex = new Texture2D((int)sprite.textureRect.width, (int)sprite.textureRect.height);
                        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);
                        tex.SetPixels(pixels);
                        tex.Apply();
                        mapPiece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex);
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
                    mapPiece.transform.localScale = new Vector3(tileSize, 20, tileSize);
                }
            }
        }

        foreach (GameObject go in cubes){
            go.transform.Translate(new Vector3((tileSize / -2f) * map.GetLength(0), 0, (tileSize / -2f) * map.GetLength(0)));
            go.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
        }
    }

    public void Start()
    {
        layoutWorld();
        StartCoroutine(shadeSpawn());
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

        RaycastHit hit;
        Physics.Raycast(playerCharacter.transform.position, -Vector3.up, out hit);
        ringLocation = hit.collider.gameObject.name.Substring(0, 1);
    }

    private IEnumerator shadeSpawn()
    {
        float randomAngle;
        while (true)
        {
            if (ringLocation.Equals("1"))
            {
                yield return new WaitForSeconds(6);
                if (ringLocation.Equals("1"))
                {
                    randomAngle = Random.Range(0, 360);
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 0.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    while (!hit.collider.gameObject.name.Substring(0, 1).Equals("1"))
                    {
                        randomAngle = Random.Range(0, 360);
                        Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 0.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    }
                    Instantiate(shadeExample, new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 0.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                }
            }
            else if (ringLocation.Equals("2"))
            {
                yield return new WaitForSeconds(3);
                if (ringLocation.Equals("2"))
                {
                    randomAngle = Random.Range(0, 360);
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 0.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    while (!hit.collider.gameObject.name.Substring(0, 1).Equals("2"))
                    {
                        randomAngle = Random.Range(0, 360);
                        Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 0.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    }
                    Instantiate(shadeExample, new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 0.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                }
            }
            else if (ringLocation.Equals("3"))
            {
                yield return new WaitForSeconds(2);
                if (ringLocation.Equals("3"))
                {
                    randomAngle = Random.Range(0, 360);
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 0.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    if (hit.collider != null)
                    {
                        while (!hit.collider.gameObject.name.Substring(0, 1).Equals("3"))
                        {
                            randomAngle = Random.Range(0, 360);
                            Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 0.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                        }
                        Instantiate(shadeExample, new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 0.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                    }
                }
            } else
            {
                yield return new WaitForSeconds(0);
            }
        }
    }
}
