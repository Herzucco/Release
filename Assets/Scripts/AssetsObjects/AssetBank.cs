using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AssetBank<T> 
	where T : AssetObject
{
	[Header("Bank Settings")]
	public List<string> assetsFolders;

	private Dictionary<string, T> _assets = new Dictionary<string, T>();

	public void Initialize(Transform parent){
		for (int i = 0; i < assetsFolders.Count; i++) {
			T[] assets = Resources.LoadAll<T>(assetsFolders[i]);
			for(int x = 0; x < assets.Length; x++){
				T asset = assets[x];

				T val;
				if(_assets.TryGetValue(asset.callName, out val)){
					Debug.LogError("The Asset "+asset.callName+" has been loaded twice. Fix its name.");
				}else{
					_assets[asset.callName] = GameObject.Instantiate(asset);
					_assets[asset.callName].transform.parent = parent;
				}
			}
		}
	}

	public T Get(string name){
		return _assets [name];
	}
}
