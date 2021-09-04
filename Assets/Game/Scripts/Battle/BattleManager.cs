using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAHAHA;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleManager : USingleton<BattleManager> {

	private BattleModel _battleData;

	public void Init() {
		_battleData             = GameData.Battle;
		GameData.Battle.Round   = 0;
		GameData.Battle.Enemies = new List<TeammateModel>();
		InitPlayer();
		InitEnemies();
		InitPlayerInfo();
		InitEnemyInfo();
		UpdateView();
		BattleView.Instance.UpdateView();
	}

	private int GetTotalTeamDominance => GameData.Player.LimitedDominance + _battleData.TeamTotal.Dominance;

	private int GetTotalTeamAttack => GameData.Player.LimitedAttack +
	                                  _battleData.TeamTotal.Attack;

	private int GetTotalTeamDefence => GameData.Player.LimitedDefence +
	                                   _battleData.TeamTotal.Defence;

	private int GetTotalEnemyAttack =>  _battleData.EnemyTotal.Attack;

	private int GetTotalEnemyDefence =>  _battleData.EnemyTotal.Defence;

	private int GetCurrentTeamHp => (_battleData.CurrentTeamDominance);

	private int GetCurrentTeamMaxHp => _battleData.CurrentMaxTeamDominance;

	private int GetCurrentEnemyHp => _battleData.CurrentEnemyDominance;

	private int GetCurrentEnemyMaxHp => _battleData.CurrentMaxEnemyDominance;

	
	bool IsPlayerFirst() {

			if (_battleData.TeamTotal.Speed >= _battleData.EnemyTotal.Speed) {
				Debug.Log("我方速度先手");
				return true;
			}

			Debug.Log("对方度先手");
			return false;

	}


	void UpdateView() {
		Publish(GameKey.TotalTeamAttack, GetTotalTeamAttack);
		Publish(GameKey.TotalTeamDefence, GetTotalTeamDefence);
		Publish(GameKey.TotalEnemyAttack, GetTotalEnemyAttack);
		Publish(GameKey.TotalEnemyDefence, GetTotalEnemyDefence);
		Publish(GameKey.TotalTeamHp, GetCurrentTeamHp + "/" + GetCurrentTeamMaxHp);
		Publish(GameKey.TotalEnemyHp, GetCurrentEnemyHp + "/" + GetCurrentEnemyMaxHp);
		Publish(GameKey.Round, $"第{_battleData.Round}回合");
		Publish(GameKey.EnemyHealth, GetCurrentEnemyHp * 1f / GetCurrentEnemyMaxHp);
		Publish(GameKey.PlayerHealth, GetCurrentTeamHp * 1f / GetCurrentTeamMaxHp);
	}

	public IEnumerator StartRound() {
		while (true) {
			if (IsPlayerFirst()) {
				PlayerAttack();
				yield return new WaitForSeconds(.5f);
				UpdateView();


				yield return new WaitForSeconds(.5f);
				EnemyAttack();
				yield return new WaitForSeconds(.5f);
				UpdateView();
	
				yield return new WaitForSeconds(.5f);
	
			}
			else {
				EnemyAttack();
				yield return new WaitForSeconds(.5f);
				UpdateView();


				yield return new WaitForSeconds(.5f);
				PlayerAttack();
				yield return new WaitForSeconds(.5f);
				UpdateView();
	
				yield return new WaitForSeconds(.5f);

			}

			GameData.Battle.Round++;

			
			UpdateView();

			
			if (GameData.Battle.Round >= GameData.BattleConfig.MaxRound) {
				FinalPk();
				yield break;
			}

			if (GetCurrentEnemyHp > 0 && GetCurrentTeamHp > 0) {
				
				StartCoroutine(StartRound());
				yield break;
			}

			StopAllCoroutines();
		}
	}
	
	void PlayerAttack() {
		
		
		var basicDamage = Mathf.CeilToInt(GameData.Battle.CurrentTeamDominance * 1f / 10f *
		                                  (1f + (GameData.Battle.TeamTotal.Attack * 1f -
		                                         GameData.Battle.EnemyTotal.Defence * 1f) / 15f));
		if (basicDamage >= 1) {
			basicDamage += Random.Range(1, 10);
		}
		else {
			basicDamage = 1;
		}

		int magicDamage = 0;
	

		if (magicDamage < 0) {
			magicDamage = 0;
		}

		var totalDamage = basicDamage + magicDamage;
		GameData.Battle.CurrentEnemyDominance -= totalDamage;
		BattleView.Instance.ShowEnemyHpCost("-" + totalDamage, true);
		Debug.Log($"我方出击 基础伤害{basicDamage} 魔法伤害{magicDamage}");
		Debug.Log($"双方血量 我方{GameData.Battle.CurrentTeamDominance} 敌方{GameData.Battle.CurrentEnemyDominance}");
		if (GameData.Battle.CurrentEnemyDominance <= 0) {
			
			GameData.Battle.CurrentEnemyDominance = 0;
			StopAllCoroutines();
			UpdateView();
			Invoke(nameof(ShowPlayerResult), 1f);
		}
	}

	public void NextRound() {
		GameData.Player.Round++;
		GameData.Player.Energy = GameData.PlayerConfig.MaxEnergy;
		Publish("GlobalRound","当前回合："+GameData.Player.Round);
		Publish("CountDown", 99-GameData.Player.Round);
	}

	
	void ShowPlayerResult() {
		NextRound();
		MapManager.Instance.GetIncome();
		MapManager.Instance.AttackCity();
		BattleView.Instance.ShowResult("这场战斗的胜利方是" + Environment.NewLine +
		                               "我方"+ Environment.NewLine +"因此 "+MapManager.CurrentSelected.Name + " -"+GameData.Battle.SelectedTroop.Siege+"守备力");
	}

	void ShowEnemyResult() {
		NextRound();
		MapManager.Instance.GetIncome();
		BattleView.Instance.ShowResult("这场战斗的胜利方是" + Environment.NewLine +
		                               "敌方");
	}

	void EnemyAttack() {
		var basicDamage = Mathf.CeilToInt(GameData.Battle.CurrentEnemyDominance * 1f / 10f *
		                                  (1f + (GameData.Battle.EnemyTotal.Attack * 1f -
		                                         GameData.Battle.TeamTotal.Defence * 1f) / 15f));
		if (basicDamage >= 1) {
			basicDamage += Random.Range(1, 10);
		}
		else {
			basicDamage = 1;
		}

		int magicDamage = 0;
	

		if (magicDamage < 0) {
			magicDamage = 0;
		}

		var totalDamage = basicDamage + magicDamage;
		GameData.Battle.CurrentTeamDominance -= totalDamage;
		BattleView.Instance.ShowPlayerHpCost("-" + totalDamage, true);
		Debug.Log($"敌方出击 基础伤害{basicDamage} 魔法伤害{magicDamage}");
		Debug.Log($"双方血量 我方{GameData.Battle.CurrentTeamDominance} 敌方{GameData.Battle.CurrentEnemyDominance}");
		if (GameData.Battle.CurrentTeamDominance <= 0) {
			
			GameData.Battle.CurrentTeamDominance = 0;
			StopAllCoroutines();
			UpdateView();
			
			
			Invoke(nameof(ShowEnemyResult), 1f);
		}
	}

	

	void FinalPk() {
		Debug.Log("开始最终判定");
		if (GameData.Battle.CurrentTeamDominance * 1f / GetCurrentTeamMaxHp >
		    GameData.Battle.CurrentEnemyDominance * 1f / GetCurrentEnemyMaxHp) {
			Debug.Log("我方胜利");
			ShowPlayerResult();
			
			
		}
		else {
			Debug.Log("敌方胜利");
			ShowEnemyResult();
			
			
		}
	}
	
	void InitEnemies() {
		
		for (int i = 0; i < GameData.BattleConfig.MaxEnemy; i++)
			GameData.Battle.Enemies.Add(TeammateManager
			                            .Instance
			                            .GetShuffleAttribute(GameData.Enemies.First(e => e.Id == GameData
			                                                                                     .Battle
			                                                                                     .SelectedEnemy)));
		
		
		


		
		_battleData.EnemyTotal        = new TeammateModel();

		var team = GameData.Battle.Enemies;

		foreach (var t in team) {
			

			
			
			
			
			_battleData.EnemyTotal += t;
			
		}

		var troop = GameData.TroopConfig.First(t => t.Id == GameData.Battle.Enemies[0].RoleID);
		GameData.Battle.EnemyTroop = new TeammateModel {
			Attack    = troop.Attack,
			Id        = troop.Id,
			Defence   = troop.Defence,
			Dominance = troop.Dominance,
			Speed     = troop.Speed,
			Siege = troop.Siege
		};
		_battleData.EnemyTotal += GameData.Battle.EnemyTroop;



		if (_battleData.Round == 0) {
			GameData.Battle.CurrentEnemyDominance =
				_battleData.EnemyTotal.Dominance;
			GameData.Battle.CurrentEnemyDominance    *= 100;
			GameData.Battle.CurrentMaxEnemyDominance =  GameData.Battle.CurrentEnemyDominance;
		}
	}

	

	void InitPlayer() {
	
		_battleData.TeamTotal          = new TeammateModel();
		var team = GameData.Battle.Teammates;
		foreach (var t in team) {

		
			
			
			_battleData.TeamTotal += t;
			
		}

		_battleData.TeamTotal += GameData.Battle.SelectedTroop;


		
			


		if (_battleData.Round == 0) {
			GameData.Battle.CurrentTeamDominance = _battleData.ActualDominance;
			
			
			
			
			
			GameData.Battle.CurrentMaxTeamDominance = GameData.Battle.CurrentTeamDominance;
		}
	}

	
	void InitEnemyInfo() {
		StringBuilder enemyInfo     = new StringBuilder();
		StringBuilder abilitiesInfo = new StringBuilder();
		enemyInfo.AppendLine("<b>※当前生效属性※</b>");

		var team = GameData.Battle.Enemies;

		foreach (var t in team) {
			enemyInfo.AppendLine($"  ({t.Rank}) {t.Name} 攻击{t.Attack} 防御{t.Defence} 速度{t.Speed} 统率{t.Dominance}");
		}

		enemyInfo.AppendLine();

		abilitiesInfo
			.AppendLine($"主将属性=攻击{GameData.Battle.EnemyTroop.Attack} 防御{GameData.Battle.EnemyTroop.Defence} 速度{GameData.Battle.EnemyTroop.Speed} 统率{GameData.Battle.EnemyTroop.Dominance}");

	


		abilitiesInfo.AppendLine();
	
		abilitiesInfo.AppendLine("<b>※攻城属性※</b>");
		abilitiesInfo.AppendLine("攻城点数=" + GameData.Battle.EnemyTroop.Siege);
		var info = enemyInfo.Append(abilitiesInfo).ToString();

		GameData.Battle.EnemyInfo = info;
	}


	void InitPlayerInfo() {
		StringBuilder teammatesInfo = new StringBuilder();
		StringBuilder abilitiesInfo = new StringBuilder();
		teammatesInfo.AppendLine("<b>※当前生效属性※</b>");

		var team = GameData.Battle.Teammates;

		foreach (var t in team) {
			teammatesInfo.AppendLine($"  ({t.Rank}) {t.Name} 攻击{t.Attack} 防御{t.Defence} 速度{t.Speed} 统率{t.Dominance}");
		}

		teammatesInfo.AppendLine();

		abilitiesInfo
			.AppendLine($"主角属性=攻击{GameData.Player.LimitedAttack} 防御{GameData.Player.LimitedDefence} 速度{GameData.Player.LimitedSpeed} 统率{GameData.Player.LimitedDominance}");
		abilitiesInfo
			.AppendLine($"主将属性=攻击{GameData.Battle.SelectedTroop.Attack} 防御{GameData.Battle.SelectedTroop.Defence} 速度{GameData.Battle.SelectedTroop.Speed} 统率{GameData.Battle.SelectedTroop.Dominance}");

	
		 abilitiesInfo.AppendLine("<b>※攻城属性※</b>");
		abilitiesInfo.AppendLine("攻城点数=" + GameData.Battle.SelectedTroop.Siege);
		var info = teammatesInfo.Append(abilitiesInfo).ToString();

		GameData.Battle.PlayerInfo = info;
	}

	public void Play() {
		StopAllCoroutines();
		StartCoroutine(StartRound());
	}
}