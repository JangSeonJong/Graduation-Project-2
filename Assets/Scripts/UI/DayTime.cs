using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTime : MonoBehaviour
{
    [SerializeField]
    Text dayTxt;

    public void UpdateDay()
    {
        if (!dayTxt.gameObject.activeSelf)
            dayTxt.gameObject.SetActive(true);

        dayTxt.text = "DAY " + GameManager.instance.dayCount.ToString();
    }
}
