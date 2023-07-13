using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer Sprite { get; set; }

    [System.Serializable] 
    public class PlayerStats
    {
        public int maxHealth = 200;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public Color DamageColor = new();
        public Color StandardColor = new();
        public float damageColorTime = .5f;

        public void Init()
        {
            curHealth = maxHealth;
        }

    }

    public PlayerStats stats = new PlayerStats();

    public int fallBoundaryY = -20;

    public AudioClip bigBawk;

    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        stats.Init();

        if (statusIndicator == null)
        {
            Debug.LogError("NO STATUS INDICATOR REFERENCED ON PLAYER");
        } 
        else
        {
            Debug.LogError("STATUS INDICATOR REFERENCED ON PLAYER");
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    private void Update()
    {
        if (transform.position.y <= fallBoundaryY)
            DamagePlayer(999999999);
    }

    public void DamagePlayer (int damage)
    {
        StartCoroutine(AlterColorDamaged());
        AudioSource.PlayClipAtPoint(bigBawk, new Vector2(transform.position.x, transform.position.y), 5f);
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }

        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);

    }

    public IEnumerator AlterColorDamaged()
    {
        Sprite.color = stats.DamageColor;
        yield return new WaitForSeconds(stats.damageColorTime);
        Sprite.color = stats.StandardColor;

    }
}
