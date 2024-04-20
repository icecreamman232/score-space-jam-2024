using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NextLevelButton : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.LoadLevel();
        base.OnPointerClick(eventData);
    }
}
