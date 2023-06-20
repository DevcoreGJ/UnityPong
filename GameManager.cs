using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player 1")]
    public GameObject Player1Paddle;
    public GameObject Player1Goal;

    [Header("Player 2")]
    public GameObject Player2Paddle;
    public GameObject Player2Goal;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;

    [Header("QLearning Settings")]
    public float learningRate = 0.1f; // Adjust the value according to your requirements
    public float discountFactor = 0.9f; // Adjust the value according to your requirements
    public float explorationRate = 0.1f; // Adjust the value according to your requirements

    private int Player1Score;
    private int Player2Score;

    private QLearningAgent qLearningAgent;

    public void Player1Scored()
    {
        Player1Score++;
        Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();
        ResetPosition();
    }

    public void Player2Scored()
    {
        Player2Score++;
        Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
        ResetPosition();

        // Update Q-value after being scored against
        string state = GetGameState();
        int action = 1; // Assuming action 1 corresponds to the CPUPaddle moving up
        float reward = -1f; // Negative reward for being scored against
        string nextState = GetNextGameState();
        qLearningAgent.UpdateQValue(state, action, reward, nextState);
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        Player1Paddle.GetComponent<Paddle>().Reset();
        Player2Paddle.GetComponent<Paddle>().Reset();
        Player2Paddle.GetComponent<CPUPaddle>().Reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; // Ensure that time scale is set to 1 when the scene starts
        qLearningAgent = new QLearningAgent(learningRate, discountFactor, explorationRate);
    }

    // Update is called once per frame
    void Update()
    {
        // Deactivate all currently loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != SceneManager.GetActiveScene().name)
            {
                SceneManager.SetActiveScene(scene);
                SceneManager.UnloadSceneAsync(scene);
            }
        }

        // Choose action for CPUPaddle
        string state = GetGameState();
        int action = qLearningAgent.ChooseAction(state);

        // Notify CPUPaddle of the chosen action
        Player2Paddle.GetComponent<MLPaddle>().SetMovement(action);
    }

    private string GetGameState()
    {
        Vector3 ballPosition = ball.transform.position;
        Vector3 player1PaddlePosition = Player1Paddle.transform.position;
        Vector3 player2PaddlePosition = Player2Paddle.transform.position;

        string gameState = $"{ballPosition.x},{ballPosition.y};{player1PaddlePosition.y};{player2PaddlePosition.y}";
        return gameState;
    }

    private string GetNextGameState()
    {
        // Implement your logic to calculate the next game state after the score

        // Example:
        Vector3 ballStartPosition = ball.GetComponent<Ball>().startPosition;
        Vector3 player1PaddleStartPosition = Player1Paddle.GetComponent<Paddle>().startPosition;
        Vector3 player2PaddleStartPosition = Player2Paddle.GetComponent<Paddle>().startPosition;

        string nextState = $"{ballStartPosition.x},{ballStartPosition.y};{player1PaddleStartPosition.y};{player2PaddleStartPosition.y}";
        return nextState;
    }
}
