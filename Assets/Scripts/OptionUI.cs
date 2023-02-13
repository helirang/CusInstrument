using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }
    [SerializeField] List<GameObject> keyItems;
    List<TextMeshProUGUI> keyTextUIs;
    List<Button> keyBtns;

    private PlayerInputActions inputActions;
    void Start()
    {
        keyTextUIs = new List<TextMeshProUGUI>();
        keyBtns = new List<Button>();
        for(int i=0; i < keyItems.Count; i++)
        {
            keyBtns.Add(keyItems[i].GetComponentInChildren<Button>());
            keyTextUIs.Add(keyBtns[i].GetComponentInChildren<TextMeshProUGUI>());
        }
        UpdateVisual();
        this.gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        for(int i=0; i< keyItems.Count; i++)
        {
            keyTextUIs[i].text = GameInput.Instance.GetBindingText(i);
        }
    }
}
