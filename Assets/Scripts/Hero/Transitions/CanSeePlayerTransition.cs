using Assets.Scripts.Systems.Common.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Hero.Transitions {

  public class CanSeePlayerTransition : Transition {
    public CanSeePlayerTransition(State from, State to) : base(from, to) { }

    public override bool Condition(StateMachine owner) {
      return CanSeePlayer(owner);
    }

    private bool CanSeePlayer(StateMachine owner) {
      // Set the radius of the circle cast
      float radius = 5f;

      // Set the maximum distance for the circle cast
      float maxDistance = 10f; // Adjust this value as needed

      var heroController = owner.GetComponent<HeroController>();

      // Perform circle cast around the enemy
      RaycastHit2D[] hits = Physics2D.CircleCastAll(heroController.transform.position, radius, Vector2.up, maxDistance);

      // Check each hit to see if it's a player
      foreach (RaycastHit2D hit in hits) {
        if (hit.collider.CompareTag("Player")) {
          return true;
        }
      }

      return false;
    }
  }
}