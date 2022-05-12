using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Varialbes:  
    //Vector3 entrancePos = new Vector3(-6.9f, 8.68f, -16.65f);
    GameObject entrance;
    private float grassLimit = 11.0f;

    private GameObject dog;
    [SerializeField]
    private GameObject[] sheep;

    private float rotAngleInBoundary = 180f;
    private float titleScreenDisplayTime = 2f;
    private GameObject titleObj;
    public static GameManager Instance { get; private set; }
    public GameObject selectedSheep;
    public bool beSheepSelected;
    public Vector3 mouseHitPos;
    public bool haveMousePos;
    public bool haveDogArrive;
    public bool isGameStart;
    public bool isGateClosing;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // End of new code
        Instance = this;

        entrance = GameObject.Find("Entrance");
        dog = GameObject.Find("Dog1");
        titleObj = GameObject.Find("Title");

        //Debug.Log("sheep 1 pos: " + sheep[1].transform.position);

        beSheepSelected = false;
        haveMousePos = false;
        haveDogArrive = false;
        isGameStart = false;
        isGateClosing = false;

        StartCoroutine(DisableTitleScreen());
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateInBoundary(dog);

        for (int i = 0; i < sheep.Length; i++)
        {
            RotateInBoundary(sheep[i]);
        }

        //Debug.Log(beSheepSelected);
        if (beSheepSelected== true)
        {
            PointToTarget(dog, selectedSheep);
            //beSheepSelected=false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isGameStart=true;
            mouseHitPos = GetMousePos();
            //Debug.Log("mouseHit: "+ mouseHitPos);
            if (!isGateClosing)
            {
                haveMousePos = true;
            }else
            {
                haveMousePos=false;
            }
            
        }

        // check nearby sheep
        //if (haveDogArrive)
        //{
         //   SheepNearby(dog.transform.position, 1f);
          //  haveDogArrive=false;
        //}
    }

    public bool pathComplete(Vector3 pos1, UnityEngine.AI.NavMeshAgent m_NavAgent)
    {

        //if (Vector3.Distance(GameManager.Instance.mouseHitPos, m_NavAgent.transform.position) <= 0.2f)
        if (Vector3.Distance(pos1, m_NavAgent.transform.position) <= 0.5f)
        {
            //if (!m_NavAgent.hasPath || m_NavAgent.velocity.sqrMagnitude == 0f)
            //{
            //    return true;
            //}
            return true;
        }

        return false;
    }

    // the animal rotates to face to gate entrance
    public void PointToTarget(Transform target, Transform transform)
    {
        Vector3 relativePos = target.position - transform.position;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1f * Time.deltaTime);
    }

    void SheepNearby(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.name != "Dog1" && 
                hitCollider.transform.parent.gameObject.name != "Fence Frames")
            {
                //Debug.Log("Fence: " + hitCollider.transform.parent.gameObject.name);
                //Debug.Log("test: " + hitCollider.name);
                PointToTarget(hitCollider.transform.parent.gameObject, entrance);
            }
            
        }
    }

    void PointToTarget(GameObject obj, GameObject target)
    {
        Vector3 relativePos = target.transform.position - obj.transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, rotation, 5f*Time.deltaTime);
    }

    private void RotateInBoundary(GameObject obj)
    {
        if (obj.transform.position.x >grassLimit || obj.transform.position.x < -grassLimit
            || obj.transform.position.z > grassLimit || obj.transform.position.z < -grassLimit)
        {
            // Rotate animal.
            obj.transform.rotation *= Quaternion.Euler(0, rotAngleInBoundary, 0);
        }
    }

    IEnumerator DisableTitleScreen()
    {
        yield return new WaitForSeconds(titleScreenDisplayTime);
        titleObj.SetActive(false);

        StartGame();
    }

    private void StartGame()
    {

    }

    Vector3 GetMousePos()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Vector3 mousePosition= Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            mousePosition = ray.GetPoint(distance);
        }
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z += Camera.main.nearClipPlane;
        return mousePosition;
        //mouseHitPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
