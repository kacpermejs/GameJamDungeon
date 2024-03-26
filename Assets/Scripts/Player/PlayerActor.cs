using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActor : MonoBehaviour, IActor
{
  [SerializeField]
  protected Collider2D _collider;
  [SerializeField]
  protected ContactFilter2D _filter;

  [SerializeField]
  private InputActionReference interactionActionRef;

  private void OnInteractButtonPressed(InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
      TryInteract();
    }
  }

  private void OnEnable() {
    interactionActionRef.action.Enable();
    interactionActionRef.action.performed += OnInteractButtonPressed;
  }

  private void OnDisable() {
    interactionActionRef.action.Disable();
    interactionActionRef.action.performed -= OnInteractButtonPressed;
  }

  public bool TryInteract() {
    var hits = new Collider2D[5];
    Physics2D.OverlapCollider(_collider, _filter, hits);

    foreach (Collider2D hit in hits) {
      if (hit == null) {
        break;
      }
      // TODO: Choose closest and available

      if (hit.TryGetComponent(out IInteractive interactive)) {
        if (!interactive.CanInteract(this)) {
          Debug.Log("Cannot interact, skipping to next one!");
        }

        interactive.Interact(this);
        return true;
      }
    }

    return false;
  }
}
