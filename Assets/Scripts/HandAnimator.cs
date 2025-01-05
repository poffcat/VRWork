using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimator : MonoBehaviour
{
    [Header("Hand Settings")]
    public string leftHandTag = "LeftHand"; // ���ֵ�Tag
    public string rightHandTag = "RightHand"; // ���ֵ�Tag

    public  Animator leftHandAnimator; // ���ֵ�Animator
    public  Animator rightHandAnimator; // ���ֵ�Animator
    GameObject leftHand;
    GameObject rightHand;

  [Header("Input Actions")]
    public InputActionProperty leftGripAction; // ����Grip����
    public InputActionProperty leftTriggerAction; // ����Trigger����
    public InputActionProperty rightGripAction; // ����Grip����
    public InputActionProperty rightTriggerAction; // ����Trigger����

    [Header("Blend Tree Parameters")]
    public string gripParameterName = "Grip"; // Grip��������
    public string triggerParameterName = "Trigger"; // Trigger��������

    void Start()
    {
        StartCoroutine(FindHand());
       
    }

    void Update()
    {
        // �������ֶ�������
        if (leftHandAnimator != null)
        {
            UpdateHandAnimator(
                leftHandAnimator,
                leftGripAction.action.ReadValue<float>(),
                leftTriggerAction.action.ReadValue<float>()
            );
        }

        // �������ֶ�������
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

        // ӳ��ֵ��Χ�������������� -0.2 �� 1 �ķ�Χ�ڣ�
        gripValue = Mathf.Clamp(gripValue, -0.2f, 1.0f);
        triggerValue = Mathf.Clamp(triggerValue, -0.2f, 1.0f);

        // ����Animator�Ĳ���
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
