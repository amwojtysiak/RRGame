using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpaceShip_Enemy : MonoBehaviour
{
    public Rigidbody2D RB { get; private set; }
    public SpriteRenderer Sprite { get; private set; }
    public Vector2 transformChange;

    public bool walkingLeft;
    public float radiusFromStart;
    public float startPosX;

    [System.Serializable]
    public class SpaceShipStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth;  }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 40;

        public int knockBackForce = 20;

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public SpaceShipStats enemyStats = new();

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    public int fallBoundaryY = -20;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        enemyStats.Init();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }

        walkingLeft = true;
        startPosX = RB.position.x;

    }

    private void Update()
    {
        if (transform.position.y <= fallBoundaryY)
            DamageObject(999999999);

        if (walkingLeft == true)
        {
            //sprite.flipX = true;
            RB.position += -transformChange * Time.deltaTime;

        }
        else
        {
            //sprite.flipX = false;
            RB.position += transformChange * Time.deltaTime;
        }

        if (RB.position.x < (startPosX - 10))
        {
            //RB.velocity.x 
            walkingLeft = false;
        }

        if (RB.position.x > (startPosX + 10))
        {
            walkingLeft = true;
        }
    }

    public void DamageObject(int damage)
    {
        enemyStats.curHealth -= damage;
        if (enemyStats.curHealth <= 0)
        {
            GameMaster.KillEnemy(this);
        }
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }
    }
   
    void OnCollisionEnter2D(Collision2D _collInfo)
    {
        Player _player = _collInfo.collider.GetComponent<Player>();
        Rigidbody2D _rb = _collInfo.rigidbody;
        Rigidbody2D _rbEnemy = _collInfo.otherRigidbody;
        Collider2D playerCollider = _collInfo.collider;
        //Vector3 colVelocity = _collInfo.relativeVelocity;
        

        Vector2 kbDirection = (playerCollider.transform.position - transform.position).normalized;

        if (_player != null)
        {
            _player.DamagePlayer(enemyStats.damage);
            //_rb.AddForce(colVelocity * -50, ForceMode2D.Impulse);
            _rb.AddForce(kbDirection * enemyStats.knockBackForce, ForceMode2D.Impulse);
            _rbEnemy.AddForce(-1 * (enemyStats.knockBackForce/10) * kbDirection, ForceMode2D.Impulse);

        }
    }
}




