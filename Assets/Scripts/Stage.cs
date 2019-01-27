using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Player player;
    public Luggage luggage;
    public Player playerPrefab;
    public Luggage luggagePrefab;
    public Transform playerStartPoint;
    public Transform luggageStartPoint;
    public LevelTrigger levelTrigger;

    public void Start()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            player = Instantiate(playerPrefab, playerStartPoint.position, Quaternion.identity);
        }
        else
        {
            player.transform.position = playerStartPoint.position;
        }

        luggage = FindObjectOfType<Luggage>();
        if (luggage == null)
        {
            luggage = Instantiate(luggagePrefab, luggageStartPoint.position, Quaternion.identity);
        }
        else
        {
            luggage.transform.position = luggageStartPoint.position;
        }

        levelTrigger = GetComponentInChildren<LevelTrigger>();
    }
    
    public void Restart()
    {
        player.transform.position = playerStartPoint.position;
        luggage.transform.position = luggageStartPoint.position;

        player.Reset();
        luggage.Reset();
    }
}