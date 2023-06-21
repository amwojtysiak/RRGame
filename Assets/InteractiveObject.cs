using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{


    [System.Serializable]
    public class ObjectStats
    {
        public int Health = 10;

    }

    public ObjectStats objectStats = new();

    public int fallBoundaryY = -20;

    private void Update()
    {
        if (transform.position.y <= fallBoundaryY)
            DamageObject(999999999);
    }

    public void DamageObject(int damage)
    {
        objectStats.Health -= damage;
        if (objectStats.Health <= 0)
        {
            GameMaster.KillObject(this);
        }
    }
}
