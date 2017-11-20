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

	public Node(Map map, int id, int x, int y, float movement){
		this.movement = movement;
		this.map = map;
		this.id = id;
		this.x = x;
		this.y = y;

		hCost = 0;
		gCost = 0;
		pathParent = null;
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

}
