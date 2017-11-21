using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
	Node[,] nodes;

	float curTime;
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

	void Update(){
		curTime = Time.time;
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


				if(movement == 0){
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
		//UnityEngine.Debug.Log(curTime);
		if(agent.LastRequestTime + 0.5f > curTime){
			return;//Wait 500 ms between each agent request
		}

		Vector3 curPos = agent.CurPos;
		Vector3 targetPos = agent.TargetPos;
		Node curNode, targetNode;



		curNode = this.PosToNode(curPos);
		targetNode = this.PosToNode(targetPos);
		agent.Path = IterativeDeepeningAStar(curNode, targetNode);
		agent.LastRequestTime = curTime;
	}


	Node[] IterativeDeepeningAStar(Node start, Node target){
		UnityEngine.Debug.Log("start node is: " + start.X + ", " + start.Y);
		Node[] lastPath = null;
		int curDepth = 1;

		int msLimit = 14;
		int msHalfLimit = msLimit / 2;

		Stopwatch sw = new Stopwatch();
		sw.Start();

		while(sw.Elapsed.TotalMilliseconds < msHalfLimit){
			lastPath = DepthLimitedAStar(start, target, curDepth);
			curDepth++;
		}

		sw.Stop();

		int timePassed = (int)sw.Elapsed.TotalMilliseconds;
		if(timePassed > msLimit){
			UnityEngine.Debug.LogWarning("Pathfinding took " + timePassed + " before it was able to abort.");
		}

		return lastPath;
	}

	Node[] DepthLimitedAStar(Node start, Node target, int maxDepth){
		List<Node> open = new List<Node>();//TODO: replace this with a heap
		List<Node> closed = new List<Node>();
		List<Node> fringe = new List<Node>();//TODO: replace this with a heap
		open.Add(start);
		start.Depth = 0;

		Node cur;

		while(open.Count != 0){
			cur = Node.LowestFCost(open);

			if(cur.Depth == maxDepth){
				fringe.Add(cur);
			}

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
					n.Depth = cur.Depth + 1;

					if(!open.Contains(n) && cur.Depth < maxDepth){
						open.Add(n);
					}
				}
			}

		}


		return Node.RebuildPath(Node.LowestFCost(fringe));
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

