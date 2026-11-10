using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundGradient : MonoBehaviour
{
    [SerializeField] private Color _topColor = new Color(0.20f, 0.45f, 0.85f);    // koyu mavi
    [SerializeField] private Color _bottomColor = new Color(0.65f, 0.88f, 1.00f); // açık mavi

    private void Start()
    {
        // 1 piksel genişliğinde, 256 piksel yüksekliğinde bir doku oluştur
        Texture2D texture = new Texture2D(1, 256);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Bilinear;

        // Alttan üste doğru iki renk arasında geçiş yap
        for (int y = 0; y < 256; y++)
        {
            texture.SetPixel(0, y, Color.Lerp(_bottomColor, _topColor, y / 255f));
        }
        texture.Apply();

        // Dokudan bir sprite oluştur ve kendine ata
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 1, 256), new Vector2(0.5f, 0.5f), 1f);
        GetComponent<SpriteRenderer>().sprite = sprite;

        // Kameranın gördüğü alanı tamamen kaplayacak şekilde ölçekle
        Camera cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;
        transform.localScale = new Vector3(width, height / 256f, 1f);
    }
}