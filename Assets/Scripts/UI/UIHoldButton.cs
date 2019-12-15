using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class UIHoldButton : UIBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool m_isPointerDown = false;
    private Action m_onHoldStart, m_onHoldEnd;
    public bool IsPointerDown { get { return m_isPointerDown; } }
    public void RegisterEvent(Action onHoldStart, Action onHoldEnd)
    {
        m_onHoldStart = onHoldStart;
        m_onHoldEnd = onHoldEnd;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        m_isPointerDown = true;
        if (m_onHoldStart != null)
            m_onHoldStart();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_isPointerDown = false;
        if (m_onHoldEnd != null)
            m_onHoldEnd();
    }
}
