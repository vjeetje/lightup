using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour {

    [System.Serializable]
    public enum Type { EMPTY, LIGHTBULB, CROSS, INDICATOR, UNREACHABLE }

    public Color colorDefault;
    public Color colorIndicator;
    public Color colorUnreachable;
    public Color colorCross;
    public Color colorLightbulb;
    public Color colorLightbulbError;
    public Color colorLit;
    public Color colorTextDefault;
    public Color colorTextCorrect;
    public Color colorTextError;

    public Sprite imageIconLightbulb;
    public Sprite imageIconCrate;
    public Sprite imageIconStoneBlock;
    public Sprite imageIconUnreachable;

    public Sprite imageBackgroundCenter;
    public Sprite imageBackgroundTopLeft;
    public Sprite imageBackgroundTop;
    public Sprite imageBackgroundTopRight;
    public Sprite imageBackgroundRight;
    public Sprite imageBackgroundBottomRight;
    public Sprite imageBackgroundBottom;
    public Sprite imageBackgroundBottomLeft;
    public Sprite imageBackgroundLeft;

    public Sprite[] decorations;
    public float decorationChance;

    public Button button;
    public Image imageBackground;
    public Image imageDecoration;
    public Image imageHint;
    public Image imageIcon;
    public Text text;

    public bool onLeftBorder;
    public bool onRightBorder;
    public bool onTopBorder;
    public bool onBottomBorder;
    public bool isLit;
    public Type type;
    public int number;
    public bool indicatorError;
    public bool indicatorCorrect;
    public bool hitsOtherLightsource;
    public bool isSetByHint;

    private void Start() {
        SetRandomImageDecoration();
        SetImageDecorationRotation();
        SetImageBackgroundRotation();
    }

    private void Update() {
        UpdateImageBackground();
        UpdateImageIcon();
        UpdateImageHint();
        UpdateText();
    }

    private void SetRandomImageDecoration() {
        imageDecoration.enabled = decorationChance > Random.value;
        imageDecoration.sprite = decorations[Random.Range(0, decorations.Length)];
    }

    private void SetImageDecorationRotation() {
        int rotation = Random.Range(0, 4) * 90;
        imageDecoration.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
    }

    private void SetImageBackgroundRotation() {
        int rotation = Random.Range(0, 4) * 90;
        if(!onLeftBorder && !onRightBorder && !onTopBorder && !onBottomBorder && type != Type.INDICATOR) {
            imageBackground.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
        } else {
            imageBackground.transform.rotation = Quaternion.identity;
        }
    }

    private void UpdateImageBackground() {
        if(isLit) {
            imageBackground.color = colorLit;
        } else if(type == Type.UNREACHABLE) {
            imageBackground.color = colorUnreachable;
        } else if(type == Type.INDICATOR) {
            imageBackground.color = colorIndicator;
        } else {
            imageBackground.color = colorDefault;
        }

        if(onLeftBorder && onTopBorder) {
            imageBackground.sprite = imageBackgroundTopLeft;
        } else if(onRightBorder && onTopBorder) {
            imageBackground.sprite = imageBackgroundTopRight;
        } else if(onTopBorder) {
            imageBackground.sprite = imageBackgroundTop;
        } else if(onBottomBorder && onLeftBorder) {
            imageBackground.sprite = imageBackgroundBottomLeft;
        } else if(onBottomBorder && onRightBorder) {
            imageBackground.sprite = imageBackgroundBottomRight;
        } else if(onBottomBorder) {
            imageBackground.sprite = imageBackgroundBottom;
        } else if(onLeftBorder) {
            imageBackground.sprite = imageBackgroundLeft;
        } else if(onRightBorder) {
            imageBackground.sprite = imageBackgroundRight;
        } else {
            imageBackground.sprite = imageBackgroundCenter;
        }
    }

    private void UpdateImageIcon() {
        imageIcon.enabled = type != Type.EMPTY;
        switch(type) {
            case Type.LIGHTBULB:
                imageIcon.sprite = imageIconLightbulb;
                imageIcon.color = hitsOtherLightsource ? colorLightbulbError : colorLightbulb;
                break;
            case Type.CROSS:
                imageIcon.sprite = imageIconCrate;
                imageIcon.color = colorCross;
                break;
            case Type.INDICATOR:
                imageIcon.sprite = imageIconStoneBlock;
                imageIcon.color = colorLit;
                break;
            case Type.UNREACHABLE:
                imageIcon.sprite = imageIconUnreachable;
                imageIcon.color = colorUnreachable;
                break;
        }
    }

    private void UpdateImageHint() {
        imageHint.enabled = isSetByHint;
    }

    private void UpdateText() {
        text.enabled = type == Type.INDICATOR;
        if(indicatorError) {
            text.color = colorTextError;
        } else if(indicatorCorrect) {
            text.color = colorTextCorrect;
        } else {
            text.color = colorTextDefault;
        }
        text.text = number.ToString();
    }
}
