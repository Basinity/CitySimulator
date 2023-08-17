using System;
using UnityEngine;

namespace StateMachine
{
    public class HumanWalkState : State
    {
        private readonly HumanFSM humanFSM;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Run = Animator.StringToHash("Run");

        public HumanWalkState(HumanFSM humanFSM) : base(humanFSM)
        {
            this.humanFSM = humanFSM;
        }

        public override void OnEnter()
        {
            humanFSM.Animator.SetBool(Walk, true);
            SetRunningAnimation(WeatherManager.Instance.isRaining);
            WeatherManager.Instance.rainingEvent += SetRunningAnimation;
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
            humanFSM.Animator.SetBool(Walk, false);
            WeatherManager.Instance.rainingEvent -= SetRunningAnimation;
            onExit();
        }

        private void SetRunningAnimation(bool isRaining)
        {
            humanFSM.Animator.SetBool(Run, isRaining);
            humanFSM.navMeshAgent.speed = isRaining ? humanFSM.runningSpeed : humanFSM.walkingSpeed;
        }
    }
}