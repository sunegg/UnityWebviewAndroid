using UnityEngine;
using UnityEngine.UI;

namespace HAHAHA
{
    [RequireComponent(typeof(Text))]
    public class GameText : GameBehaviour
    {
        protected Text Text;

        [Tooltip("文本标签")]
        [SerializeField] private string key;

        [SerializeField] private bool nameAsTag = false;

        [Tooltip("需要显示的文本格式")]
        [SerializeField] private string _format = "{0}";

        [Tooltip("是否使用string.Format解析文本")]
        [SerializeField] private bool _isFormatString = false;

        [SerializeField] private DataType _dataType = DataType.String;

        protected virtual void Awake()
        {
            key = nameAsTag ? gameObject.name : key;
            Text = GetComponent<Text>();
            switch (_dataType)
            {
                case DataType.String:
                    AutoSubscribe<string>(key, OnValueChanged);
                    break;
                case DataType.Int:
                    AutoSubscribe<int>(key, OnValueChanged);
                    break;
            }
        }
      
        protected virtual void OnValueChanged(string v) => Text.text = _isFormatString ? string.Format(_format, v) : v;

        protected virtual void OnValueChanged(int v) => Text.text = _isFormatString ? string.Format(_format, v) : v.ToString();
    }
}