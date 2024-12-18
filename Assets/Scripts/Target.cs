using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour, IPointerClickHandler
{
    public event Action<GameObject> OnClicked;

    // Méthode appelée lorsqu'on clique sur l'élément UI
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Target clicked!");
        OnClicked?.Invoke(gameObject);
    }
}