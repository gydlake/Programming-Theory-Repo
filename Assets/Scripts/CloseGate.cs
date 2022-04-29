using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGate : MonoBehaviour
{
    GameObject fenceGate;
    Vector3 targetPos;
    private float speed = 1.0f;
    private bool isClosing;

    // Start is called before the first frame update
    void Start()
    {
        isClosing = false;
        fenceGate = GameObject.Find("Fence Gate");
        targetPos = new Vector3(1.24f, fenceGate.transform.position.y, fenceGate.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isClosing)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            fenceGate.transform.position = Vector3.MoveTowards(fenceGate.transform.position, targetPos, step);
        }
        
    }

    private void OnMouseDown()
    {
        Debug.Log("gate");
        isClosing = true;
        
    }
}