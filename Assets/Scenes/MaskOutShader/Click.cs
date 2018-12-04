using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

    Camera _cam;
    RaycastHit _hit;
    Ray _ray;
    Vector3 _mousePos, _smoothPoint;

    public float _radius, _softness, _smoothSpeed, _scaleFactor;


    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _radius += _scaleFactor * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _radius -= _scaleFactor * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _softness -= _scaleFactor * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _softness += _scaleFactor * Time.deltaTime;
        }

        _radius = Mathf.Clamp(_radius, 0, 100);
        _softness = Mathf.Clamp(_softness, 0, 100);

        _mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        _ray = _cam.ScreenPointToRay(_mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_ray, out _hit))
            {
                //_smoothPoint = Vector3.MoveTowards(_smoothPoint, _hit.point, _smoothSpeed * Time.deltaTime);
                _smoothPoint = _hit.point;
                Vector4 pos = new Vector4(_smoothPoint.x, _smoothPoint.y, _smoothPoint.z);
                Shader.SetGlobalVector("_World_Position", pos);
            }
        }
        
        Shader.SetGlobalFloat("_Sphere_Radius", _radius);
        Shader.SetGlobalFloat("_Sphere_Softness", _softness);
    }

}
