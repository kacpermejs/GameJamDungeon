using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractive {
  public void Interact(Actor actor);
  public bool CanInteract(Actor actor);
}

public class Actor : MonoBehaviour {

  [SerializeField]
  private Collider2D _collider;
  [SerializeField]
  private ContactFilter2D _filter;

  [SerializeField]
  private InputActionReference interactionActionRef;

  private bool TryInteract() {
    var hits = new Collider2D[5];
    Physics2D.OverlapCollider(_collider, _filter, hits);

    foreach (Collider2D hit in hits) {
      if(hit == null) {
        break;
      }
      // TODO: Choose closest and available

      if (hit.TryGetComponent(out IInteractive interactive)) {
        if(!interactive.CanInteract(this)) {
          Debug.Log("Cannot interact, skipping to next one!");
          continue;
        }

        interactive.Interact(this);
        return true;
      }
    }

    return false;
  }

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
}
