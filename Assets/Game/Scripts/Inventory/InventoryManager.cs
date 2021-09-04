using System;
using System.Linq;
using HAHAHA;
using UnityEngine;

public class InventoryManager : USingleton<InventoryManager> {
	[SerializeField] private GameObject listView;

	public void ShowType(InventoryType type) {
		GameData.CurrentInventory = GameData.Inventory.Where(i => i.TypeId == (int) type).ToArray();
		listView.SetActive(true);
	}

	public void AddEquipment(InventoryModel data) {
		var list = GameData.Inventory.ToList();
		list.Add(data);
		GameData.Inventory = list.ToArray();
	}

	public void Sort() {
		var list = GameData.CurrentInventory.ToList();
		list.Sort((a, b) => {
			if ((int) a.Rank > (int) b.Rank)
				return -1;
			return 1;
		});

		var beforeSort = GameData.CurrentInventory.ToList();

		beforeSort.Sort((a, b) => {
			if ((int) a.Rank > (int) b.Rank)
				return -1;
			return 1;
		});

		var idle   = beforeSort.Where(t => t.StatusType == StatusType.Idle).ToList();
		var active = beforeSort.Where(t => t.StatusType == StatusType.Active).ToList();

		GameData.CurrentInventory = active.Concat(idle).ToArray();
	}

	private void AddStatus(InventoryModel data) {
		GameData.Player.Attack    += data.Attack;
		GameData.Player.Defence   += data.Defence;
		GameData.Player.Speed     += data.Speed;
		GameData.Player.Dominance += data.Dominance;
	}

	private void RemoveStatus(InventoryModel data) {
		GameData.Player.Attack    -= data.Attack;
		GameData.Player.Defence   -= data.Defence;
		GameData.Player.Speed     -= data.Speed;
		GameData.Player.Dominance -= data.Dominance;
	}

	public void SelectEquipment(InventoryModel data) {
		switch ((InventoryType) data.TypeId) {
			case InventoryType.Weapon:
				if (GameData.Player.Weapon != null) {
					DeselectEquipment(GameData.Player.Weapon);
				}

				GameData.Player.Weapon            = data;
				GameData.Player.Weapon.StatusType = StatusType.Active;
				break;
			case InventoryType.Cloth:
				if (GameData.Player.Cloth != null) {
					DeselectEquipment(GameData.Player.Cloth);
				}

				GameData.Player.Cloth            = data;
				GameData.Player.Cloth.StatusType = StatusType.Active;
				break;
			case InventoryType.Helmet:
				if (GameData.Player.Helmet != null) {
					DeselectEquipment(GameData.Player.Helmet);
				}

				GameData.Player.Helmet            = data;
				GameData.Player.Helmet.StatusType = StatusType.Active;
				break;
			case InventoryType.Glove:
				if (GameData.Player.Glove != null)
					DeselectEquipment(GameData.Player.Glove);
				GameData.Player.Glove            = data;
				GameData.Player.Glove.StatusType = StatusType.Active;
				break;
			case InventoryType.Boot:
				if (GameData.Player.Boot != null)
					DeselectEquipment(GameData.Player.Boot);
				GameData.Player.Boot            = data;
				GameData.Player.Boot.StatusType = StatusType.Active;
				break;
			case InventoryType.Accessory:
				if (GameData.Player.Accessory != null)
					DeselectEquipment(GameData.Player.Accessory);
				GameData.Player.Accessory            = data;
				GameData.Player.Accessory.StatusType = StatusType.Active;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		AddStatus(data);
		Sort();
		InventoryListView.Instance.SetData(GameData.CurrentInventory);
	}

	public void DeselectEquipment(InventoryModel data) {
		if (data.StatusType == StatusType.Active) {
			data.StatusType = StatusType.Idle;
			switch ((InventoryType) data.TypeId) {
				case InventoryType.Weapon:
					if (GameData.Player.Weapon == data)
						GameData.Player.Weapon = null;
					break;
				case InventoryType.Cloth:
					if (GameData.Player.Cloth == data)
						GameData.Player.Cloth = null;
					break;
				case InventoryType.Helmet:
					if (GameData.Player.Helmet == data)
						GameData.Player.Helmet = null;
					break;
				case InventoryType.Glove:
					if (GameData.Player.Glove == data)
						GameData.Player.Glove = null;
					break;
				case InventoryType.Boot:
					if (GameData.Player.Boot == data)
						GameData.Player.Boot = null;
					break;
				case InventoryType.Accessory:
					if (GameData.Player.Accessory == data)
						GameData.Player.Accessory = null;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		RemoveStatus(data);
		Sort();
		InventoryListView.Instance.SetData(GameData.CurrentInventory);
	}

	public void Sell(InventoryModel data) {
		DeselectEquipment(data);
		var before = GameData.Inventory.ToList();
		before.Remove(data);
		var after = before.ToArray();
		GameData.Inventory    =  after;
		GameData.Player.Money += GameHelper.GetPriceByRank(data.Rank);
		ShowType((InventoryType) data.TypeId);
	}
}