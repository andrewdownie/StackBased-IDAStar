using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node{
	float movement;
	Map parent;
	[SerializeField]
	int id;
	[SerializeField]
	int x, y;

	public Node(Map parent, int id, int x, int y, float movement){
		this.movement = movement;
		this.parent = parent;
		this.id = id;
		this.x = x;
		this.y = y;
	}


	//TODO: pretty sure x and y are mixed up-------------------------------------------------
	public int X{
		get{return x;}
	}

	public int Y{
		get{return y;}
	}

	public float Movement{
		get{return movement;}
	}

	public int ID{
		get{return id;}
	}

}


public class Map : MonoBehaviour {
	[SerializeField]
	List<Node> testingNeighbours;// ---------

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
	Node[,] nodes;
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

	public int Width{
		get{return width;}
	}
	public int Height{
		get{return height;}
	}

	void Start(){
		nodes = new Node[width, height];

		//Fill the map with random movement speeds
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				float movement = Random.Range(0f, 1f);
				if(movement < 0.1f){
					movement = 0;
				}
				int id = y * width + x;
				nodes[x, y] = new Node(this, id, x, y, movement);
			}
		}
	
	}

	void OnDrawGizmos(){
		if(nodes == null || nodes.Length == 0){ return; }

		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				float movement = nodes[x, y].Movement;


				if(testingNeighbours.Contains(nodes[x, y])){
					Gizmos.color = Color.yellow;
				}
				else if(movement == 0){
					Gizmos.color = Color.red;
				}
				else{
					Gizmos.color = new Color(movement, movement, movement);
				}
				Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(0.6f, 0.1f, 0.6f));

			}
		}
	}



	public void FindPath(PathAgent agent){
		Vector3 curPos = agent.CurPos;
		Vector3 targetPos = agent.TargetPos;
		Node curNode, targetNode;



		//agent.Path = IDAStar(curPos, targetPos);
		curNode = this.PosToNode(curPos);
		//targetNode = this.PosToNode(targetPos);
		Debug.Log("Neighbours are... " + curPos + " - " + curNode.X + "," + curNode.Y);
		testingNeighbours = new List<Node>(this.Neighbours(curNode));
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

	public Node this[int x, int y]{
		get{

			if(x >= width){
				x = width - 1;
			}
			else if(x < 0){
				x = 0;
			}

			if(y >= height){
				y = height - 1;
			}
			else if(y < 0){
				y = 0;
			}

			return nodes[x, y];
		}
	}

}

