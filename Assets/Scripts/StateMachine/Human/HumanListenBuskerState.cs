using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StateMachine
{
    public class HumanListenBuskerState : State
    {
        private float radius;
        private readonly HumanFSM humanFSM;
        private float leavingTime;
        private Vector3 targetPosition;
        
        public HumanListenBuskerState(HumanFSM humanFSM) : base(humanFSM)
        {
            this.humanFSM = humanFSM;
        }
        
        public override void OnEnter()
        {
            leavingTime = Random.Range(20, 40);
            var buskerPosition = AIManager.Instance.Busker.position;
            radius = 4f;
            var angle = Random.Range(110f, 250f);
            targetPosition = new Vector3(radius * Mathf.Cos(angle) + buskerPosition.x, 0, radius * Mathf.Sin(angle) + buskerPosition.z);
        }

        public override void OnUpdate()
        {
            humanFSM.navMeshAgent.destination = targetPosition;
            humanFSM.transform.LookAt(new Vector3(AIManager.Instance.Busker.position.x, humanFSM.transform.position.y, AIManager.Instance.Busker.position.z));
            leavingTime -= Time.deltaTime;

            if (leavingTime <= 0)
            {
                FSM.SwitchState(humanFSM.states["Walk"]);
            }
        }

        public override void OnExit(Action onExit)
        {
            onExit();
        }
    }
}