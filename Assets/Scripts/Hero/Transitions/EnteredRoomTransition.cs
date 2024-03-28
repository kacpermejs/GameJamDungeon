using Assets.Scripts.Systems.Common.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Hero.Transitions {
  public class EnteredRoomTransition : Transition {
    public EnteredRoomTransition(State from, State to) : base(from, to) {
    }

    public override bool Condition(StateMachine owner) {
      return true;
    }
  }
}
