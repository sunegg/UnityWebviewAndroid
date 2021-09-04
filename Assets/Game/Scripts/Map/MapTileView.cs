using System;
using UnityEngine;
using UnityEngine.UI;

public class MapTileView : MonoBehaviour {
	[SerializeField] private Image _tile, _league, _attitude;

	[SerializeField] private GameObject _half, _full;

	private Button _button;

	public int Id;

	public int X;

	public int Y;

	public Visibility Visibility = Visibility.None;

	void Awake() {
		_button = GetComponent<Button>();
	}

	void Start() {
		_button.onClick.AddListener(() => {
			if (Visibility == Visibility.All)
				MapManager.Instance.SelectMapTile(X, Y);
		});
	}

	public void Occupied(Sprite league, Sprite attitude) {
		_league.sprite   = league;
		_attitude.sprite = attitude;
		Visibility = Visibility.All;
		ShowAll();
	}
	public void SetView(Sprite tile, Sprite league, Sprite attitude) {
		_tile.sprite     = tile;
		_league.sprite   = league;
		_attitude.sprite = attitude;
	}

	public void SetId(int id) {
		Id = id;
	}

	public void SetPos(int x, int y) {
		X = x;
		Y = y;
	}

	public void SetVisibility(Visibility visibility) {
		if (visibility > Visibility) return;
		Visibility = visibility;
		switch (visibility) {
			case Visibility.All:
				ShowAll();
				break;
			case Visibility.Half:
				ShowHalf();
				break;
			case Visibility.None:
				Hide();
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(visibility), visibility, null);
		}
	}

	private void ShowHalf() {
		_half.SetActive(true);
		_full.SetActive(false);
	}

	private void Hide() {
		_half.SetActive(false);
		_full.SetActive(true);
	}

	private void ShowAll() {
		_half.SetActive(false);
		_full.SetActive(false);
	}
}