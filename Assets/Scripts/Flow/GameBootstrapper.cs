using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core;

namespace Flow
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IEnumerator Start()
        {
            SaveSystem.Initialize();

            yield return SceneManager.LoadSceneAsync(SceneNames.UI, LoadSceneMode.Additive);
            yield return SceneManager.LoadSceneAsync(SceneNames.MainMenu, LoadSceneMode.Additive);
        }
    }
}