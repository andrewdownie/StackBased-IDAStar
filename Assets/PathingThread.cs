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

	float unityTime;

	public Map Map{
		//TODO: how will this work if the map is split into chunks
		get{return map;}
	}

	void Update(){
		unityTime = Time.time;
	}

	void Awake(){
		singleton = this;

		agents = new List<PathAgent>();
		curAgentIndex = 0;

	}

	void Start(){
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
				Debug.Log("This is thread");


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

					if(unityTime >= currentAgent.LastRequestTime + 0.5f){
						FindPath(currentAgent);
					}	

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
		catch(ThreadAbortException e) {
			Debug.LogWarning("Thread aborted: " + e);
		}
		catch(System.Exception e){
			Debug.LogError(e);
		}



	}

	void FindPath(PathAgent agent){
		int curNode, targetNode;

		curNode = map.PosToNode(agent.CurPos);
		targetNode = map.PosToNode(agent.TargetPos);

		int[] newPath = IDAStar.IterativeDeepeningAStar(map, curNode, targetNode, 14);
		agent.SetAgentPath(newPath, unityTime);

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
