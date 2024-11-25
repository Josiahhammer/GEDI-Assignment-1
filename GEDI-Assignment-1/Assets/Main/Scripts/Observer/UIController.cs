using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Text text;
    [SerializeField] private float currentScore;
    [SerializeField] public bool enemyKilled;

    private void Awake()
    {
        currentScore = 0;
        enemyKilled = false;
    }

    private void Update()
    {
        if (enemyKilled == true)
        {
            currentScore++;
            enemyKilled = false;
            text.text = currentScore.ToString();
        }

    }



    private void OnEnable()
    {
        // Find the player and subscribe to health updates
        var player = FindObjectOfType<PlayerCommand>();
        if (player != null)
        {
            player.OnHealthChanged += SetHealthPercent;
        }
        else
        {
            Debug.LogWarning("PlayerCommand not found in scene!");
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        var player = FindObjectOfType<PlayerCommand>();
        if (player != null)
        {
            player.OnHealthChanged -= SetHealthPercent;
        }
    }

    public void SetHealthPercent(float obj)
    {
        scrollbar.size = obj;
        Debug.Log(obj.ToString());
    }








}
