using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitLevelButton : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.StopBGM();
        SceneLoader.Instance.LoadScene("GameplayScene","MenuScene");
        base.OnPointerClick(eventData);
    }
}
