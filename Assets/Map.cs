using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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


	//TODO: candidate to become extension method
	public void FindPath(PathAgent agent){
		Vector3 curPos = agent.CurPos;
		Vector3 targetPos = agent.TargetPos;
		Node curNode, targetNode;



		curNode = this.PosToNode(curPos);
		targetNode = this.PosToNode(targetPos);
		agent.Path = IterativeDeepeningAStar(curNode, targetNode);
		//testingNeighbours = new List<Node>(this.Neighbours(curNode));
	}


	Node[] IterativeDeepeningAStar(Node start, Node target){
		//TODO: put a time limit, and then perform IDAStar until that time limit is hit, return the most recent completed result
		Node[] lastPath = null;
		int curDepth = 1;

		for(int i = 0; i < 10; i++){//TOOD: change to time, instead of depth
			lastPath = DepthLimitedAStar(start, target, curDepth);
			curDepth++;
		}
		

		return lastPath;
	}

	Node[] DepthLimitedAStar(Node start, Node target, int depth){
		List<Node> open = new List<Node>();
		List<Node> closed = new List<Node>();
		open.Add(start);

		Node cur;

		while(open.Count != 0){
			cur = open[0];//Node with lowest f-cost
			open.Remove(cur);
			closed.Add(cur);

			if(cur == target){
				return Node.RebuildPath(target);
			}

			foreach(Node n in this.Neighbours(cur)){
				if(n.Movement == 0 || closed.Contains(n)){
					continue;
				}

				if(!open.Contains(n) || cur.GCost + this.NodeDist(cur, n) < n.GCost){
					n.GCost = cur.GCost + this.NodeDist(cur, n);
					n.HCost = this.NodeDist(n, target);
					n.PathParent = cur;
					if(!open.Contains(n)){
						open.Add(n);
					}
				}
			}

		}

		return null;
	}

	Node LowestFCost(Node[] nodes){
		Node lowest;
		lowest = nodes[0];

		foreach(Node n in nodes){
			if(n.FCost < lowest.FCost){
				lowest = n;
			}
			else if(n.FCost == lowest.FCost && n.GCost < lowest.GCost){
				lowest = n;
			}
		}

		return lowest;
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

