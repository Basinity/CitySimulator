using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class FiniteStateMachine : MonoBehaviour
    {
        public string State; // To see current State in Editor
        public State currentState;
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
