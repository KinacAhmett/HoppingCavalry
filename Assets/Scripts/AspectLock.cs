using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AspectLock : MonoBehaviour
{
    [SerializeField] private float _targetAspect = 9f / 16f;   // dikey oran

    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        float windowAspect = (float)Screen.width / Screen.height;

        if (windowAspect > _targetAspect)
        {
            // Pencere çok geniş: ortada dikey bir şerit çiz, iki yan boş kalsın
            float w = _targetAspect / windowAspect;
            _cam.rect = new Rect((1f - w) / 2f, 0f, w, 1f);
        }
        else
        {
            // Pencere çok dar/uzun: ortada yatay bir şerit çiz, üst ve alt boş kalsın
            float h = windowAspect / _targetAspect;
            _cam.rect = new Rect(0f, (1f - h) / 2f, 1f, h);
        }
    }
}