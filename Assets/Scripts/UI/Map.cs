using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MapButton
{
    public Button btn;
    public string btnTxt;
    public Text apTxt;
    public int count;
    public bool isActivated;
}

public class Map : MonoBehaviour
{
    [SerializeField]
    GameObject mapPanel;

    [SerializeField]
    MapButton[] mapButtons;

    [SerializeField]
    Text apTxt;

    Dictionary<string, MapButton> mapDict = new Dictionary<string, MapButton>();

    public bool isActive = false;

    void Start()
    {
        foreach (MapButton mapBtn in mapButtons)
        {
            if (mapBtn.apTxt != null)
                mapBtn.apTxt.text = "/ " + mapBtn.count.ToString();

            mapDict.Add(mapBtn.btnTxt, mapBtn);
        }
    }

    public void OpenMap()
    {
        isActive = !isActive;
        GameManager.canMove = !isActive;

        mapPanel.SetActive(isActive);

        apTxt.text = GameManager.instance.activePoint.ToString() + " / " + GameManager.instance.maxActivePoint.ToString();

        foreach (MapButton mapBtn in mapButtons)
        {
            if (GameManager.instance.activePoint < mapBtn.count)
                mapBtn.btn.interactable = false;
            else
            {
                if (mapBtn.isActivated)
                    mapBtn.btn.interactable = true;
            }
        }
    }

    public void CloseMap()
    {
        isActive = !isActive;
        mapPanel.SetActive(isActive);
    }

    public void OffButton(string scene)
    {
        if (mapDict[scene].btnTxt != "House")
            mapDict[scene].btn.interactable = false;

        mapDict[scene].isActivated = false;

        GameManager.instance.ActivePoint(-mapDict[scene].count);
    }

    public void OnButton()
    {
        foreach (MapButton btn in mapButtons)
        {
            btn.isActivated = true;
            btn.btn.interactable = true;
        }
    }
}
