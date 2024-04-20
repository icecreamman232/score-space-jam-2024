using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitLevelButton : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
        base.OnPointerClick(eventData);
    }
}
