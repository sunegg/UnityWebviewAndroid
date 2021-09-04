using HAHAHA;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class TeammateListView : GameListView<TeammatePrefab, TeammateModel> {
	[SerializeField] private Text titleText, rankText, attackText, speedText, defendText, dominanceText, abilityText;

	[SerializeField] private Button         dismissButton, abilityButton;
	[SerializeField] private UIToggleButton buttonToggle;
	[SerializeField] private GameObject     detailPanel;
	[SerializeField] private DialogView     dialogView;

	public override void OnInit() {
		if (GameData.Teammates != null)
			Refresh();
		
	}

	new void Refresh() {
		TeammateManager.Instance.Sort();
		SetData(GameData.Teammates);
	}

	protected override void OnEachInit(TeammatePrefab prefab, TeammateModel data) {
		prefab.SetData(data);
		prefab.AddOnClickListener(() => {
			var color                           = GameHelper.GetColorByRank(data.Rank);
			titleText.color    = rankText.color = color;
			titleText.text     = data.Name;
			rankText.text      = $"({data.Rank.ToString()})";
			attackText.text    = data.Attack.ToString();
			speedText.text     = data.Speed.ToString();
			defendText.text    = data.Defence.ToString();
			dominanceText.text = data.Dominance.ToString();
			abilityText.color = GameHelper.GetColorByRank(data.AbilityRank);
			buttonToggle.IsOn = data.StatusType == StatusType.Active;
			detailPanel.SetActive(true);

			abilityButton.onClick.RemoveAllListeners();

			dismissButton.onClick.RemoveAllListeners();
			dismissButton.onClick.AddListener(() => {
				dialogView.SetTitle("是否遣离");
				dialogView.SetContent(data.Name, GameHelper.GetColorByRank(data.Rank));
				dialogView.SetOkAction(() => {
					TeammateManager.Instance.SellTeammate(data);
					SetData(GameData.Teammates);
					dialogView.Hide();
					detailPanel.SetActive(false);
				});
				dialogView.Show();
			});
			buttonToggle.RemoveAllListener();
			buttonToggle.AddListener(() => {
				buttonToggle.Switch();
				if (buttonToggle.IsOn) {
					prefab.SetStatus(StatusType.Active);
					var isFull = TeammateManager.Instance.AddTeammates(data);
					if (!isFull) {
						buttonToggle.Switch();
						prefab.SetStatus(StatusType.Idle);
						ShowToast("已满员");
					}
				}
				else {
					prefab.SetStatus(StatusType.Idle);
					TeammateManager.Instance.RemoveTeammates(data);
				}

				detailPanel.SetActive(false);
			});
		});
	}
}