using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent : MonoBehaviour {

	[SerializeField]
	Vector3 curPos, targetPos;

	Node[] curPath;

	float lastRequestTime;


	void Start(){
		PathingThread.singleton.RegisterPathAgent(this);
	}
	void OnDestroy() {
		PathingThread.singleton.DeregisterPathAgent(this);
    }

	void Update(){
		//curPos = new Vector2(transform.position.x, transform.position.z);

		//TODO: follow path here ------------------------------
	}

	void OnDrawGizmos(){
		if(curPath == null || curPath.Length == 0){ return; }

		for(int i = 0; i < curPath.Length; i++){
			Node n = curPath[i];

			if(i == 0){
				Gizmos.color = new Color(70, 255, 0);
			}
			else if(i == curPath.Length - 1){
				Gizmos.color = new Color(150, 0, 150);
			}
			else{
				Gizmos.color = new Color(0, 150, 150);
			}
			Gizmos.DrawCube(new Vector3(n.X, 3, n.Y), new Vector3(0.3f, 1f, 0.3f));
		}

	}

	public Vector3 CurPos{get{return new Vector3(curPos.x, curPos.y, curPos.z);}}
	public Vector3 TargetPos{get{return targetPos;}}

	public Node[] Path{set{curPath = value;}}

	public float LastRequestTime{
		get{return lastRequestTime;}
		set{lastRequestTime = value;}
	}


}