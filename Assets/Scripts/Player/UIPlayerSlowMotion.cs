using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSlowMotion : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    [SerializeField] private Image backgroundImage;

     public void SetHealthBarPercentage(float percentage){
        float parentWidth =GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
