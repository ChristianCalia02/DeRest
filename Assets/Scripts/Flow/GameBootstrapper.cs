using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flow
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return SceneManager.LoadSceneAsync(SceneNames.UI, LoadSceneMode.Additive);
            yield return SceneManager.LoadSceneAsync(SceneNames.MainMenu, LoadSceneMode.Additive);
        }
    }
}