using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace RSB.Main.Scripts
{
	public class LevelButton : MonoBehaviour
	{
		[SerializeField] private int targetSceneIndex;
		[SerializeField] private Button levelButton;
		private AsyncOperation _asyncLoadOperation;
		private bool _gameSceneLoaded;

		private void OnEnable() => levelButton.onClick.AddListener(LoadTargetScene);

		private void OnDisable() => levelButton.onClick.RemoveListener(LoadTargetScene);

		private void LoadTargetScene() => StartCoroutine(LoadAsyncRoutine());

		private IEnumerator LoadAsyncRoutine()
		{
			_asyncLoadOperation = SceneManager.LoadSceneAsync(targetSceneIndex);
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