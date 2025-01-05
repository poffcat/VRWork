using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDoor : MonoBehaviour, ITimeTricker
{
    int time = 0;

    void Start()
    {
        TimeManager.Instance.RegistTricker(this);
    }
    public void HandleTime()
    {
        time++;
        if (gameObject.activeSelf)
        {
            if (time == 5)
            {
                time = 0;
                gameObject.SetActive(false);
            }
        }
        else {
            if (time == 2) { 
            time = 0;
                gameObject.SetActive(true);
            }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            PlayerHealth.Instance.TakeDamageClicked();
            print("Found Player");
            GuardManager.Instance.FoundPlayer();
            SoundManager.Instance.PlayOneShot(0,0.6f);
        }
    }

}
