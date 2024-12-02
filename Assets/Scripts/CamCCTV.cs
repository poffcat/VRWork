using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCCTV : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GuardManager.Instance.PlayerInSight();
            print("CCTV Found Player");
        }
    }
}
