using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : Animal
{
    NavMeshAgent m_NavAgent;
    Animator anim;
    private GameObject entrance;
    private AudioSource dogBark;


    // Start is called before the first frame update
    void Start()
    {
        m_NavAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        entrance = GameObject.Find("Entrance");
        dogBark = GetComponent<AudioSource>();
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

        if (GameManager.Instance.pathComplete(GameManager.Instance.mouseHitPos, m_NavAgent) && GameManager.Instance.isGameStart)
        {
            anim.SetInteger("Walk", 0);


            GameManager.Instance.PointToTarget(entrance.transform, transform);
            GameManager.Instance.haveDogArrive = true;

        }
        if ((GameManager.Instance.canBark) && (GameManager.Instance.isMouseClick))
        {
            dogBark.Play(); // play dog bark sound
            Speak();

            GameManager.Instance.canBark = false;
            GameManager.Instance.isMouseClick = false;
        }
    }

    override public void Speak()
    {
        Debug.Log("The dog is barking");
    }



}
