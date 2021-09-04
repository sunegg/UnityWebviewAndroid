using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UILongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler {

    [SerializeField] private float _duration = 1.5f;

    [SerializeField] private float _span = 0.1f;

    [SerializeField] private UnityEvent _onClick;

    private bool _isDown = false;
    private bool _longPress = false;
    private float _touchTime;


    private void Update() {
        if (_isDown && !_longPress) {
            if (Time.time - _touchTime > _duration) {
                _longPress = true;
                LongPress();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        _touchTime = Time.time;
        _isDown = true;
        _longPress = false;
    }

    public void OnPointerUp(PointerEventData eventData) {
        _isDown = false;
        StopAllCoroutines();
    }

    public void OnPointerExit(PointerEventData eventData) => _isDown = false;

    public void OnPointerClick(PointerEventData eventData) {
        if (!_longPress) {
            PressDown();
        }
    }

    IEnumerator OnLongPress() {
        while (_isDown) {
            PressDown();
             yield return new WaitForSeconds(_span);
        }
    }


    private void LongPress() => StartCoroutine(OnLongPress());

    private void PressDown() =>_onClick?.Invoke();

}
