using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] private AudioSource characterAudio;

    public void WalkingStepAudio()
    {
        characterAudio.Play();
    }
}