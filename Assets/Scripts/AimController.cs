using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimController : MonoBehaviour
{

    [SerializeField]
    float m_Speed = 1.0f;

    Slider m_Slider = null;

    float m_Time = 0.0f;

    public Slider slider { get => m_Slider; }
    void Awake()
    {
        m_Slider = GetComponent<Slider>();
        m_Time = m_Slider.maxValue;
    }

    public void ResetSlider()
    {
        m_Time = m_Slider.maxValue;
        m_Slider.value = 0.0f;
    }

    void Update()
    {
        if(m_Slider.interactable)
        {
            m_Time += Time.deltaTime;
            float value = Mathf.PingPong(m_Time * m_Speed, m_Slider.maxValue * 2.0f) - m_Slider.maxValue;
            m_Slider.value = value;
        }
    }
}
