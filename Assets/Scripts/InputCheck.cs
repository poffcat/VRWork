using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputCheck : MonoBehaviour
{
    [Header("UI Elements")]
    public UnityEngine.UI.InputField inputField; // 输入框
    public UnityEngine.UI.Button button0;        // 按钮“0”
    public UnityEngine.UI.Button button1;        // 按钮“1”
    public UnityEngine.UI.Button confirmButton;  // 确定按钮
    public Animator door;

    [Header("Validation Settings")]
    public string correctValue = "10101"; // 正确的二进制值

    void Start()
    {
        // 清空输入框
        inputField.text = "";

        // 绑定按钮点击事件
        button0.onClick.AddListener(() => AppendValue("0"));
        button1.onClick.AddListener(() => AppendValue("1"));
        confirmButton.onClick.AddListener(ValidateInput);
    }

    /// <summary>
    /// 向输入框中追加值
    /// </summary>
    /// <param name="value">追加的值（"0" 或 "1"）</param>
    void AppendValue(string value)
    {
        inputField.text += value;
    }

    /// <summary>
    /// 验证输入框中的值是否正确
    /// </summary>
    void ValidateInput()
    {
        if (inputField.text == correctValue)
        {
            door.SetTrigger("DoorOpen");
            SoundManager.Instance.PlayOneShot(1, 0.3f);
        }
        else
        {
            
        }
    }
}
