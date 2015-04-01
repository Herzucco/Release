using UnityEngine;
using System.Collections;

public class SingleBehaviour<T, ST> : MonoBehaviour
	where T : MonoBehaviour
	where ST : Object
{
	protected ST settings;

	protected static T instance_;
	public static T instance
	{
		get
		{
			if (instance_ == null)
				instance_ = GameObject.FindObjectOfType(typeof(T)) as T;
			if (instance_ == null)
			{
				GameObject o = new GameObject("_SingleBehaviour_<" + typeof(T).ToString() + ">");
				instance_ = o.AddComponent<T>();
			}
			return instance_;
		}
	}

	public SingleBehaviour(){
		ST[] preLoad = Resources.LoadAll<ST>("ManagersSettings");
		if (preLoad.Length > 0) {
			settings = preLoad[0];		
		}
	}
}

public class SingleBehaviour<T> : MonoBehaviour	where T : MonoBehaviour {

	protected static T instance_;
	public static T instance {
		get {
			if (instance_ == null)
				instance_ = GameObject.FindObjectOfType(typeof(T)) as T;
			if (instance_ == null)
			{
				GameObject o = new GameObject("_SingleBehaviour_<" + typeof(T).ToString() + ">");
				instance_ = o.AddComponent<T>();
			}
			return instance_;
		}
	}
}