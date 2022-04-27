using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector3 entrancePos = new Vector3(-6.9f, 8.5f, -16.1f);
    private float grassLimit = 11.0f;

    private GameObject dog;
    [SerializeField]
    private GameObject[] sheep;

    private float rotAngleInBoundary = 180f;

    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("Dog1");
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateInBoundary(dog);

        for (int i = 0; i < sheep.Length; i++)
        {
            RotateInBoundary(sheep[i]);
        }
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
}
