using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [SerializeField] Transform sunTransform;

    [SerializeField] float fullCycleTimeSeconds = 60;

    public bool isDay => sunTransform.rotation.x <= 180f;

    float counter = 0;

    private void Update()
    {
        counter += (Time.deltaTime * (360/fullCycleTimeSeconds));

        if (counter > 360)
        {
            counter = 0;
            GameManager.instance.NextDay();
        }

        if(!isDay && Mathf.Round(counter) % 30 == 0)
        {
            GameManager.instance.SpawnEnemies();
        }
    
        sunTransform.rotation = Quaternion.Euler(counter, sunTransform.rotation.y, sunTransform.rotation.z);
    }
}
