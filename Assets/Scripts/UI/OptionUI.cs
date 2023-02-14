using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }
    [SerializeField] GameObject pressReadyUI;
    [SerializeField] List<GameObject> keyItems;
    [SerializeField] Button exitBtn;
    List<TextMeshProUGUI> keyTextUIs;
    List<Button> keyBtns;

    private PlayerInputActions inputActions;
    void Start()
    {
        keyTextUIs = new List<TextMeshProUGUI>();
        keyBtns = new List<Button>();
        for(int i=0; i < keyItems.Count; i++)
        {
            int index = i;
            keyBtns.Add(keyItems[i].GetComponentInChildren<Button>());
            keyTextUIs.Add(keyBtns[i].GetComponentInChildren<TextMeshProUGUI>());
            keyBtns[i].onClick.AddListener(()=>KeyRebindingStart(index));
        }
        UpdateVisual();
        exitBtn.onClick.AddListener(ExitGame);
        this.gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        for(int i=0; i< keyItems.Count; i++)
        {
            keyTextUIs[i].text = GameInput.Instance.GetBindingText(i);
        }
        pressReadyUI.SetActive(false);
    }

    private void KeyRebindingStart(int num)
    {
        pressReadyUI.SetActive(true);
        GameInput.Instance.RebindBinding(num,KeyRebindEnd);
    }

    private void KeyRebindEnd()
    {
        UpdateVisual();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
