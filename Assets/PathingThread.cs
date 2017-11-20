using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PathingThread : MonoBehaviour{
	public static PathingThread singleton;
	[SerializeField]
	Map map;

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

	void OnApplicationQuit()
    {
		workerThread.Abort();
    }


	void PathFinding(){
		PathAgent currentAgent;
		try{

			while(true){
				//Debug.Log("This is thread");


				if(agents.Count > 0){
					lock(agents){
						currentAgent = agents[curAgentIndex];

						while(currentAgent == null){
							agents.RemoveAt(curAgentIndex);
							currentAgent = agents[curAgentIndex];
							if(curAgentIndex >= agents.Count){
								curAgentIndex = 0;
							}
						}

						curAgentIndex++;
					}

					map.FindPath(currentAgent);

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
		catch(System.Exception e){
			Debug.Log(e);
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




}
