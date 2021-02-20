using UnityEngine;

public class GraphicMoveTo : MonoBehaviour {

    public Vector3 posStart;
    public Vector3 posEnd;
    public float time;
    public float delay;

    private float lerpTime;
    private RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update() {
        rectTransform.anchoredPosition = Vector3.Lerp(posStart, posEnd, Mathf.Clamp01(lerpTime / time));
        lerpTime += Time.deltaTime;
    }

    private void OnEnable() {
        rectTransform.anchoredPosition = posStart;
        lerpTime = -delay;
    }
}
