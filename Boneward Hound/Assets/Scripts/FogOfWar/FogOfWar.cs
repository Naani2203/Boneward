using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField]
    private GameObject _FogOfWarPlane;
    [SerializeField]
    private Transform _Player;
    public LayerMask _FogLayer;
    [SerializeField]
    private float _Radius;
    [SerializeField]
    private float _SquaredRadius{get{return _Radius*_Radius;}}
    [SerializeField]
    private Color _PlaneColor;

    private Mesh _Mesh;
    private Vector3[] _Vertices;
    private Color[] _Colors; 
    
    private void Start() 
    {
        Initialize();
    }

    void Update()
    {
        Ray ray= new Ray(transform.position,_Player.position-transform.position);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,1000,_FogLayer,QueryTriggerInteraction.Collide))
        {
            for(int i=0;i<_Vertices.Length;i++)
            {
                Vector3 v = _FogOfWarPlane.transform.TransformPoint(_Vertices[i]);
                float distance = Vector3.SqrMagnitude(v - hit.point);
                if(distance<_SquaredRadius)
                {
                    float alpha = Mathf.Min(_Colors[i].a, distance/_SquaredRadius);
                    _Colors[i].a = alpha;
                }
            }
            UpdateColor();
        }
        
    }
    private void Initialize()
    {
        _Mesh=_FogOfWarPlane.GetComponent<MeshFilter>().mesh;
        _Vertices=_Mesh.vertices;
        _Colors=new Color[_Vertices.Length];
        for(int i=0;i<_Vertices.Length;i++)
        {
            _Colors[i] = _PlaneColor;
        }
        UpdateColor();
    }

    private void UpdateColor()
    {
        _Mesh.colors=_Colors;
    }
}
