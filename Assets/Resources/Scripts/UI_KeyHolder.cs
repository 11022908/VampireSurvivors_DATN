
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_KeyHolder : MonoBehaviour
{
    [SerializeField] private KeyHolder keyHolder;
    private Transform container;
    private Transform keyTemplate;

    //private void Awake()
    //{
    //    keyHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyHolder>();
    //    container = transform.Find("Container");
    //    keyTemplate = container.transform.Find("KeyTemplate");
    //    keyTemplate.gameObject.SetActive(false);
    //}
    private void Start()
    {
        keyHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyHolder>();
        container = transform.Find("Container");
        keyTemplate = container.transform.Find("KeyTemplate");
        keyTemplate.gameObject.SetActive(false);
        keyHolder.OnKeyChanged += KeyHolder_OnKeysChanged;
    }
    private void KeyHolder_OnKeysChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        foreach (Transform item in container)
        {
            if (item == keyTemplate) continue;
            Destroy(item.gameObject);
        }
        List<Key.KeyType> keyList = keyHolder.GetkeyList();
        for (int i = 0; i < keyList.Count; i++)
        {
            Key.KeyType keyType = keyList[i];
            Transform keytransform = Instantiate(keyTemplate, container);
            keytransform.gameObject.SetActive(true);
            keytransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(50 * i, 0);
            Image keyImage = keytransform.Find("Image").GetComponent<Image>();
            switch (keyType)
            {
                case Key.KeyType.Red:
                    keyImage.color = new Color(255,0,0,255);
                    break;
                case Key.KeyType.Green:
                    keyImage.color = new Color(0, 255, 0, 255);
                    break;
                case Key.KeyType.Blue:
                    keyImage.color = new Color(0, 0, 255, 255);
                    break;
                default:
                    keyImage.color = new Color(255, 255, 255, 255);
                    break;
            }
        }
    }
}
