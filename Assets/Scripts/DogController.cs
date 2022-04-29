using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent m_NavAgent;
    Animator anim;
    GameObject entrance;

    //public GameObject dogTarget;

    // Start is called before the first frame update
    void Start()
    {
        m_NavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        entrance = GameObject.Find("Entrance");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.Instance.haveMousePos)
        {
            m_NavAgent.SetDestination(GameManager.Instance.mouseHitPos);
            GameManager.Instance.haveMousePos = false;
            anim.SetInteger("Walk", 1);
        }

        if (pathComplete())
        {
            anim.SetInteger("Walk", 0);
            PointToGate();
        }
    }

    void PointToGate()
    {
        Vector3 relativePos = entrance.transform.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1f * Time.deltaTime);
    }

    protected bool pathComplete()
    {
        if (Vector3.Distance(m_NavAgent.destination, m_NavAgent.transform.position) <= m_NavAgent.stoppingDistance)
        {
            if (!m_NavAgent.hasPath || m_NavAgent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }

}
