using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public float highlightSpeed = 0.5f; // 高亮速度

    private Material cardMaterial;
    private Color originalColor;
    private float originalSmoothness;

    void OnEnable()
    {
        // 获取材质
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            cardMaterial = renderer.material;

            // 确保材质支持透明
            SetMaterialToTransparent();

            originalColor = cardMaterial.color;
            originalSmoothness = cardMaterial.HasProperty("_Glossiness") ? cardMaterial.GetFloat("_Glossiness") : 0f;
        }
        else
        {
            Debug.LogError("找不到 Renderer，请确保卡片有材质！");
        }
    }

    void Update()
    {
        if (cardMaterial != null)
        {
            // 使用 PingPong 动态调整光泽和发光效果
            float gloss = Mathf.PingPong(Time.time * highlightSpeed, 1);
            cardMaterial.SetFloat("_Glossiness", Mathf.Lerp(originalSmoothness, 1f, gloss)); // 高光变化
            cardMaterial.SetColor("_EmissionColor", Color.white * gloss); // 发光效果

            // 动态调整透明度
            Color currentColor = cardMaterial.color;
            currentColor.a = Mathf.Lerp(0.5f, 1f, gloss); // 透明度在 0.5 到 1 之间波动
            cardMaterial.color = currentColor;
        }
    }
    
    /// <summary>
    /// 将材质设置为透明模式
    /// </summary>
    private void SetMaterialToTransparent()
    {
        cardMaterial.SetFloat("_Mode", 3); // 设置为 Transparent 模式
        cardMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        cardMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        cardMaterial.SetInt("_ZWrite", 0);
        cardMaterial.DisableKeyword("_ALPHATEST_ON");
        cardMaterial.EnableKeyword("_ALPHABLEND_ON");
        cardMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        cardMaterial.renderQueue = 3000; // 设置渲染队列为透明物体
    }
    public void ResetToDefault()
    {
        if (cardMaterial != null)
        {
            // 恢复颜色和透明度
            Color resetColor = originalColor;
            resetColor.a = 1f; // 设置为完全不透明
            cardMaterial.color = resetColor;

            // 恢复光泽度
            if (cardMaterial.HasProperty("_Glossiness"))
            {
                cardMaterial.SetFloat("_Glossiness", originalSmoothness);
            }

            // 关闭发光效果
            cardMaterial.SetColor("_EmissionColor", Color.black);
        }
    }
}
