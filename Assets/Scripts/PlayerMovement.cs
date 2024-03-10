using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

  public float moveSpeed = 5f;

  public Rigidbody2D rb;

  public Animator animator;

  [SerializeField]
  private InputActionReference move;

  private Vector2 _movement;

  void Update()
  {
    _movement = move.action.ReadValue<Vector2>();

    if (animator)
    {
      animator.SetFloat("Horizontal", _movement.x);
      animator.SetFloat("Vertical", _movement.y);
      animator.SetFloat("Speed", _movement.sqrMagnitude);
    }
  }

  void FixedUpdate()
  {
    rb.MovePosition(rb.position + _movement.normalized * moveSpeed * Time.fixedDeltaTime);
  }

}
