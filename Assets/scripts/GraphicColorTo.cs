using UnityEngine;
using UnityEngine.UI;

public class GraphicColorTo : MonoBehaviour {

    public Graphic graphic;
    public Color colorStart;
    public Color colorEnd;
    public float time;
    public float delay;

    private float lerpTime;

    void Update() {
        graphic.color = Color.Lerp(colorStart, colorEnd, Mathf.Clamp01(lerpTime / time));
        lerpTime += Time.deltaTime;
    }

    private void OnEnable() {
        graphic.color = colorStart;
        lerpTime = -delay;
    }
}
