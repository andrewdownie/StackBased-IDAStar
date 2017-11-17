using UnityEngine;
[System.Serializable]
public struct Int2{
	[SerializeField]
	int x, y;

	public int X{get{return x;}}
	public int Y{get{return y;}}

	public Int2(int x, int y){
		this.x = x;
		this.y = y;
	}
}