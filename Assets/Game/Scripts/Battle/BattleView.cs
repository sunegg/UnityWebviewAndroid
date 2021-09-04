using HAHAHA;
using UnityEngine;
using UnityEngine.UI;

public class BattleView : USingleton<BattleView> {
	[SerializeField] private Text infoText;

	[SerializeField] private GameObject detailView;

	[SerializeField] private GameObject resultView;
	[SerializeField] private Text       resultText;
	[SerializeField] private Animator   playerHpAnim,playerHpAnim2, enemyHpAnim, enemyHpAnim2, battleAnim;
	[SerializeField] private Text       playerHpText,playerHpText2, enemyHpText,enemyHpText2;

	[SerializeField] private Sprite[] _troopSprites;

	[SerializeField] private Image _player,_enemy;

	
	private bool playerHpTrigger,enemyHpTrigger;
	
	[Multiline] [SerializeField] private string stringFormat;

	public void UpdateView() {
		_player.sprite = _troopSprites[GameData.Battle.SelectedTroop.Id-1];
		_enemy.sprite = _troopSprites[GameData.Battle.EnemyTroop.Id-1];
		
	}
	
	public void ShowPlayerInfo() {
		infoText.text = GameData.Battle.PlayerInfo;
		detailView.SetActive(true);
	}

	public void ShowEnemyInfo() {
		infoText.text = GameData.Battle.EnemyInfo;
		detailView.SetActive(true);
	}

	public void ShowResult(string text) {
		resultText.text = text;
		resultView.SetActive(true);
	}

	public void ShowPlayerHpCost(string hp, bool isAttack = false) {
		if (isAttack) {
			playerHpText2.color    = playerHpText.color    = new Color(97 / 255f, 0, 0,1);
			playerHpText2.fontSize = playerHpText.fontSize = 35;
		}
		else {
			playerHpText2.color = playerHpText.color = Color.green;
			playerHpText2.fontSize = playerHpText.fontSize= 40;
		}
		if (playerHpTrigger==false) {
			playerHpTrigger = true;
			playerHpText2.text = hp;
			playerHpAnim2.Play("Hp");
		}
		else {
			playerHpTrigger = false;
			playerHpText.text = hp;
			playerHpAnim.Play("Hp");
		}

		if (isAttack)
			battleAnim.Play("EnemyAttack");
	}


	protected override void OnDisable() {
		base.OnDisable();
		playerHpText.text = "";
		enemyHpText.text = "";
	}

	public void ShowEnemyHpCost(string hp, bool isAttack = false) {
		if (isAttack) {
			enemyHpText2.color    = enemyHpText.color    = new Color(97 / 255f, 0, 0,1);
		}
		else {
			enemyHpText2.color    = enemyHpText.color    = Color.green;
		}
		if (enemyHpTrigger==false) {
			enemyHpTrigger = true;
			enemyHpText2.text = hp;
			enemyHpAnim2.Play("Hp");
		}
		else {
			enemyHpTrigger = false;
			enemyHpText.text = hp;
			enemyHpAnim.Play("Hp");
		}
		if (isAttack)
			battleAnim.Play("PlayerAttack");
	}
}