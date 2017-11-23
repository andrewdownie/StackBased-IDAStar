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
	Node[] nodes;

	public int Width{
		get{return width;}
	}
	public int Height{
		get{return height;}
	}

	public int NodeX(Node n){
		return n.ID % width;
	}
	public int NodeY(Node n){
		return n.ID / width;
	}
	public int PathNodeX(PathNode n){
		return n.ID % width;
	}
	public int PathNodeY(PathNode n){
		return n.ID / width;
	}

	void Start(){
		nodes = new Node[width * height];

		//Fill the map with random movement speeds
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				float movement = Random.Range(0f, 1f);
				if(movement < 0.1f){
					movement = 0;
				}
				int id = y * width + x;
				nodes[y * width + x] = new Node(id, movement);
			}
		}
	
	}

	void OnDrawGizmos(){
		if(nodes == null || nodes.Length == 0){ return; }

		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				float movement = nodes[y * width + x].Movement;


				if(movement == 0){
					Gizmos.color = Color.red;
				}
				else{
					Gizmos.color = new Color(movement, movement, movement);
				}
				Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(0.95f, 0.1f, 0.95f));

			}
		}
	}


	public Map CloneMap(){
		//TODO: hwo to clone array in C#
		//TODO: clone the nodes, clone the map
		return null;
	}

	public float Movement(int nodeID){
		return nodes[nodeID].Movement;
	}

	public int this[int x, int y]{
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

			return y * width + x;
		}
	}

}

