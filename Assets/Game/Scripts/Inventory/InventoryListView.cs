using HAHAHA;
using UnityEngine;
using UnityEngine.UI;

public class InventoryListView : GameListView<InventoryItem, InventoryModel> {
	[SerializeField] private GameObject detailPanel;

	[SerializeField] private Text attackText, defenceText, speedText, dominanceText, abilityText, nameText;

	[SerializeField] private Button sellButton, abilityButton;

	[SerializeField] private UIToggleButton buttonToggle;

	[SerializeField] private DialogView dialogView;

	public override void OnInit() {
		InventoryManager.Instance.Sort();
		SetData(GameData.CurrentInventory);
	}

	protected override void OnEachInit(InventoryItem prefab, InventoryModel data) {
		prefab.SetNameText(data.Name);
		prefab.SetRankText($"({data.Rank})");
		prefab.SetTextColor(GameHelper.GetColorByRank(data.Rank));
		prefab.SetStatus(data.StatusType);
		prefab.AddOnClickListener(() => {
			attackText.text    = data.Attack.ToString();
			defenceText.text   = data.Defence.ToString();
			speedText.text     = data.Speed.ToString();
			dominanceText.text = data.Dominance.ToString();
			nameText.text      = data.Name;
			nameText.color     = GameHelper.GetColorByRank(data.Rank);

			buttonToggle.IsOn = data.StatusType == StatusType.Active;
			detailPanel.SetActive(true);

			abilityButton.onClick.RemoveAllListeners();

			sellButton.onClick.RemoveAllListeners();
			sellButton.onClick.AddListener(() => {
				dialogView.SetTitle("是否出售");
				dialogView.SetContent(data.Name, GameHelper.GetColorByRank(data.Rank));
				dialogView.SetOkAction(() => {
					InventoryManager.Instance.Sell(data);

					SetData(GameData.CurrentInventory);
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
					InventoryManager.Instance.SelectEquipment(data);
				}
				else {
					prefab.SetStatus(StatusType.Idle);
					InventoryManager.Instance.DeselectEquipment(data);
				}

				detailPanel.SetActive(false);
			});
		});
	}
}