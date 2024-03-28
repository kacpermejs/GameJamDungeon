using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Hero {
  public class AIActor : MonoBehaviour, IActor {
    [SerializeField]
    private IInteractive _target;

    public bool TryInteract() {
      if (_target == null) {
        return false;
      }

      if (!_target.CanInteract(this)) {
        return false;
      }

      _target.Interact(this);
      return true;
    }
  }
}