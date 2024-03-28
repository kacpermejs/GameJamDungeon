using Assets.Scripts.Hero;
using Assets.Scripts.Systems.Common.StateMachine;
using UnityEngine;

public class FightState : State {
  private readonly float circleCastRadius = 20f;

  public override void OnEnter(StateMachine owner) {
    base.OnEnter(owner);
    var aiAgent = owner.GetComponent<AIAgent>();
    // Perform a circle cast to detect nearby enemy objects
    Collider2D[] colliders = Physics2D.OverlapCircleAll(aiAgent.transform.position, circleCastRadius);

    // Find the closest enemy
    Transform closestEnemy = null;
    float closestDistance = Mathf.Infinity;
    foreach (Collider2D collider in colliders) {
      // Check if the collider belongs to an enemy
      if (collider.CompareTag("Player")) {
        float distance = Vector2.Distance(aiAgent.transform.position, collider.transform.position);
        if (distance < closestDistance) {
          closestDistance = distance;
          closestEnemy = collider.transform;
        }
      }
    }

    if (closestEnemy != null) {
      // Set the target of the AI agent to the transform of the closest enemy
      aiAgent.SetTarget(closestEnemy);
    } else {

    }
  }

  public override void OnExit(StateMachine owner) {
    base.OnExit(owner);

    owner.GetComponent<AIAgent>().SetTarget(null);
  }
}
