using UnityEngine;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainMenuTransitionController transitionController;

        public void OnPlayButtonPressed() => transitionController.OnPlayPressed();
        public void OnQuitButtonPressed() => Application.Quit();
    }
}