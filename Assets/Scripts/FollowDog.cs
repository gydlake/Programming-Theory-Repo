using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// let camera follow the dog

public class FollowDog : Animal
{
    Vector3 CameraDogOffset = new Vector3(-5.5f, 8.7f, -15.5f);

    private GameObject dog;

    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("Dog1");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = dog.transform.position + CameraDogOffset;
    }
}
