using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RangerRPG.UI {
    [RequireComponent(typeof(Image))]
    public class TabButton: MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler {
        private Image _bgImage;

        [SerializeField] private TMP_Text _text = default;
        [SerializeField] private UnityEvent _onTabSelected = default;
        [SerializeField] private UnityEvent _onTabDeSelected = default;
        private ITabBtnClickHandler _clickHandler;

        public Image BgImage => _bgImage;
        public TMP_Text Text => _text;

        private TabState _state;
        public TabState State { // Can be used to check current state; useful when animations need to be added
            get => _state;
            set => _state = value;
        }

        private void Start() {
            _bgImage = GetComponent<Image>();
        }
		
        public void SetClickHandler(ITabBtnClickHandler clickHandler) {
            _clickHandler = clickHandler;
        }

        public void Select(bool isSelected) {
            if (isSelected) {
                _onTabSelected.Invoke();
            } else {
                _onTabDeSelected.Invoke();
            }
        }

		#region Pointer Events On Button

        public void OnPointerEnter(PointerEventData eventData) {
            _clickHandler.OnTabEnter(this);
        }

        public void OnPointerClick(PointerEventData eventData) {
            _clickHandler.OnTabSelect(this);
        }

        public void OnPointerExit(PointerEventData eventData) {
            _clickHandler.OnTabExit(this);
        }

		#endregion
    }
}