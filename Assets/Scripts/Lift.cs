using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public bool playerInLift;
    private bool startUp;
    private float yDif = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { 
        playerInLift = true;
        }
        yDif = transform.position.y - PlayerPositionManager.Instance.GetPlayerPositon().y;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") { 
        playerInLift=false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (playerInLift && startUp) {
            Vector3 pos = PlayerPositionManager.Instance.GetPlayerPositon();
            PlayerPositionManager.Instance.SetPlayerPosition(new Vector3(pos.x, transform.position.y - yDif, pos.z));
        }
    }
    void OnAnimationEvent() {
        startUp = true;
    }
}
