using UnityEngine;
using System.Collections;

public abstract class BaseObject : MonoBehaviour {
	#region General (public)
	public float lifeTime{
		get{
			return _lifeTime;
		}
		set{
			_lifeTime = value;
			if(value > 0f){
				StartCoroutine (_destroyRoutine);
			}
		}
	}
	
	[Header("General Values")]
	[Tooltip("Optionnal base life for the object - non effective if == 0")]
	public float baseLife;
	
	[Tooltip("Optionnal delay before dying")]
	public float delayBeforeDying;
	
	[HideInInspector]
	public float life;
	[HideInInspector]
	public bool willDie = false;
	[HideInInspector]
	public float uniqueGeneratedID;
	#endregion
	
	#region General (protected)
	protected bool _stopped;
	#endregion
	
	#region General (private)
	[SerializeField]
	[Tooltip("Optionnal lifetime for the object - non effective if == 0")]
	private float _lifeTime;
	
	private float _timeAlive;
	private IEnumerator _destroyRoutine;
	#endregion
	
	protected virtual void Start(){
		_timeAlive = 0;
		uniqueGeneratedID = Random.Range (0f, 1000000000f);

		StartCoroutine(Play ());
	}
	
	protected virtual void OnDestroy(){
		StopAllCoroutines ();
	}
	
	public virtual void Stop(){
		_stopped = true;
		if (_destroyRoutine != null) {
			StopCoroutine (_destroyRoutine);
		}
	}
	
	public virtual void Reset(){
		_timeAlive = 0f;
	}
	
	public virtual IEnumerator Play(){
		_stopped = false;
		
		_lifeTime = lifeTime;
		life = baseLife;
		
		_destroyRoutine = DestroyObjectRoutine ();
		
		if (lifeTime > 0f) {
			StartCoroutine (_destroyRoutine);
		}
		
		yield return null;
	}
	
	protected virtual void Update(){
		if (!_stopped) {
			PlayUpdate ();
		} else {
			StopUpdate();
		}
	}
	
	protected virtual void FixedUpdate(){
		if (!_stopped) {
			FixedPlayUpdate ();
		} 
	}
	
	protected virtual void LateUpdate(){
		LatePlayUpdate();		
	}
	
	protected virtual void FixedPlayUpdate(){} 
	protected virtual void LatePlayUpdate(){}
	
	protected virtual void PlayUpdate(){
		_timeAlive += DeltaTime();
	} 
	
	protected virtual void StopUpdate(){} 
	
	public float DeltaTime(){
		return TimeManager.instance.DeltaTime ();
	}
	
	public float FixedDeltaTime(){
		return TimeManager.instance.FixedDeltaTime ();
	}
	
	public float SpeedFactor(){
		return TimeManager.instance.speedFactor;
	}
	
	protected virtual IEnumerator DestroyObjectRoutine(){
		yield return this.WaitForSeconds(_lifeTime - _timeAlive);
		if (lifeTime != 0f) {
			StartCoroutine(DestroyObject ());
		}
	}
	
	public virtual IEnumerator DestroyObject(){
		if (delayBeforeDying > 0f) {
			yield return WaitForSeconds (delayBeforeDying);
		}
		
		yield return WaitForSeconds(0f);
	}
	
	public YieldInstruction WaitForSeconds(float duration)
	{
		if (gameObject.activeInHierarchy)
			return StartCoroutine(Wait(duration));
		else
			return null;
	}
	
	protected IEnumerator Wait(float duration)
	{
		for (float timer = 0; timer < duration && gameObject.activeInHierarchy; timer += DeltaTime())
			yield return null;
	}
	
	protected virtual void AttachTo(Transform parent){
		StartCoroutine (_AttachTo (parent));
	}
	
	private IEnumerator _AttachTo(Transform parent){
		yield return WaitForSeconds(0f);
		transform.parent = parent;
	}
	
	
	public virtual void ChangeLife(float change){
		life = Mathf.Clamp(life + change, 0f, baseLife);
		StartCoroutine(CheckDeath ());
	}
	
	public virtual IEnumerator CheckDeath(){
		willDie = (life <= 0) ? true : false;
		yield return WaitForSeconds (0f);
		if (willDie) {
			Stop();
			StartCoroutine(DestroyObject());
		}
	}
}
