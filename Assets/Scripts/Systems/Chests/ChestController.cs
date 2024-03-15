using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChestState
{
  public bool isLocked;
  public bool isOpen;

  public ChestState(bool isLocked, bool isOpen) {
    this.isLocked = isLocked;
    this.isOpen = isOpen;
  }

}

public class ChestController : MonoBehaviour, IInteractive
{
  [SerializeField]
  private ChestState chestState;

  public UnityEvent<ChestState> onChestStateChanged;

  public bool CanInteract(Actor actor) {
    return !chestState.isLocked;
  }

  public void Interact(Actor actor) {
    if (!chestState.isOpen && !chestState.isLocked) {
      Open();
    } else if (!chestState.isOpen && chestState.isLocked) {
      Unlock();
    } else if (chestState.isOpen && !chestState.isLocked) {
      Lock();
    } else {
      Debug.Log("Forbidden state");
    }
  }

  public void Open() {
    Debug.Log("Chest opened!");
    chestState.isOpen = true;
    onChestStateChanged?.Invoke(chestState);
  }

  public void Lock() {
    chestState.isLocked = true;
    chestState.isOpen = false;
    onChestStateChanged?.Invoke(chestState);
  }

  public void Unlock() {
    chestState.isLocked = false;
    onChestStateChanged?.Invoke(chestState);
  }
}
