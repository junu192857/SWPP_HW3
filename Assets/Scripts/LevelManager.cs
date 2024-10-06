using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int curLevel;

    // for Level 1
    public int money;

    public PlayerBehaviour player;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        
        instance = this;
    }
    private void Start()
    {
        Initialize(1);
    }

    public void Initialize(int level) {
        curLevel = level;
        player.Initialize(level);

        ResetLevel(level);
    }

    private void ResetLevel(int level) {
        switch (level) {
            case 1:
                money = 0;
                break;
            default:
                throw new NotImplementedException();
        }
        ResetLevel(level + 1);
    }
}
