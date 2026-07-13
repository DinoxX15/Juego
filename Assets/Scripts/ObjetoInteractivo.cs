using UnityEditor;
using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour, IInteractuable {
    [SerializeField] private ItemData datos;

    public void Interactuar(GameObject gameObject) {
        
    }

    public ItemData Datos => datos;
}
