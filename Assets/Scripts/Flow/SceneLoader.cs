using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core;

namespace Flow
{

    public class SceneLoader : MonoBehaviour
    {
        private string currentSceneName = SceneNames.MainMenu;

        private void OnEnable()
        {
            EventBus.Subscribe<LoadGameRequestedEvent>(OnLoadGame);
            EventBus.Subscribe<ReturnToMainMenuRequestedEvent>(OnReturnToMenu);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<LoadGameRequestedEvent>(OnLoadGame);
            EventBus.Unsubscribe<ReturnToMainMenuRequestedEvent>(OnReturnToMenu);
        }

        private void OnLoadGame(LoadGameRequestedEvent e) =>
            StartCoroutine(SwapScene(SceneNames.Gameplay));

        private void OnReturnToMenu(ReturnToMainMenuRequestedEvent e) =>
            StartCoroutine(SwapScene(SceneNames.MainMenu));

        private IEnumerator SwapScene(string targetScene)
        {
            string previousScene = currentSceneName;

            Time.timeScale = 1f;

            var loadOp = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
            if (loadOp == null)
            {
                Debug.LogError($"Impossible to load: '{targetScene}'---> check in Build Settings.");
                yield break;
            }
            yield return loadOp;

            var unloadOp = SceneManager.UnloadSceneAsync(previousScene);
            if (unloadOp != null) yield return unloadOp;

            currentSceneName = targetScene;
            EventBus.Publish(new SceneTransitionCompletedEvent(targetScene));
        }
    }
}