using UnityEngine;

public class FightState : IHeroState
{
    private readonly HeroController heroController;
    private readonly float circleCastRadius;

    public FightState(HeroController heroController)
    {
        this.heroController = heroController;
        this.circleCastRadius = 20f; // Radius of the circle cast
    }

    public void Enter()
    {
        // Perform a circle cast to detect nearby enemy objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(heroController.transform.position, circleCastRadius);

        // Find the closest enemy
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to an enemy
            if (collider.CompareTag("Player"))
            {
                float distance = Vector2.Distance(heroController.transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            // Set the target of the AI agent to the transform of the closest enemy
            heroController.aiAgent.SetTarget(closestEnemy);
        }
        else
        {
           
        }
    }

    public void Update()
    {
        // Update fight state logic
    }

    public void Exit()
    {
        // Exit fight state logic
    }
}
