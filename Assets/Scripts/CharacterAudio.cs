using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] private AudioSource characterAudio;

    private void WalkingStepAudio()
    {
        characterAudio.Play();
    }
}