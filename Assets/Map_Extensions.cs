using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Map_Extensions{

	public static Node[] Neighbours(this Map map, Node node){
		bool top = false, left = false, bottom = false, right = false;
		Node[] neighbours = null;
		int x, y;

		y = node.Y;
		x = node.X;
		//Debug.Log("neighbours of node: " + x + ", " + y);

		top = y == 0;
		bottom = y == map.Height - 1;
		left = x == 0;
		right = x == map.Width - 1;

		//Debug.Log("x,y: " + y + "," + x);
		//Debug.Log(top + ", " + left + ", " + bottom + ", " + right);


		//Very unclever, but should perform fast enough
		if(top){
			if(left){
				neighbours = new Node[3];
				neighbours[0] = map[x + 1, y];//Right
				neighbours[1] = map[x + 1, y + 1];//Bottom right
				neighbours[2] = map[x, y + 1];//Bottom
			}
			else if(right){
				neighbours = new Node[3];
				neighbours[0] = map[x, y + 1];//Bottom
				neighbours[1] = map[x - 1, y + 1];//Bottom left
				neighbours[2] = map[x - 1, y];//Left
			}
			else{
				neighbours = new Node[5];
				neighbours[0] = map[x + 1, y];//Right
				neighbours[1] = map[x + 1, y + 1];//Bottom right
				neighbours[2] = map[x, y + 1];//Bottom
				neighbours[3] = map[x - 1, y + 1];//Bottom left
				neighbours[4] = map[x - 1, y];//Left
			}
		}
		else if(bottom){
			if(left){
				neighbours = new Node[3];
				neighbours[0] = map[x, y - 1];//Top
				neighbours[1] = map[x + 1, y - 1];//Top right
				neighbours[2] = map[x + 1, y];
			}
			else if(right){
				neighbours = new Node[3];
				neighbours[0] = map[x - 1, y - 1];//Top left
				neighbours[1] = map[x, y - 1];//Top
				neighbours[2] = map[x - 1, y];//Left
			}
			else{
				neighbours = new Node[5];
				neighbours[0] = map[x - 1, y - 1];//Top left
				neighbours[1] = map[x, y - 1];//Top
				neighbours[2] = map[x + 1, y - 1];//Top right
				neighbours[3] = map[x + 1, y];//Right
				neighbours[4] = map[x - 1, y];//Left
			}
		}
		else if(left){
			neighbours = new Node[5];
			neighbours[0] = map[x, y - 1];//Top
			neighbours[1] = map[x + 1, y - 1];//Top right
			neighbours[2] = map[x + 1, y];//Right
			neighbours[3] = map[x + 1, y + 1];//Bottom right
			neighbours[4] = map[x, y + 1];//Bottom
		}
		else if(right){
			neighbours = new Node[5];
			neighbours[0] = map[x - 1, y - 1];//Top left
			neighbours[1] = map[x, y - 1];//Top
			neighbours[2] = map[x, y + 1];//Bottom
			neighbours[3] = map[x - 1, y + 1];//Bottom left
			neighbours[4] = map[x - 1, y];//Left
		}
		else{
			neighbours = new Node[8];
			neighbours[0] = map[x - 1, y - 1];//Top left
			neighbours[1] = map[x, y - 1];//Top
			neighbours[2] = map[x + 1, y - 1];//Top right
			neighbours[3] = map[x + 1, y];//Right
			neighbours[4] = map[x + 1, y + 1];//Bottom right
			neighbours[5] = map[x, y + 1];//Bottom
			neighbours[6] = map[x - 1, y + 1];//Bottom left
			neighbours[7] = map[x - 1, y];//Left
		}



		return neighbours;
	}


	public static Node PosToNode(this Map map, Vector3 pos){
		Node n = null;

		int x = (int)pos.x;
		int y = (int)pos.z;

		n = map[x, y];

		return n;
	}


	public static int NodeDist(this Map map, Node a, Node b){
		//TODO: how do I add node weighting to this? ---------------------
		int dx, dy;
		dx = Mathf.Abs(a.X - b.X);
		dy = Mathf.Abs(a.Y - b.Y);


		int straight, angle;
		straight = Mathf.Min(dx, dy);
		angle = Mathf.Max(dx, dy);


		return Mathf.RoundToInt(angle * 14f + straight * 10);
	}




}
