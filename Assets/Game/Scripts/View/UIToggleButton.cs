using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIToggleButton : UIButton {

    [SerializeField] public bool IsOn = true;

    [SerializeField] protected bool AllowSwitchOff = true;

    [SerializeField] protected UnityEvent OnToggleOn, OnToggleOff;

    [SerializeField] protected Sprite SpriteOn, SpriteOff;

    [SerializeField] protected bool UseText = false;

    [SerializeField] protected string TextOn = "On", TextOff = "Off";

    [SerializeField] protected Text _text;

    private Image image;

    protected override void Awake() {
        base.Awake();
        image = GetComponent<Image>();
    }

    private void OnEnable() {
        if (IsOn) {
            ShowOnSprite();
            ShowOnText();
        }
        else {
            ShowOffSprite();
            ShowOffText();
        }
    }

    public void RemoveAllListener() {
        Button.onClick?.RemoveAllListeners();
    }
    public void Switch() {
        IsOn = !IsOn;
        if (IsOn) {
            OnToggleOn?.Invoke();
            ShowOnSprite();
            ShowOnText();

        } else {
            OnToggleOff.Invoke();
            ShowOffSprite();
           ShowOffText();
        }
    }

    private void ShowOnText() {
        if (UseText) {
            _text.text = TextOn;
        }
    }

    private void ShowOffText() {
        if (UseText) {
            _text.text = TextOff;
        }
    }
    
    protected override void Start() {
    }


    private void ShowOnSprite() {
        if (SpriteOn != null)
            image.sprite = SpriteOn;
    }

    private void ShowOffSprite() {
        if (SpriteOff != null)
            image.sprite = SpriteOff;
    }

}
