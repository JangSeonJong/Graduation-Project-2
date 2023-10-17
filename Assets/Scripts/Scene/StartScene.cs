using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Product
{
    public Item item;
    public Text productPriceTxt;
    public int itemPrice;
}

public class StartScene : MonoBehaviour
{
    [SerializeField]
    Text moneyTxt;
    [SerializeField]
    int money;
    [SerializeField]
    int poorMoney;
    [SerializeField]
    int richMoney;

    [SerializeField]
    Product[] products;

    Define.Difficulty difficulty;

    void Start()
    {
        for (int i = 0; i < products.Length; i++)
        {
            products[i].productPriceTxt.text = "/ " + products[i].itemPrice.ToString();
        }
    }

    public void NextPanel(GameObject go)
    {
        go.SetActive(true);
    }

    public void ClosePanel(GameObject go)
    {
        go.SetActive(false);
    }

    public void GameStart()
    {
        GameManager.isGameStart = true;
        GameManager.instance.dayTime.UpdateDay();
    }

    public void Difficulty(string choice)
    {
        switch(choice)
        {
            case "Poor":
                money = poorMoney;
                difficulty = Define.Difficulty.Poor;
                break;
            case "Rich":
                money = richMoney;
                difficulty = Define.Difficulty.Rich;
                break;
        }

        moneyTxt.text = "$ " + string.Format("{0:#,###}", money);
    }

    public void Buy(int index)
    {
        int price = products[index].itemPrice;

        if (price > money)
            return;

        money -= price;

        if (money > 0)
            moneyTxt.text = "$ " + string.Format("{0:#,###}", money);
        else
            moneyTxt.text = "$ " + money.ToString();

        GameManager.instance.inven.AcquireItem(products[index].item);
    }

    public void Refund()
    {
        foreach(Slot slot in GameManager.instance.inven.invenSlots)
        {
            if (slot.item != null)
                slot.ClearSlot();
        }

        switch (difficulty)
        {
            case Define.Difficulty.Poor:
                money = poorMoney;
                break;
            case Define.Difficulty.Rich:
                money = richMoney;
                break;
        }

        moneyTxt.text = "$ " + string.Format("{0:#,###}", money);
    }
}
