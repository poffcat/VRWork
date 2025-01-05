using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform xrCamera;       // 主相机
    public float shakeDuration = 0.2f; // 抖动时长
    public float shakeStrength = 0.02f; // 抖动强度
    public int vibrato = 5;          // 抖动频率
    public float randomness = 60f;   // 随机性

    private Vector3 initialPosition; // 父对象初始位置

    void Start()
    {
        // 确保 XR 主相机有父对象
        if (xrCamera == null)
        {
            Debug.LogError("XR Camera reference is missing!");
            return;
        }

        // 保存父对象的初始位置
        initialPosition = transform.localPosition;
    }

    public void Shake()
    {
        // 执行抖动动画
        transform.DOShakePosition(
            shakeDuration,  // 持续时间
            shakeStrength,  // 抖动强度
            vibrato,        // 抖动频率
            randomness      // 随机性
        ).OnComplete(() =>
        {
            // 恢复初始位置
            transform.localPosition = initialPosition;
        });
    }
}
