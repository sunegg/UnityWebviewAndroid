using UnityEngine;
using UnityEngine.UI;

public class UIStartBattle : MonoBehaviour
{
    void Start()
    {
      GetComponent<Button>().onClick.AddListener(() => {
	    BattleManager.Instance.Play();
	      gameObject.SetActive(false);
      });
    }

   
}
