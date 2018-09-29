using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrAttacky : MonoBehaviour {
    public float m_InteractSpeed = 5;
    public float m_PatrolSpeed = 1;
    public float m_fleeRadius = 5f;
    public float m_attackRadius = 2f;

    public GameObject m_NavPointsHolder;
    public GameObject m_Player;

    private GameObject m_CurrentTarget;
    private int m_CurrentChild = 0;
    private float m_DistanceToPoint = 0.5f;
    private bool m_Interact = false;
    private bool m_Flee = false;

    void Start()
    {
        m_CurrentTarget = m_NavPointsHolder.transform.GetChild(m_CurrentChild).gameObject;
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, m_Player.transform.position) < m_fleeRadius)
        {
            m_Interact = true;
            if (Vector3.Distance(this.transform.position, m_Player.transform.position) < m_attackRadius)
            {
                transform.position = Vector3.Lerp(transform.position, m_Player.transform.position, m_InteractSpeed * Time.deltaTime);
                Debug.Log("Chasing");
                
            }
            else
            {
                Debug.Log("Fleeing");

                this.gameObject.transform.Rotate(0, 180, 0);
                this.transform.position += this.gameObject.transform.forward * Time.deltaTime * m_InteractSpeed;
            }

        }
        else
        {
            m_Interact = false;
        }
        if (!m_Interact)
        {
            Debug.Log("Patrolling");
            if (Vector3.Distance(this.transform.position, m_CurrentTarget.transform.position) > m_DistanceToPoint)
            {
                transform.position = Vector3.Lerp(transform.position, m_CurrentTarget.transform.position, m_PatrolSpeed * Time.deltaTime);
            }
            else
            {
                if (m_CurrentChild < m_NavPointsHolder.transform.childCount - 1)
                {
                    m_CurrentChild++;
                    m_CurrentTarget = m_NavPointsHolder.transform.GetChild(m_CurrentChild).gameObject;
                }
                else
                {
                    m_CurrentChild = 0;
                    m_CurrentTarget = m_NavPointsHolder.transform.GetChild(m_CurrentChild).gameObject;
                }
            }
        }
    }
}
