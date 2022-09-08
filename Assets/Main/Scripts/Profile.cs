using UnityEngine;
using UnityEngine.UI;
namespace RSB.Main.Scripts
{
	public class Profile : MonoBehaviour
	{
		public Button button;
		[SerializeField] private RawImage profileImage;

		private void OnEnable()
		{
			button.onClick.AddListener(SetImage);
		}

		private void OnDisable()
		{
			button.onClick.RemoveListener(SetImage);
		}

		private void SetImage()
		{
			ProfileUI.Instance.ProfileImage.texture = profileImage.texture;
		}
	}
}