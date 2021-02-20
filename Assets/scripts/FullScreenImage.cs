using UnityEngine;
using UnityEngine.UI;

public class FullScreenImage : MonoBehaviour {

    public Image image;
    public float fillHeightPercentage;

    // Update is called once per frame
    void Awake() {
        Rect rect = image.GetComponent<RectTransform>().rect;
        float scale = Screen.height * fillHeightPercentage / rect.height;
        image.transform.localScale = new Vector3(scale, scale, 1);
    }
}
