using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCollisionHolder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
           GetComponent<SphereCollider>().enabled = false;
           GetComponent<MeshRenderer>().enabled = false;
    }
}
