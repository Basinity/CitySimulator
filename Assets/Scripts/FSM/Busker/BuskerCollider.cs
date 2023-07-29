using System;
using UnityEngine;

namespace FSM
{
    public class BuskerCollider : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("NavAgent")) return;
            other.gameObject.GetComponent<HumanFSM>().OnCollisionWithTrigger("ListenBusker");
        }
    }
}