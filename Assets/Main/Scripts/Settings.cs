using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace RSB.Main.Scripts
{
	public class Settings : MonoBehaviour
	{
		[SerializeField] private GameObject panelHolder;
		[SerializeField] private Button enterButton,exitButton;
		[SerializeField] private Button goToMenuButton;
		private AsyncOperation _asyncLoadOperation;
		private bool _gameSceneLoaded;

		private void OnEnable()
		{
			enterButton.onClick.AddListener(OpenSettings);
			exitButton.onClick.AddListener(CloseSettings);
			goToMenuButton.onClick.AddListener(LoadMenuButton);
		}

		private void OnDisable()
		{
			enterButton.onClick.RemoveListener(OpenSettings);
			exitButton.onClick.RemoveListener(CloseSettings);
			goToMenuButton.onClick.RemoveListener(LoadMenuButton);
		}

		private void OpenSettings() => panelHolder.SetActive(true);

		private void CloseSettings() => panelHolder.SetActive(false);

		private void LoadMenuButton() => StartCoroutine(LoadAsyncRoutine());

		private IEnumerator LoadAsyncRoutine()
		{
			_asyncLoadOperation = SceneManager.LoadSceneAsync(0);
			while(!_asyncLoadOperation.isDone)
			{
				if(_asyncLoadOperation.progress >= 0.9f) _gameSceneLoaded = true;
				if(_gameSceneLoaded)
				{
					_asyncLoadOperation.allowSceneActivation = true;
					break;
				}

				yield return null;
			}
		}
	}
}