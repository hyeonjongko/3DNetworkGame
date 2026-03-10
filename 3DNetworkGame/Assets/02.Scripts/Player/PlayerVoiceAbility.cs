using Photon.Voice.PUN;
using Photon.Voice.Unity;
using UnityEngine;
using UnityEngine.Profiling;
using Recorder = Photon.Voice.Unity.Recorder;

public class PlayerVoiceAbility : PlayerAbility
{
    public GameObject SpeakIcon;

    [SerializeField] private PhotonVoiceView _voiceView;

    private Recorder _recorder;

    private void Start()
    {
        _recorder = FindAnyObjectByType<Recorder>();

        _recorder.VoiceDetection = true;
        _recorder.VoiceDetectionThreshold = 0.01f;
        _recorder.VoiceDetectionDelayMs = 300;
    }
    private void Update()
    {
        bool isSpeaking = false;

        if(_owner.PhotonView.IsMine)
        {
            isSpeaking = _recorder.IsCurrentlyTransmitting;
        }
        else
        {
            isSpeaking = _voiceView.IsSpeaking;
        }

        SpeakIcon.gameObject.SetActive(isSpeaking);

        if (Input.GetKeyDown(KeyCode.M))
        {
            _recorder.TransmitEnabled = !_recorder.TransmitEnabled;
        }
    }
}
