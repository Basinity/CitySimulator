using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public abstract class FiniteStateMachine : MonoBehaviour
    {
        protected State currentState;
        public readonly Dictionary<string, State> states = new();

        protected virtual void Update()
        {
            currentState?.OnUpdate();
        }

        public void SwitchState(State newState)
        {
            currentState.OnExit();
            currentState = newState;
            currentState.OnEnter();
        }
    }
}
