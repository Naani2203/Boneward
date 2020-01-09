using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _RB;
    [SerializeField]
    private float _BaseMoveAcceleration;
     [SerializeField]
    private float _MaxMoveSpeed;
    private float _HorizontalMove;
    private float _VerticalMove;
    private Vector3 _MovementDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _HorizontalMove = Input.GetAxis("Horizontal");
        _VerticalMove = Input.GetAxis("Vertical");
        _MovementDirection = new Vector3(_HorizontalMove,0,_VerticalMove);
    }
    private void FixedUpdate() 
    {
        MoveDoggo();
        
    }

    private void MoveDoggo()
    {
        if(_MovementDirection.sqrMagnitude>0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(_MovementDirection),0.15f);
        }
        if(_RB.velocity.magnitude<_MaxMoveSpeed)
        {
            _RB.AddForce(_MovementDirection.normalized*_BaseMoveAcceleration,ForceMode.Acceleration);
        }
    }
}
