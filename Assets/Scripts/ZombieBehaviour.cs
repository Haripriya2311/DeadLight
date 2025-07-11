using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public float detectionRadius = 15f;
    public float attackRadius = 2f;
    public float moveSpeed = 1.5f;
    public float runSpeed = 3f;

    // 🔸 Added for player damage
    private PlayerHealth playerHealth;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    private bool isDead = false;
    private bool isAttacking = false;

    // 🔊 Audio
    private AudioSource zombieAudio;               // 🔊 growl looping
    public AudioClip biteClip;                     // 🔊 bite sound clip
    public float maxGrowlVolume = 1f;              // 🔊 optional max volume
    public float minGrowlVolume = 0.1f;            // 🔊 optional min volume
    public float maxDistance = 20f;                // 🔊 distance for min growl

    // 🩸 Blood hit effect
    private PlayerHitEffect playerHitEffect;       // 🆕

    void Start()
    {
        if (animator != null)
            animator.Play("Z_Walk");

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();

            // 🩸 Get reference to blood effect script
            playerHitEffect = player.GetComponent<PlayerHitEffect>();
        }

        zombieAudio = GetComponent<AudioSource>(); // 🔊 get AudioSource
        if (zombieAudio != null)
        {
            zombieAudio.loop = true;              // 🔊 ensure loop is on
            zombieAudio.playOnAwake = false;      // 🔊 don't auto play
            zombieAudio.volume = 0f;              // 🔊 start silent
            zombieAudio.Play();                   // 🔊 start growling
        }
    }

    void Update()
    {
        if (isDead || player == null || animator == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 🔊 Adjust zombie growl volume based on distance
        if (zombieAudio != null)
        {
            float t = Mathf.Clamp01(1 - (distance / maxDistance));
            zombieAudio.volume = Mathf.Lerp(minGrowlVolume, maxGrowlVolume, t);
        }

        // Face the player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        transform.forward = direction;

        if (distance <= attackRadius)
        {
            isAttacking = true;
            animator.Play("Z_Attack");

            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackCooldown;

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);

                    // 🩸 Show blood screen on player
                    if (playerHitEffect != null)
                    {
                        playerHitEffect.ShowBloodEffect();
                    }

                    // 🔊 Play bite sound once per attack
                    if (biteClip != null)
                        AudioSource.PlayClipAtPoint(biteClip, transform.position);
                }
            }
        }
        else if (distance <= detectionRadius)
        {
            isAttacking = false;
            animator.Play("Z_Run");
            transform.position += direction * runSpeed * Time.deltaTime;
        }
        else
        {
            isAttacking = false;
            animator.Play("Z_Walk");
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    public void Die()
    {
        isDead = true;

        Debug.Log("💀 Zombie died"); // ✅ Check if this is printed

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(1);         // ✅ Increase score
            ScoreManager.Instance.DecreaseEnemies(1);  // ✅ Decrease enemies
        }
        else
        {
            Debug.LogWarning("❌ ScoreManager.instance is NULL!");
        }

        if (animator != null)
            animator.enabled = false;

        Destroy(gameObject);
    }
}
