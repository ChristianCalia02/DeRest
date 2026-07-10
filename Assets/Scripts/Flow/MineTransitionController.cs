using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core;

namespace Flow
{
    public class MineTransitionController : MonoBehaviour
    {
        private bool isInMine;

        private void OnEnable()
        {
            EventBus.Subscribe<EnterMineRequestedEvent>(OnEnterMineRequested);
            EventBus.Subscribe<ExitMineRequestedEvent>(OnExitMineRequested);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<EnterMineRequestedEvent>(OnEnterMineRequested);
            EventBus.Unsubscribe<ExitMineRequestedEvent>(OnExitMineRequested);
        }

        private void OnEnterMineRequested(EnterMineRequestedEvent e)
        {
            if (isInMine) return;
            StartCoroutine(EnterMineRoutine());
        }

        private void OnExitMineRequested(ExitMineRequestedEvent e)
        {
            if (!isInMine) return;
            StartCoroutine(ExitMineRoutine());
        }

        private IEnumerator EnterMineRoutine()
        {
            yield return SceneManager.LoadSceneAsync(SceneNames.Mine, LoadSceneMode.Additive);
            isInMine = true;
            EventBus.Publish(new GameplayModeChangedEvent(GameplayMode.Mine));
        }

        private IEnumerator ExitMineRoutine()
        {
            yield return SceneManager.UnloadSceneAsync(SceneNames.Mine);
            isInMine = false;
            EventBus.Publish(new GameplayModeChangedEvent(GameplayMode.Surface));
        }
    }
}