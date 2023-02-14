using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialOnOff : MonoBehaviour
{
    [SerializeField] string tutorialKey = "TutorialComplete";
    [SerializeField] Button closeBtn;
    private void Start()
    {
        bool isFirst = 
            PlayerPrefs.GetInt(tutorialKey, 0) == 0 ?
            true : false;
        if (isFirst)
        {
            closeBtn.onClick.AddListener(CloseMainTutorial);
        }
        else
        {
            CloseMainTutorial();
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(tutorialKey, 1);
        closeBtn.onClick.RemoveListener(CloseMainTutorial);
    }
    void CloseMainTutorial()
    {
        this.gameObject.SetActive(false);
    }
}
