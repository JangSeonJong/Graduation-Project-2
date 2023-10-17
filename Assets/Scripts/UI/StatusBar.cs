using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField]
    PlayerStatus status;
    [SerializeField]
    Image hpBar;
    [SerializeField]
    Image hungerBar;
    [SerializeField]
    Image thirstBar;

    [SerializeField]
    Text hpText;
    [SerializeField]
    Text hungerText;
    [SerializeField]
    Text thirstText;

    void FixedUpdate()
    {
        hpBar.fillAmount = (float)status.Hp / status.MaxHp;
        hpText.text = status.Hp.ToString();

        hungerBar.fillAmount = (float)status.Hunger / status.MaxHunger;
        hungerText.text = status.Hunger.ToString();

        thirstBar.fillAmount = (float)status.Thirst / status.MaxThirst;
        thirstText.text = status.Thirst.ToString();
    }
}
