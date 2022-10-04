using System.Collections.Generic;
using RangerRPG.Core;
using UnityEngine;

namespace RangerRPG.UI {
	public class TabGroup: MonoBehaviour, ITabBtnClickHandler {
		[SerializeField] private List<TabButton> _tabButtons = default;
		[SerializeField] private List<GameObject> _tabPages = default;
		[SerializeField] private ITabButtonEffect _btnEffect = default;
		
		// state
		private TabButton _selectedTab;

		// public void Subscribe(TabButton button) {
		// 	_tabButtons.Add(button);
		// }

		private void Start() {
			if (_tabButtons.Count != _tabPages.Count) {
				Log.Err(ToString(), $"mismatch in count, tabs: {_tabButtons.Count} | pages: {_tabPages.Count}");
			}
			foreach (var button in _tabButtons) {
				button.SetClickHandler(this);
			}
			if (_tabButtons.Count > 0) {
				OnTabSelect(_tabButtons[0]);
			}
		}

		private void ResetTabs() {
			foreach(var tabButton in _tabButtons) {
				if ((_selectedTab != null) && (tabButton == _selectedTab))
					continue;
				if (_btnEffect != null) {
					_btnEffect.UpdateTabView(tabButton, TabState.IDLE);
				}
				// _btnEffect?.UpdateTabView(tabButton, TabState.IDLE);
			}
		}
		
		private void ActivatePage() {
			var index = _tabButtons.IndexOf(_selectedTab);
			for (var i = 0; i < _tabPages.Count; i++) {
				_tabPages[i].SetActive(i == index);
			}
		}
		
		#region ITabGroupDelegate Implementation

		public void OnTabEnter(TabButton tb) {
			ResetTabs();
			if (_btnEffect != null) {
				_btnEffect.UpdateTabView(tb, TabState.HOVER);
			}
			// _btnEffect?.UpdateTabView(tb, TabState.HOVER);
		}
		
		public void OnTabExit(TabButton tb) {
			ResetTabs();
		}
		
		public void OnTabSelect(TabButton tb) {
			ResetTabs();
			if (_selectedTab != null) {
				_selectedTab.Select(false);
			}

			_selectedTab = tb;
			_selectedTab.Select(true);

			ResetTabs();
			if (_btnEffect != null) {
				_btnEffect.UpdateTabView(_selectedTab, TabState.SELECTED);
			}
			// _btnEffect?.UpdateTabView(_selectedTab, TabState.SELECTED);
			ActivatePage();
		}

		#endregion
		
		public void NextTab()
		{
			var currentIndex = _tabButtons.IndexOf(_selectedTab);
			var nextIndex = currentIndex + (currentIndex < _tabButtons.Count - 1? 1: 0);
			OnTabSelect(_tabButtons[nextIndex]);
		}

		public void PreviousTab()
		{
			var currentIndex = _tabButtons.IndexOf(_selectedTab);
			var prevIndex = currentIndex - (currentIndex > 0? 1: 0);
			OnTabSelect(_tabButtons[prevIndex]);
		}
	}

	public enum TabState {
		IDLE, HOVER, SELECTED
	}

	public abstract class ITabButtonEffect: MonoBehaviour {
		public abstract void UpdateTabView(TabButton btn, TabState state);
	}

	public interface ITabBtnClickHandler {
		void OnTabEnter(TabButton tb);
		void OnTabExit(TabButton tb);
		void OnTabSelect(TabButton tb);
	}
}