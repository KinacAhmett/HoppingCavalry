using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _runtimePanel; // Oyun içi arayüz paneli (Skor vb.)
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector2 _playerStartPosition = new Vector2(0, -3f);
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private GameObject _highScoreLabel; // High Score başlığı için

    private PlatformSpawner _platformSpawner;
    private float _currentScore = 0f;
    private float _highScore = 0f;
    private const string HighScoreKey = "HighScore";

    private void Awake()
    {
        _platformSpawner = FindFirstObjectByType<PlatformSpawner>();

        // Kayıtlı yüksek skoru yükle
        _highScore = PlayerPrefs.GetFloat(HighScoreKey, 0);
        _highScoreText.text = ""; // Oyun başında sadece sayı yazılacak
        if (_highScoreLabel != null) _highScoreLabel.SetActive(true);

        // Başlangıç durumu: Menü açık, oyun arayüzü ve oyuncu kapalı
        _menuPanel.SetActive(true);
        _runtimePanel.SetActive(false);
        _player.SetActive(false);
    }

    private void Update()
    {
        if (_player == null || _menuPanel == null || _runtimePanel == null || _currentScoreText == null || _platformSpawner == null)
        {
            Debug.LogError("UI_Manager script'indeki referanslardan biri eksik!");
            return; 
        }

        if (_player.activeSelf)
        {
            // Oyuncu hayattaysa skoru güncelle
            float score = _player.transform.position.y - _playerStartPosition.y;
            _currentScore = Mathf.Max(0, score);
            _currentScoreText.text = $"Score: {_currentScore:F0}";
            // Oyun sırasında high score yazılarını gizle
            _highScoreText.gameObject.SetActive(false);
            if (_highScoreLabel != null) _highScoreLabel.SetActive(false);
        }
        else
        {
            // Oyuncu öldüyse ve menü henüz aktif değilse
            if (!_menuPanel.activeSelf)
            {
                // Yüksek skoru kontrol et ve kaydet
                if (_currentScore > _highScore)
                {
                    _highScore = _currentScore;
                    PlayerPrefs.SetFloat(HighScoreKey, _highScore);
                }

                // High score yazısını sadece sayı olarak güncelle
                _highScoreText.text = _highScore.ToString("F0");

                // Arayüz panellerini değiştir
                _menuPanel.SetActive(true);
                _runtimePanel.SetActive(false);
                _platformSpawner.HandleDeath();
            }
            // Menü paneli aktifken high score yazılarını göster
            if (_menuPanel.activeSelf)
            {
                _highScoreText.gameObject.SetActive(true);
                if (_highScoreLabel != null) _highScoreLabel.SetActive(true);
            }
        }
    }

    public void StartGame()
    {

        // PlatformSpawner referansını her başlatmada tekrar bul
        _platformSpawner = FindFirstObjectByType<PlatformSpawner>();
        if (_player == null || _menuPanel == null || _runtimePanel == null || _platformSpawner == null)
        {
            return;
        }

        // Arayüz panellerini değiştir
        _menuPanel.SetActive(false);
        _runtimePanel.SetActive(true);

        // Oyuncuyu ve oyunu başlat
        _player.SetActive(true);
        _player.transform.position = _playerStartPosition;
        Camera.main.transform.position = new Vector3(0f, 0f, Camera.main.transform.position.z);
        
        _currentScore = 0f;
        _currentScoreText.text = " ";

        _platformSpawner.HandleRespawn();
    }
}