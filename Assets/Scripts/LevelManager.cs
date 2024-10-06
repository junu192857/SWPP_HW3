using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public UIManager um;

    public int curLevel;

    // for Level 1
    public int money;
    public GameObject gate;
    public Vector3 gatePos;
    public GameObject moneyParticle;

    public PlayerBehaviour player;

    private GameObject[] moneys;
    private GameObject[] bombs;

    private int level1MaxMoney = 25;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        
        instance = this;
    }
    private void Start()
    {
        moneys = GameObject.FindGameObjectsWithTag("Money");
        bombs = GameObject.FindGameObjectsWithTag("Bomb");
        gatePos = gate.transform.position;
        Initialize(2, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Initialize(curLevel, true);
    }

    public void Initialize(int level, bool setPlayerPos) {
        curLevel = level;
        ResetLevel(level, setPlayerPos);
    }

    public void ResetLevel(int level, bool spp) {
        if (spp) player.Initialize(level);
        switch (level) {
            case 1:
                money = 0;
                um.UpdateMoney(money, level1MaxMoney);
                foreach (GameObject money in moneys) {
                    money.SetActive(true);
                }
                CloseGate();
                break;
            case 2:
                um.CloseText();
                foreach (GameObject bomb in bombs) {
                    bomb.SetActive(true);
                }
                break;
            default:
                throw new NotImplementedException();
        }
        foreach (ParticleSystem particle in FindObjectsOfType<ParticleSystem>())
        {
            Destroy(particle.gameObject);
        }
    }

    public void AddMoney(Vector3 moneyPos) {
        money++;
        um.UpdateMoney(money, level1MaxMoney);
        ParticleSystem ps = Instantiate(moneyParticle, moneyPos, Quaternion.identity).GetComponent<ParticleSystem>();
        ps.Play();

        if (money == level1MaxMoney) {
            OpenGate();
        }
    }

    private void OpenGate() => gate.GetComponent<Animator>().SetBool("Open_Condition", true);

    private void CloseGate() {
        gate.GetComponent<Animator>().SetBool("Open_Condition", false);
        gate.transform.position = gatePos;
    }
}
