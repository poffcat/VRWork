using Leguar.LowHealth;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth :Singleton<PlayerHealth>
{

    public int Health=100;
    public LowHealthController shaderControllerScript;
    public LowHealthDirectAccess shaderAccessScript;
    Coroutine coroutine;
    private float takingDamage;
    void Start()
    {
        takingDamage = 0f;
    }
    public void TakeDamageClicked()
    {
        takingDamage = 1f;
    }
    void Update()
    {
   

        if (takingDamage > 0f)
        {
            takingDamage -= Time.deltaTime * 0.5f;
            shaderAccessScript.SetColorLossEffect(takingDamage, 1f);
            //				shaderAccessScript.SetVisionLossEffect(takingDamage*0.5f); // Uncomment to add darkening effect to taking damage, but this will then clash with "Waking up 1" effect
        }
        if (Health <= 0 && coroutine == null) {
          coroutine = StartCoroutine(ReStart());
        }
    }

    public void HealthDown() {
        if(Health>0)
        Health -= 10;

        
        setNewPlayerHealth(Health / 150f, true);

    }
    private void setNewPlayerHealth(float newPlayerHealthPercent, bool healthGoingDown)
    {

        if (healthGoingDown)
        {

            // Player took damage, make transition faster (1 second)
            shaderControllerScript.SetPlayerHealthSmoothly(newPlayerHealthPercent, 1f);

        }
        else
        {

            // Player gained health (medikit?), make transition slightly slower (2 seconds)
            shaderControllerScript.SetPlayerHealthSmoothly(newPlayerHealthPercent, 2f);

        }

    }
    IEnumerator ReStart() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
