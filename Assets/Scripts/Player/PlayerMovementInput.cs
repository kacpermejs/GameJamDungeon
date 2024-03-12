using UnityEngine;
using UnityEngine.InputSystem;

class PlayerMovementInput : MonoBehaviour, IMovementInput {

  [SerializeField]
  private InputActionReference move;
  public Vector2 MovementDirection { get; private set; }

  void Update() {
    MovementDirection = move.action.ReadValue<Vector2>();
  }


}

