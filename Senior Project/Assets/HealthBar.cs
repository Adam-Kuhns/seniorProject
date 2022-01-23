using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
	private Slider slider;

	public void Initialize()
	{
		slider = gameObject.GetComponent<Slider>();
	}

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

    public void SetHealth(int health)
	{
		slider.value = health;
	}
}
