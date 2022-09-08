using System;
using UnityEngine;
using UnityEngine.UI;
namespace RSB.Main.Scripts
{
	public class ProfileUI : MonoBehaviour
	{
		public static ProfileUI Instance;
		public GameObject panelHolder;
		public Button exitButton;
		internal RawImage ProfileImage;

		private void Awake()
		{
			Instance = this;
		}
		private void OnEnable()
		{
			exitButton.onClick.AddListener(Exit);
		}

		private void OnDisable()
		{
			exitButton.onClick.RemoveListener(Exit);
		}

		public void Enter(RawImage image)
		{
			ProfileImage = image;
			panelHolder.SetActive(true);
		}

		private void Exit()
		{
			panelHolder.SetActive(false);
		}
	}
}