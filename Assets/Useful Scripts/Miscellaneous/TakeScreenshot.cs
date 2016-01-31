using UnityEngine;
using System.Collections;

namespace UsefulThings {
    public class TakeScreenshot : MonoBehaviour {

        public KeyCode screenshotKey;

        void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        void Update() {
            if (Input.GetKeyDown(screenshotKey)) {
                Application.CaptureScreenshot(Application.dataPath + "/../Screenshots/screenshot_" + System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".png");
            }
        }
    }
}
