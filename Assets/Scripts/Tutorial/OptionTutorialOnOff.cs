using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionTutorialOnOff : MonoBehaviour
{
    [SerializeField] string tutorialKey = "TutorialComplete";
    [SerializeField] Button openBtn;
    [SerializeField] Button closeBtn;
    bool isFirst = false;
    private void Start()
    {
        isFirst = 
            PlayerPrefs.GetInt(tutorialKey, 0) == 0 ?
            true : false;

        if (isFirst)
        {
            openBtn.onClick.AddListener(OnTutorial);
            closeBtn.onClick.AddListener(CloseGuide);
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    public void OnTutorial()
    {
        this.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        CloseGuide();
    }

    void CloseGuide()
    {
        openBtn.onClick.RemoveListener(OnTutorial);
        closeBtn.onClick.RemoveListener(CloseGuide);
        PlayerPrefs.SetInt(tutorialKey, 1);
        this.gameObject.SetActive(false);
    }
}
