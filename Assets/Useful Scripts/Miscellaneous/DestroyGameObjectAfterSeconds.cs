using UnityEngine;
using System.Collections;

public class DestroyGameObjectAfterSeconds : MonoBehaviour {
    public float seconds;

    void Start() {
        Destroy(gameObject, seconds);
    }
}
