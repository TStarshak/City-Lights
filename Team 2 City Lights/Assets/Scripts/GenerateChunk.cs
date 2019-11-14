//City Lights
//Kevin Szmyd [kps59]

//TODO
//Allow users to modify the size of the terrain features on initial generation.
//Add tiling.
//Name cubes so grif can access them.
//Dump chunk data to file, load when loading rather than generating a new map.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;



public struct ChunkStruct {
    public List<int> tileAmount, edgeType;

    public ChunkStruct(List<int> inTile, List<int> inEdge) {
        this.tileAmount = inTile;
        this.edgeType = inEdge;
    }
}

//int[chunk][feature] = number of tiles
//int[chunk][openings] = terrain feature int
public struct MapData {
	public List<ChunkStruct> mapChunk;

    public MapData(List<ChunkStruct> inBlock){
        this.mapChunk = inBlock;
    }
}

public class GenerateChunk : MonoBehaviour {
    //INT VALUES FOR TERRAIN FEATURES
    // 0 <- pond
    // 1 <- small hill sz1
    // 2 <- large hill sz2
    // 3 <- impass
    //
    //INT VALUES FOR MAP GENERATION
    // 0 <- walkable
    // 1 <- small hill sz1
    // 2 <- hilley hill sz2
    // 3 <- impass
    // 4 <- water
    // 5 <- left tilted tile 	[9  on top of small hill]
    // 6 <- right tilted tile	[10 on top of small hill]
    // 7 <- up tilted tile		[11 on top of small hill]
    // 8 <- down tilted tile	[12 on top of small hill]


    public static int seed = 482;
    public System.Random rand = new System.Random(seed);
    [SerializeField] private GameObject regularCube;
    [SerializeField] private GameObject tiltedCube;
    private int sizeX = 7;
    private int sizeY = 7;
    private int chunkSizeX = 28;
    private int chunkSizeY = 28;
    private List<int[][]> chunkData = new List<int[][]>();
    public List<GameObject[,,]> worldData = new List<GameObject[,,]>();

    //hacky, i don't like this var
    private int numberOfFeatures = 4;

    //helpful edge guide from below
    // 	[0, 1, 2, 3]  <- top of map
    // [4, .., .., 5] <- top-mid edges
    // [6, .., .., 7] <- bot-mid edges
    // [8, 9, 10, 11] <- bottom of map
    //potentially flip a chunk edge
    private void modifyChunkEdge(List<int> chunkOne, List<int> chunkTwo, int featureType, int side) {
        int sideMod = rand.Next(0, 2);
        if (rand.Next(0, 12) > 10) {
            //up
            if (side == 0) {
                chunkOne[1 + sideMod] = featureType;
                chunkTwo[9 + sideMod] = featureType;
            }

            //right
            if (side == 1) {
                sideMod = sideMod * 2;
                chunkOne[5 + sideMod] = featureType;
                chunkTwo[4 + sideMod] = featureType;
            }

            //left
            if (side == 2) {
                sideMod = sideMod * 2;
                chunkOne[4 + sideMod] = featureType;
                chunkTwo[5 + sideMod] = featureType;
            }

            //down
            if (side == 3) {
                chunkOne[9 + sideMod] = featureType;
                chunkTwo[1 + sideMod] = featureType;
            }
        }
    }

    //0 <- northeast, 1 <- southeast, 2 <- southwest, 3 <- northwest
    private void modifyChunkCorner(List<int> chunkOne, List<int> chunkTwo, List<int> chunkThree, List<int> chunkFour, int featureType, int corner) {
        if(rand.Next(0, 12) > 8) {
            //northeast
            if (corner == 0) {
                chunkOne[3] = featureType;
                chunkTwo[11] = featureType;
                chunkThree[8] = featureType;
                chunkFour[0] = featureType;
            }

            //southeast
            if (corner == 1) {
                chunkOne[11] = featureType;
                chunkTwo[8] = featureType;
                chunkThree[0] = featureType;
                chunkFour[3] = featureType;
            }

            //southwest
            if (corner == 2) {
                chunkOne[8] = featureType;
                chunkTwo[0] = featureType;
                chunkThree[3] = featureType;
                chunkFour[11] = featureType;
            }

            //northwest
            if (corner == 3) {
                chunkOne[0] = featureType;
                chunkTwo[3] = featureType;
                chunkThree[11] = featureType;
                chunkFour[8] = featureType;
            }
        }
    }

    //PARAMETERS
    //featureTiles (amount of tiles for each feature)
    // [feature] <- tiles to generate
    //
    //plains (amount of plain chunks)
    //chunks (amount of chunks)
    //
    //RETURNS
    //MapData
    private MapData assignFeatures(int[] feature, int plains, int chunks) {
        MapData generatedMap = new MapData(new List<ChunkStruct>());
        int usableChunks = chunks - plains;
        int minimumFeatureSize = 35;
        int maximumFeatureSize = 200;
        int middleBlock = (((sizeX - 1) / 2) * sizeY) + ((sizeY - 1) / 2);
        int selectedChunk, tempFeatureSize;

		//generate blank chunks
		for(int x = 0; x < chunks; x++) {
			generatedMap.mapChunk.Add(new ChunkStruct(new List<int>(), new List<int>()));
			
			for(int y = 0; y < feature.Length; y++) {
				generatedMap.mapChunk[x].tileAmount.Add(0);
			}
			
			for(int y = 0; y < 16; y++) {
				generatedMap.mapChunk[x].edgeType.Add(numberOfFeatures);
			}
		}
		
		//assign feature tiles to chunks
		for(int x = 0; x < numberOfFeatures; x++) {
			while(feature[x] > 0) {
				tempFeatureSize = rand.Next(minimumFeatureSize, maximumFeatureSize);
                selectedChunk = rand.Next(0, chunks);

                while(selectedChunk == middleBlock) {
                    selectedChunk = rand.Next(0, chunks);
                }
				generatedMap.mapChunk[selectedChunk].tileAmount[x] += tempFeatureSize;
				feature[x] -= tempFeatureSize;
			}
		}
		
		return generatedMap;
	}
	
	public void setEdges(List<ChunkStruct> workingMap, int mapX, int mapY) {
		//no need to visit the center or edges, so remove them from the counter
		int tilesToVisit = ((mapX - 2) * (mapY - 2)) - 1;
		
		//start at the chunk directly to the right of center
		int workingT = (((mapX - 1) / 2) * mapY) + ((mapY - 1) / 2) + 1;

		//set variables to allow for a walk
		int walkingDirection = 0;
		int walkingDistance = 1;
		int walkingCounter = 0;
		bool changeDistance = true;
		
		//walk to every relevent tile and set the edges
		while(tilesToVisit > 0) {
            //operate on current tile by feature typeof
            for(int x = 0; x < numberOfFeatures; x++) {
				if(workingMap[workingT].tileAmount[x] > 0) {
                    //look in every direction and decide if the neighbor tiles contain this feature
                    bool[] containsFeature = new [] {false, false, false, false};
					int howManyFound = 0;
					
					//detect features to the north
					if(workingMap[workingT - mapX].tileAmount[x] > 0) {
						containsFeature[0] = true;
						howManyFound++;
					}
					
					//detect features to the east
					if(workingMap[workingT - mapX].tileAmount[x] > 0) {
						containsFeature[1] = true;
						howManyFound++;
					}

					//detect features to the south
					if(workingMap[workingT - mapX].tileAmount[x] > 0) {
						containsFeature[2] = true;
						howManyFound++;
					}					
					
					//detect features to the west
					if(workingMap[workingT - mapX].tileAmount[x] > 0) {
						containsFeature[3] = true;
						howManyFound++;
					}
					
					//if two neighbors share a feature make it a corner
					if(howManyFound > 1) {
						howManyFound = 1;
						
						if(containsFeature[0] && containsFeature[1]) {
							modifyChunkCorner(workingMap[workingT].edgeType, workingMap[workingT - mapX].edgeType, workingMap[workingT - mapX + 1].edgeType, workingMap[workingT + 1].edgeType, x, 0);
							howManyFound = 0;
						}
						
						if(containsFeature[1] && containsFeature[2]) {
							modifyChunkCorner(workingMap[workingT].edgeType, workingMap[workingT + 1].edgeType, workingMap[workingT + mapX + 1].edgeType, workingMap[workingT + mapX].edgeType, x, 1);
							howManyFound = 0;
						}
						
						if(containsFeature[2] && containsFeature[3]) {
							modifyChunkCorner(workingMap[workingT].edgeType, workingMap[workingT + mapX].edgeType, workingMap[workingT + mapX - 1].edgeType, workingMap[workingT - 1].edgeType, x, 2);
							howManyFound = 0;
						}
						
						if(containsFeature[3] && containsFeature[0]) {
							modifyChunkCorner(workingMap[workingT].edgeType, workingMap[workingT - 1].edgeType, workingMap[workingT - mapX - 1].edgeType, workingMap[workingT - mapX].edgeType, x, 3);
							howManyFound = 0;
						}
					}
					
					//if one neighbor shares a feature set at least one edge on both
					if(howManyFound == 1) {
						if(containsFeature[0]) {
							modifyChunkEdge(workingMap[workingT].edgeType, workingMap[workingT - mapX].edgeType, x, 0);
						}
						
						if(containsFeature[1]) {
							modifyChunkEdge(workingMap[workingT].edgeType, workingMap[workingT + 1].edgeType, x, 1);
						}
						
						if(containsFeature[3]) {
							modifyChunkEdge(workingMap[workingT].edgeType, workingMap[workingT + mapX].edgeType, x, 2);
						}
						
						if(containsFeature[1]) {
							modifyChunkEdge(workingMap[workingT].edgeType, workingMap[workingT - 1].edgeType, x, 3);
						}
					}
				}
			}

			//increase walk distance by one every two direction changes
			if(walkingCounter == walkingDistance) {
				if(changeDistance) {
					walkingDistance++;
				}

                changeDistance = !changeDistance;
				walkingDirection++;
				walkingCounter = 0;
			}

            //Debug.Log(walkingCounter);

			//walk in set direction
			//up is 0
			if(walkingDirection % 4 == 0) {
				workingT -= mapX;
			}
			
			//left is 1
			if(walkingDirection % 4 == 1) {
				workingT -= 1;
			}
			
			//down is 2
			if(walkingDirection % 4 == 2) {
				workingT += mapX;
			}
			
			//right is 3
			if(walkingDirection % 4 == 3) {
				workingT += 1;
			}
			
			walkingCounter++;
            tilesToVisit--;
		}
	}
	
	//PARAMETERS
	//openings (edge terrain):
	// 	[0, 1, 2, 3]  <- top of map
	// [4, .., .., 5] <- top-mid edges
	// [6, .., .., 7] <- bot-mid edges
	// [8, 9, 10, 11] <- bottom of map
	// contents follow conventions for int generation
	//
	//size:
	// [x, y]
	//
	//features:
	// array 1 -> [pond, hill]
	// contents are number of tiles remaining in that category
	//
	//RETURNS
	//3d array with dimensions (x, y) of int values for map generation
	public int[][] genChunk(int[] features, int[] openings, int xSize, int ySize) {
		//arrays for centers are [x, y, type]
		List<int[]> hotBlocks = new List<int[]>();
		List<int[]> listReturn = new List<int[]>();
        int tempX, tempY;
		int toGo = 0;

        for (int x = 0; x < xSize; x++) {
            List<int> tempList = new List<int>();

            for (int y = 0; y < ySize; y++) {
                tempList.Add(0);
            }

            listReturn.Add(tempList.ToArray());
        }

        for (int x = 0; x < numberOfFeatures; x++) {
			toGo += features[x];
		}
		
		//add all the edges as centers
		for(int x = 0; x < 12; x++) {
			if(openings[x] != numberOfFeatures) {
                tempX = tempY = 0;
				if(x == 4 | x == 5) {
					tempY = (int)((float)ySize * 0.33);
				}
				
				if(x == 6 | x == 7) {
					tempY = (int)((float)ySize * 0.66);
				}
				
				if(x == 1 | x == 2) {
					tempX = (int)((float)xSize * 0.33);
				}
				
				if(x == 9 | x == 10) {
					tempX = (int)((float)xSize * 0.66);
				}
				
				if(x == 3 || x == 5 || x == 7 || x == 11) {
					tempX = xSize - 1;
				}
				
				if(x == 8 || x == 9 || x == 10 || x == 11) {
					tempY = ySize - 1;
				}
				
				hotBlocks.Add(new [] {tempX, tempY, openings[x]});
				listReturn[tempX][tempY] = x;
			}
		}

        //Debug.Log(hotBlocks.Count);

		//calculate centers for edgeless features
		//aon this is random
		for(int x = 0; x < numberOfFeatures; x++) {
            //Debug.Log(features[x]);
			if(features[x] != 0) {
                bool found = false;
                //Debug.Log("wtf");
                for (int y = 0; y < 12; y++) {
                    if(openings[y] == x) {
                        found = true;
                    }

                    if(y == 11 && found == false) {
                        tempX = rand.Next(0, xSize);
                        tempY = rand.Next(0, ySize);
                        hotBlocks.Add(new[] { tempX, tempY, x });
                        listReturn[tempX][tempY] = x;
                    }
				}
			}
		}
		
		//hot blocks walk into neigbors at random
		while(toGo > 0) {
			int growBlock = rand.Next(0, hotBlocks.Count);
            //Debug.Log(toGo);
			int currentFeature = hotBlocks[growBlock][2];
			List<int[]> possibleFeatures = new List<int[]>();
            //Debug.Log(currentFeature);
			if(features[currentFeature] > 0) {
			
				//leftside and topside represent if a block is at the x or y edge of the chunk
				bool leftSide = (hotBlocks[growBlock][0] == 0);
				bool rightSide = (hotBlocks[growBlock][0] == (xSize - 1));
				bool topSide = (hotBlocks[growBlock][1] == 0);
				bool bottomSide = (hotBlocks[growBlock][1] == (ySize - 1));
				
				if(!leftSide) {
					if(!topSide) {
						possibleFeatures.Add(new [] {hotBlocks[growBlock][0] - 1, hotBlocks[growBlock][1] - 1, currentFeature});
					}
					
					if(!bottomSide) {
						possibleFeatures.Add(new [] {hotBlocks[growBlock][0] - 1, hotBlocks[growBlock][1] + 1, currentFeature});
					}
					
					possibleFeatures.Add(new [] {hotBlocks[growBlock][0] - 1, hotBlocks[growBlock][1], currentFeature});
				}
				
				if(!rightSide) {
					if(!topSide) {
						possibleFeatures.Add(new [] {hotBlocks[growBlock][0] + 1, hotBlocks[growBlock][1] - 1, currentFeature});
					}
					
					if(!bottomSide) {
						possibleFeatures.Add(new [] {hotBlocks[growBlock][0] + 1, hotBlocks[growBlock][1] + 1, currentFeature});
					}
					
					possibleFeatures.Add(new [] {hotBlocks[growBlock][0] + 1, hotBlocks[growBlock][1], currentFeature});
				}
				
				if(!topSide) {
					possibleFeatures.Add(new [] {hotBlocks[growBlock][0], hotBlocks[growBlock][1] - 1, currentFeature});
				}
				
				if(!bottomSide) {
					possibleFeatures.Add(new [] {hotBlocks[growBlock][0], hotBlocks[growBlock][1] + 1, currentFeature});
				}
				
				int randomWalk = rand.Next(0, possibleFeatures.Count);
				hotBlocks.Add(possibleFeatures[randomWalk]);
                features[currentFeature]--;

                //wump wump the lake is 4 not 0
                if (currentFeature == 0) {
                    currentFeature = 4;
                }

                listReturn[possibleFeatures[randomWalk][0]][possibleFeatures[randomWalk][1]] = currentFeature;
				toGo--;
			}
		}

        return listReturn.ToArray();
	}
	
	//PARAMETERS
	//chunkMap:
	// 2d array of size [chunk width][chunk height]
	// where each entry is an int value for map generation
	//
	//xStart:
	// x position where the placement of blocks starts
	//
	//yStart:
	// y posiiton where the placement of blocks starts
	public void buildChunk(GameObject[,,] chunkSpace, int[][] chunkMap, float xStart, float yStart, int index, int xCurrent = 0, int yCurrent = 0) {
		int newX = xCurrent, newY = yCurrent, workingZ = 0, blockNum;

		if(yCurrent == chunkMap[0].Length) {
			if(xCurrent == (chunkMap.Length - 1)) {
				return;
			}
			
			else {
				newX++;
                newY = 0;
			}
		}

		blockNum = chunkMap[newX][newY];
		
		if(blockNum == 4) {
			buildChunk(chunkSpace, chunkMap, xStart, yStart, index, newX, (newY + 1));
			return;
		}
		
		chunkSpace[newX, newY, workingZ] = Instantiate(regularCube, new Vector3((xStart + (float)newX),  workingZ, (yStart + (float)newY)), Quaternion.identity);
		
		if(blockNum == 0) {
			buildChunk(chunkSpace, chunkMap, xStart, yStart, index, newX, (newY + 1));
            return;
		}

		if(blockNum < 4 || blockNum > 8) {
			workingZ++;
			chunkSpace[newX, newY, workingZ] = Instantiate(regularCube, new Vector3((xStart + (float)newX),  workingZ, (yStart + (float)newY)), Quaternion.identity);

            if (blockNum == 3) {
                chunkSpace[newX, newY, workingZ + 2] = Instantiate(regularCube, new Vector3((xStart + (float)newX), (workingZ + 2), (yStart + (float)newY)), Quaternion.identity);
                chunkSpace[newX, newY, workingZ + 3] = Instantiate(regularCube, new Vector3((xStart + (float)newX), (workingZ + 3), (yStart + (float)newY)), Quaternion.identity);
                blockNum--;
            }

            if (blockNum == 2) {
                chunkSpace[newX, newY, workingZ + 1] = Instantiate(regularCube, new Vector3((xStart + (float)newX), (workingZ + 1), (yStart + (float)newY)), Quaternion.identity);
            }
			
			if(blockNum < 4) {
				buildChunk(chunkSpace, chunkMap, xStart, yStart, index, newX, (newY + 1));
				return;
			}
			
			blockNum -= 4;
		}
	
		blockNum -= 5;
		workingZ++; 
		
		chunkSpace[newX, newY, workingZ] = Instantiate(tiltedCube, new Vector3((xStart + (float)newX),  workingZ, (yStart + (float)newY)), Quaternion.identity);
		//chunkSpace[newX, newY, workingZ].name = (string)('I' + index.ToString() + 'X' + newX.ToString() + 'Y' + newY.ToString() + 'Z' + workingZ.ToString() + 'T');
		chunkSpace[newX, newY, workingZ].transform.Rotate((blockNum * 90), 0, 0);

        buildChunk(chunkSpace, chunkMap, xStart, yStart, index, newX, (newY + 1));
		return;
	}

    //Map initiation at game start.
    void Start() {
        int index = 0;
        MapData worldMap;

        if (sizeX % 2 == 0) {
            throw new System.ArgumentException("Map x dimension cannot be even.", "sizeX");
        }

        if (sizeY % 2 == 0) {
            throw new System.ArgumentException("Map y dimension cannot be even.", "sizeY");
        }

        worldMap = assignFeatures(new int[] { 8000, 8000, 100, 1000 }, 2, (sizeX * sizeY));
        setEdges(worldMap.mapChunk, sizeX, sizeY);

        for(int x = 0; x < (sizeX * sizeY); x++) {
            chunkData.Add(genChunk(worldMap.mapChunk[x].tileAmount.ToArray(), worldMap.mapChunk[x].edgeType.ToArray(), chunkSizeX, chunkSizeY));
            Debug.Log("Generated Chunk " + x);
        }

        for(int x = 0; x < sizeX; x++) {
            for(int y = 0; y < sizeY; y++) {
                worldData.Add(new GameObject[28, 28, 5]);
                buildChunk(worldData[(x * sizeX) + y], chunkData[(x * sizeX) + y], x * chunkSizeX, y * chunkSizeY, index);
            }
        }

        //tile all nine loaded chunks
        //load relevent shades/fireflies
        //wump wump couldn't name cubes during instantiation
		//for...each?
		int numChunks = 0;
        for(int numChunksX = 0; numChunks < sizeY; numChunksX++) {
			for(int numChunksY = 0; numChunks < sizeX; numChunksY++) {
				for(int x = 0; x < chunkSizeX; x++) {
					for(int y = 0; y < chunkSizeY; y++) {
						for(int z = 0; z < 5; z++) {
							if(worldData[numChunks][x, y, z]) {
								worldData[numChunks][x, y, z].name = (string)('I' + numChunks.ToString() + 'X' + x.ToString() + 'Y' + y.ToString() + 'Z' + z.ToString() + 'R' + currentRing(numChunksX, numChunksY, sizeX - 1, sizeY - 1));
							}
						}
					}
				}
				numChunks++;
            }
        }

        this.gameObject.GetComponent<MapFeatureGeneration>().beginGeneration(worldData);
    }

	public int currentRing(int cX, int cY, int mX, int mY) {
		int careAbout = 0;
		
		if(cX >= (mX / 2)) {
			cX = (cX % ((mX / 2) + 1));
		}
		
		if(cY >= (mY / 2)) {
			cY = (cY % ((mY / 2) + 1));
		}
		
		if(cX > cY) {
			careAbout = cY;
		}
		
		if(cX <= cY) {
			careAbout = cX;
		}
		
		int ringSize = mX / 6;
		
		return 4 - (int)(careAbout / ringSize);
	}

    //Called on frame update, used to update what chunks are being rendered and their tiling.
    void Update() {
    //check if chunk fell out of range and dump it to load the new area
    //generate relevent shades/fireflies in the new area
    //tile the new chunk
    }
}