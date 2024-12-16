using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerPositionManager : Singleton<PlayerPositionManager>
{
    private Vector3 playerPosition;
    public bool beFound=false;
    public CharacterController characterController;
    public CapsuleCollider capsuleCollider;
    public bool isQuiet=false;
    public DynamicMoveProvider dynamicMoveProvider;
    public InputActionReference playMoveAction;
    public Vector2 preMove;
    private void Update()
    {
        KeepCollider();
        UpdateState();
        SwitchSpeed();
    }
    private void OnEnable()
    {
        playerPosition = transform.position;
    }
    public void KeepCollider() { 
    
    capsuleCollider.center=characterController.center;
    }
    public Vector3 GetPlayerPositon() { 
    return playerPosition;
    }
    public void UpdateState() { 
        playerPosition =transform.position;

    }
    public void SwitchSpeed() {
        if (Keyboard.current.shiftKey.isPressed) {
            print("111");
            dynamicMoveProvider.moveSpeed = 1.5f;
            isQuiet = true;
        }
        else {
            dynamicMoveProvider.moveSpeed = 3f;
            isQuiet = false;       
        }
        print(playMoveAction.action.ReadValue<Vector2>()-preMove==Vector2.zero);
        if (playMoveAction.action.ReadValue<Vector2>() == Vector2.zero) {
            isQuiet = true;
        }

    }
    public void SetPlayerPosition(Vector3 vector3) { 
    transform.position = vector3;
    }
}
