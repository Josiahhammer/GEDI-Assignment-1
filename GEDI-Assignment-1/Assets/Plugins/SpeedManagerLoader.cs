using UnityEngine;
using GameSpeedController; // Namespace from the DLL

public class SpeedManagerLoader : MonoBehaviour
{
    void Start()
    {
        // Attach SpeedController from the DLL to the GameObject
        gameObject.AddComponent<SpeedController>();
    }
}
