using System.Collections;
using System.Collections.Generic;
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
        public int Health = 10;

    }

    public SpaceShipStats enemyStats = new();

    public int fallBoundaryY = -20;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
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
        enemyStats.Health -= damage;
        if (enemyStats.Health <= 0)
        {
            Debug.Log(this);
            GameMaster.KillEnemy(this);
        }
    }
   
}




