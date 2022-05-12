using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game manager to transfer control data between different files.

public class GameManager : MonoBehaviour
{
    #region Varialbes:  
    public static GameManager Instance { get; private set; }

    private float titleScreenDisplayTime = 2f;
    private GameObject titleObj;
    
    public Vector3 mouseHitPos;
    public bool haveMousePos;
    public bool haveDogArrive;
    public bool isGameStart;
    public bool isGateClosing;
    public bool canBark; // when the dog arrives the mouse clicked position near a sheep.
    public bool isMouseClick;
    public bool isGateClosed;

    private GameObject endObj; // the object for end text.
    private float speed = 200.0f; // the moving up speed of the end text.
    private Vector3 targetPos; // the target position of the end text.

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        titleObj = GameObject.Find("Title");
        endObj = GameObject.Find("EndText");
        endObj.transform.position = new Vector3(endObj.transform.position.x, -50f, endObj.transform.position.z);
        targetPos = new Vector3(endObj.transform.position.x, 300f, endObj.transform.position.z);
        
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
    }

    public bool pathComplete(Vector3 pos1, UnityEngine.AI.NavMeshAgent m_NavAgent)
    {
        if (Vector3.Distance(pos1, m_NavAgent.transform.position) <= 0.5f)
        {
            return true;
        }

        return false;
    }

    // the animal rotates to face to target
    public void PointToTarget(Transform target, Transform transform)
    {
        Vector3 relativePos = target.position - transform.position;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1f * Time.deltaTime);
    }

    IEnumerator DisableTitleScreen() //hide the title text
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
