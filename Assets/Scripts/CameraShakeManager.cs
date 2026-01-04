using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager Instance { get; private set; }

    private CinemachineVirtualCamera _currentVCam;
    private CinemachineBasicMultiChannelPerlin _noiseComponent;
    
    private Coroutine _shakeCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Shake(float intensity, float duration)
    {
        if (_currentVCam == null || _noiseComponent == null || !_currentVCam.gameObject.activeInHierarchy)
        {
            FindCurrentCamera();
        }

        if (_noiseComponent != null)
        {
            if (_shakeCoroutine != null) StopCoroutine(_shakeCoroutine);

            _noiseComponent.m_AmplitudeGain = 0f; 

            if (_currentVCam != null)
            {
                _currentVCam.transform.rotation = Quaternion.identity; 
            }

            _shakeCoroutine = StartCoroutine(ProcessShake(intensity, duration));
        }
    }

    IEnumerator ProcessShake(float intensity, float duration)
    {
        _noiseComponent.m_AmplitudeGain = intensity;

        yield return new WaitForSecondsRealtime(duration);

        _noiseComponent.m_AmplitudeGain = 0f;
        
        if (_currentVCam != null)
        {
            _currentVCam.transform.rotation = Quaternion.identity;
        }
    }

    void FindCurrentCamera()
    {
        if (Camera.main != null)
        {
            var brain = Camera.main.GetComponent<CinemachineBrain>();
            if (brain != null && brain.ActiveVirtualCamera != null)
            {
                _currentVCam = brain.ActiveVirtualCamera as CinemachineVirtualCamera;
            }
        }

        if (_currentVCam == null)
        {
            _currentVCam = FindObjectOfType<CinemachineVirtualCamera>();
        }

        if (_currentVCam != null)
        {
            _noiseComponent = _currentVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }
}