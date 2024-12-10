using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCheck : MonoBehaviour
{
    public Material material;
    public Animator lift;
    private void Start()
    {
        material.SetColor("_EmissionColor", new Color(191, 160, 0) * 0.015f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Card") return;
        print("IN!");
        if (PlayerInteractionManager.Instance.CheckHasCard()) {
            material.SetColor("_EmissionColor", new Color(17, 191, 0)*0.015f);
            lift.SetBool("Active", true); 
            
        }
    }
    private void OnDestroy()
    {
        material.SetColor("_EmissionColor", new Color(191, 160, 0) * 0.015f);
    }
}
