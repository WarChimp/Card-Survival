using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverTextBox : MonoBehaviour
{

    private static HoverTextBox instance;

    [SerializeField] private Camera uiCamera;
    [SerializeField] private RectTransform canvasRectTransform;


    private Text tooltipText;
    private RectTransform backgroundRectTransform;
    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("TooltipBackground").GetComponent<RectTransform>();
        tooltipText = transform.Find("TooltipText").GetComponent<Text>();

        gameObject.SetActive(false);

        //ShowToolTip("random tooltip text");
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;

        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }
    private void ShowToolTip(string tooltipString)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;

        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideToolTip()
    {
        gameObject.SetActive(false);

    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowToolTip(tooltipString);
    }

    public static void HideTooltip_Static()
    {
        instance.HideToolTip();
    }
}
