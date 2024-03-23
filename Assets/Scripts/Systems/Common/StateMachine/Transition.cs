namespace Assets.Scripts.Systems.Common.StateMachine {
  public abstract class Transition {
    private State _from;
    private State _to;

    public Transition(State from, State to) {
      _from = from;
      _to = to;
    }

    public State Source { get => _from; }
    public State Destination { get => _to; }

    public abstract bool Condition(StateMachine owner);
  }
}
