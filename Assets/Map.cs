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
	byte[] movementModifier;
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

	void Start(){
		movementModifier = new byte[width * height];

		//Fill the map with random movement speeds
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				int index = x * height + y;
				movementModifier[index] = (byte)Random.Range(0, 255);
				if(movementModifier[index] > 200){
					movementModifier[index] = 0;
				}
			}
		}
	
	}

	void OnDrawGizmos(){
		if(movementModifier == null || movementModifier.Length == 0){ return; }

		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				byte b = movementModifier[x * height + y];

				if(b == 0){
					Gizmos.color = Color.red;
				}
				else{
					float hue = (float)(b + 55)/255;
					Gizmos.color = new Color(hue, hue, hue);
				}
				Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(0.6f, 0.1f, 0.6f));

			}
		}
	}



	public void FindPath(PathAgent agent){
		Vector2 curPos = agent.CurPos;
		Vector2 targetPos = agent.TargetPos;

		int[] newPath = null;

		//TODO: put pathfinding here

		agent.Path = newPath;
	}

}

