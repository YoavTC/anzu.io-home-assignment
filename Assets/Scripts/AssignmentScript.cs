using UnityEngine;

public class AssignmentScript : MonoBehaviour
{
    //To enable the Texture2D inspection button, just add the [InspectTexture] attribute
    [InspectTexture] public Texture2D publicTexture;
    [InspectTexture] [SerializeField] private Texture2D privateTexture;
    public Texture2D nonInspectableTexture;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}