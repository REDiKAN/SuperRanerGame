using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Finish : MonoBehaviour
{
    [Header("UI")]
    [SerializeField, Tooltip("Окно")] GameObject staticticPanel;
    [SerializeField, Tooltip("text")] TMP_Text textFinalTime;
    [SerializeField] TMP_Text jumpPanel;
    [SerializeField] TMP_Text downPlatformPanel;
    [SerializeField] TMP_Text lungePanel;

    [SerializeField] Timer timer;
    [SerializeField] float finishTime;
    [SerializeField] Image finishPanel;
    public List<float> timePoint;

    public List<GameObject> prefabsTimePanelList;
    public GameObject prefabsTimePanelZone;
    public GameObject prefabsTimePanelTrigger;
    [SerializeField] GameObject contentMenu;
    [SerializeField] TMP_Text textCountTimePanel;

    [Header("Количество действий")]
    [HideInInspector] public int nJump;
    [HideInInspector] public int nDownPlatform;
    [HideInInspector] public int nLunge;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timer.isTimerRuning = false;
            finishTime = timer.maxTime;
            Time.timeScale = 0f;
            textFinalTime.text = "Ваше время: " + finishTime.ToString("F3");
            staticticPanel.SetActive(true);
            finishUI();
        }
    }

    private void Start()
    {
        staticticPanel.SetActive(false);
    }

    public void AddTimeTrigger(float time, string namePoint)
    {
        GameObject prefabsPanel = Instantiate(prefabsTimePanelTrigger);
        prefabsPanel.transform.parent = contentMenu.transform;
        prefabsPanel.transform.localScale = new Vector3(1, 1, 1);

        prefabsPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = namePoint;
        prefabsPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = time.ToString("F3");
        prefabsTimePanelList.Add(prefabsPanel);
    }

    public void AddTimeZone(float timeOne, float timeTwo, string namePoint)
    {
        GameObject prefabsPanel = Instantiate(prefabsTimePanelZone);
        prefabsPanel.transform.parent = contentMenu.transform;
        prefabsPanel.transform.localScale = new Vector3(1, 1, 1);

        prefabsPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = namePoint;
        prefabsPanel.transform.GetChild(1).GetComponent<TMP_Text>().text =  "Время входа:" + timeOne.ToString("F3");
        prefabsPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = "Время выхода:" + timeTwo.ToString("F3");
        prefabsTimePanelList.Add(prefabsPanel);
    }

    public void RestartGameButton()
    {
        
    }

    void finishUI()
    {
        jumpPanel.text = nJump.ToString();
        downPlatformPanel.text = nDownPlatform.ToString();
        lungePanel.text = nLunge.ToString();
    }

    public void DropDownTimePoint(int value)
    {
        if (value == 0)
        {
            for (int i = 0; i < prefabsTimePanelList.Count; i++)
            {
                if (prefabsTimePanelList[i].transform.childCount == 3)
                {
                    prefabsTimePanelList[i].SetActive(true);
                }
                else { prefabsTimePanelList[i].SetActive(false); }
            }
        }
        else if (value == 1)
        {
            foreach (var item in prefabsTimePanelList)
            {
                if (item.transform.childCount == 2)
                {
                    item.SetActive(true);
                }
                else { item.SetActive(false); }
            }
        }
        else if (value == 2)
        {
            foreach (var item in prefabsTimePanelList)
            {
                item.SetActive(true);
            }
        }
    }

}
