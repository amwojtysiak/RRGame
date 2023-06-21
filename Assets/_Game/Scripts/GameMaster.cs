using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;

    public Transform boxBreakPoint;
    public Transform boxBreakPrefab;

    private void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        }
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        Debug.Log("Player Died");
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public static void KillObject(InteractiveObject intObj)
    {
        
        Transform clone = Instantiate(gm.boxBreakPrefab, gm.boxBreakPoint.position, gm.boxBreakPoint.rotation) as Transform;
        Destroy(clone.gameObject, 3f);
        Destroy(intObj.gameObject);
        Debug.Log("object Destroyed");
    }

    public IEnumerator RespawnPlayer()
    {
        Debug.Log("TODO: Waiting for spawn sound");
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone.gameObject, 3f);
        
    }
}
