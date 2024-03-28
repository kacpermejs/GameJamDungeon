using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Assets.Scripts.Hero {
  [RequireComponent(typeof(NavMeshAgent))]
  public class AIAgent : MonoBehaviour {

    [SerializeField]
    private Transform _target;

    private NavMeshAgent agent;

    public float remainingDistance { get => agent.remainingDistance; }
    public float stoppingDistance { get => agent.stoppingDistance; }
    public bool HasTarget { get => _target != null; }

    void Awake() {
      agent = GetComponent<NavMeshAgent>();
      agent.updateRotation = false;
      agent.updateUpAxis = false;
    }
    public void SetTarget(Transform target) => _target = target;

    void Update() {
      if (_target != null) {
        agent.SetDestination(_target.position);
      }
    }
  }
}