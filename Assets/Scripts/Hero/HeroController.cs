using UnityEngine;

using Assets.Scripts.Systems.Common.StateMachine;
using Assets.Scripts.Hero.Transitions;
using Assets.Scripts.Hero.States;
using UnityEngine.AI;

namespace Assets.Scripts.Hero {

  [RequireComponent(typeof(MapMemory), typeof(AIAgent))]
  public class HeroController : StateMachine {

    private GoToPrioritizedWaypointState _goToWayPointState;
    private InteractWithWaypointObjectState _interactionState;

    private void Awake() {

      _goToWayPointState = new GoToPrioritizedWaypointState();
      _interactionState = new InteractWithWaypointObjectState();
    }

    private void Start() {
      Initialize(_goToWayPointState);
    }


  }
}