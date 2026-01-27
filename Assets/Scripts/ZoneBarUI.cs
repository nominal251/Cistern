using UnityEngine;
using UnityEngine.UI;

public class ZoneBarUI : MonoBehaviour
{
    public ZoneControl zone;
    public Image bar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = zone.percentActive;
    }
}

