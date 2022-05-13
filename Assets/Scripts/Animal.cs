using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the parent class for the dog and all the sheep

public abstract class Animal : MonoBehaviour
{
    private string color;
    public string tagName { get { return gameObject.tag; } }
    private string description;
    private float age;
    private float speed;
    
    private void Start()
    {
        
    }

    
    public virtual void Idle()
    {

    }

    public virtual void Walk()
    {

    }

    public virtual void Jump()
    {

    }

    public virtual void Talk()
    {
        print("Show the animal's sound");
    }

    

}
