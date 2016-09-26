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
        public AudioClip[] loopBank;

        private GameObject[] sfxObjects;
        private GameObject[] loopObjects;

        private bool[] sfxLocks;
        private bool[] loopLocks;

        void Start()
        {
            instance = this;

            sfxObjects = new GameObject[sfxBank.Length];
            loopObjects = new GameObject[loopBank.Length];

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

            for (int i = 0; i < loopBank.Length; i++)
            {
                if (loopBank[i])
                {
                    GameObject loop = new GameObject(loopBank[i].name);
                    loop.SetActive(false);
                    loop.transform.parent = transform;
                    AudioSource audioSource = loop.AddComponent<AudioSource>();
                    audioSource.loop = true;
                    audioSource.clip = loopBank[i];
                    loopObjects[i] = loop;
                }
            }

            sfxLocks = new bool[sfxBank.Length];
            loopLocks = new bool[loopBank.Length];
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

        /// <summary>
        /// Plays a looped sound effect. Only one of each loop can be played at a time.
        /// </summary>
        /// <param name="loopId">The index of the sound effect in the loopBank array.</param>
        /// <param name="volume">The volume to play the sound effect at. (Default 1) </param>
        public static void PlayLoop(int loopId, float volume = 1)
        {
            if (instance) instance.playLoop(loopId, volume);
        }

        private void playLoop(int loopId, float volume)
        {
            if (loopObjects[loopId])
            {
                loopObjects[loopId].SetActive(true);
                loopObjects[loopId].GetComponent<AudioSource>().volume = volume;
            }
        }

        /// <summary>
        /// Stop playing a looped sound effect.
        /// </summary>
        /// <param name="loopId">The index of the sound effect in the loopBank array.</param>
        public static void StopLoop(int loopId)
        {
            if (instance) instance.stopLoop(loopId);
        }

        private void stopLoop(int loopId)
        {
            if (loopObjects[loopId]) loopObjects[loopId].SetActive(false);
        }
    }
}