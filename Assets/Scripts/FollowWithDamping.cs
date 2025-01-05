using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithDamping : MonoBehaviour
{
    public Transform target; // VR相机或视角的中心点
    public float positionDamping = 5f; // 阻尼系数（位置）
    public float rotationDamping = 5f; // 阻尼系数（旋转）

    void Update()
    {
        if (target == null) return;

        // 平滑地跟随位置
        Vector3 targetPosition = target.position + target.forward * 2f + Vector3.up * 0.5f; // 调整相对位置
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * positionDamping);

        // 平滑地跟随旋转
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - target.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationDamping);
    }
}
