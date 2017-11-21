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
		PrintCurPath();
	}

	void OnDrawGizmos(){
		if(curPath == null || curPath.Length == 0){ return; }

		foreach(Node n in curPath){
			Gizmos.color = Color.yellow;
			Gizmos.DrawCube(new Vector3(n.X, 3, n.Y), new Vector3(0.3f, 1f, 0.3f));
		}

	}

	void PrintCurPath(){
		return;


		if(curPath == null){
			return;
		}

		string output = "{ ";
		foreach(Node n in curPath){
			output += "(" + n.X + "," + n.Y + "), ";
		}
		output += " }";
		Debug.Log(output);
	}

	public Vector3 CurPos{get{return curPos;}}
	public Vector3 TargetPos{get{return targetPos;}}

	public Node[] Path{set{curPath = value;}}

	public float LastRequestTime{
		get{return lastRequestTime;}
		set{lastRequestTime = value;}
	}


}