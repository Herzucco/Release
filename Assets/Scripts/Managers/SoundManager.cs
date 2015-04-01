using UnityEngine;
using System.Collections;

/// <summary>
/// SoundManager used to spawn specific sounds at the time and position we want to - not implemented.
/// </summary>
public class SoundManager : SingleBehaviour<SoundManager, SoundManagerSettings> {
	private AssetBank<Sound> _bank;
	public AssetBank<Sound> bank{
		get{
			if(_bank == null){
				_bank = new AssetBank<Sound>();
				_bank.assetsFolders = settings.soundsFoldersNames;
				_bank.Initialize(transform);
			}

			return _bank;
		}
	}

	private void Awake(){
		_bank = bank;
		GameObject.DontDestroyOnLoad(gameObject);
    }

	public void Play( string name ){
		if  ( _bank != null ) {
            _bank.Get(name).Play();
        } 
	}

	public void Stop(string name){
        if ( _bank != null ) {
            _bank.Get(name).Stop();
        }
	}

	public void FadeOut(string name, float time){
		if ( _bank != null ) {
			_bank.Get(name).FadeOut(time);
		}
	}

	public void FadeIn(string name, float sound, float time){
		if ( _bank != null ) {
			_bank.Get(name).FadeIn(sound, time);
		}
	}
}
