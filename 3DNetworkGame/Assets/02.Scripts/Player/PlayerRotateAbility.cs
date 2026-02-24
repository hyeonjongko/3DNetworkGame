using Unity.Cinemachine;
using UnityEngine;

public class PlayerRotateAbility : PlayerAbility
{
    public Transform CameraRoot; // ��

    private float _mx;
    private float _my;

    private void Start()
    {
        // �̰� ���濡�� ���� ��ġ�� ���� ���׸� ���� ����Ű�� ���
        if (!_owner.PhotonView.IsMine) return;

        Cursor.lockState = CursorLockMode.Locked;


        if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

        CinemachineCamera vcam = GameObject.Find("FollowCamera").GetComponent<CinemachineCamera>();
        vcam.Follow = CameraRoot.transform;

        CopyPosition minimapCopy = FindObjectOfType<CopyPosition>();
        if (minimapCopy != null)
            minimapCopy.SetTarget(transform);
    }

    private void Update()
    {
        if (!_owner.PhotonView.IsMine) return;

        _mx += Input.GetAxis("Mouse X") * _owner.Stat.RotationSpeed * Time.deltaTime;
        _my += Input.GetAxis("Mouse Y") * _owner.Stat.RotationSpeed * Time.deltaTime;

        _my = Mathf.Clamp(_my, -90f, 90f);

        transform.eulerAngles = new Vector3(0f, _mx, 0f);
        CameraRoot.localRotation = Quaternion.Euler(-_my, 0f, 0f);

        if (Input.GetKey(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
