using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Systems.Common.StateMachine {

  public abstract class StateMachine : MonoBehaviour {

    private State _currentState;
    private List<Transition> _transitions = new List<Transition>();

    protected void Initialize(State state) {
      if (_currentState == null) {
        _currentState = state;
      } else {
        Debug.Log("Wrong initialization");
      }
    }

    protected void RegisterTransition(Transition transition) {
      _transitions.Add(transition);
      transition.Source.AttatchTransition(transition);
    }

    public void TransitionTo(State to) {
      _currentState.OnExit(this);
      _currentState = to;
      _currentState.OnEnter(this);
    }

    #region UnityMethods

    public void Update() {
      _currentState.OnUpdate(this);
    }

    #endregion
  }
}
