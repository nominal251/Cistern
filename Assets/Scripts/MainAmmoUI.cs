using UnityEngine;
using UnityEngine.UI;

public class MainAmmoUI : MonoBehaviour
{
    public MainWeapon ship;
    public Image ammoBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ammoBar.fillAmount = ship.ammoPercent;
    }
}
