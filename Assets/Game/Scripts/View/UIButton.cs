using System;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour {

    [SerializeField] private bool _playSfx = true;

    protected Button Button;
    protected virtual void Awake() => Button = GetComponent<Button>();

    protected virtual void Start() => AddListener(() => {
        if (_playSfx)
            AudioManager.Instance.PlayButtonSfx();
    });

    public void AddListener(Action act) {
        if (act != null)
            Button.onClick.AddListener(() => {
                act();
            });
    }

}
