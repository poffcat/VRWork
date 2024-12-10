using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : Singleton<GuardManager>
{
    public List<Guard> guards;
    public void PlayerInSight() { 
    foreach (var guard in guards)
        {
            guard.playerInSight=true;
        }
    }
}
