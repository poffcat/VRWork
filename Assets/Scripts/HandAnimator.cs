using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimator : MonoBehaviour
{
    [Header("Hand Settings")]
    public string leftHandTag = "LeftHand"; // 左手的Tag
    public string rightHandTag = "RightHand"; // 右手的Tag

    public  Animator leftHandAnimator; // 左手的Animator
    public  Animator rightHandAnimator; // 右手的Animator
    GameObject leftHand;
    GameObject rightHand;

  [Header("Input Actions")]
    public InputActionProperty leftGripAction; // 左手Grip输入
    public InputActionProperty leftTriggerAction; // 左手Trigger输入
    public InputActionProperty rightGripAction; // 右手Grip输入
    public InputActionProperty rightTriggerAction; // 右手Trigger输入

    [Header("Blend Tree Parameters")]
    public string gripParameterName = "Grip"; // Grip参数名称
    public string triggerParameterName = "Trigger"; // Trigger参数名称

    void Start()
    {
        StartCoroutine(FindHand());
       
    }

    void Update()
    {
        // 更新左手动画参数
        if (leftHandAnimator != null)
        {
            UpdateHandAnimator(
                leftHandAnimator,
                leftGripAction.action.ReadValue<float>(),
                leftTriggerAction.action.ReadValue<float>()
            );
        }

        // 更新右手动画参数
        if (rightHandAnimator != null)
        {
            UpdateHandAnimator(
                rightHandAnimator,
                rightGripAction.action.ReadValue<float>(),
                rightTriggerAction.action.ReadValue<float>()
            );
        }
    }

    private void UpdateHandAnimator(Animator animator, float gripValue, float triggerValue)
    {
        if (animator == null) return;

        // 映射值范围（将输入限制在 -0.2 到 1 的范围内）
        gripValue = Mathf.Clamp(gripValue, -0.2f, 1.0f);
        triggerValue = Mathf.Clamp(triggerValue, -0.2f, 1.0f);

        // 设置Animator的参数
        animator.SetFloat(gripParameterName, gripValue);
        animator.SetFloat(triggerParameterName, triggerValue);
    }
    IEnumerator FindHand() { 
    yield return new WaitForSeconds(0.1f);
       leftHand = GameObject.FindGameObjectWithTag(leftHandTag);
       rightHand = GameObject.FindGameObjectWithTag(rightHandTag);
        if (leftHand != null)
        {
            leftHandAnimator = leftHand.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("Left hand object not found! Make sure the tag is set correctly.");
        }

        if (rightHand != null)
        {
            rightHandAnimator = rightHand.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("Right hand object not found! Make sure the tag is set correctly.");
        }
    }
}
