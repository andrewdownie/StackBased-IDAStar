using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Node{
	[SerializeField]
	float movement;
	int id;

	public Node(int id, float movement){
		this.movement = movement;
		this.id = id;
	}

	public float Movement{
		get{return movement;}
	}

	public int ID{
		get{return id;}
	}

}
