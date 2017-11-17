using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class SearchManager : MonoBehaviour{
	public static SearchManager singleton;

	Thread workerThread;
	List<PathAgent> agents;
	int curAgentIndex;

	void Awake(){
		singleton = this;

		agents = new List<PathAgent>();
		curAgentIndex = 0;

		Debug.Log("Start thread");

		workerThread = new Thread(PathFinding);
		workerThread.Start();
	}


	void PathFinding(){
		PathAgent currentAgent;

		while(true){
			//Debug.Log("This is thread");
			


			if(agents.Count > 0){
				lock(agents){
					currentAgent = agents[curAgentIndex];
					if(currentAgent == null){
						agents.RemoveAt(curAgentIndex);
					}
					else{
						curAgentIndex++;
					}
				}
				currentAgent.FindPath();
				if(curAgentIndex >= agents.Count){
					curAgentIndex = 0;
				}
			}
			else{
				curAgentIndex = 0;
			}

			 Thread.Sleep(5);
		}


	}

	public void RegisterPathAgent(PathAgent newAgent){
		if(agents.Contains(newAgent) == false){
			lock(agents){
				agents.Add(newAgent);
			}
		}
	}



	public void DeregisterPathAgent(PathAgent existingAgent){
		if(agents.Contains(existingAgent) == true){
			lock(agents){
				agents.Remove(existingAgent);
			}
		}
	}

	 void OnApplicationQuit()
    {
		workerThread.Abort();
    }



}
