using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controlled by the AIBehaviorManager
public class MeanBoi : MonoBehaviour
{
    public Transform m_Player;
    public float m_TimeBetweenSwitches = 4.0f; //in seconds
    private float m_LastSwitchTime = 0.0f;

    private EvilBehavior m_CurrentBehavior;

    private void Start()
    {
        //to make it so on start, a behavior can be set;
        m_LastSwitchTime = -m_TimeBetweenSwitches;
    }

    public void ControlledUpdate()
    {
        if (m_CurrentBehavior != null)
        {
            Debug.Log("Updating");
            m_CurrentBehavior.UpdatingBehavior(transform);
        } 
    }

    public void SetBehavior(EvilBehavior newBehavior)
    {
        if (Time.time - m_LastSwitchTime >= m_TimeBetweenSwitches)
        {
            newBehavior.EnteringBehavior();
            m_CurrentBehavior = newBehavior;

            m_LastSwitchTime = Time.time;
        }
    }
}
