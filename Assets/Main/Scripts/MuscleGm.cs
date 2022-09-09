using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RSB.Main.Scripts
{
	public class MuscleGm : MonoBehaviour
	{
		public static MuscleGm Instance;
		[SerializeField] private Button readyButton,resetButton;
		[SerializeField] private TextMeshProUGUI rsbTMP;
		[SerializeField] private Image p1Image,p2Image;
		private int _player1Count,_player2Count;
		[SerializeField] private TextMeshProUGUI player1CountTMP,player2CountTMP;
		public static event Action OnGameReset,OnGameReady;
		public static Action<int> OnGameFinished;
		public static bool GameStarted;
		
		private void Awake()
		{
			Instance = this;
		}
		private void Start()
		{
			ResetGame();
		}
		private void OnEnable()
		{
			readyButton.onClick.AddListener(StartGame);
			resetButton.onClick.AddListener(ResetGame);
			OnGameFinished += OpenResetButton;
		}

		private void OnDisable()
		{
			readyButton.onClick.RemoveListener(StartGame);
			resetButton.onClick.RemoveListener(ResetGame);
			OnGameFinished -= OpenResetButton;
		}

		private void StartGame()
		{
			OnGameReady?.Invoke();
			readyButton.gameObject.SetActive(false);
			StartCoroutine(StartRoutine());
		}

		private IEnumerator StartRoutine()
		{
			int counter = 3;
			rsbTMP.SetText(counter.ToString());
			while(counter > 0)
			{
				yield return new WaitForSeconds(1);
				counter--;
				rsbTMP.SetText(counter.ToString());
			}
			rsbTMP.gameObject.SetActive(false);
			GameStarted = true;
		}

		private void OpenResetButton(int winningPlayer)
		{
			GameStarted = false;
			float angle = winningPlayer == 1 ? 180 : 0;
			Vector3 target = new Vector3(0,0,angle);
			rsbTMP.transform.eulerAngles = target;
			resetButton.gameObject.SetActive(true);
			rsbTMP.gameObject.SetActive(true);
			rsbTMP.SetText("PLAY AGAIN");
			player1CountTMP.gameObject.SetActive(false);
			player2CountTMP.gameObject.SetActive(false);
		}

		private void ResetGame()
		{
			rsbTMP.SetText("READY");
			readyButton.gameObject.SetActive(true);
			resetButton.gameObject.SetActive(false);
			_player1Count = 0;
			_player2Count = 0;
		
			player1CountTMP.SetText(_player1Count.ToString());
			player2CountTMP.SetText(_player2Count.ToString());
			
			AdjustImages();
			OnGameReset?.Invoke();
		}

		public void AdjustPlayerCount(int playerNumber)
		{
			if(playerNumber == 1)
			{
				_player1Count++;
				_player2Count--;
				if(_player2Count < 0) player2CountTMP.gameObject.SetActive(false);
			}
			else
			{
				_player1Count--;
				_player2Count++;
			}
			player1CountTMP.gameObject.SetActive(_player1Count >= 0);
			player2CountTMP.gameObject.SetActive(_player2Count >= 0);
			player1CountTMP.SetText(_player1Count.ToString());
			player2CountTMP.SetText(_player2Count.ToString());
			if(_player1Count >= 10) OnGameFinished?.Invoke(1);
			if(_player2Count >= 10) OnGameFinished?.Invoke(2);
			AdjustImages();
		}

		private void AdjustImages()
		{
			var anchor = p1Image.rectTransform.anchorMax;
			float newYAnchor = Mathf.InverseLerp(-10,10,_player1Count);
			p1Image.rectTransform.anchorMax = new Vector2(anchor.x,newYAnchor);

			anchor = p2Image.rectTransform.anchorMin;
			newYAnchor = Mathf.InverseLerp(10,-10,_player2Count);
			p2Image.rectTransform.anchorMin = new Vector2(anchor.x,newYAnchor);
		}

	}

}