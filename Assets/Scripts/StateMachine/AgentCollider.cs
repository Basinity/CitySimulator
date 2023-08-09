using UnityEngine;

namespace StateMachine
{
    public class AgentCollider : MonoBehaviour
    {
        [SerializeField] private string agentTag;
        [SerializeField] private string stateCollision;
        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(agentTag)) return;
            other.gameObject.GetComponent<HumanFSM>().OnCollisionWithTrigger(stateCollision);
        }
    }
}