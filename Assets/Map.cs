using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: it currently hangs on start...

public struct Node{
	public int id;
	public byte movement;

	public Node(int id, byte movement){
		this.movement = movement;
		this.id = id;
	}

}

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
	Node[] nodes;
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
		nodes = new Node[width * height];

		//Fill the map with random movement speeds
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				int index = x * height + y;
				byte b = (byte)Random.Range(0, 255);
				if(b > 200){
					b = 0;
				}
				int id = y * width + x;
				nodes[id] = new Node(id, b);
			}
		}
	
	}

	void OnDrawGizmos(){
		return;
		if(nodes == null || nodes.Length == 0){ return; }

		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				byte b = nodes[x * height + y].movement;

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


	int[] IDAStar(Vector2 curPos, Vector2 targetPos){
		//TODO: put a time limit, and then perform IDAStar until that time limit is hit, return the most recent completed result
		int start = (int)curPos.y * width + (int)curPos.x;
		int target = (int)targetPos.y * width + (int)targetPos.x;

		Debug.Log("Start: " + start);
		int[] lastPath = null;
		int curDepth = 1;

		for(int i = 0; i < 10; i++){//TOOD: change to time, instead of depth
			lastPath = _IDAStar(start, target, curDepth);
			curDepth++;
		}
		

		return lastPath;
	}

	int[] _IDAStar(int start, int target, int depth){
		List<int> open = new List<int>();//How do I store f-costs without using objects
		List<int> closed = new List<int>();
		open.Add(start);

		while(open.Count != 0){

		}

		return null;
	}



	int[] GetNeighbours(int start){
		bool top = false, left = false, bottom = false, right = false;
		int[] neighbours = null;
		int x, y;

		y = start / width;
		x = start % width;

		top = y == 0;
		bottom = y == height - 1;
		left = x == 0;
		right = x == width - 1;

		Debug.Log("x,y: " + y + "," + x);
		Debug.Log(top + ", " + left + ", " + bottom + ", " + right);


		//Very unclever, but should perform fast enough
		if(top){
			if(left){
				neighbours = new int[3];
				neighbours[0] = start + 1;//Right
				neighbours[1] = start + width + 1;//Bottom right
				neighbours[2] = start + width;//Bottom
			}
			else if(right){
				neighbours = new int[3];
				neighbours[0] = start + width;//Bottom
				neighbours[1] = start + width - 1;//Bottom left
				neighbours[2] = start - 1;//Left
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
				neighbours[0] = start - width;//Top
				neighbours[1] = start - width + 1;//Top right
				neighbours[2] = start + 1;//Right
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

