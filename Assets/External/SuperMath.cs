using UnityEngine;
using System.Collections;

public static class SuperMath {
	public static float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	public static float RemapCurve (AnimationCurve curve, float minCurve, float maxCurve, float xTime, float xMaxTime){
		float x = Remap (xTime, 0f, xMaxTime, 0f, 1f);
		float evaluatedX = curve.Evaluate (x);

		return Remap(evaluatedX, 0f, 1f, minCurve, maxCurve);
	}
}