using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwingController : MonoBehaviour
{
    [SerializeField]
    AimController m_AimController = null;
    [SerializeField]
    PowerController m_PowerController = null;
    [SerializeField]
    Button m_StopButton = null;
    [SerializeField]
    Button m_SwingButton = null;
    [SerializeField]
    Button m_ResetButton = null;
    [SerializeField]
    Transform m_Ball = null;
    [SerializeField]
    Transform m_Hole = null;

    [SerializeField]
    PutterState m_PutterState = PutterState.None;
    [SerializeField]
    float m_MaxAngle = 10.0f;
    [SerializeField]
    float m_ForceScaler = 10.0f;

    Vector3 m_OriginalBallPosition = Vector3.zero;
    void Awake()
    {
        m_OriginalBallPosition = m_Ball.transform.position;

        m_ResetButton.onClick.AddListener(() =>
        {
            m_PutterState = PutterState.Swing;

            m_AimController.ResetSlider();
            m_PowerController.ResetSlider();

            m_Ball.transform.position = m_OriginalBallPosition;
            m_Ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            m_Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        });

        m_StopButton.onClick.AddListener(() =>
        {
            m_PutterState++;
        });

        m_SwingButton.onClick.AddListener(() =>
        {
            m_PutterState = PutterState.None;

            Vector3 lookDirection = m_Hole.transform.position - m_Ball.transform.position;

            Quaternion currentRotation = Quaternion.AngleAxis(m_AimController.slider.value * m_MaxAngle, Vector3.up);
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

            Vector3 lockDirection = currentRotation * lookDirection;

            Quaternion rotation = currentRotation * lookRotation;

            Quaternion forceRotation = Quaternion.AngleAxis(m_AimController.slider.value * m_MaxAngle, Vector3.up) * rotation;
            Vector3 forceDirection = forceRotation * Vector3.forward;

            Vector3 force = forceDirection * (m_PowerController.slider.value * m_ForceScaler);
            m_Ball.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
        });
    }
    
    void Update()
    {
        /*switch (m_PutterState)
        {
            case PutterState.None:
                if(m_StopButton.interactable ) m_StopButton.interactable  = false;
                if(m_SwingButton.interactable ) m_SwingButton.interactable  = false;
                if(m_ResetButton.interactable ) m_ResetButton.interactable  = false;

                if (m_AimController.slider.interactable)  m_AimController.slider.interactable  = false;
                if (m_PowerController.slider.interactable) m_PowerController.slider.interactable  = false;                             
                break;
            case PutterState.Aim:
                if (!m_StopButton.interactable) m_StopButton.interactable = true;
                if (m_SwingButton.interactable ) m_SwingButton.interactable  = false;
                if (m_ResetButton.interactable ) m_ResetButton.interactable  = true;

                if (!m_AimController.slider.interactable) m_AimController.slider.interactable = true;
                if (m_PowerController.slider.interactable) m_PowerController.slider.interactable = false;
                break;
            case PutterState.Power:
                if (!m_StopButton.interactable) m_StopButton.interactable = true;
                if (m_SwingButton.interactable) m_SwingButton.interactable  = false;
                if (m_ResetButton.interactable ) m_ResetButton.interactable  = true;

                if (m_AimController.slider.interactable) m_AimController.slider.interactable = false;
                if (!m_PowerController.slider.interactable) m_PowerController.slider.interactable = true;
                break;
            case PutterState.Swing:
                if (m_StopButton.interactable) m_StopButton.interactable = false;
                if (!m_SwingButton.interactable ) m_SwingButton.interactable  = true;
                if (!m_ResetButton.interactable ) m_ResetButton.interactable  = true;

                if (m_AimController.slider.interactable) m_AimController.slider.interactable = false;
                if (m_PowerController.slider.interactable) m_PowerController.slider.interactable = false;
                break;
            default:
                break;
        }*/
    }
}
