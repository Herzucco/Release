using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SoundManagerSettings : ScriptableObject {
	public List<string> soundsFoldersNames;

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/ManagerSettings/SoundManager")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<SoundManagerSettings> ();
	}
	#endif	
}
