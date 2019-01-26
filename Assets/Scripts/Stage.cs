using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : BaseEntity
{
    public Player player;
    public Luggage luggage;
    public Player playerPrefab;
    public Luggage luggagePrefab;
    public Transform playerStartPoint;
    public Transform luggageStartPoint;
    public BaseTrick[] tricks;
    public TuningFork[] forks;
    public BaseEntity[] npcs;
    public Radio[] radios;
    public List<Note> notes = new List<Note>();
    public LevelTrigger levelTrigger;
    public bool stageEnded;

    public void Destroy()
    {
        tricks= null;
        forks = null;
        npcs = null;
        radios = null;
        notes.Clear();
        Destroy(gameObject);
    }

    public override void OnStart()
    {
        levelTrigger = GetComponentInChildren<LevelTrigger>();
        levelTrigger.OnStart();

        player = FindObjectOfType<Player>();
        if (player == null)
        {
            player = Instantiate(playerPrefab, playerStartPoint.position, Quaternion.identity);
            player.OnStart();
        }
        else
        {
            player.transform.position = playerStartPoint.position;
        }

        luggage = FindObjectOfType<Luggage>();
        if (luggage == null)
        {
            luggage = Instantiate(luggagePrefab, luggageStartPoint.position, Quaternion.identity);
            luggage.OnStart();
        }
        else
        {
            luggage.transform.position = luggageStartPoint.position;
        }

        tricks = FindObjectsOfType<BaseTrick>();
        foreach (var t in tricks)
            t.OnStart();

        forks = FindObjectsOfType<TuningFork>();
        foreach (var f in forks)
            f.OnStart();

        if (npcs != null)
        {
            foreach (var n in npcs)
                n.OnStart();
        }

        radios = FindObjectsOfType<Radio>();
        foreach (var r in radios)
            r.OnStart();
    }

    public override void OnUpdate()
    {
        if(stageEnded)
            return;

        player.OnUpdate();
        luggage.OnUpdate();

        foreach (var t in tricks)
        {
            if (t != null)
                t.OnUpdate();
        }

        foreach (var f in forks)
            f.OnUpdate();

        if (npcs != null)
        {
            foreach (var n in npcs)
                n.OnUpdate();
        }

        foreach (var r in radios)
            r.OnUpdate();

        for (int i = notes.Count - 1; i >= 0; i--)
            notes[i].OnUpdate();
    }
}