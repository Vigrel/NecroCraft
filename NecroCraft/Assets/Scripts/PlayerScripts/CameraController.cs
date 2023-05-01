using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class CameraController : MonoBehaviour
    {
        private Vector3 _offset;

        // Start is called before the first frame update
        void Start()
        {
            _offset = transform.position - PlayerController.Instance.Position;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = PlayerController.Instance.Position + _offset;
        }
    }
}