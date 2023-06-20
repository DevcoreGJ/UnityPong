using System.Collections.Generic;
using UnityEngine;

public class QLearningAgent
{
    private Dictionary<string, Dictionary<int, float>> qTable;
    private float learningRate;
    private float discountFactor;
    private float explorationRate;

    public QLearningAgent(float learningRate, float discountFactor, float explorationRate)
    {
        this.learningRate = learningRate;
        this.discountFactor = discountFactor;
        this.explorationRate = explorationRate;

        qTable = new Dictionary<string, Dictionary<int, float>>();
    }

    public int ChooseAction(string state)
    {
        if (!qTable.ContainsKey(state))
        {
            // Add state to Q-table if it doesn't exist
            qTable[state] = new Dictionary<int, float>();
        }

        if (Random.value < explorationRate)
        {
            // Explore: choose a random action
            int randomAction = Random.Range(0, 2);
            return randomAction;
        }
        else
        {
            // Exploit: choose the action with the highest Q-value
            float maxQValue = float.MinValue;
            int bestAction = 0;

            foreach (var kvp in qTable[state])
            {
                if (kvp.Value > maxQValue)
                {
                    maxQValue = kvp.Value;
                    bestAction = kvp.Key;
                }
            }

            return bestAction;
        }
    }

    public void UpdateQValue(string state, int action, float reward, string nextState)
    {
        if (!qTable.ContainsKey(state))
        {
            // Add state to Q-table if it doesn't exist
            qTable[state] = new Dictionary<int, float>();
        }

        if (!qTable.ContainsKey(nextState))
        {
            // Add next state to Q-table if it doesn't exist
            qTable[nextState] = new Dictionary<int, float>();
        }

        float currentQValue = qTable[state].ContainsKey(action) ? qTable[state][action] : 0f;
        float maxNextQValue = GetMaxQValue(nextState);
        float updatedQValue = (1 - learningRate) * currentQValue + learningRate * (reward + discountFactor * maxNextQValue);

        qTable[state][action] = updatedQValue;
    }

    private float GetMaxQValue(string state)
    {
        if (!qTable.ContainsKey(state))
        {
            return 0f;
        }

        float maxQValue = float.MinValue;

        foreach (var kvp in qTable[state])
        {
            if (kvp.Value > maxQValue)
            {
                maxQValue = kvp.Value;
            }
        }

        return maxQValue;
    }
}
