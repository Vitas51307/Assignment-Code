using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetact : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print("Land on the floor");
    }
    private void OnCollisionStay(Collision collision)
    {
        print("Walking on the floor");
    }
    private void OnCollisionExit(Collision collision)
    {
        print("Leaving the floor");
    }
}
