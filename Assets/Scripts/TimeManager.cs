using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TimeManager : Singleton<TimeManager>
{
   List<ITimeTricker> trickers = new List<ITimeTricker>();
    float time=0;
    public SequenceEventExecutor exe;

    private void Start()
    {
        exe.Init((x)=> { UIManager.CloseDialogueBox(); });
        exe.Execute();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1) { 
        Tick();
        time = 0;
        }
    }
    public void RegistTricker(ITimeTricker tricker)
    {
        if (trickers.Contains(tricker)) return;
        trickers.Add(tricker);
    }
    public void DeRegistTricker(ITimeTricker tricker) {
        if (trickers.Contains(tricker)) { 
        trickers.Remove(tricker);
        }
    }
    public void Tick() { 
    foreach (var tricker in trickers)
        {
            tricker.HandleTime();
        }
    }
}
