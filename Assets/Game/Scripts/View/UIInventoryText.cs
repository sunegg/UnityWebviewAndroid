using HAHAHA;
using UnityEngine;

public class UIInventoryText : GameText {

    [SerializeField] private InventoryType type;

    private string _defaultText;
    
    void Start() {
        _defaultText = Text.text;
        this.Subscribe<InventoryModel>(type.ToString(),OnValueChanged);
    }

    private void OnValueChanged(InventoryModel v) {
        if (v != null) {
            Text.text  = $"({v.Rank}) {v.Name}";
            Text.color = GameHelper.GetColorByRank(v.Rank);
        }
        else {
            Text.text = _defaultText;
            Text.color = Color.black;
        }
    }
    
}
