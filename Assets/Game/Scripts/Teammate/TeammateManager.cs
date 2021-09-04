using System.Linq;
using HAHAHA;

public class TeammateManager : Singleton<TeammateManager> {


	public TeammateModel GetShuffleAttribute(TeammateModel origin) {
		var data = (TeammateModel)origin.Clone();
		
		data.Attack    = UnityEngine.Random.Range(data.Min, data.Max + 1);
		data.Defence   = UnityEngine.Random.Range(data.Min, data.Max + 1);
		data.Speed     = UnityEngine.Random.Range(data.Min, data.Max + 1);
		data.Dominance = UnityEngine.Random.Range(data.Min, data.Max + 1);
		return data;
	}
	
	public bool AddTeammates(TeammateModel teammate) {
		var currentList = GameData.Battle.Teammates;
		if (currentList.Count >= GameData.BattleConfig.MaxTeammate) return false;
		if (!currentList.Contains(teammate)) {
			currentList.Add(teammate);
			teammate.StatusType = StatusType.Active;
		}

		Sort();
		TeammateListView.Instance.SetData(GameData.Teammates);
		this.Publish(GameKey.Teammate, GameData.Battle.Teammates.Count + "/"+GameData.BattleConfig.MaxTeammate);
		return true;
	}

	public void Sort() {
		GameData.Battle.Teammates.Sort((a, b) => {
			if ((int) a.Rank > (int) b.Rank)
				return -1;
			return 1;
		});

		var beforeSort = GameData.Teammates.ToList();

		beforeSort.Sort((a, b) => {
			if ((int) a.Rank > (int) b.Rank)
				return -1;
			return 1;
		});
		
		var afterSort = beforeSort.Where(t => t.StatusType == StatusType.Idle).ToList();

		GameData.Teammates = GameData.Battle.Teammates.Concat(afterSort).ToArray();
	}

	public void RemoveTeammates(TeammateModel teammate) {
		var currentList = GameData.Battle.Teammates;
		if (currentList.Contains(teammate)) {
			currentList.Remove(teammate);
			teammate.StatusType = StatusType.Idle;
		}

		Sort();
		TeammateListView.Instance.SetData(GameData.Teammates);

		this.Publish(GameKey.Teammate, GameData.Battle.Teammates.Count + "/3");
	}

	public void SellTeammate(TeammateModel teammate) {
		if (teammate.StatusType == StatusType.Active) {
			teammate.StatusType = StatusType.Idle;
			RemoveTeammates(teammate);
		}

		var before = GameData.Teammates.ToList();
		before.Remove(teammate);
		var after = before.ToArray();
		GameData.Teammates    =  after;
		GameData.Player.Money += GameHelper.GetPriceByRank(teammate.Rank);
	}
}