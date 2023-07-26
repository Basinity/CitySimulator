using UnityEngine;

namespace FSM
{
    public class BuskerCollider : MonoBehaviour
    {
        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("A human collided with me!");
            collision.gameObject.GetComponent<HumanFSM>().OnCilliderWithBuskerTrigger(collision);
        }
    }
}