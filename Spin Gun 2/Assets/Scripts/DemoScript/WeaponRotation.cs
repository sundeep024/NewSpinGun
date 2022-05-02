using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _weaponRB;
    [SerializeField] private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _weaponRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var impulse = (_speed * Mathf.Deg2Rad) * _weaponRB.inertia;
        _weaponRB.AddTorque(impulse, ForceMode2D.Force);
    }
}
