using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TeammatePrefab : MonoBehaviour {
  
  [SerializeField] private Text title,rank,attack,defend,speed,dominance,status;

  public void SetData(TeammateModel data) {
    var color = GameHelper.GetColorByRank(data.Rank);
    title.color = rank.color = color;
    title.text = data.Name;
    rank.text = $"({data.Rank.ToString()})";
    attack.text = data.Attack.ToString();
    defend.text = data.Defence.ToString();
    speed.text = data.Speed.ToString();
    dominance.text = data.Dominance.ToString();
    SetStatus(data.StatusType);
  }

  public void SetStatus(StatusType statusType) {
    switch (statusType) {
      case StatusType.Idle:
        status.text = "待命";
        status.color=Color.black;
        break;
      case StatusType.Active:
        status.text = "出征";
        status.color = Color.red;
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(statusType), statusType, null);
    }
  }

  public void AddOnClickListener(UnityAction act) {
    GetComponent<Button>().onClick.AddListener(act);
  }
  
}
