namespace FSM
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
            FSM.SwitchState(FSM.states["Walk"]);
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}