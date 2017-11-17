using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapChunk : MonoBehaviour {
	[Header("World")]
	[SerializeField]
	Vector3 chunkWorldPosition;
	[SerializeField]
	Vector3 chunkWorldScale;

	[Header("Chunk")]
	[SerializeField]
	Vector3 chunkDimensions;
	[SerializeField]
	byte[] movementModifier;
	//Variable: movement ----
	//	Inverse percent.
	//	100 means the player moves regular speed.
	//	50 means the player moves twice as fast
	//	200 means the player moves half speed.
	//	Does 0 mean unwalkable? ...I think it does.
	//	(SO when calculating weighting for pathfinding, the distance being moved will be multiplied by the movementModifier.
	//		the lower the modifier the lower the weight, the better the path. That's why it's inverted.)
	//	(WHEN doing player movement, the player movement speed can be stored already divided by 1/100, then simply multiply the
	//		player movement speed by the movmentModifier of the tile they are touching, and it will act as a movement speed percent modifier)
}

