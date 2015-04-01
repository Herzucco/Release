using UnityEngine;
using System.Collections;

public abstract class AssetObject : MonoBehaviour {
	[Header("Asset Parameters")]
	public string callName;		

	public abstract void Play();
	public abstract void Stop();
}
