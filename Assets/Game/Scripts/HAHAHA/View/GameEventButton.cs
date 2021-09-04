using HAHAHA;
using UnityEngine;
using UnityEngine.UI;

public class GameEventButton : MonoBehaviour {

    [SerializeField] private GameEventType gameEvent=GameEventType.OnStart;
    void Start() => GetComponent<Button>().onClick.AddListener(() => { EventManager.RaiseGameEvent(gameEvent); });
    
}
