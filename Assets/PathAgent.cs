using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent : MonoBehaviour {
	string objectName;
	void Start(){
		objectName = transform.name;
		SearchManager.singleton.RegisterPathAgent(this);
	}
	void OnDestroy() {
		SearchManager.singleton.DeregisterPathAgent(this);
    }

	public void FindPath(){
		//NOTE: warning, this is not run on main unity thread, I'm not sure what this is allowed to touch that won't cause a black hole to appear and destroy whatever city you happen to be in when you modify this code. BE CAREFUL.
		//TODO: put pathfinding here, it will be run on the pathfinding thread
		Debug.Log("This is find path for object: " + objectName);
	}

}
