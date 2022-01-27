using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel, GamePanel, WinPanel, StartPanel, PausePanel, FastPanel, Stats;

    private int peasant, warrior, wave, food, peasantHiredStat, warriorHiredStat, foodGatheredStat, enemyStat, peasantKilledStat, warriorKilledStat;
    private float peasantHireTime, warriorHireTime, currentTime, remainingTime, foodTime;
    private bool peasantClicked, warriorClicked;

    public Image peasantCover, warriorCover, enemyCover;
    public Text foodText, peasantText, warriorText, cycleTimeText, waveText, enemyText, statisticText;
    public float peasantHireDelay, warriorHireDelay, round, foodUpdateTime;
    public int peasantIntake, warriorIntake, enemies, StartFood;

    public void RestartButton()
    {
        food = StartFood;
        peasant = 0;
        warrior = 0;
        wave = 0;
        enemies = 0;
        //currentTime = 0;
        remainingTime = 0;
        foodTime = 0;
        Time.timeScale = 1;

        GamePanel.SetActive(true);
        WinPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        StartPanel.SetActive(false);
        Stats.SetActive(false);
    }

    public void PeasantHire()
    {
        if (!peasantClicked && (food >= 1))
        {
            peasant++;
            peasantClicked = true;
            peasantHireTime = 0;
            food -= 1;
            peasantHiredStat++;
        }
    }

    public void WarriortHire()
    {
        if (!warriorClicked && (food >= 2))
        {
            warrior++;
            warriorClicked = true;
            warriorHireTime = 0;
            food -= 2;
            warriorHiredStat++;
        }
    }

    private void FoodIntake()
    {
        food += peasant * peasantIntake;
        food = food + warrior * warriorIntake;
        foodGatheredStat += peasant * peasantIntake;
        foodTime = 0;
    }

    private void GameOver()
    {
        GamePanel.SetActive(false);
        GameOverPanel.SetActive(true);
        Stats.SetActive(true);
        FastPanel.SetActive(false);
        Time.timeScale = 0;
    }

    private void WinGame()
    {
        WinPanel.SetActive(true);
        GamePanel.SetActive(false);
        Stats.SetActive(true);
        FastPanel.SetActive(false);
        Time.timeScale = 0;
    }

    private void CallEnemy()
    {
        enemyStat += enemies;
        peasant = peasant - Convert.ToInt32(Mathf.Round(enemies * 0.3f));
        peasantKilledStat += Convert.ToInt32(Mathf.Round(enemies * 0.3f));
        if (peasantKilledStat > peasantHiredStat) peasantKilledStat = peasantHiredStat;
        warrior = warrior - Convert.ToInt32(Mathf.Round(enemies * 0.7f));
        warriorKilledStat += Convert.ToInt32(Mathf.Round(enemies * 0.7f));
        if (warriorKilledStat > warriorHiredStat) warriorKilledStat = warriorHiredStat;
        enemies += 2;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        FastPanel.SetActive(false);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        FastPanel.SetActive(false);

    }

    public void FastGame()
    {
        Time.timeScale = 2;
        FastPanel.SetActive(true);
        GamePanel.SetActive(true);
        PausePanel.SetActive(false);
    }

    private void UpdateText()
    {
        foodText.text = food.ToString();
        peasantText.text = peasant.ToString();
        warriorText.text = warrior.ToString();
        waveText.text = wave.ToString();
        enemyText.text = "Врагов\nожидается:\n" + enemies.ToString();
        cycleTimeText.text = Mathf.Round(remainingTime + 0.5f).ToString();
        statisticText.text = "Еды добыто: " + foodGatheredStat + "\nЕды съедено: " + (foodGatheredStat - food) +
                                "\n\nКрестьян нанято: " + peasantHiredStat + "\nВоинов нанято: " + warriorHiredStat +
                                "\n\nВрагов прибыло: " + enemyStat + "\nКрестьян погибло: " + peasantKilledStat + "\nВоинов погибло: " + warriorKilledStat +
                                "\n\nУровень врагов: " + wave;
    }

    private void UpdateTime()
    {
        peasantHireTime += Time.deltaTime;
        warriorHireTime += Time.deltaTime;
        //currentTime += Time.deltaTime;
        foodTime += Time.deltaTime;
        remainingTime += Time.deltaTime;
    }

    void Start()
    {
        food = StartFood;
    }

    void Update()
    {
        UpdateText();
        UpdateTime();

        if (peasantClicked)
        {
            peasantCover.fillAmount = 1 - (peasantHireTime / peasantHireDelay);
            if (peasantCover.fillAmount == 0) peasantClicked = false;
        }

        if (warriorClicked)
        {
            warriorCover.fillAmount = 1 - (warriorHireTime / warriorHireDelay);
            if (warriorCover.fillAmount == 0) warriorClicked = false;
        }

        enemyCover.fillAmount = remainingTime / round;
        if (remainingTime > round)
        {
            remainingTime = 0;
            wave++;
            CallEnemy();
        }

        if (foodTime > foodUpdateTime)
        {
            FoodIntake();
        }

        if ((food < 0) || (peasant < 0) || (warrior < 0))
        {
            GameOver();
            Time.timeScale = 0;
        }

        if ((food > 1000) || ((peasant > 20) && (warrior > 20)))
        {
            WinGame();
            Time.timeScale = 0;
        }
    }
}
