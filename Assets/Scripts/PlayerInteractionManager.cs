using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInteractionManager : Singleton<PlayerInteractionManager>
{
    [SerializeField]
    private bool hasCard;
    public GameObject card;
    private void Start()
    {
        RegistCardEvent();
    }
    private void RegistCardEvent() {
        XRGrabInteractable cardI=card.GetComponent<XRGrabInteractable>();
        cardI.focusEntered.AddListener(GetCard);
        cardI.focusExited.AddListener(PutCard);
    }
    public bool CheckHasCard() { 
    return hasCard;
    }
    private void GetCard(FocusEnterEventArgs a) { 
    hasCard = true;
        print("get");
    }
    private void PutCard(FocusExitEventArgs a) { 
    hasCard=false;
        print("put");
    }

}
