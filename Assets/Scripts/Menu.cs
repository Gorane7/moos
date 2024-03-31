using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Menu : MonoBehaviour {
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI healthUI;

    private bool isMenuOpen = true;

    public void ToggleMenu() {
        RectTransform rectTransform = GetComponent<RectTransform>();
        // Do the reverse animation
        if (isMenuOpen) {
            rectTransform.DOMove(new Vector3(0, -200, 0), 0.5f).SetEase(Ease.Linear);
        } else {
            rectTransform.DOMove(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.Linear);
            
        }

        isMenuOpen = !isMenuOpen;
    }

    private void OnGUI(){
        currencyUI.text = LevelManager.main.currency.ToString();
        healthUI.text = LevelManager.main.castle.GetComponent<CastleBehavior>().GetHealth().ToString();
    }
}
