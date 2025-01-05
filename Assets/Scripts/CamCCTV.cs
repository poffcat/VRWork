using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCCTV : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GuardManager.Instance.FoundPlayer();
            SoundManager.Instance.PlayOneShot(0, 0.6f);
            PlayerHealth.Instance.TakeDamageClicked();
            print("CCTV Found Player");
        }
    }
}
