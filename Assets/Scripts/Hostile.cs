using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : MonoBehaviour {

    public float m_Speed = 5f;
    
    
    public GameObject m_NavPointsHolder;
    public GameObject m_Player;

    private GameObject m_CurrentTarget;

    private float m_FleeRange = 5f;
    private float m_DistanceToPoint = 0.5f;
    private bool m_InteractingWithPlayer = false;
    private bool m_Fleeing = false;


    private int m_CurrentChild = 0;

    // Use this for initialization
    void Start () {
        m_CurrentTarget = m_NavPointsHolder.transform.GetChild(m_CurrentChild).gameObject;

	}
	
	// Update is called once per frame
	void Update () {
        if (!m_InteractingWithPlayer)
        {
            if (Vector3.Distance(this.transform.position, m_CurrentTarget.transform.position) > m_DistanceToPoint)
            {
                transform.position = Vector3.Lerp(transform.position, m_CurrentTarget.transform.position, m_Speed * Time.deltaTime);
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

    private void OnTriggerStay(Collider other)
    {
        m_InteractingWithPlayer = true;
        
        if(other.gameObject.tag == "Player")
        {
            if (Vector3.Distance(this.transform.position, other.gameObject.transform.position) > m_FleeRange)
            {
                //Aggro
                transform.position = Vector3.Lerp(transform.position, other.gameObject.transform.position, m_Speed * Time.deltaTime);
            }
            else
            {
                //Flee
                this.gameObject.transform.LookAt(other.gameObject.transform);
                this.gameObject.transform.Rotate(0, 180, 0);
                this.transform.position += this.gameObject.transform.forward * Time.deltaTime * m_Speed * 10;
                m_FleeRange = 30f;
                
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_FleeRange = 3f;
            m_InteractingWithPlayer = false;
        }
    }

}
