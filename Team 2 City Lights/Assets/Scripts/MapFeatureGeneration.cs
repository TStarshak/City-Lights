﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Diagnostics;

public class MapFeatureGeneration : MonoBehaviour
{
    [SerializeField] GameObject playerCharacter;
    [SerializeField] GameObject shadeExample;
    [SerializeField] GameObject shadeExample2;
    [SerializeField] GameObject shadeExample3;
    [SerializeField] GameObject fireflyExample;


    private List<GameObject[,,]> worldData;
    private List<GameObject> landmarks;
    private List<Vector3> lakeTiles;
    List<Vector3> treePositions = new List<Vector3>();
    List<Vector3> fireflyPositions = new List<Vector3>();
    List<Vector3> groundItemPositions = new List<Vector3>();
    List<GameObject> trees = new List<GameObject>();
    List<GameObject> fireflies = new List<GameObject>();
    List<GameObject> groundItems = new List<GameObject>();
    List<GameObject> snails = new List<GameObject>();
    List<GameObject> lakeBlockers = new List<GameObject>();
    float minX, maxX, minZ, maxZ, minY;
    string ringLocation = "0";
    public bool readyForNavMesh = false;

    void Start()
    {
        worldData = new List<GameObject[,,]>();
        landmarks = new List<GameObject>();
        lakeTiles = new List<Vector3>();
    }

    public void beginGeneration(List<GameObject[,,]> wd, List<GameObject> lm, List<Vector3> lt)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        worldData = wd;
        minX = worldData[0][0, 0, 0].transform.position.x;
        minY = worldData[0][0, 0, 0].transform.position.y;
        minZ = worldData[0][0, 0, 0].transform.position.z;
        maxX = minX;
        maxZ = minZ;

        landmarks = lm;
        lakeTiles = lt;

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
        generateGroundItems();
        applyTextures();
        generateFireflies();
        spawnSnails();
        spawnLakeBlockers();
        centerMap();

        if (PlayerProgress.Instance.savedPlayerData.currentMission.fireflyGoal >= 15)
        {
            StartCoroutine(shadeSpawn());
        }
        if (PlayerProgress.Instance.savedPlayerData.currentMission.fireflyGoal >= 25)
        {
            StartCoroutine(shadeSpawn2());
        }
        if (PlayerProgress.Instance.savedPlayerData.currentMission.fireflyGoal >= 35)
        {
            StartCoroutine(shadeSpawn3());
        }
        sw.Stop();
        UnityEngine.Debug.Log("Time Taken: " + sw.ElapsedMilliseconds);
        readyForNavMesh = true;
    }

    public void Update()
    {
        RaycastHit hit;
        Physics.Raycast(playerCharacter.transform.position, -Vector3.up, out hit);
        if (PauseController.isPaused){
            ringLocation = "0";
        }
        else if (hit.collider != null && Regex.IsMatch(hit.collider.gameObject.name.Substring(hit.collider.gameObject.name.Length-1, 1), "[0-9]")){
            ringLocation = hit.collider.gameObject.name.Substring(hit.collider.gameObject.name.Length-1, 1);
        }
    }

    private void spawnLakeBlockers()
    {
        GameObject lakeBlocker;

        foreach (Vector3 pos in lakeTiles)
        {
            lakeBlocker = Instantiate((GameObject)Resources.Load("Prefabs/lakeBlocker", typeof(GameObject)), new Vector3(pos.x, pos.y + 1, pos.z), new Quaternion(0f, 0f, 0f, 0f));
            lakeBlockers.Add(lakeBlocker);
        }
    }

    private void spawnSnails()
    {
        float randomAngle;
        int attemptCount;
        int randomChance;
        foreach (GameObject groundItem in groundItems)
        {
            if (groundItem.name.Equals("mushroom(Clone)"))
            {
                randomChance = Random.Range(0, 100);
                if (randomChance < 20)
                {
                    attemptCount = 100;
                    randomAngle = Random.Range(0, 360);
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(groundItem.transform.position.x + (0.9f * Mathf.Cos(randomAngle)), 20f, groundItem.transform.position.z + (0.9f * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    while (hit.collider == null || (hit.collider != null && hit.collider.transform.position.y == (groundItem.transform.position.y + 0.49f)))
                    {
                        randomAngle = Random.Range(0, 360);
                        Physics.Raycast(new Vector3(groundItem.transform.position.x + (0.9f * Mathf.Cos(randomAngle)), 20f, groundItem.transform.position.z + (0.9f * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                        attemptCount--;
                        if (attemptCount == 0)
                        {
                            break;
                        }
                    }
                    if (attemptCount != 0)
                    {
                        Physics.Raycast(new Vector3(groundItem.transform.position.x + (0.9f * Mathf.Cos(randomAngle)), 20f, groundItem.transform.position.z + (0.9f * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                        GameObject snail = null;
                        float randAngle = Random.Range(0, 360);
                        snail = Instantiate((GameObject)Resources.Load("Prefabs/snail", typeof(GameObject)), new Vector3(groundItem.transform.position.x + (0.9f * Mathf.Cos(randomAngle)), groundItem.transform.position.y + 0.01f, groundItem.transform.position.z + (0.9f * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                        if (snail != null) {
                            snail.transform.RotateAround(snail.transform.position, Vector3.up, randAngle);
                            snail.GetComponent<snailWalk>().mushroom = groundItem;
                            snail.GetComponent<snailWalk>().currentAngle = randAngle;
                            snails.Add(snail);
                        }
                    }
                }
            }
        }
    }

    private void centerMap()
    {
        foreach (GameObject[,,] cubeList in worldData)
        {
            foreach (GameObject cube in cubeList)
            {
                if (cube != null)
                {
                    cube.transform.position = new Vector3(cube.transform.position.x - (maxX / 2), cube.transform.position.y - 1f, cube.transform.position.z - (maxX / 2));
                    if (cube.transform.position.y == 0f && cube.GetComponent<BoxCollider>() != null)
                    {
                        cube.GetComponent<BoxCollider>().size = new Vector3(1, 10, 1);
                    }
                }
            }
        }

        foreach (GameObject lakeBlocker in lakeBlockers)
        {
            lakeBlocker.transform.position = new Vector3(lakeBlocker.transform.position.x - (maxX / 2), lakeBlocker.transform.position.y - 1f, lakeBlocker.transform.position.z - (maxX / 2));
        }

        foreach (GameObject tree in trees)
        {
            tree.transform.position = new Vector3(tree.transform.position.x - (maxX / 2), tree.transform.position.y - 1f, tree.transform.position.z - (maxX / 2));
            if(Vector3.Distance(tree.transform.position, new Vector3(0, tree.transform.position.y, 0)) < 7f){
                Destroy(tree);
            }
            foreach (GameObject landmark in landmarks)
            {
                if (Vector3.Distance(new Vector3(tree.transform.position.x, 20f, tree.transform.position.z), new Vector3(landmark.transform.position.x, 20f, landmark.transform.position.z)) < 4f)
                {
                    Destroy(tree);
                }
            }
        }

        foreach (GameObject firefly in fireflies)
        {
            firefly.transform.position = new Vector3(firefly.transform.position.x - (maxX / 2), firefly.transform.position.y - 1f, firefly.transform.position.z - (maxX / 2));
            if(Vector3.Distance(firefly.transform.position, new Vector3(0, firefly.transform.position.y, 0)) < 7f){
                Destroy(firefly);
            }
            foreach (GameObject landmark in landmarks)
            {
                if (Vector3.Distance(new Vector3(firefly.transform.position.x, 20f, firefly.transform.position.z), new Vector3(landmark.transform.position.x, 20f, landmark.transform.position.z)) < 5f)
                {
                    Destroy(firefly);
                }
            }
        }

        List<GameObject> TBD = new List<GameObject>();
        foreach (GameObject groundItem in groundItems)
        {
            groundItem.transform.position = new Vector3(groundItem.transform.position.x - (maxX / 2), groundItem.transform.position.y, groundItem.transform.position.z - (maxX / 2));
            if(Vector3.Distance(groundItem.transform.position, new Vector3(0, groundItem.transform.position.y, 0)) < 7f){
                Destroy(groundItem);
            }
            foreach (GameObject landmark in landmarks)
            {
                if (Vector3.Distance(new Vector3(groundItem.transform.position.x, 20f, groundItem.transform.position.z), new Vector3(landmark.transform.position.x, 20f, landmark.transform.position.z)) < 2f)
                {
                    Destroy(groundItem);
                }
            }
            foreach (GameObject groundItem2 in groundItems)
            {
                if (groundItem != groundItem2)
                {
                    if (groundItem.name.Equals("mushroom(Clone)"))
                    {
                        if (groundItem2.name.Equals("mushroom(Clone)"))
                        {
                            if (Vector3.Distance(new Vector3(groundItem.transform.position.x, 20f, groundItem.transform.position.z), new Vector3(groundItem2.transform.position.x, 20f, groundItem2.transform.position.z)) < 0.6f)
                            {
                                TBD.Add(groundItem);
                            }
                        }
                        else
                        {
                            if (Vector3.Distance(new Vector3(groundItem.transform.position.x, 20f, groundItem.transform.position.z), new Vector3(groundItem2.transform.position.x, 20f, groundItem2.transform.position.z)) < 1.3f)
                            {
                                TBD.Add(groundItem);
                            }
                        }
                    }
                    else
                    {
                        if (groundItem2.name.Equals("mushroom(Clone)"))
                        {
                            if (Vector3.Distance(new Vector3(groundItem.transform.position.x, 20f, groundItem.transform.position.z), new Vector3(groundItem2.transform.position.x, 20f, groundItem2.transform.position.z)) < 1.3f)
                            {
                                TBD.Add(groundItem);
                            }
                        }
                        else
                        {
                            if (Vector3.Distance(new Vector3(groundItem.transform.position.x, 20f, groundItem.transform.position.z), new Vector3(groundItem2.transform.position.x, 20f, groundItem2.transform.position.z)) < 1.8f)
                            {
                                TBD.Add(groundItem);
                            }
                        }
                    }
                }
            }
        }

        foreach (GameObject tbd in TBD)
        {
            Destroy(tbd);
        }

        foreach (GameObject snail in snails)
        {
            snail.transform.position = new Vector3(snail.transform.position.x - (maxX / 2), snail.transform.position.y, snail.transform.position.z - (maxX / 2));
            if(Vector3.Distance(snail.transform.position, new Vector3(0, snail.transform.position.y, 0)) < 7f){
                Destroy(snail);
            }
            foreach (GameObject landmark in landmarks)
            {
                if (Vector3.Distance(new Vector3(snail.transform.position.x, 20f, snail.transform.position.z), new Vector3(landmark.transform.position.x, 20f, landmark.transform.position.z)) < 2f)
                {
                    Destroy(snail);
                }
            }
        }
    }

    private void generateTrees()
    {
        int treeNum = 400;
        int newPosAttempt;
        float randomX;
        float randomZ;
        GameObject tree;
        float randScale;

        RaycastHit hit;
        for (int i = 0; i < maxX / 2; i++)
        {
            randomX = minX + (2 * i);
            randomZ = Random.Range(minZ, minZ + 2);
            Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
            if (hit.collider != null)
            {
                treePositions.Add(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 0.4f, randomZ));
            }
        }
        for (int i = 0; i < maxX / 2; i++)
        {
            randomX = minX + (2 * i);
            randomZ = Random.Range(maxZ - 2, maxZ);
            Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
            if (hit.collider != null)
            {
                treePositions.Add(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 0.4f, randomZ));
            }
        }
        for (int i = 2; i < maxZ / 2; i++)
        {
            randomX = Random.Range(minX, minX + 2);
            randomZ = minZ + (2 * i);
            Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
            if (hit.collider != null)
            {
                treePositions.Add(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 0.4f, randomZ));
            }
        }
        for (int i = 2; i < maxZ / 2; i++)
        {
            randomX = Random.Range(maxX - 2, maxX);
            randomZ = minZ + (2 * i);
            Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
            if (hit.collider != null)
            {
                treePositions.Add(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 0.4f, randomZ));
            }
        }

        while (treeNum > 0)
        {
            randomX = Mathf.Floor(Random.Range(minX, maxX)) + 0.05f;
            randomZ = Mathf.Floor(Random.Range(minZ, maxZ)) + 0.05f;
            newPosAttempt = 0;

            for (int i = 0; i < treePositions.Count; i++)
            {
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
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                if (hit.collider != null)
                {
                    treePositions.Add(new Vector3(randomX, hit.collider.gameObject.transform.position.y + 0.4f, randomZ));
                }
            }
            treeNum--;
        }


        for(int i = 0; i < treePositions.Count; i++)
        {
            tree = Instantiate((GameObject)Resources.Load("Prefabs/TreePrefab", typeof(GameObject)), treePositions[i], new Quaternion(0f, 0f, 0f, 0f));
            if (i < 1200)
            {
                randScale = Random.Range(0.009f, 0.011f);
            } else
            {
                randScale = Random.Range(0.007f, 0.013f);
            }
            tree.transform.localScale = new Vector3(randScale, randScale, randScale);
            tree.transform.RotateAround(tree.transform.position, Vector3.up, Random.Range(0, 360));
            trees.Add(tree);
        }
    }

    private void generateGroundItems()
    {
        int groundNum = 1000;
        int newPosAttempt;
        float randomX;
        float randomZ;
        RaycastHit hit;

        foreach (GameObject tree in trees)
        {
            tree.GetComponent<CapsuleCollider>().enabled = false;
            tree.GetComponent<MeshCollider>().enabled = true;
        }

        while (groundNum > 0)
        {
            randomX = Mathf.Floor(Random.Range(minX, maxX));
            randomZ = Mathf.Floor(Random.Range(minZ, maxZ));
            newPosAttempt = 0;

            Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
            while (hit.collider == null)
            {
                randomX = Mathf.Floor(Random.Range(minX, maxX));
                randomZ = Mathf.Floor(Random.Range(minZ, maxZ));
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
            }
            Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
            while (true)
            {
                randomX = Mathf.Floor(Random.Range(minX, maxX));
                randomZ = Mathf.Floor(Random.Range(minZ, maxZ));
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                newPosAttempt++;
                while (hit.collider == null)
                {
                    randomX = Mathf.Floor(Random.Range(minX, maxX));
                    randomZ = Mathf.Floor(Random.Range(minZ, maxZ));
                    Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                }
                if (newPosAttempt == 100 || !Physics.CheckBox(new Vector3(randomX - 0.15f, hit.collider.gameObject.transform.position.y + 1.54f, randomZ + 0.44f), new Vector3(2.35f, 0.695f, 2.35f), new Quaternion(0f, 0f, 0f, 0f)))
                {
                    break;
                }
            }
            if (newPosAttempt != 100)
            {
                Physics.Raycast(new Vector3(randomX, 20f, randomZ), -Vector3.up, out hit);
                if (hit.collider != null)
                {
                    groundItemPositions.Add(new Vector3(randomX, 0, randomZ));
                }
            }
            groundNum--;
        }

        foreach (GameObject tree in trees)
        {
            tree.GetComponent<MeshCollider>().enabled = false;
        }

        GameObject groundItem;
        float randScale;
        int randChoice;
        foreach (Vector3 pos in groundItemPositions)
        {
            Physics.Raycast(new Vector3(pos.x, 20f, pos.z), -Vector3.up, out hit);
            randChoice = Random.Range(1, 4);
            if (randChoice == 1)
            {
                groundItem = Instantiate((GameObject)Resources.Load("Prefabs/rock1", typeof(GameObject)), new Vector3(pos.x, hit.collider.gameObject.transform.position.y - 0.52f, pos.z), new Quaternion(0f, 0f, 0f, 0f));
                randScale = Random.Range(0.1f, 0.4f);
                groundItem.transform.localScale = new Vector3(randScale, randScale, randScale);
                groundItem.transform.RotateAround(groundItem.transform.position, Vector3.up, Random.Range(0, 360));
            } else if (randChoice == 2)
            {
                groundItem = Instantiate((GameObject)Resources.Load("Prefabs/rock2", typeof(GameObject)), new Vector3(pos.x, hit.collider.gameObject.transform.position.y - 0.52f, pos.z), new Quaternion(0f, 0f, 0f, 0f));
                randScale = Random.Range(0.1f, 0.4f);
                groundItem.transform.localScale = new Vector3(randScale, randScale, randScale);
                groundItem.transform.RotateAround(groundItem.transform.position, Vector3.up, Random.Range(0, 360));
            } else
            {
                groundItem = Instantiate((GameObject)Resources.Load("Prefabs/mushroom", typeof(GameObject)), new Vector3(pos.x, hit.collider.gameObject.transform.position.y - 0.51f, pos.z), new Quaternion(0f, 0f, 0f, 0f));
                randScale = Random.Range(0.3f, 0.9f);
                groundItem.transform.localScale = new Vector3(0.2f, randScale, 0.2f);
                groundItem.transform.RotateAround(groundItem.transform.position, Vector3.up, Random.Range(0, 360));
            }
            
            groundItems.Add(groundItem);
        }

        foreach (GameObject tree in trees)
        {
            tree.GetComponent<CapsuleCollider>().enabled = true;
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

        if (PlayerProgress.Instance.savedPlayerData.currentMission.fireflyGoal == 10)
        {
            fireflyNum1 = 10;
            fireflyNum2 = 10;
            fireflyNum3 = 20;
        } else if (PlayerProgress.Instance.savedPlayerData.currentMission.fireflyGoal == 15)
        {
            fireflyNum1 = 20;
            fireflyNum2 = 80;
            fireflyNum3 = 240;
        }
        else if (PlayerProgress.Instance.savedPlayerData.currentMission.fireflyGoal == 20)
        {
            fireflyNum1 = 20;
            fireflyNum2 = 80;
            fireflyNum3 = 240;
        }
        else if (PlayerProgress.Instance.savedPlayerData.currentMission.fireflyGoal == 25)
        {
            fireflyNum1 = 40;
            fireflyNum2 = 160;
            fireflyNum3 = 480;
        }
        else if (PlayerProgress.Instance.savedPlayerData.currentMission.fireflyGoal == 30)
        {
            fireflyNum1 = 40;
            fireflyNum2 = 160;
            fireflyNum3 = 480;
        }
        else
        {
            fireflyNum1 = 50;
            fireflyNum2 = 200;
            fireflyNum3 = 600;
        }

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
            firefly.GetComponent<Animator>().SetFloat("Offset", Random.value * 1.5f);
            firefly.GetComponent<Animator>().speed = Random.value * 1.5f + 0.1f;
        }
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
                    Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    while (hit.collider == null || (hit.collider != null && hit.collider.transform.position.y != -1f))
                    {
                        randomAngle = Random.Range(0, 360);
                        Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    }
                    Instantiate(shadeExample, new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), -1.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                    UnityEngine.Debug.Log("Spawn shade 1 - zone " + ringLocation);
                }
            }
            else if (ringLocation.Equals("2"))
            {
                yield return new WaitForSeconds(3);
                if (ringLocation.Equals("2"))
                {
                    randomAngle = Random.Range(0, 360);
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    while (hit.collider == null || (hit.collider != null && hit.collider.transform.position.y != -1f))
                    {
                        randomAngle = Random.Range(0, 360);
                        Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    }
                    Instantiate(shadeExample, new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), -1.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                    UnityEngine.Debug.Log("Spawn shade 1 - zone " + ringLocation);
                }
            }
            else if (ringLocation.Equals("3"))
            {
                yield return new WaitForSeconds(2);
                if (ringLocation.Equals("3"))
                {
                    randomAngle = Random.Range(0, 360);
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    while (hit.collider == null || (hit.collider != null && hit.collider.transform.position.y != -1f))
                    {
                        randomAngle = Random.Range(0, 360);
                        Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    }
                    Instantiate(shadeExample, new Vector3(playerCharacter.transform.position.x + (7 * Mathf.Cos(randomAngle)), -1.5f, playerCharacter.transform.position.z + (7 * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                    UnityEngine.Debug.Log("Spawn shade 1 - zone " + ringLocation);
                }
            }
            else
            {
                yield return new WaitForSeconds(0);
            }
        }
    }

    private IEnumerator shadeSpawn2()
    {
        float randomAngle;
        GameObject enemy;
        while (true)
        {
            if (ringLocation.Equals("2"))
            {
                yield return new WaitForSeconds(10);
                if (ringLocation.Equals("2"))
                {
                    randomAngle = Random.Range(0, 360);
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (9 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (9 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    while (hit.collider == null || (hit.collider != null && hit.collider.transform.position.y != -1f))
                    {
                        randomAngle = Random.Range(0, 360);
                        Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (9 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (9 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    }
                    enemy = Instantiate(shadeExample2, new Vector3(playerCharacter.transform.position.x + (9 * Mathf.Cos(randomAngle)), -1.5f, playerCharacter.transform.position.z + (9 * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                    enemy.SetActive(true);
                    UnityEngine.Debug.Log("Spawn shade 2");
                }
            }
            else if (ringLocation.Equals("3"))
            {
                yield return new WaitForSeconds(7);
                if (ringLocation.Equals("3"))
                {
                    randomAngle = Random.Range(0, 360);
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (9 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (9 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    while (hit.collider == null || (hit.collider != null && hit.collider.transform.position.y != -1f))
                    {
                        randomAngle = Random.Range(0, 360);
                        Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (9 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (9 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    }
                    enemy = Instantiate(shadeExample2, new Vector3(playerCharacter.transform.position.x + (9 * Mathf.Cos(randomAngle)), -1.5f, playerCharacter.transform.position.z + (9 * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                    enemy.SetActive(true);
                    UnityEngine.Debug.Log("Spawn shade 2");
                }
            }
            else
            {
                yield return new WaitForSeconds(0);
            }
        }
    }

    private IEnumerator shadeSpawn3()
    {
        float randomAngle;
        GameObject enemy;
        while (true)
        {
            if (ringLocation.Equals("2"))
            {
                yield return new WaitForSeconds(20);
                if (ringLocation.Equals("2"))
                {
                    randomAngle = Random.Range(0, 360);
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (15 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (15 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    while (hit.collider == null || (hit.collider != null && hit.collider.transform.position.y != -1f))
                    {
                        randomAngle = Random.Range(0, 360);
                        Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (15 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (15 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    }
                    enemy = Instantiate(shadeExample3, new Vector3(playerCharacter.transform.position.x + (15 * Mathf.Cos(randomAngle)), -1.5f, playerCharacter.transform.position.z + (15 * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                    enemy.SetActive(true);
                    UnityEngine.Debug.Log("Spawn shade 3");
                }
            }
            else if (ringLocation.Equals("3"))
            {
                yield return new WaitForSeconds(12);
                if (ringLocation.Equals("3"))
                {
                    randomAngle = Random.Range(0, 360);
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (15 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (15 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    while (hit.collider == null || (hit.collider != null && hit.collider.transform.position.y != -1f))
                    {
                        randomAngle = Random.Range(0, 360);
                        Physics.Raycast(new Vector3(playerCharacter.transform.position.x + (15 * Mathf.Cos(randomAngle)), 20f, playerCharacter.transform.position.z + (15 * Mathf.Sin(randomAngle))), -Vector3.up, out hit);
                    }
                    enemy = Instantiate(shadeExample3, new Vector3(playerCharacter.transform.position.x + (15 * Mathf.Cos(randomAngle)), -1.5f, playerCharacter.transform.position.z + (15 * Mathf.Sin(randomAngle))), new Quaternion(0f, 0f, 0f, 0f));
                    enemy.SetActive(true);
                    UnityEngine.Debug.Log("Spawn shade 3");
                }
            }
            else
            {
                yield return new WaitForSeconds(0);
            }
        }
    }
}
