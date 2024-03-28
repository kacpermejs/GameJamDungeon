using Assets.Scripts.Systems.Common.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Hero.States {

  public interface IMoveState {
    public bool IsMoving { get; }
  }
  public class GoToPrioritizedWaypointState : State, IMoveState {

    private AIAgent _agent;
    private MapMemory _mapMemory;

    private bool _isMoving = false;
    private bool _targetReached = false;
    private NavMeshAgent _navMeshAgent;

    private WayPoint _target;

    public bool IsMoving { get => _isMoving; private set => _isMoving = value; }
    public bool TargetReached { get => _targetReached; private set => _targetReached = value; }

    public override void OnEnter(StateMachine owner) {
      base.OnEnter(owner);
      _agent = owner.GetComponent<AIAgent>();
      _navMeshAgent = owner.GetComponent<NavMeshAgent>();
      _mapMemory = owner.GetComponent<MapMemory>();

      AcquireTarget();
    }

    public override void OnUpdate(StateMachine owner) {
      base.OnUpdate(owner);

      if (!TargetReached && !IsMoving && _navMeshAgent.remainingDistance > 0) {
        _isMoving = true;
      }

      if (!TargetReached && IsMoving && _agent.HasTarget && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) {
        if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f) {
          // Done
          OnTargetReached();
        }
      }
    }

    private void OnTargetReached() {
      IsMoving = false;
      TargetReached = true;
      Debug.Log("Waypoint reached");

      _mapMemory.MarkAsVisited(_target);
      AcquireTarget();

    }

    private void AcquireTarget() {
      _target = _mapMemory.SelectNextWaypoint(_mapMemory.CurrentRoom);
      if (_target == null) {
        TargetReached = true;
        Debug.Log("No viable target left");
      } else {
        _agent.SetTarget(_target.transform);
        TargetReached = false;
      }
    }
  }
}
