using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Map_Extensions{

	public static int[] Neighbours(this Map map, PathNode node){
		bool top = false, left = false, bottom = false, right = false;
		int[] neighbours = null;
		int x, y;

		//y = node.Y;
		//x = node.X;
		y = map.PathNodeY(node);
		x = map.PathNodeX(node);
		//Debug.Log("neighbours of node: " + x + ", " + y + ", id: " + node.ID);

		top = y == 0;
		bottom = y == map.Height - 1;
		left = x == 0;
		right = x == map.Width - 1;

		int id = node.ID;
		int width = map.Width;
		int height = map.Height;

		//Debug.Log("x,y: " + y + "," + x);
		//Debug.Log("T:" + top + ", L:" + left + ", B:" + bottom + ", R:" + right);


		//Very unclever, but should perform fast enough
		if(top){
			if(left){
				neighbours = new int[3];
				neighbours[0] = id + 1;//Right
				neighbours[1] = id + width + 1;//Bottom right
				neighbours[2] = id + width;//Bottom
			}
			else if(right){
				neighbours = new int[3];
				neighbours[0] = id + width;//Bottom
				neighbours[1] = id + width - 1;//Bottom left
				neighbours[2] = id - 1;//Left
			}
			else{
				neighbours = new int[5];
				neighbours[0] = id + 1;//Right
				neighbours[1] = id + width + 1;//Bottom right
				neighbours[2] = id + width;//Bottom
				neighbours[3] = id + width - 1;//Bottom left
				neighbours[4] = id - 1;//Left
			}
		}
		else if(bottom){
			if(left){
				neighbours = new int[3];
				neighbours[0] = id - width;//Top
				neighbours[1] = id - width + 1;//Top right
				neighbours[2] = id + 1;//Right
			}
			else if(right){
				neighbours = new int[3];
				neighbours[0] = id - width - 1;//Top left
				neighbours[1] = id - width;//Top
				neighbours[2] = id - 1;//Left
			}
			else{
				neighbours = new int[5];
				neighbours[0] = id - width - 1;//Top left
				neighbours[1] = id - width;//Top
				neighbours[2] = id - width + 1;//Top right
				neighbours[3] = id + 1;//Right
				neighbours[4] = id - 1;//Left
			}
		}
		else if(left){
			neighbours = new int[5];
			neighbours[0] = id - width;//Top
			neighbours[1] = id - width + 1;//Top right
			neighbours[2] = id + 1;//Right
			neighbours[3] = id + width + 1;//Bottom right
			neighbours[4] = id + width;//Bottom
		}
		else if(right){
			neighbours = new int[5];
			neighbours[0] = id - width - 1;//Top left
			neighbours[1] = id - width;//Top
			neighbours[2] = id + width;//Bottom
			neighbours[3] = id + width - 1;//Bottom left
			neighbours[4] = id - 1;//Left
		}
		else{
			neighbours = new int[8];
			neighbours[0] = id - width - 1;//Top left
			neighbours[1] = id - width;//Top
			neighbours[2] = id - width + 1;//Top right
			neighbours[3] = id + 1;//Right
			neighbours[4] = id + width + 1;//Bottom right
			neighbours[5] = id + width;//Bottom
			neighbours[6] = id + width - 1;//Bottom left
			neighbours[7] = id - 1;//Left
		}


		return neighbours;
	}


	public static int PosToNode(this Map map, Vector3 pos){
		int n = -1;

		int x = (int)pos.x;
		int y = (int)pos.z;

		n = map[x, y];

		return n;
	}






}
