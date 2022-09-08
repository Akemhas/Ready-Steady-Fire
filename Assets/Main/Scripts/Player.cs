using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RSB.Main.Scripts
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private Button playerButton;
		[SerializeField] private int playerNumber;
		[SerializeField] private TextMeshProUGUI playerTMP;
		public static Action<int> OnShot;
		[SerializeField] private Color winColor,loseColor;

		private void Awake()
		{
			playerButton.interactable = false;
		}

		private void OnEnable()
		{
			playerButton.onClick.AddListener(Bang);
			GM.OnBangReady += ActivateButton;
			GM.OnGameReady += DisableTMP;
			OnShot += DisableButton;
			OnShot += CalculateShot;
		}

		private void OnDisable()
		{
			playerButton.onClick.RemoveListener(Bang);
			GM.OnBangReady -= ActivateButton;
			GM.OnGameReady -= DisableTMP;
			OnShot -= DisableButton;
			OnShot -= CalculateShot;
		}

		private void ActivateButton()
		{
			playerButton.interactable = true;
		}

		private void DisableButton(int x)
		{
			playerButton.interactable = false;
		}

		private void DisableTMP()
		{
			playerTMP.gameObject.SetActive(false);
			playerButton.targetGraphic.color = Color.white;
		}

		private void CalculateShot(int pNumber)
		{
			if(playerNumber == pNumber) Win();
			else Lose();
		}

		private void Win()
		{
			playerTMP.gameObject.SetActive(true);
			playerTMP.SetText("YOU WIN");
			playerButton.targetGraphic.color = winColor;
		}

		private void Lose()
		{
			playerTMP.gameObject.SetActive(true);
			playerTMP.SetText("YOU LOST");
			playerButton.targetGraphic.color = loseColor;
		}

		private void Bang()
		{
			OnShot?.Invoke(playerNumber);
		}
	}
}