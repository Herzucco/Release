using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Sound : AssetObject {
	protected AudioSource source;

	public void Awake(){
		source = GetComponent<AudioSource> ();
		source.Stop ();
	}

	public override void Play ()
	{
		source.Stop ();
		source.Play ();
	}

	public override void Stop ()
	{
		source.Stop ();
	}

	public void FadeOut(float time){
		source.DOFade (0f, time);
	}

	public void FadeIn(float sound, float time){
		source.DOFade (sound, time);
	}
}
