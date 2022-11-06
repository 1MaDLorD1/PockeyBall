using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float _maxJumpForce;
    [SerializeField] private Stick _stick;
    [SerializeField] private TMP_Text _text;

    private float _jumpForce = 0;
    private Rigidbody _rigidBody;
    private bool isIncrease = true;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _text.text = $"Power = {(int)_jumpForce}";

        if (Input.GetMouseButtonDown(0))
        {
            _jumpForce = 0;

            Ray ray = new Ray(transform.position, Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out Block block))
                {
                    _rigidBody.isKinematic = false;
                }
                else if (hitInfo.collider.TryGetComponent(out Segment segment))
                {
                    _stick.gameObject.SetActive(true);
                    _rigidBody.isKinematic = true;
                    _rigidBody.velocity = Vector3.zero;
                }
                else if (hitInfo.collider.TryGetComponent(out Finish finish))
                {
                    _stick.gameObject.SetActive(true);
                    _rigidBody.isKinematic = true;
                    _rigidBody.velocity = Vector3.zero;
                    Debug.Log("You Win!");
                    Time.timeScale = 0;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (isIncrease)
            {
                _jumpForce += 5f * Time.deltaTime;
                if (_jumpForce >= _maxJumpForce)
                {
                    isIncrease = false;
                }
            }
            else
            {
                _jumpForce -= 5f * Time.deltaTime;
                if (_jumpForce <= 0)
                {
                    isIncrease = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _stick.gameObject.SetActive(false);

            _rigidBody.isKinematic = false;
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
