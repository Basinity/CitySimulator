using System;

namespace StateMachine
{
    public class HumanSitBenchState : State
    {
        private readonly HumanFSM humanFSM;
        
        public HumanSitBenchState(HumanFSM humanFSM) : base(humanFSM)
        {
            this.humanFSM = humanFSM;
        }

        public override void OnEnter()
        {
            
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit(Action onExit)
        {
            onExit();
        }
    }
}