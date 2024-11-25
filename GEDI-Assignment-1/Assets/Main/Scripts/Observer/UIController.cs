using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private Scrollbar scrollbar;

/*    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }*/

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
