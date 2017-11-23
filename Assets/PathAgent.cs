using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent : MonoBehaviour {

	[SerializeField]
	Vector3 curPos, targetPos;

	[SerializeField]
	int[] curPath;

	float lastRequestTime;


	void Start(){
		//TODO: this is for testing
		curPos = new Vector3(Random.Range(0, 49), 0, Random.Range(0, 49));
		targetPos = new Vector3(Random.Range(0, 49), 0, Random.Range(0, 49));


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
			int n = curPath[i];

			if(i == 0){
				Gizmos.color = new Color(70, 255, 0);
			}
			else{
				Gizmos.color = new Color(0, 150, 150);
			}
			int mapWidth = PathingThread.singleton.Map.Width;
			int x = n % mapWidth;
			int y = n / mapWidth;


			Gizmos.DrawCube(new Vector3(x, 3, y), new Vector3(0.3f, 1f, 0.3f));
		}

	}

	public Vector3 CurPos{get{return new Vector3(curPos.x, curPos.y, curPos.z);}}
	public Vector3 TargetPos{get{return targetPos;}}

	public void SetAgentPath(int[] newPath, float lastRequestTime){
		this.lastRequestTime = lastRequestTime;
		curPath = newPath;
	}


	public float LastRequestTime{
		get{return lastRequestTime;}
	}


}