using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image penguin;
    Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Hovered", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Hovered", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = penguin.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
