using UnityEngine;
using UnityEngine.UI;

public class UINextRound : MonoBehaviour {
    
    [SerializeField] private DialogView _dialog;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            _dialog.SetContent("是否进入下一回合？",Color.black);
            _dialog.SetOkAction(() => {
             
            });
            _dialog.gameObject.SetActive(true);
        });
    }
    
}
