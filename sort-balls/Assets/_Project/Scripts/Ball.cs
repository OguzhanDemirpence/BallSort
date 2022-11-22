using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Renderer renderer;

    public EColor ColorType { get; private set; }
    
    public void Init(Material ballMaterial, EColor colorType)
    {
        renderer.material = ballMaterial;
        ColorType = colorType;
    }
}
