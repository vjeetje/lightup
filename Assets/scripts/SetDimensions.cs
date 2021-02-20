using UnityEngine;

public class SetDimensions : MonoBehaviour {

    public Transform targetTransform;
    public float percentageWidth;
    public float percentageHeight;

    // Use this for initialization
    void Start() {
        RectTransform currentRectTransform = transform.GetComponent<RectTransform>();
        RectTransform targetRectTransform = targetTransform.GetComponent<RectTransform>();
        currentRectTransform.sizeDelta = new Vector2(targetRectTransform.rect.width * percentageWidth, targetRectTransform.rect.height * percentageHeight);
    }
}
