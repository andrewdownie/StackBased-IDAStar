using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent : MonoBehaviour {
	string objectName;

	[SerializeField]
	Vector2 curPos, targetPos;

	int[] curPath;


	void Start(){
		targetPos = new Vector2(Random.Range(0, 9), Random.Range(0, 9));
		objectName = transform.name;
		PathingThread.singleton.RegisterPathAgent(this);
	}
	void OnDestroy() {
		PathingThread.singleton.DeregisterPathAgent(this);
    }

	void Update(){
		//curPos = new Vector2(transform.position.x, transform.position.z);

		//TODO: follow path here ------------------------------
	}

	public Vector2 CurPos{get{return curPos;}}
	public Vector2 TargetPos{get{return targetPos;}}

	public int[] Path{set{curPath = value;}}


}