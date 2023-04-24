using UnityEngine;

namespace PlayerScripts
{
    public class HealthBar : MonoBehaviour
    {
        private RectTransform _barImage;
        private float _hpBarMaxSize;
        private Vector2 _startingHealthBarPosition;
        
        private void Start()
        {
            _barImage = GetComponent<RectTransform>();
            PlayerController.Instance.OnHealthChanged += UpdateHealthBar;
            _hpBarMaxSize = _barImage.rect.width;
            _startingHealthBarPosition = _barImage.anchoredPosition;
        }
        private void UpdateHealthBar(float pctHp)
        {
            float newSize = _hpBarMaxSize * pctHp;

            _barImage.sizeDelta = new Vector2(newSize, _barImage.sizeDelta.y);
            _barImage.anchoredPosition = new Vector2(_startingHealthBarPosition.x - (_hpBarMaxSize - newSize) / 2,
                _startingHealthBarPosition.y);
        }
    }
}