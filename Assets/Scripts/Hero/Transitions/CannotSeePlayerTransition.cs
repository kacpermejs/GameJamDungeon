using Assets.Scripts.Systems.Common.StateMachine;

namespace Assets.Scripts.Hero.Transitions {
  public class CannotSeePlayerTransition : CanSeePlayerTransition {
    public CannotSeePlayerTransition(State from, State to) : base(from, to) { }

    public override bool Condition(StateMachine owner) { return !base.Condition(owner); }
  }
}