using UnityEngine;
using UnityEngine.Events;

public abstract class InteractiveBehaviour : MonoBehaviour, IInteractive {

  public UnityEvent<IActor> onSuccessfulInteraction;
  public UnityEvent<IActor> onUnsuccessfulInteraction;

  public abstract bool CanInteract(IActor actor);

  protected abstract void ActionSuccess(IActor actor);
  protected abstract void ActionFailure(IActor actor);

  public virtual void Interact(IActor actor) {
    if (!CanInteract(actor)) {
      Debug.Log("Fail");
      ActionFailure(actor);
      onUnsuccessfulInteraction.Invoke(actor);
      return;
    }
    Debug.Log("Success");
    ActionSuccess(actor);
    onSuccessfulInteraction.Invoke(actor);
  }
}
