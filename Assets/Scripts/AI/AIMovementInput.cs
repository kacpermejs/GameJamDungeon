using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementInput : MonoBehaviour, IMovementInput {
  public Vector2 MovementDirection { get; private set; }

  void Update() {
    MovementDirection = new Vector2(1.0f, 0);
  }
}
