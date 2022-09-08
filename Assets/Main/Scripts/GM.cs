using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = System.Numerics.Quaternion;
using Random = UnityEngine.Random;
namespace RSB.Main.Scripts
{
	public class GM : MonoBehaviour
	{
		[SerializeField] private Button readyButton,resetButton;
		[SerializeField] private TextMeshProUGUI rsbTMP;
		[SerializeField] private float minDuration,maxDuration;
		public static bool CanBang,Shot;
		public static event Action OnGameReset,OnGameReady;

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
			CanBang = false;
			Shot = false;
			rsbTMP.SetText("READY");
			readyButton.gameObject.SetActive(true);
			resetButton.gameObject.SetActive(false);
			OnGameReset?.Invoke();
		}

		private void OpenResetButton(int playerNumber)
		{
			float angle = CanBang ? playerNumber == 1 ? 180 : 0 : playerNumber == 1 ? 0 : 180;
			Vector3 target = new Vector3(0,0,angle);
			rsbTMP.transform.eulerAngles = target;
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
			OnGameReady?.Invoke();
			rsbTMP.SetText("STEADY");
			float duration = Random.Range(minDuration,maxDuration);
			float elapsedTime = 0;
			while(elapsedTime < duration)
			{
				elapsedTime += Time.deltaTime;
				if(Shot) yield break;
				yield return null;
			}
			rsbTMP.SetText("BANG");
			CanBang = true;
		}
	}
}