using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

enum ChestAnimationState {
  Closed = 0,
  Open = 1,
  Locked = 2
}

[RequireComponent(typeof(ChestController), typeof(Animator))]
public class ChestAnimationController : MonoBehaviour
{
  ChestController controller;
  Animator animator;

  private void Awake() {
    controller = GetComponent<ChestController>();
    animator = GetComponent<Animator>();
  }

  private void OnEnable() {
    controller.onChestStateChanged.AddListener(HandleAnimation);
  }

  private void OnDisable() {
    controller.onChestStateChanged.RemoveListener(HandleAnimation);
  }

  private void HandleAnimation(ChestState chestState) {

    ChestAnimationState state = ChestAnimationState.Closed;

    if (chestState.isLocked) {
      //state = ChestAnimationState.Locked;
      state = ChestAnimationState.Closed;
    } else if (chestState.isOpen) {
      state = ChestAnimationState.Open;
    }

    Debug.Log("Result state: " + state);

    animator.SetInteger("chestState", (int)state);
  }




}
