using UnityEngine;

public class CharacterInputs : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    
    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
    }
}