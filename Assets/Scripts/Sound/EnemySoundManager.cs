using System.Linq;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    public AudioClip[] enemyHurtSounds;
    [SerializeField]
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        enemyHurtSounds = Resources.LoadAll("EnemyHurt", typeof(AudioClip)).Cast<AudioClip>().ToArray();

    }

    public AudioClip randomEnemyHurt()
    {
        int hurtIndex = Random.Range(0, enemyHurtSounds.Length - 1);
        return enemyHurtSounds[hurtIndex];
    }

    public void playSound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
