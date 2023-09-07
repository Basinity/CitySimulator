using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class FiniteStateMachine : MonoBehaviour
    {
        public string State; // Only to see current State in Editor Inspector
        private State currentState;
        public readonly Dictionary<string, State> states = new();

        protected virtual void Update()
        {
            currentState?.OnUpdate();
        }

        public void SwitchState(State newState)
        {
            currentState?.OnExit(newState.OnEnter);
            currentState = newState;
            State = currentState.ToString();
        }
    }
}
