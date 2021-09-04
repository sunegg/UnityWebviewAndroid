using System;
using System.Linq;
using Fs.Extension;
using HAHAHA;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameListView<TComponent, TModel> : USingleton<GameListView<TComponent, TModel>>
	where TComponent : MonoBehaviour {
	[SerializeField] protected Transform  container;
	[SerializeField] protected TComponent component;
	[SerializeField] protected Constraint constraint;
	[SerializeField] protected int        fixedRowCount;
	[SerializeField] protected Text       pageText;
	[SerializeField] protected Button     nextPageButton, previousPageButton;
	[SerializeField] protected bool       initOnEnable;
	private                    TModel[]   _data;
	private                    int        _currentIndex, _maxPage;
	
	protected override void OnEnable() {
		base.OnEnable();
		if (initOnEnable)
			OnInit();
	}

	void Start() {
		nextPageButton.onClick.AddListener(NextPage);
		previousPageButton.onClick.AddListener(PreviousPage);
		if (!initOnEnable)
			OnInit();
	}

	public abstract void OnInit();
	public void SetData(TModel[] data) {
		_data = data;
		switch (constraint) {
			case Constraint.Flexible:
				UpdateList(data);
				break;
			case Constraint.FixedRowCount:
				_maxPage = Mathf.CeilToInt(data.Length * 1f / fixedRowCount);
				if (_maxPage == 0) _maxPage = 1;
				UpdatePageText();
				UpdateList(GetDataByIndex);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private TModel[] GetDataByIndex => _data.Skip(_currentIndex * fixedRowCount).Take(fixedRowCount).ToArray();

	public void NextPage() {
		if (_currentIndex + 1 < _maxPage) {
			_currentIndex++;
			UpdateListByIndex();
		}
	}

	public void Refresh() {
		UpdateListByIndex();
	}
	
	public void PreviousPage() {
		if (_currentIndex - 1 >= 0) {
			_currentIndex--;
			UpdateListByIndex();
		}
	}

	private void UpdateListByIndex() {
		UpdateList(GetDataByIndex);
		UpdatePageText();
	}

	private void UpdateList(TModel[] data) {
		container.DestroyChildrens();
		foreach (var d in data) {
			var item = Instantiate(component.gameObject, container).GetComponent<TComponent>();
			OnEachInit(item, d);
		}
	}

	private void UpdatePageText() => pageText.text = $"{_currentIndex + 1}/{_maxPage}";

	protected abstract void OnEachInit(TComponent prefab, TModel data);

	protected enum Constraint {
		Flexible,
		FixedRowCount
	}
}