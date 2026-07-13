using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData", order = 1)]
public class ItemData : ScriptableObject {
    [SerializeField] private string nombre;
    [SerializeField] private string descripcion;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string btnAccion;
    [SerializeField] private string accion;

    public string Nombre {
        get => nombre;
        set => nombre = value;
    }

    public string Descripcion {
        get => descripcion;
        set => descripcion = value;
    }

    public Sprite Sprite {
        get => sprite;
        set => sprite = value;
    }

    public string BtnAccion {
        get => btnAccion;
        set => btnAccion = value;
    }

    public string Accion {
        get => accion;
        set => accion = value;
    }
}
