using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicScaleTo : MonoBehaviour {

    public Vector3 scaleStart;
    public Vector3 scaleEnd;
    public float time;
    public float delay;

    private float lerpTime;

    void Update() {
        gameObject.transform.localScale = Vector3.Lerp(scaleStart, scaleEnd, Mathf.Clamp01(lerpTime / time));
        lerpTime += Time.deltaTime;
    }

    private void OnEnable() {
        gameObject.transform.localScale = scaleStart;
        lerpTime = -delay;
    }
}
