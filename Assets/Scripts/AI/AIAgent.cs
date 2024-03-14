using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIAgent : MonoBehaviour {

  [SerializeField]
  private Transform _target;

  private NavMeshAgent agent;

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
