using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMinimap : MonoBehaviour
{
    [SerializeField] private Camera _minimapCamera;
    [SerializeField] private float _zoomMin = 1f;
    [SerializeField] private float _zoomMax = 30f;
    [SerializeField] private float _zoomOneStep = 1f;
    [SerializeField] private TextMeshProUGUI _textMapName;

    private void Awake()
    {
        _textMapName.text = SceneManager.GetActiveScene().name;
    }

    public void ZoomIn()
    {
        _minimapCamera.orthographicSize = Mathf.Max(_minimapCamera.orthographicSize-_zoomOneStep, _zoomMin);
    }
    public void ZoomOut()
    {
        _minimapCamera.orthographicSize = Mathf.Min(_minimapCamera.orthographicSize + _zoomOneStep, _zoomMax);
    }
}
