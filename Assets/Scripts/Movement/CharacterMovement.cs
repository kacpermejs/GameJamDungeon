using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(IMovementInput))]
public class CharacterMovement : MonoBehaviour {

  public float moveSpeed = 5f;

  public Rigidbody2D rb;

  public Animator animator;

  private Vector2 _movement;

  private IMovementInput _input;

  void Awake() {
    _input = GetComponent<IMovementInput>();
  }

  void Update() {
    _movement = _input.MovementDirection;

    if (animator) {
      animator.SetFloat("Horizontal", _movement.x);
      animator.SetFloat("Vertical", _movement.y);
      animator.SetFloat("Speed", _movement.sqrMagnitude);
    }
  }

  void FixedUpdate() {
    rb.MovePosition(rb.position + _movement.normalized * moveSpeed * Time.fixedDeltaTime);
  }

}
