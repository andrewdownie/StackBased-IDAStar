using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode{
	int hCost, gCost;
	int id;

	[SerializeField]
	PathNode pathParent;

	int depth;

	public PathNode(int id){
		pathParent = null;
		this.id = id;
		hCost = 0;
		gCost = 0;
	}


	public int ID{
		get{return id;}
		set{id = value;}
	}
	public int Depth{
		get{return depth;}
		set{depth = value;}
	}

	public PathNode PathParent{
		get{return pathParent;}
		set{pathParent = value;}
	}


	public int HCost{
		get{return hCost;}
		set{hCost = value;}
	}

	public int GCost{
		get{return gCost;}
		set{gCost = value;}
	}

	public int FCost{
		get{return hCost + gCost;}
	}

	public static int[] RebuildPath(PathNode targetNode){
		List<int> path = new List<int>();
		PathNode curNode = targetNode;

		while(curNode != null){
			path.Add(curNode.ID);
			curNode = curNode.PathParent;
		}

		path.Reverse();

		return path.ToArray();
	}

	public static PathNode LowestFCost(List<PathNode> nodes){
		PathNode lowest;

		if(nodes.Count == 0){
			return new PathNode(-1);//TODO: what should I return here
		}

		lowest = nodes[0];

		foreach(PathNode n in nodes){
			if(n.FCost < lowest.FCost){
				lowest = n;
			}
			else if(n.FCost == lowest.FCost && n.GCost < lowest.GCost){
				lowest = n;
			}
		}

		return lowest;
	}

	public static int NodeDist(Map map, PathNode a, PathNode b){
		//TODO: how do I add node weighting to this? ---------------------
		int dx, dy;
		dx = Mathf.Abs(map.PathNodeX(a) - map.PathNodeX(b));
		dy = Mathf.Abs(map.PathNodeY(a) - map.PathNodeY(b));


		int straight, angle;
		straight = Mathf.Min(dx, dy);
		angle = Mathf.Max(dx, dy);


		return Mathf.RoundToInt(angle * 14f + straight * 10);
	}
}
