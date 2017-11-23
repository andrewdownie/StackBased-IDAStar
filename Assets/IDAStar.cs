using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IDAStar{

	public static int[] IterativeDeepeningAStar(Map map, int start, int target, int msLimit){
		
		//Debug.Log("start: " + start + ", target: " + target);

		int[] lastPath = null;
		int curDepth = 1;

		int msHalfLimit = msLimit / 2;

		System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
		sw.Start();

		while(sw.Elapsed.TotalMilliseconds < msHalfLimit){
			lastPath = DepthLimitedAStar(map, start, target, curDepth);
			curDepth++;
		}

		sw.Stop();

		int timePassed = (int)sw.Elapsed.TotalMilliseconds;
		if(timePassed > msLimit){
			Debug.LogWarning("Pathfinding took " + timePassed + " before it was able to abort.");
		}

		/*string path = "";
		foreach(int i in lastPath){
			path += "(" + i + ") ";
		}
		Debug.Log(path);*/

		return lastPath;
	}

	private static int[] DepthLimitedAStar(Map map, int start, int target, int maxDepth){
		PathNode[] pathNodes = new PathNode[map.Width * map.Height];

		for(int x = 0; x < map.Width; x++){
			for(int y = 0; y < map.Height; y++){
				int id = y * map.Width + x;
				pathNodes[id] = new PathNode(id);
			}
		}

		List<PathNode> open = new List<PathNode>();//TODO: replace this with a heap
		List<PathNode> closed = new List<PathNode>();
		List<PathNode> fringe = new List<PathNode>();//TODO: replace this with a heap
		open.Add(pathNodes[start]);
		pathNodes[start].Depth = 0;
		

		int[] path;
		PathNode curNode;
		PathNode targetNode = pathNodes[target];

		while(open.Count != 0){
			curNode = PathNode.LowestFCost(open);

			if(curNode.Depth == maxDepth){
				fringe.Add(curNode);
			}

			open.Remove(curNode);
			closed.Add(curNode);

			if(curNode.ID == targetNode.ID){
				path = PathNode.RebuildPath(targetNode);
				return path;
			}

			foreach(int i in map.Neighbours(curNode)){
				//Debug.Log("i is: " + i + ", pathNodes length: " + pathNodes.Length);// //////////////////

				PathNode n = pathNodes[i];
				if(map.Movement(i) == 0 || closed.Contains(n)){
					continue;
				}

				if(!open.Contains(n) || curNode.GCost + PathNode.NodeDist(map, curNode, n) < n.GCost){
					n.GCost = curNode.GCost + PathNode.NodeDist(map, curNode, n);
					n.HCost = PathNode.NodeDist(map, n, targetNode);
					n.PathParent = curNode;
					n.Depth = curNode.Depth + 1;

					if(!open.Contains(n) && curNode.Depth < maxDepth){
						open.Add(n);
					}
				}
			}

		}

		path = PathNode.RebuildPath(PathNode.LowestFCost(fringe));
		return path;
	}

}
