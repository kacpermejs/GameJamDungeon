using System.Collections.Generic;

namespace Assets.Scripts.Systems.Common.StateMachine {
  public abstract class State {

    private List<Transition> _transitions = new List<Transition>();

    public virtual void OnEnter(StateMachine owner) { }
    public virtual void OnUpdate(StateMachine owner) {

      CheckTransition(owner, out State nextState);
      if (nextState != null) {
        owner.TransitionTo(nextState);
        return;
      }
    }
    public virtual void OnExit(StateMachine owner) { }

    private bool CheckTransition(StateMachine owner, out State nextState) {
      foreach (var transition in _transitions) {
        if (transition.Condition(owner)) {
          nextState = transition.Destination;
          return true;
        }
      }
      nextState = null;
      return false;
    }

    public void AttatchTransition(Transition transition) {
      _transitions.Add(transition);
    }
  }
}
