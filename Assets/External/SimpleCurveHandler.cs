using UnityEngine;
using System.Collections;

[System.Serializable]
public class SimpleCurveHandler {
	public AnimationCurve curve;
	public float duration;
	public float minValue;
	public float maxValue;

	public bool resetAtMaxValue = false;
	public float time;

	public float currentValue{
		get{
			return SuperMath.RemapCurve(curve, minValue, maxValue, time, duration);
		}
	}

	public void UpdateValue(float t){
		time = Mathf.Clamp(time + t, 0f, duration);
		if (time >= duration && resetAtMaxValue) {
			Reset();		
		}
	}

	public void Reset(){
		time = 0f;
	}

	public float Evaluate(float t){
		time = t;
		return currentValue;
	}

	public float Evaluate(int t){
		time = (float) t;
		return currentValue;
	}
}
