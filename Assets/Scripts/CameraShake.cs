using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform xrCamera;       // �����
    public float shakeDuration = 0.2f; // ����ʱ��
    public float shakeStrength = 0.02f; // ����ǿ��
    public int vibrato = 5;          // ����Ƶ��
    public float randomness = 60f;   // �����

    private Vector3 initialPosition; // �������ʼλ��

    void Start()
    {
        // ȷ�� XR ������и�����
        if (xrCamera == null)
        {
            Debug.LogError("XR Camera reference is missing!");
            return;
        }

        // ���游����ĳ�ʼλ��
        initialPosition = transform.localPosition;
    }

    public void Shake()
    {
        // ִ�ж�������
        transform.DOShakePosition(
            shakeDuration,  // ����ʱ��
            shakeStrength,  // ����ǿ��
            vibrato,        // ����Ƶ��
            randomness      // �����
        ).OnComplete(() =>
        {
            // �ָ���ʼλ��
            transform.localPosition = initialPosition;
        });
    }
}
