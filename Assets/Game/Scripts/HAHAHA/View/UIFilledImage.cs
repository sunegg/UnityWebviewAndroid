using HAHAHA;
using UnityEngine;

    public class UIFilledImage : GameBehaviour
    {
        private UnityEngine.UI.Image _image;

        [Tooltip("文本标签")]
        [SerializeField] private string _tag = "Health";

        void Awake()
        {
            _image = GetComponent<UnityEngine.UI.Image>();
            AutoSubscribe<float>(_tag, OnValueChanged);
        }

        void OnValueChanged(float v) => _image.fillAmount = v;
        
    }

