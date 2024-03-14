using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IHeroState
{
    private readonly HeroController heroController;

    public PatrolState(HeroController heroController)
    {
        this.heroController = heroController;
    }

    public void Enter()
    {
        // Enter patrol state logic
    }

    public void Update()
    {
        // Update patrol state logic

        // Check for player detection
        if (CanSeePlayer())
        {
            // Transition to the fighting state if the player is seen
            heroController.TransitionToState(new FightState(heroController));
            return; // Exit the Update method to avoid continuing patrol logic
        }
    }

    public void Exit()
    {
        // Exit patrol state logic
    }
    private bool CanSeePlayer()
    {
        // Set the radius of the circle cast
        float radius = 5f;

        // Set the maximum distance for the circle cast
        float maxDistance = 10f; // Adjust this value as needed

        // Perform circle cast around the enemy
        RaycastHit2D[] hits = Physics2D.CircleCastAll(heroController.transform.position, radius, Vector2.up, maxDistance);

        // Check each hit to see if it's a player
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }


}
