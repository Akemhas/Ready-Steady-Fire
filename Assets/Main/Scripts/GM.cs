using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
namespace RSB.Main.Scripts
{
	public class GM : MonoBehaviour
	{
		[SerializeField] private Button readyButton,resetButton;
		[SerializeField] private TextMeshProUGUI rsbTMP;
		[SerializeField] private float minDuration,maxDuration;
		[SerializeField] private float resetDuration;
		public static event Action OnBangReady;
		public static event Action OnGameReady;

		private void Start()
		{
			ResetGame();
		}
		private void OnEnable()
		{
			readyButton.onClick.AddListener(StartCounting);
			resetButton.onClick.AddListener(ResetGame);
			Player.OnShot += OpenResetButton;
		}

		private void OnDisable()
		{
			readyButton.onClick.RemoveListener(StartCounting);
			resetButton.onClick.RemoveListener(ResetGame);
			Player.OnShot -= OpenResetButton;
		}

		private void ResetGame()
		{
			OnGameReady?.Invoke();
			rsbTMP.SetText("READY");
			readyButton.gameObject.SetActive(true);
			resetButton.gameObject.SetActive(false);
		}

		private void OpenResetButton(int x)
		{
			resetButton.gameObject.SetActive(true);
			rsbTMP.gameObject.SetActive(true);
			rsbTMP.SetText("PLAY AGAIN");
		}
		
		private void StartCounting()
		{
			readyButton.gameObject.SetActive(false);
			StartCoroutine(CountDownRoutine());
		}

		private IEnumerator CountDownRoutine()
		{
			rsbTMP.SetText("STEADY");
			yield return new WaitForSeconds(Random.Range(minDuration,maxDuration));
			rsbTMP.SetText("BANG");
			OnBangReady?.Invoke();
		}
	}
}