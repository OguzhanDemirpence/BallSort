using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [SerializeField] private ColorMaterial[] colorMaterials;
    [SerializeField] private ColorMaterial colorMaterial;
    [SerializeField] private Material defaultMaterial;
    

    public Material GetColorMaterial(EColor color)
    {
        for (int i = 0; i < colorMaterials.Length; i++)
        {
            if (colorMaterials[i].color == color) return colorMaterials[i].colorMaterialX;
        }

        return null;
    }
}

[System.Serializable]
public class ColorMaterial
{
    public EColor color;
    public Material colorMaterialX;
}