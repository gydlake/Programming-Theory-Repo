using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDog : MonoBehaviour
{
    Vector3 CameraDogOffset = new Vector3(-6.9f, 8.5f, -16.1f);

    private GameObject dog;

    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("Dog1");
        Debug.Log(dog.transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = dog.transform.position + CameraDogOffset;
    }
}
