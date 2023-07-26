using System;
using Object = UnityEngine.Object;

namespace FSM
{
    public class HumanWalkState : State
    {
        private readonly HumanFSM humanFSM;

        public HumanWalkState(HumanFSM humanFSM) : base(humanFSM)
        {
            this.humanFSM = humanFSM;
        }

        public override void OnEnter()
        {
            
        }

        public override void OnUpdate()
        {
            humanFSM.navMeshAgent.destination = humanFSM.destination.position;

            if (humanFSM.transform.position == humanFSM.destination.position)
            {
                Object.Destroy(humanFSM.gameObject);
            }

            if ((humanFSM.destination.position - humanFSM.transform.position).sqrMagnitude < 2f)
            {
                Object.Destroy(humanFSM.gameObject);
            }
        }

        public override void OnExit(Action onExit)
        {
            onExit();
        }
    }
}