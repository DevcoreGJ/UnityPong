using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // The ball game object
    [Header("Ball")]
    public GameObject ball;

    // Player 1 game objects
    [Header("Player 1")]
    public GameObject Player1Paddle; // The paddle controlled by Player 1
    public GameObject Player1Goal; // The goal where Player 1 scores

    // Player 2 game objects
    [Header("Player 2")]
    public GameObject Player2Paddle; // The paddle controlled by Player 2
    public GameObject Player2Goal; // The goal where Player 2 scores

    // UI elements for displaying scores
    [Header("Score UI")]
    public GameObject Player1Text; // Text displaying Player 1's score
    public GameObject Player2Text; // Text displaying Player 2's score

    [Header("QLearning Settings")]
    public float learningRate = 0.1f; // Adjust the value according to your requirements
    public float discountFactor = 0.9f; // Adjust the value according to your requirements
    public float explorationRate = 0.1f; // Adjust the value according to your requirements

    // variable holding scores
    private int Player1Score;
    private int Player2Score;

    private QLearningAgent qLearningAgent; // Q-learning agent for AI

    public void Player1Scored()
    {
        Player1Score++; // Increment Player 1's score
        Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString(); // Update Player 1's score text
        ResetPosition(); // Reset the positions of objects
    }

    public void Player2Scored()
    {
        Player2Score++; // Increment Player 2's score
        Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString(); // Update Player 2's score text
        ResetPosition(); // Reset the positions of objects

        // Update Q-value after being scored against
        string state = GetGameState(); // Get the current game state
        int action = 1; // Assuming action 1 corresponds to the CPUPaddle moving up
        float reward = -1f; // Negative reward for being scored against
        string nextState = GetNextGameState(); // Get the next game state after the score
        qLearningAgent.UpdateQValue(state, action, reward, nextState); // Update the Q-value in the Q-learning agent (AI)

        /* Note: AI learning from its mistakes. Oops, it got scored against!
           It's time to teach the AI a lesson: "Don't let the ball pass you!"
           Let's update the Q-value so the AI learns from this mishap. */
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset(); // Reset the ball's position and velocity
        Player1Paddle.GetComponent<Paddle>().Reset(); // Reset Player 1's paddle position
        Player2Paddle.GetComponent<Paddle>().Reset(); // Reset Player 2's paddle position
        Player2Paddle.GetComponent<CPUPaddle>().Reset(); // Reset the CPU's paddle position

        /* When life knocks you down, reset and get ready for a comeback!
        Let's reset all the objects to their initial positions. */
    }


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; // Ensure that time scale is set to 1 when the scene starts

        // Create a magical QLearningAgent that learns from its mistakes and explores new possibilities
        qLearningAgent = new QLearningAgent(learningRate, discountFactor, explorationRate);
    }

    // Update is called once per frame
    void Update()
    {
        // Deactivate all currently loaded scenes (No scene left behind!)
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            // If the scene is not the active scene, deactivate and unload it
            if (scene.name != SceneManager.GetActiveScene().name)
            {
                SceneManager.SetActiveScene(scene);
                SceneManager.UnloadSceneAsync(scene);
            }
        }

        // Choose a mind-boggling action for CPUPaddle
        string state = GetGameState();

        // Let the QLearningAgent decide the action based on the current state
        int action = qLearningAgent.ChooseAction(state);

        // Deliver the chosen action to MLPaddle, unleashing its paddle fury!
        Player2Paddle.GetComponent<MLPaddle>().SetMovement(action);
    }

    // Get the current state of the game, a snapshot of existence within the matrix
    private string GetGameState()
    {
        Vector3 ballPosition = ball.transform.position;
        Vector3 player1PaddlePosition = Player1Paddle.transform.position;
        Vector3 player2PaddlePosition = Player2Paddle.transform.position;

        // Assemble the state into a mystical string representation
        string gameState = $"{ballPosition.x},{ballPosition.y};{player1PaddlePosition.y};{player2PaddlePosition.y}";
        return gameState;
    }

    // Get the next state of the game after the score, peering into the realm of possibilities
    private string GetNextGameState()
    {
        // Implement your logic to calculate the next game state after the score

        // For now, let's gaze into the crystal ball to retrieve the initial positions
        Vector3 ballStartPosition = ball.GetComponent<Ball>().startPosition;
        Vector3 player1PaddleStartPosition = Player1Paddle.GetComponent<Paddle>().startPosition;
        Vector3 player2PaddleStartPosition = Player2Paddle.GetComponent<Paddle>().startPosition;

        // Unveil the next state, shrouded in mystery and anticipation
        string nextState = $"{ballStartPosition.x},{ballStartPosition.y};{player1PaddleStartPosition.y};{player2PaddleStartPosition.y}";
        return nextState;
    }
}
