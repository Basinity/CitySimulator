using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StateMachine
{
    public class HumanSitBenchState : State
    {
        private readonly HumanFSM humanFSM;
        private float leavingTime;
        private static readonly int Sit = Animator.StringToHash("Sit");
        private Transform bench;

        public HumanSitBenchState(HumanFSM humanFSM) : base(humanFSM)
        {
            this.humanFSM = humanFSM;
        }

        public override void OnEnter()
        {
            leavingTime = Random.Range(5, 10);
            var availableBenches = new List<Transform>();
            for (var i = 0; i < AIManager.Instance.Benches.Length; i++)
            {
                var transform = AIManager.Instance.Benches[i];
                if (!AIManager.Instance.BenchesTaken[i])
                {
                    availableBenches.Add(transform);
                }
            }

            if (availableBenches.Count == 0)
            {
                FSM.SwitchState(humanFSM.states["Walk"]);
                return;
            }

            bench = availableBenches[Random.Range(0, availableBenches.Count)];
            AIManager.Instance.BenchesTaken[Array.IndexOf(AIManager.Instance.Benches, bench)] = true;
        }

        public override void OnUpdate()
        {
            if (bench == null) return;
            humanFSM.navMeshAgent.destination = bench.position;
            
            if ((bench.position - humanFSM.transform.position).sqrMagnitude < 2f)
            {
                humanFSM.transform.rotation = bench.rotation;
                humanFSM.Animator.SetBool(Sit, true);
                
                leavingTime -= Time.deltaTime;

                if (leavingTime <= 0)
                {
                    FSM.SwitchState(humanFSM.states["Walk"]);
                }
            }
            
            if (WeatherManager.Instance.isRaining) FSM.SwitchState(humanFSM.states["Walk"]);
        }

        public override void OnExit(Action onExit)
        {
            if (bench != null) AIManager.Instance.BenchesTaken[Array.IndexOf(AIManager.Instance.Benches, bench)] = false;
            humanFSM.Animator.SetBool(Sit, false);
            onExit();
        }
    }
}