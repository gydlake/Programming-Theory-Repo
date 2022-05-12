using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// close the fence gate after all sheep go inside

public class CloseGate : MonoBehaviour
{
    GameObject fenceGate;
    Vector3 targetPos;
    private float speed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        fenceGate = GameObject.Find("Fence Gate");
        targetPos = new Vector3(1.24f, fenceGate.transform.position.y, fenceGate.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGateClosing)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            fenceGate.transform.position = Vector3.MoveTowards(fenceGate.transform.position, targetPos, step);
        }

        if (Vector3.Distance(fenceGate.transform.position, targetPos) < 0.1f)
        {
            GameManager.Instance.isGateClosed = true;
        }

    }

    private void OnMouseDown()
    {
        Debug.Log("gate");
        GameManager.Instance.isGateClosing = true;

    }
}
