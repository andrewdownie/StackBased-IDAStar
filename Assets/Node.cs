using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node{
	float movement;
	Map map;
	[SerializeField]
	int id;
	[SerializeField]
	int x, y;
	int hCost, gCost;

	[SerializeField]
	Node pathParent;

	int depth;

	public void Clear(){
		hCost = 0;
		gCost = 0;
		pathParent = null;
		depth = 0;
	}

	public Node(Map map, int id, int x, int y, float movement){
		this.movement = movement;
		this.map = map;
		this.id = id;
		this.x = x;
		this.y = y;

		pathParent = null;
		hCost = 0;
		gCost = 0;
		depth = 0;
	}

	public int Depth{
		get{return depth;}
		set{depth = value;}
	}

	public Node PathParent{
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

	public static Node[] RebuildPath(Node targetNode){
		List<Node> path = new List<Node>();
		Node curNode = targetNode;

		while(curNode != null){
			path.Add(curNode);
			curNode = curNode.PathParent;
		}

		path.Reverse();

		return path.ToArray();
	}

	public static Node LowestFCost(List<Node> nodes){
		Node lowest;

		if(nodes.Count == 0){
			return null;
		}

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

}
