using UnityEngine;

public class PerlinTexture : MonoBehaviour
{
    //private const float OFFSET = 0.5f;
    
    [SerializeField] private Texture2D _texture;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] [Range (0.0000001f, 3f)] private float _scale = 1f;
    private float _oldScale = -1f;


    private void Awake()
    {
        _texture = new Texture2D(100, 100);
        _renderer.material.mainTexture = _texture;
    }

    private void Update()
    {
        _oldScale = _scale;

        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                float grayLevel = Mathf.PerlinNoise(x*_scale+Time.time, y*_scale+Time.time);
                _texture.SetPixel(x, y, new Color(grayLevel, grayLevel,grayLevel));
            }
        }


        _texture.Apply();
    }
}
