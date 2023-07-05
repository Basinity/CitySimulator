namespace FSM
{
    public class HumanFSM : FiniteStateMachine
    {
        private void Start()
        {
            var HumanWalkState = new HumanWalkState(this);
            var HumanSitBenchState = new HumanSitBenchState(this);
            var HumanListenBuskerState = new HumanListenBuskerState(this);
            
            states.Add("Walk", HumanWalkState);
            states.Add("SitBench", HumanSitBenchState);
            states.Add("ListenBusker", HumanListenBuskerState);

            currentState = HumanWalkState;
            currentState.OnEnter();
        }
    }
}