using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaFollowHand : MonoBehaviour
{
  
    public Transform rightHand; // 右手控制器或右手的 Transform
    public Vector3 offset = new Vector3(0.1f, 0f, 0.1f); // 相对于右手的偏移
    public Vector3 rotationOffset = new Vector3(0f, 0f, 0f); // 旋转偏移

    void Update()
    {
        if (rightHand != null)
        {
            // 设置 Canvas 的位置
            transform.position = rightHand.position + rightHand.TransformDirection(offset);

            // 设置 Canvas 的旋转
            transform.rotation = rightHand.rotation * Quaternion.Euler(rotationOffset);
        }
    }
}
