using System.Collections;
using UnityEngine;

public class SheepController : Animal
{
    // Leg and body object variables
    private GameObject FrontLegL;
    private GameObject FrontLegR;
    private GameObject RearLegL;
    private GameObject RearLegR;

    // Leg and body rotation variables
    private Vector3 legStartPosA = new Vector3(10.0f, 0f, 0f);
    private Vector3 legEndPosA = new Vector3(-10.0f, 0f, 0f);

    private Vector3 legStartPosB = new Vector3(-10.0f, 0f, 0f);
    private Vector3 legEndPosB = new Vector3(10.0f, 0f, 0f);

    private float rotSpeed;

    // Wander variables.
    private float movSpeed = 1f; // Define speed that animal moves. This is also used to calculate leg movement speed.
    private float grassLimit = 11f;

    //private bool canRotate = true;
    private GameObject dog;
    UnityEngine.AI.NavMeshAgent m_NavAgent;
    private GameObject fenceCentre;
    private bool haveSheepArrive;
    private bool isWander;
    private AudioSource sheepSound;
    private bool canSpeak;

    // sheep related control

    private void Start()
    {
        // Ensure child objects of legs are named EPA_FL, EPA_FR, EPA_RL and EPA_RR so the searches below can assign them to leg variables.
        FrontLegL = transform.Find("BaseAnimal").transform.Find("Legs").transform.Find("EPA_FL").gameObject;    // Find child object for front left leg.
        FrontLegR = transform.Find("BaseAnimal").transform.Find("Legs").transform.Find("EPA_FR").gameObject;    // Find child object for front right leg.
        RearLegL = transform.Find("BaseAnimal").transform.Find("Legs").transform.Find("EPA_RL").gameObject;    // Find child object for rear left leg.
        RearLegR = transform.Find("BaseAnimal").transform.Find("Legs").transform.Find("EPA_RR").gameObject;    // Find child object for rear right leg.

        rotSpeed = movSpeed * 4; // Set legs to move relative to animal moving speed.

        dog = GameObject.Find("Dog1");
        m_NavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        fenceCentre = GameObject.Find("Fence Centre");
        haveSheepArrive = false;
        isWander = true;
        sheepSound = GetComponent<AudioSource>();
        canSpeak = true;

    }

    private void Update()
    {
        if (!haveSheepArrive)
        {
            SheepLegMovement();
        }


        // Wander
        if (isWander)
        {
            movSpeed = 1f;
            transform.Translate((Vector3.forward * Time.deltaTime) * movSpeed);
            if (Mathf.Abs(transform.position.x) > grassLimit || Mathf.Abs(transform.position.z) > grassLimit
                || (transform.position.x > 0 && transform.position.z > -3))
            {
                transform.Rotate(Vector3.up, 90f);
            }

        }


        if (Vector3.Distance(fenceCentre.transform.position, m_NavAgent.transform.position) < 2f)
        {
            haveSheepArrive = true;
            m_NavAgent.updateRotation = false;
            m_NavAgent.speed = 0f;
            rotSpeed = 0;

        }

        if (!haveSheepArrive)
        {
            StartCoroutine(MoveToEntrance());
        }
    }

    IEnumerator MoveToEntrance()
    {
        if (GameManager.Instance.haveDogArrive)
        {

            if (Vector3.Distance(dog.transform.position, transform.position) < 2f)
            {
                GameManager.Instance.canBark = true;

                yield return new WaitForSeconds(0.5f);

                isWander = false;
                sheepSound.Play();

                if (canSpeak)
                {
                    Talk();
                    canSpeak = false;
                }


                m_NavAgent.SetDestination(fenceCentre.transform.position);
                m_NavAgent.speed = 2f;



            }
        }
    }

    // sheep animation
    private void SheepLegMovement()
    {
        Quaternion legAngleFromA = Quaternion.Euler(this.legStartPosA);         // Set first start angle of leg.
        Quaternion legAngleToA = Quaternion.Euler(this.legEndPosA);             // Set first end angle of leg.

        Quaternion legAngleFromB = Quaternion.Euler(this.legStartPosB);         // Set second start angle of leg.
        Quaternion legAngleToB = Quaternion.Euler(this.legEndPosB);             // Set second end angle of leg.

        float lerp = 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * this.rotSpeed));

        FrontLegL.transform.localRotation = Quaternion.Lerp(legAngleFromA, legAngleToA, lerp);
        FrontLegR.transform.localRotation = Quaternion.Lerp(legAngleFromB, legAngleToB, lerp);

        RearLegL.transform.localRotation = Quaternion.Lerp(legAngleFromB, legAngleToB, lerp);
        RearLegR.transform.localRotation = Quaternion.Lerp(legAngleFromA, legAngleToA, lerp);
    }

    override public void Talk()
    {
        Debug.Log("The " + tagName + " is baaing");
    }


}

