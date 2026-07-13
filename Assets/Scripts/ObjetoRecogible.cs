using System;
using UnityEngine;

public class ObjetoRecogible : MonoBehaviour {
    [SerializeField] private ItemData datos;
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void Interactuar(Transform mano) {
        transform.SetParent(mano);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void soltar() {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.isKinematic = false;
    }
    
}
