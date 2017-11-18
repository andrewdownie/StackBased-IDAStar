using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
	[Header("World")]
	[SerializeField]
	Vector3 chunkWorldPosition;
	/*
	[SerializeField]
	Vector3 chunkWorldScale;
	*/

	[Header("Map")]
	[SerializeField]
	int width;
	[SerializeField]
	int height;
	[SerializeField]
	byte[] movementModifier;
	//Variable: movement ----
	//	Inverse percent. Think about it as modifying the effective distance the player has to move to get to a point.
	//	100 means the player moves regular speed.
	//	50 means the player moves twice as fast
	//	200 means the player moves half speed.
	//	Does 0 mean unwalkable? ...I think it does.
	//	(SO when calculating weighting for pathfinding, the distance being moved will be multiplied by the movementModifier.
	//		the lower the modifier the lower the weight, the better the path. That's why it's inverted.)
	//	(WHEN doing player movement, the player movement speed can be stored already divided by 1/100, then simply multiply the
	//		player movement speed by the movmentModifier of the tile they are touching, and it will act as a movement speed percent modifier)

	void Start(){
		movementModifier = new byte[width * height];

		//Fill the map with random movement speeds
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				int index = x * height + y;
				movementModifier[index] = (byte)Random.Range(0, 255);
				if(movementModifier[index] > 200){
					movementModifier[index] = 0;
				}
			}
		}
	
	}

	void OnDrawGizmos(){
		if(movementModifier == null || movementModifier.Length == 0){ return; }

		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				byte b = movementModifier[x * height + y];

				if(b == 0){
					Gizmos.color = Color.red;
				}
				else{
					float hue = (float)(b + 55)/255;
					Gizmos.color = new Color(hue, hue, hue);
				}
				Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(0.6f, 0.1f, 0.6f));

			}
		}
	}



	public void FindPath(PathAgent agent){
		Vector2 curPos = agent.CurPos;
		Vector2 targetPos = agent.TargetPos;
		agent.Path = IDAStar(curPos, targetPos);
	}

	public int[] IDAStar(Vector2 curPos, Vector2 targetPos){
		int start = (int)curPos.x * height + (int)curPos.y;
		int target = (int)targetPos.x * height + (int)targetPos.y;

		int[] neighbours = Neighbours(start);
		string output = "{";
		foreach(int i in neighbours){
			output += i + ",";
		}
		output += "}";
		Debug.Log(output);

		return null;
	}



	int[] Neighbours(int start){
		int[] neighbours = null;
		int row, col;

		row = start / width;
		col = start % width;

		Debug.Log("x,y: " + row + "," + col);

		//TODO: since it wraps around, how do I detect edges?.
		//Top left is zero, counter clockwise movement
		bool top = false, left = false, bottom = false, right = false;

		top = row == 0;
		bottom = row == height - 1;
		left = col == 0;
		right = col == width - 1;


		//Very unclever, but should perform fast
		if(top){
			if(left){
				neighbours = new int[3];
				neighbours[0] = start + 1;//Right
				neighbours[1] = start + width + 1;//Bottom right
				neighbours[2] = start + width;//Bottom
			}
			else if(right){
				neighbours = new int[3];
				neighbours[5] = start + width;//Bottom
				neighbours[6] = start + width - 1;//Bottom left
				neighbours[7] = start - 1;//Left
			}
			else{
				neighbours = new int[5];
				neighbours[0] = start + 1;//Right
				neighbours[1] = start + width + 1;//Bottom right
				neighbours[2] = start + width;//Bottom
				neighbours[3] = start + width - 1;//Bottom left
				neighbours[4] = start - 1;//Left
			}
		}
		else if(bottom){
			if(left){
				neighbours = new int[3];
				neighbours[1] = start - width;//Top
				neighbours[2] = start - width + 1;//Top right
				neighbours[3] = start + 1;//Right
			}
			else if(right){
				neighbours = new int[3];
				neighbours[0] = start - width - 1;//Top left
				neighbours[1] = start - width;//Top
				neighbours[2] = start - 1;//Left
			}
			else{
				neighbours = new int[5];
				neighbours[0] = start - width - 1;//Top left
				neighbours[1] = start - width;//Top
				neighbours[2] = start - width + 1;//Top right
				neighbours[3] = start + 1;//Right
				neighbours[4] = start - 1;//Left
			}
		}
		else{
			neighbours = new int[8];
			neighbours[0] = start - width - 1;//Top left
			neighbours[1] = start - width;//Top
			neighbours[2] = start - width + 1;//Top right
			neighbours[3] = start + 1;//Right
			neighbours[4] = start + width + 1;//Bottom right
			neighbours[5] = start + width;//Bottom
			neighbours[6] = start + width - 1;//Bottom left
			neighbours[7] = start - 1;//Left
		}



		return neighbours;
	}

}

