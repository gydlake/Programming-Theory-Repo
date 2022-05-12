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
    public bool canBark;
    public bool isMouseClick;
    public bool isGateClosed;
    private GameObject endObj;
    private float speed = 200.0f;
    private Vector3 targetPos;

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
        endObj = GameObject.Find("EndText");
        endObj.transform.position = new Vector3(endObj.transform.position.x, -50f, endObj.transform.position.z);
        targetPos = new Vector3(endObj.transform.position.x, 300f, endObj.transform.position.z);
        //endObj.transform.position = new Vector3(targetPos.x, -200f, targetPos.z);

        Debug.Log(targetPos);

        beSheepSelected = false;
        haveMousePos = false;
        haveDogArrive = false;
        isGameStart = false;
        isGateClosing = false;
        canBark = false;
        isMouseClick = false;
        isGateClosed = false;

        StartCoroutine(DisableTitleScreen());

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isGameStart = true;
            isMouseClick = true;
            canBark = false;

            mouseHitPos = GetMousePos();

            if (!isGateClosing)
            {
                haveMousePos = true;
            }
            else
            {
                haveMousePos = false;
            }

        }

        // after gate close, show the end text.
        if (isGateClosed == true)
        {
            ShowEndText();
        }
    }

    private void ShowEndText()
    {
        // Move end text from bottom to the middle of the screen
        var step = speed * Time.deltaTime; // calculate distance to move
        endObj.transform.position = Vector3.MoveTowards(endObj.transform.position, targetPos, step);
        Debug.Log(endObj.transform.position);
    }

    public bool pathComplete(Vector3 pos1, UnityEngine.AI.NavMeshAgent m_NavAgent)
    {
        if (Vector3.Distance(pos1, m_NavAgent.transform.position) <= 0.5f)
        {
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

    IEnumerator DisableTitleScreen()
    {
        yield return new WaitForSeconds(titleScreenDisplayTime);
        titleObj.SetActive(false);
    }

    Vector3 GetMousePos()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Vector3 mousePosition = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            mousePosition = ray.GetPoint(distance);
        }
        return mousePosition;
    }
}
