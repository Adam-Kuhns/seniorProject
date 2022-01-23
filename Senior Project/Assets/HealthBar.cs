using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
	private Slider slider;



	void Start()
	{
		slider = gameObject.GetComponent<Slider>();
		slider.value = 8;
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
