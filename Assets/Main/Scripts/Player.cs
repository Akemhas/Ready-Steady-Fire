using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RSB.Main.Scripts
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private Button profileButton;
		[SerializeField] private Button playerButton;
		[SerializeField] private RawImage playerImage;
		[SerializeField] private int playerNumber;
		[SerializeField] private TextMeshProUGUI playerTMP;
		public static Action<int> OnShot;
		[SerializeField] private Color winColor,loseColor;

		private void Awake() => playerButton.interactable = false;

		private void OnEnable()
		{
			profileButton.onClick.AddListener(OpenProfileUI);
			playerButton.onClick.AddListener(Bang);
			GM.OnGameReady += OnGameReady;
			GM.OnGameReset += OnGameReset;
			OnShot += DisableButton;
			OnShot += CalculateShot;
		}

		private void OnDisable()
		{
			profileButton.onClick.RemoveListener(OpenProfileUI);
			playerButton.onClick.RemoveListener(Bang);
			GM.OnGameReady -= OnGameReady;
			GM.OnGameReset -= OnGameReset;
			OnShot -= DisableButton;
			OnShot -= CalculateShot;
		}

		private void OpenProfileUI()
		{
			ProfileUI.Instance.Enter(playerImage);
		}

		private void DisableButton(int x)
		{
			playerButton.interactable = false;
			profileButton.targetGraphic.raycastTarget = true;
		}

		private void OnGameReset()
		{
			playerButton.targetGraphic.color = Color.white;
			playerTMP.gameObject.SetActive(false);
		}
		private void OnGameReady()
		{
			playerButton.interactable = true;
			profileButton.targetGraphic.raycastTarget = false;
		}

		private void CalculateShot(int pNumber)
		{
			if(playerNumber == pNumber)
			{
				if(GM.CanBang) Win();
				else Lose();
			}
			else
			{
				if(GM.CanBang) Lose();
				else Win();
			}
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
			GM.Shot = true;
		}
	}
}