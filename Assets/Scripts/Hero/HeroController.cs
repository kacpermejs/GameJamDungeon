using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private IHeroState currentState;
    public AIAgent aiAgent; // Reference to the AIAgent component

    private void Awake()
    {
        // Get the AIAgent component attached to the same GameObject
        aiAgent = GetComponent<AIAgent>();
    }

    void Start()
    {
        // Initialize with patrol state
        currentState = new PatrolState(this);
    }

    void Update()
    {
        // Update the current state
        currentState.Update();
    }

    public void TransitionToState(IHeroState newState)
    {
        // Exit the current state
        currentState.Exit();

        // Transition to the new state
        currentState = newState;
        currentState.Enter();
    }

}

