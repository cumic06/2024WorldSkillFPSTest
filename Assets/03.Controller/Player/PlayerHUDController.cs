using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{
    public Slider hpSlider;
    public Text hpText;
    public Slider staminaSlider;
    public Text staminaText;
    public Slider staminaHealSlider;

    public Text bulletCountText;
    public Text magazineCountText;

    public void UpdateHUD()
    {

    }

    public void UpdateHpHUD(float max, float current)
    {
        hpSlider.value = current / max;
    }

    public void UpdateStaminaHUD(float max, float current)
    {
        staminaSlider.value = current / max;
    }

    public void UpdateStaminaHealHUD(float max, float current)
    {
        staminaHealSlider.value = current / max;
    }

    public void UpdateWeaponHUD(int bulletCount, int bulletMaxCount, int magazineCount, int magazineMaxCount)
    {
        bulletCountText.text = $"{bulletCount}/{bulletMaxCount}";
        magazineCountText.text = $"{magazineCount}/{magazineMaxCount}";
    }
}