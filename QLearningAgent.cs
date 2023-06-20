using System.Collections.Generic;
using UnityEngine;

public class QLearningAgent
{
    // Dictionary to store the Q-values for state-action pairs
    private Dictionary<string, Dictionary<int, float>> qTable;

    // Parameters for Q-learning algorithm
    private float learningRate;     // Determines how much the agent learns from new experiences
    private float discountFactor;   // Determines the importance of future rewards
    private float explorationRate;  // Determines the likelihood of exploring new actions

    public QLearningAgent(float learningRate, float discountFactor, float explorationRate)
    {
        // Constructor for the QLearningAgent class.
        // It takes in three parameters: learningRate, discountFactor, and explorationRate.

        this.learningRate = learningRate;  // Assigns the learningRate parameter to the class member learningRate.
        this.discountFactor = discountFactor;  // Assigns the discountFactor parameter to the class member discountFactor.
        this.explorationRate = explorationRate;  // Assigns the explorationRate parameter to the class member explorationRate.

        qTable = new Dictionary<string, Dictionary<int, float>>();  /* Initializes a new empty dictionary called qTable.
                                                                       Empty dictionaries are like blank canvases waiting for artistic exploration.
                                                                       May it be filled with keys and values that dance together in a rhythmic symphony.
                                                                       Let the mind of the QLearningAgent become a universe of possibilities! */
    }



    public int ChooseAction(string state)
    {
        // Check if the Q-table contains the state, otherwise...
        if (!qTable.ContainsKey(state))
        {
            // ...create a new entry for the state in the Q-table
            qTable[state] = new Dictionary<int, float>();
        }

        // Roll the dice and see if we're feeling adventurous
        if (Random.value < explorationRate)
        {
            // Random action time! Who knows what will happen?
            int randomAction = Random.Range(0, 2);
            return randomAction;
        }
        else
        {
            // Time to exploit the system! Get ready for some Q-value magic.
            float maxQValue = float.MinValue;
            int bestAction = 0;

            // Let's iterate over the Q-values for the given state. Hold on tight!
            foreach (var kvp in qTable[state])
            {
                // Searching for the golden nugget... Oops, I mean the action with the highest Q-value.
                if (kvp.Value > maxQValue)
                {
                    maxQValue = kvp.Value;
                    bestAction = kvp.Key;
                }
            }

            // Drumroll, please... The chosen action is about to be revealed!
            return bestAction;
        }
    }



    public void UpdateQValue(string state, int action, float reward, string nextState)
    {
        // Check if the current state is present in the Qtable
        if (!qTable.ContainsKey(state))
        {
            // Add state to Q-table if it doesn't exist, because it wants to be part of the cool club
            qTable[state] = new Dictionary<int, float>();
        }

        // Check if the next state is present in the Q-table
        if (!qTable.ContainsKey(nextState))
        {
            // Add next state to Q-table if it doesn't exist, because everyone deserves a chance, even states
            qTable[nextState] = new Dictionary<int, float>();
        }

        // Retrieve the current Q-value for the given state and action, or initialize it to 0 if not present,
        // because sometimes even Q-values need a fresh start in life
        float currentQValue = qTable[state].ContainsKey(action) ? qTable[state][action] : 0f;

        // Get the maximum Q-value for the next state, because we need the best of the best!
        float maxNextQValue = GetMaxQValue(nextState);

        // Update the Q-value using the Q-learning equation, making sure our Q-value stays up-to-date and trendy
        float updatedQValue = (1 - learningRate) * currentQValue + learningRate * (reward + discountFactor * maxNextQValue);

        // Store the updated Q-value for the given state and action in the Q-table,
        // because memory is precious and we don't want to forget our valuable knowledge
        qTable[state][action] = updatedQValue;
    }



    // This private method calculates the maximum Q-value for a given state.
    private float GetMaxQValue(string state)
    {
        // If the state is not found in the Q-table, it assumes a value of 0 as if the state is a novice in the game of Q-learning.
        if (!qTable.ContainsKey(state))
        {
            return 0f;
        }

        // The maxQValue variable starts with the lowest possible float value, like searching for the biggest diamond in the ocean!
        float maxQValue = float.MinValue;

        // The loop checks each Q-value and if it's higher than the current maximum Q-value, it updates it.
        foreach (var kvp in qTable[state])
        {
            // It's like searching for the greatest treasure while navigating through a maze of possibilities.
            if (kvp.Value > maxQValue)
            {
                // Update the maximum Q-value if a higher value is found. My parents warned me about methods like you!
                maxQValue = kvp.Value;
            }
        }

        // Finally, the method returns the maximum Q-value calculated for the state, as if revealing the ultimate secret to success!
        return maxQValue;
    }
}
