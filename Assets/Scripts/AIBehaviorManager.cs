using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviorManager : MonoBehaviour {

    //Behaviors
    public FleeingBehavior m_FleeingBehavior;
    public AttackingBehavior m_AttackingBehavior;
    public PatrollingBehavior m_PatrollingBehavior;

    public GameObject MeanBoiTester;
    public Transform m_Player;

    public float m_FleeDistance = 4f;
    public float m_AttackDistance = 8f;
    // Use this for initialization
    void Start ()
    {
        MeanBoiTester.GetComponent<MeanBoi>().SetBehavior(m_PatrollingBehavior);
        Debug.Log("We Did It!");
	}
	
	// Update is called once per frame
	void Update ()
    {
        MeanBoiTester.GetComponent<MeanBoi>().ControlledUpdate();

        if (Vector3.Distance(MeanBoiTester.transform.position, m_Player.position) <= m_FleeDistance)
        {
            MeanBoiTester.GetComponent<MeanBoi>().SetBehavior(m_FleeingBehavior);
        }
        else if (Vector3.Distance(MeanBoiTester.transform.position, m_Player.position) <= m_AttackDistance)
        {
            MeanBoiTester.GetComponent<MeanBoi>().SetBehavior(m_AttackingBehavior);
        }
        else
        {
            MeanBoiTester.GetComponent<MeanBoi>().SetBehavior(m_PatrollingBehavior);
        }
    }
}

public abstract class EvilBehavior
{
    //first thing MeanBoi does when he starts this behavior
    public virtual void EnteringBehavior()
    {
        Debug.Log("Entering a behavior");
    }

    public virtual void UpdatingBehavior(Transform meanBoiTransform)
    {
        Debug.Log("Updating a behavior");
    }

    public virtual void ExitingBehavior()
    {
        Debug.Log("Exiting a behavior");
    }
}

[System.Serializable]
public class AttackingBehavior : EvilBehavior
{
    public Transform m_Player;
    public float m_Speed = .25f;

    public override void EnteringBehavior()
    {
        base.EnteringBehavior();
    }

    public override void UpdatingBehavior(Transform meanBoiTransform)
    {
        meanBoiTransform.LookAt(m_Player);

        Vector3 desiredPos = Vector3.Lerp(meanBoiTransform.position, m_Player.position, Time.deltaTime * m_Speed);
        desiredPos = new Vector3(desiredPos.x, m_Player.position.y, desiredPos.z);

        meanBoiTransform.position = desiredPos;
    }
}

[System.Serializable]
public class PatrollingBehavior : EvilBehavior
{
    public Transform[] m_PatrolPoints;
    public Transform m_Player;
    public float m_Speed = .25f;

    private int m_CurrentDesiredIndexPoint = 0;

    public override void EnteringBehavior()
    {
        base.EnteringBehavior();
    }

    public override void UpdatingBehavior(Transform meanBoiTransform)
    {
        if (Vector3.Distance(meanBoiTransform.position, m_PatrolPoints[m_CurrentDesiredIndexPoint].position) <= 2.5)
        {
            m_CurrentDesiredIndexPoint++;

            if (m_CurrentDesiredIndexPoint >= m_PatrolPoints.Length)
            {
                m_CurrentDesiredIndexPoint = 0;
            }
        }

        meanBoiTransform.LookAt(m_PatrolPoints[m_CurrentDesiredIndexPoint]);

        Vector3 desiredPosChange = meanBoiTransform.forward * Time.deltaTime * m_Speed;
        desiredPosChange = new Vector3(desiredPosChange.x, 0, desiredPosChange.z);

        meanBoiTransform.position += desiredPosChange;
    }
}

[System.Serializable]
public class FleeingBehavior : EvilBehavior
{
    public Transform m_Player;
    public float m_Speed = .25f;

    public override void EnteringBehavior()
    {
        base.EnteringBehavior();
    }

    public override void UpdatingBehavior(Transform meanBoiTransform)
    {

        Vector3 desiredPos = Vector3.Lerp(meanBoiTransform.position, meanBoiTransform.position - (m_Player.position - meanBoiTransform.position), Time.deltaTime * m_Speed);
        desiredPos = new Vector3(desiredPos.x, m_Player.position.y, desiredPos.z);
        Debug.Log("Fleeing");

        meanBoiTransform.LookAt(desiredPos);


        meanBoiTransform.position = desiredPos;
    }
}
