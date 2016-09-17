using UnityEngine;
using System.Collections;

namespace UsefulThings
{
    /// <summary>
    /// This class handles sound effects.
    /// Sound effects can be played from any script by calling PlaySfx with the appropriate ID.
    /// IDs are indexes into the sfxBank array. Check the inspector to find a specific sound effect ID;
    /// </summary>
    public class SfxManager : MonoBehaviour
    {
        private static SfxManager instance;

        public AudioClip[] sfxBank;
        private GameObject[] sfxObjects;

        private bool[] sfxLocks;

        void Start()
        {
            instance = this;

            sfxObjects = new GameObject[sfxBank.Length];

            for (int i = 0; i < sfxBank.Length; i++)
            {
                if (sfxBank[i])
                {
                    GameObject sfx = new GameObject(sfxBank[i].name);
                    sfx.SetActive(false);
                    sfx.transform.parent = transform;
                    AudioSource audioSource = sfx.AddComponent<AudioSource>();
                    audioSource.clip = sfxBank[i];
                    sfxObjects[i] = sfx;
                }
            }

            sfxLocks = new bool[sfxBank.Length];
        }

        /// <summary>
        /// Plays a sound effect.
        /// </summary>
        /// <param name="sfxId">The index of the sound effect in the sfxBank array.</param>
        /// <param name="volume">The volume to play the sound effect at. (Default 1) </param>
        /// <param name="delayPercent">Prevents another instance of the sound effect from playing until a certain percent of the current sound effect has played. (Default 0)</param>
        public static void PlaySfx(int sfxId, float volume = 1, float delayPercent = 0)
        {
            if (instance) instance.playSfx(sfxId, volume, delayPercent);
        }

        private void playSfx(int sfxId, float volume, float delayPercent)
        {
            if (sfxId >= sfxBank.Length || sfxBank[sfxId] == null) return;

            if (delayPercent > 0)
            {
                if (!sfxLocks[sfxId])
                {
                    sfxLocks[sfxId] = true;
                    GameObject sfx = (GameObject)Instantiate(sfxObjects[sfxId], transform);
                    sfx.SetActive(true);

                    AudioSource source = sfx.GetComponent<AudioSource>();
                    source.volume = volume;
                    Destroy(sfx, source.clip.length + 0.1f);

                    StartCoroutine(UnlockSfx(sfxId, sfx.GetComponent<AudioSource>().clip.length * delayPercent));
                }
            }
            else
            {
                GameObject sfx = (GameObject)Instantiate(sfxObjects[sfxId], transform);
                sfx.SetActive(true);

                AudioSource source = sfx.GetComponent<AudioSource>();
                source.volume = volume;
                Destroy(sfx, source.clip.length + 0.1f);
            }
        }

        private IEnumerator UnlockSfx(int sfxId, float time)
        {
            yield return new WaitForSeconds(time);
            sfxLocks[sfxId] = false;
        }
    }
}