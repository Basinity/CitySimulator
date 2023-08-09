using System;

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
            if ((humanFSM.destination.position - humanFSM.transform.position).sqrMagnitude < 2f)
            {
                FSM.SwitchState(humanFSM.states["InBuilding"]);
            }
        }

        public override void OnExit(Action onExit)
        {
            onExit();
        }
    }
}