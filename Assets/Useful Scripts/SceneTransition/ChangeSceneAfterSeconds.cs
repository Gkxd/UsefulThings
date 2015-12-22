using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UsefulThings {
    public class ChangeSceneAfterSeconds : MonoBehaviour {

        [Tooltip("The name of the scene to load.\n\nIf left blank, the current scene will be reloaded.")]
        public string sceneName;

        [Tooltip("How many seconds to wait before loading the scene.\n\nUseful for coordinating with transitions.")]
        public float delay;

        void Start() {
            StartCoroutine(changeScene());
        }

        private IEnumerator changeScene() {
            yield return new WaitForSeconds(delay);
            if (sceneName == "") {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}