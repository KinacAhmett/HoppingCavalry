using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform _player;

    private void LateUpdate()
    {
        if (transform.position.y < _player.position.y)
    {
        transform.position = new Vector3(transform.position.x, _player.position.y, transform.position.z);
    }
    }
   
}
