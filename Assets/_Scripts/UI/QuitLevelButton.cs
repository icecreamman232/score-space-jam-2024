using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitLevelButton : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        SceneLoader.Instance.LoadScene("GameplayScene","MenuScene");
        base.OnPointerClick(eventData);
    }
}
