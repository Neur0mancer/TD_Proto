using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }
    [SerializeField] private RectTransform canvasTramsform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRecTransform;
    private RectTransform rectTransform;
    private TooltipTimer tooltipTimer;

    private void Awake()
    {
        Instance = this;
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRecTransform = transform.Find("background").GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();

        if (tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if(tooltipTimer.timer <= 0)
            {
                Hide();
            }
        }
    }

    private void HandleFollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasTramsform.localScale.x;

        if (anchoredPosition.x + backgroundRecTransform.rect.width > canvasTramsform.rect.width)
        {
            anchoredPosition.x = canvasTramsform.rect.width - backgroundRecTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRecTransform.rect.height > canvasTramsform.rect.height)
        {
            anchoredPosition.y = canvasTramsform.rect.height - backgroundRecTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRecTransform.sizeDelta = textSize + padding;
    }

    public void Show(string tooltipText, TooltipTimer tooltipTimer = null)
    {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(tooltipText);
        HandleFollowMouse();



    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class TooltipTimer
    {
        public float timer;
    }
}
