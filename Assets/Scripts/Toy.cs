using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    public bool hasBeenLaunched = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenLaunched)
        {
            enemyAI.InvestigatePoint(transform.position);
            Destroy(gameObject, 1f);
            AudioManager.Instance.PlaySFX(AudioManager.EAudio.DuckSound);
        }
    }
}
