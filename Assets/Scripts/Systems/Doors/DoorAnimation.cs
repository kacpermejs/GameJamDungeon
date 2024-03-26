using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Systems.Doors {

  [RequireComponent(typeof(Door))]
  public class DoorAnimation: MonoBehaviour {
    private Animator[] _animators;
    private Door _door;

    private void Awake() {
      _animators = GetComponentsInChildren<Animator>();
      _door = GetComponent<Door>();

      UpdateDoorState(_door.CurrentState);
    }

    public void UpdateDoorState(DoorState state) {
      foreach (var animator in _animators) {
        animator.SetInteger("doorState", (int)state);
      }
    }
  }
}
