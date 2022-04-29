using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    private string color;
    private string givenName;
    private string description;
    private float age;
    private float speed;

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

    }

}
