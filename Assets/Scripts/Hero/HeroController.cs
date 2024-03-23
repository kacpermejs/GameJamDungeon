using Assets.Scripts.Systems.Common.StateMachine;
using UnityEngine;

public class HeroController : StateMachine {
  private AIAgent _aiAgent; // Reference to the AIAgent component

  private PatrolState _patrolState;
  private FightState _fightState;

  private void Awake() {

    _patrolState = new PatrolState();
    _fightState = new FightState();

    RegisterTransition(new CanSeePlayerTransition(_patrolState, _fightState));
    RegisterTransition(new CannotSeePlayerTransition(_fightState, _patrolState));

    Initialize(_patrolState);

    // Get the AIAgent component attached to the same GameObject
    _aiAgent = GetComponent<AIAgent>();
  }

  public void SetTarget(Transform transform) {
    _aiAgent.SetTarget(transform);
  }

}

