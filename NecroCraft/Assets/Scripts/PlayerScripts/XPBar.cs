using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class XpBar : MonoBehaviour
    {
        [SerializeField] private GameObject upgradePanel;

        private RectTransform _barImage;
        private float _xpBarMaxSize;
        private Vector2 _startingXpBarPosition;

        private void Start()
        {
            _barImage = GetComponent<RectTransform>();
            PlayerController.Instance.OnXpChanged += UpdateXpBar;
            _xpBarMaxSize = _barImage.rect.width;
            _barImage.sizeDelta = new Vector2(0, _barImage.sizeDelta.y);
            _startingXpBarPosition = _barImage.anchoredPosition;
            
            // Set initial bar
            _barImage.sizeDelta = new Vector2(2.5f, _barImage.sizeDelta.y);
            _barImage.anchoredPosition = new Vector2(
                _startingXpBarPosition.x - (_xpBarMaxSize - 2.5f) / 2,
                _startingXpBarPosition.y
            );
        }

        private void UpdateXpBar(float pctXp)
        {
            float newSize = _xpBarMaxSize * pctXp;
            _barImage.sizeDelta = new Vector2(newSize, _barImage.sizeDelta.y);
            _barImage.anchoredPosition = new Vector2(
                _startingXpBarPosition.x - (_xpBarMaxSize - newSize) / 2,
                _startingXpBarPosition.y
            );
            
            if (!(Math.Abs(pctXp - 1) < 0.001)) return;
            
            Time.timeScale = 0;
            upgradePanel.SetActive(true);
        }

        
    }
}