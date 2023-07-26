using System;

namespace FSM
{
    public abstract class State
    {
        protected readonly FiniteStateMachine FSM;

        protected State(FiniteStateMachine finiteStateMachine)
        {
            FSM = finiteStateMachine;
        }

        public abstract void OnEnter();

        public abstract void OnUpdate();

        public abstract void OnExit(Action onExit);
    }
}