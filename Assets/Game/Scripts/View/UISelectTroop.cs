using UnityEngine;
using UnityEngine.UI;

public class UISelectTroop : MonoBehaviour {

    [SerializeField] private int troopId;
        
    [SerializeField] private GameObject check;

    private void Awake() {
        PrepareManager.OnTroopChanged += OnTroopChanged;
    }

    void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(SelectTroop);
        check.SetActive(PrepareManager.Instance.IsSelectTroop(troopId));
    }
    
    void SelectTroop() {
        PrepareManager.Instance.TempSelectTroop(troopId);
        PrepareManager.Instance.SetAction(() => {
            PrepareManager.Instance.SelectTroop(troopId);
            check.SetActive(PrepareManager.Instance.IsSelectTroop(troopId));
        });
    }

    void OnTroopChanged(int id) {
        check.SetActive(troopId==id);
    }
}
