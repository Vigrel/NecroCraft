using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class XPBar : MonoBehaviour
    {
        [SerializeField] private GameObject upgradePanel;
        
        private RectTransform _barImage;
        private float _xpBarMaxSize;
        private Vector2 _startingXPBarPosition;
        
        private void Start()
        {
            _barImage = GetComponent<RectTransform>();
            PlayerController.Instance.OnXPChanged += UpdateHealthBar;
            _xpBarMaxSize = _barImage.rect.width;
            _barImage.sizeDelta = new Vector2(0, _barImage.sizeDelta.y);
            _startingXPBarPosition = _barImage.anchoredPosition;
        }

        private void UpdateHealthBar(float pctHp)
        {
            float newSize = _xpBarMaxSize * pctHp;
            _barImage.sizeDelta = new Vector2(newSize, _barImage.sizeDelta.y);
            _barImage.anchoredPosition = new Vector2(_startingXPBarPosition.x - (_xpBarMaxSize - newSize) / 2,
                _startingXPBarPosition.y);
            if(pctHp == 1f){
                Time.timeScale = 0;
                upgradePanel.SetActive(true);            
            }
        }

        public void LevelUp(){
            Debug.Log("btnClick");
            // Time.timeScale = 1;
            // upgradePanel.SetActive(false);            
        }
    }
}