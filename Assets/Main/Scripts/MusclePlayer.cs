using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RSB.Main.Scripts
{
	public class MusclePlayer : MonoBehaviour
	{
		[SerializeField] private int playerNumber;
		[SerializeField] private Button profileButton;
		[SerializeField] private RawImage playerImage;
		[SerializeField] private TextMeshProUGUI playerTMP;

		private float _middlePoint;

		private void Awake() => _middlePoint = Screen.height * .5f;

		private void OnEnable()
		{
			profileButton.onClick.AddListener(OpenProfileUI);
			MuscleGm.OnGameReady += DisableProfileButton;
			MuscleGm.OnGameFinished += GameEnd;
			MuscleGm.OnGameReset += ResetPlayer;
		}

		private void OnDisable()
		{
			profileButton.onClick.RemoveListener(OpenProfileUI);
			MuscleGm.OnGameReady -= DisableProfileButton;
			MuscleGm.OnGameFinished -= GameEnd;
			MuscleGm.OnGameReset -= ResetPlayer;
		}

		private void DisableProfileButton() => profileButton.targetGraphic.raycastTarget = false;
		private void EnableProfileButton() => profileButton.targetGraphic.raycastTarget = true;
		private void GameEnd(int winningPlayer)
		{
			playerTMP.gameObject.SetActive(true);
			if(winningPlayer == playerNumber) Win();
			else Lose();
		}

		private void Win()
		{
			playerTMP.SetText("YOU WON");
		}

		private void Lose()
		{
			playerTMP.SetText("YOU LOST");
		}

		private void ResetPlayer()
		{
			EnableProfileButton();
			playerTMP.gameObject.SetActive(false);
		}

		private void OpenProfileUI() => ProfileUI.Instance.Enter(playerImage);

		private void Update()
		{
			int count = Input.touchCount;
			if(count <= 0) return;
			if(!MuscleGm.GameStarted) return;
			for(int i = 0; i < count; i++)
			{
				if(Input.GetTouch(i).phase != TouchPhase.Began) continue;
				if(!IsRightHalf(Input.GetTouch(i).position.y)) continue;

				MuscleGm.Instance.AdjustPlayerCount(playerNumber);
			}
		}

		private bool IsRightHalf(float pos)
		{
			if(playerNumber == 1)
			{
				if(pos < _middlePoint) return true;
			}
			else
			{
				if(pos > _middlePoint) return true;
			}
			return false;
		}
	}
}