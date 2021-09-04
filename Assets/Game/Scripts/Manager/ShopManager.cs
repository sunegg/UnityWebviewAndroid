using System;
using System.Linq;
using HAHAHA;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : GameBehaviour {
	[SerializeField] private GameObject window;
	[SerializeField] private Text       text;
	[SerializeField] private DialogView dialog;

	public void OpenBin() {
		dialog.SetTitle("");
		dialog.SetContent("是否消耗金钱抽取神盒",Color.black);
		dialog.SetOkAction(() => {
			var binChance = GameData.BoxConfigs[0];
			var result = GetRandomRank(binChance.ChanceS, binChance.ChanceA, binChance.ChanceB, binChance.ChanceC,
			                           binChance.ChanceD);
			if (CheckPrice(GameData.BoxConfigs[0].Price)) {
				GameData.Player.Money -= GameData.BoxConfigs[0].Price;
				var isTeammate = RandomHelper.GetRandomBool();
				if (isTeammate) {
					var randomRange  = GameData.TeammateConfig.Where(t => t.Rank == result).ToArray();
					var finalResult  = TeammateManager.Instance.GetShuffleAttribute(randomRange.GetRandom());
					var teammateList = GameData.Teammates.ToList();
					teammateList.Add(finalResult);
					GameData.Teammates = teammateList.ToArray();
				//	Debug.Log(finalResult.Name + "," + finalResult.Rank);
					text.text  = $"({finalResult.Rank}) {finalResult.Name}";
					text.color = GameHelper.GetColorByRank(finalResult.Rank);
				}
				else {
					var randomRange = GameData.InventoryConfig.Where(t => t.Rank == result).ToArray();
					var item        = (InventoryModel) randomRange.GetRandom().Clone();
					item.StatusType = StatusType.Idle;
					InventoryManager.Instance.AddEquipment(item);
					text.text  = $"({item.Rank}) {item.Name}";
					text.color = GameHelper.GetColorByRank(item.Rank);
				}
				dialog.Hide();
				window.SetActive(true);
			}
		});
		dialog.Show();
	}

	public void OpenGodBox() {
		dialog.SetTitle("");
		dialog.SetContent("是否消耗金钱抽取神盒",Color.black);
		dialog.SetOkAction(() => {
			var godBoxChance = GameData.BoxConfigs[1];
			var result = GetRandomRank(godBoxChance.ChanceS, godBoxChance.ChanceA, godBoxChance.ChanceB,
			                           godBoxChance.ChanceC,
			                           godBoxChance.ChanceD);
			if (CheckPrice(GameData.BoxConfigs[1].Price)) {
				GameData.Player.Money -= GameData.BoxConfigs[1].Price;
				var isTeammate = RandomHelper.GetRandomBool();
				if (isTeammate) {
					var randomRange  = GameData.TeammateConfig.Where(t => t.Rank == result).ToArray();
					var finalResult  = TeammateManager.Instance.GetShuffleAttribute(randomRange.GetRandom());
					var teammateList = GameData.Teammates.ToList();
					teammateList.Add(finalResult);
					GameData.Teammates = teammateList.ToArray();
				//	Debug.Log(finalResult.Name + "," + finalResult.Rank);
					text.text  = $"({finalResult.Rank}) {finalResult.Name}";
					text.color = GameHelper.GetColorByRank(finalResult.Rank);
				}
				else {
					var randomRange = GameData.InventoryConfig.Where(t => t.Rank == result).ToArray();
					var item        = (InventoryModel) randomRange.GetRandom().Clone();
					item.StatusType = StatusType.Idle;
					InventoryManager.Instance.AddEquipment(item);
					text.text  = $"({item.Rank}) {item.Name}";
					text.color = GameHelper.GetColorByRank(item.Rank);
				}

				dialog.Hide();
				window.SetActive(true);
			}
		});
		dialog.Show();
	}

	string GetInventoryName(InventoryType type) {
		switch (type) {
			case InventoryType.Weapon:
				return "武器";
			case InventoryType.Cloth:
				return "衣物";
			case InventoryType.Helmet:
				return "头盔";
			case InventoryType.Glove:
				return "手套";
			case InventoryType.Boot:
				return "靴子";
			case InventoryType.Accessory:
				return "饰品";
			default:
				throw new ArgumentOutOfRangeException(nameof(type), type, null);
		}
	}

	private bool CheckPrice(int price) {
		
		if (GameData.Teammates.Length >= GameData.PlayerConfig.MaxTeammateSlot) {
			ShowToast("伙伴数量已达上限");
			return false;
		}

		for (int i = 1; i < 6; i++) {
			var cur = GameData.Inventory.Where(item => item.TypeId == (int) i).ToArray();
			
			if (cur.Length >= GameData.PlayerConfig.MaxInventorySlot) {
				ShowToast(GetInventoryName((InventoryType) (i)) + "持有数已达上限");
				return false;
			}
		}

		if (GameData.Player.Money < price) {
			ShowToast("金钱不足");
			return false;
		}

		return true;
	}

	private RankType GetRandomRank(int chanceRankS, int chanceRankA, int chanceRankB, int chanceRankC,
	                               int chanceRankD) {
		int totalChance = chanceRankA + chanceRankB + chanceRankC + chanceRankD + chanceRankS;
		var rand        = Random.Range(0, totalChance);
		
		if (rand < chanceRankS)
			return RankType.S;
		else if (rand < chanceRankS + chanceRankA)
			return RankType.A;
		else if (rand < chanceRankS + chanceRankA + chanceRankB) {
			
			return RankType.B;
		}
		else if (rand < chanceRankS + chanceRankA + chanceRankB + chanceRankC) {
			
			return RankType.C;
		}
		else if (rand < chanceRankS + chanceRankA + chanceRankB + chanceRankC + chanceRankD) {
			
			return RankType.D;
			
		}
		else {
			
			return RankType.None;
		}
	}
}