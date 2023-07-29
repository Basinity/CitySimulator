using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FSM
{
    public class HumanListenBuskerState : State
    {
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
            targetPosition = new Vector3(buskerPosition.x + Random.Range(-5, 0), 0, buskerPosition.z + Random.Range(-5, 5));
        }

        public override void OnUpdate()
        {
            humanFSM.navMeshAgent.destination = targetPosition;
            humanFSM.transform.LookAt(AIManager.Instance.Busker.position);
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