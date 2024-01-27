using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Buttons : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject underLine;

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != gameObject)
        {
            underLine.SetActive(false);
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        underLine.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        underLine.SetActive(false);
    }
}
