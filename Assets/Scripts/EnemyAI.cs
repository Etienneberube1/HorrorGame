using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform targetPosition; // Player's position
    private NavMeshAgent agent;
    public Transform hideDestination; // Destination for AI when player is hiding
    public float detectionTimer;
    public float detectionDistance;
    private bool isGameOver = false;

    private enum AIState { ChasingPlayer, Investigating, PlayerHiding }
    private AIState currentState = AIState.ChasingPlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {

        switch (currentState)
        {
            case AIState.ChasingPlayer:
                agent.SetDestination(targetPosition.position);
                // Check if AI is close to player
                if (Vector3.Distance(transform.position, targetPosition.position) < detectionDistance)
                {
                    detectionTimer += Time.deltaTime;
                    if (detectionTimer >= 1.5f && !isGameOver)
                    {
                        isGameOver = true;

                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;

                        SceneManager.LoadScene("GameOver");
                    }
                }
                else
                {
                    // Reset detection timer if AI moves away from player
                    detectionTimer = 0f;
                }
                break;
            case AIState.Investigating:
                // Do nothing while investigating
                break;
            case AIState.PlayerHiding:
                agent.SetDestination(hideDestination.position);
                break;
        }
    }

    public void SetPlayerHiding(bool isHiding)
    {
        currentState = isHiding ? AIState.PlayerHiding : AIState.ChasingPlayer;
    }

    public void InvestigatePoint(Vector3 point)
    {
        currentState = AIState.Investigating;
        StartCoroutine(InvestigatePointRoutine(point));
    }

    private IEnumerator InvestigatePointRoutine(Vector3 point)
    {
        agent.SetDestination(point);
        while (!agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2); // Wait for 2 seconds at the point

        if (currentState != AIState.PlayerHiding)
        {
            currentState = AIState.ChasingPlayer;
        }
    }
}
