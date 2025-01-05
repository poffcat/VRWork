using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{
    public static GuardManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else Instance= this;
    }
    public List<Guard> guards;
    public void FoundPlayer() { 
    foreach (var guard in guards)
        {
            guard.playerInSight=true;
        }
    }
}
