using System;

using ThirdPersonShooter.Utilities;

using TMPro;

using UnityEngine;
using UnityEngine.UIElements;

using Slider = UnityEngine.UI.Slider;

namespace ThirdPersonShooter.Scripts.UI
{
	[RequireComponent(typeof(Slider))]
	public class VolumeSlider : MonoBehaviour
	{
		private const float MIN_VALUE = 0.0001f;
		private const float MAX_VALUE = 1f;

		[SerializeField] private TextMeshProUGUI volumeText;

		private Slider slider;
		private string sliderName;
		private string paramater;

		private void Awake()
		{
			slider = gameObject.GetComponent<Slider>();
		}

		public void Activate()
		{
			paramater = GameManager.Instance.Settings[transform.GetSiblingIndex()];

			slider.minValue = MIN_VALUE;
			slider.maxValue = MAX_VALUE;
			slider.name = paramater.Replace("Volume", "");

			slider.value = PlayerPrefs.GetFloat(paramater, MAX_VALUE);
			GameManager.Instance.Settings.SetVolume(paramater, slider.value);
			UpdateText();
			
			slider.onValueChanged.AddListener(OnSliderValueChanged);

		}

		private void OnSliderValueChanged(float _value)
		{
			GameManager.Instance.Settings.SetVolume(paramater, slider.value);
			UpdateText();
		}

		private void UpdateText()
		{
			volumeText.text = $"{sliderName}: {Mathf.RoundToInt(slider.value.Remap(MIN_VALUE, MAX_VALUE, 0, 100)):000}%";
		}
	}
}