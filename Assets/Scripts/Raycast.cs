using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Raycast : MonoBehaviour
{
    [SerializeField] private float rayDistance = 3f;
    public LayerMask layerMask;
    public GameObject panelInteractuar;
    public GameObject panelAbrir;
    public GameObject panelCerrar;
    public Transform manoDerecha;
    public TextMeshProUGUI btnAccion;
    public TextMeshProUGUI Accion;
    GameObject objetoRecogido;
    public bool objetoAgarrado = false;
    public RaycastHit hit;
    public Renderer rendererActual;
    public Outline objetoMirado;
    public Color colorOriginal;
    


    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        
        // Dibuja el rayo en la vista de escena (Scene) para depuración
        Debug.DrawRay(origin, direction * rayDistance, Color.red);

        if (Physics.Raycast(origin, direction, out hit, rayDistance, layerMask))
        {
            Debug.Log("Raycast impactó: " + hit.collider.name + " en la capa: " + LayerMask.LayerToName(hit.collider.gameObject.layer));

            if (hit.collider.gameObject.CompareTag("recogible")) {
                Outline objetoMiradoActual = hit.collider.gameObject.GetComponentInParent<Outline>();
                ObjetoRecogible objetoRecogible = hit.collider.gameObject.GetComponent<ObjetoRecogible>();
    
                if (objetoMiradoActual != null) {
                    if (objetoMirado != objetoMiradoActual) {
                        apagarOutline();
                        objetoMirado = objetoMiradoActual;
                        objetoMirado.enabled = true;
                        btnAccion.text = hit.collider.gameObject.GetComponent<ObjetoInteractivo>().Datos.BtnAccion;
                        Accion.text = hit.collider.gameObject.GetComponent<ObjetoInteractivo>().Datos.Accion;
                        panelInteractuar.SetActive(true);
                    }
        
                    // Inputs fuera del if de cambio de objeto
                    if (Input.GetKeyDown(KeyCode.E) && objetoRecogible != null) {
                        objetoRecogible.Interactuar(manoDerecha);
                        objetoAgarrado = true;
                        objetoRecogido = objetoRecogible.gameObject;
                        Debug.Log("Objeto recogido: " + objetoRecogido);
                        apagarOutline();
                        panelInteractuar.SetActive(false);
                    } 
                    
                } else if (objetoAgarrado) {
                    objetoRecogido.gameObject.GetComponent<Outline>().enabled = false;
                }
                else {
                    Debug.Log("NO se encontró componente Outline en " + hit.collider.name + " ni en sus padres.");
                    apagarOutline();
                    panelInteractuar.SetActive(false);
                }
            }
            
        } 
        else {
            apagarOutline();
            panelInteractuar.SetActive(false);
        }
    }
    
    void apagarOutline() {
        if (objetoMirado != null) {
            objetoMirado.enabled = false;
            objetoMirado = null;
        }
    }
    
    void agarrarObjeto (GameObject objeto)
    {   
        objetoRecogido = objeto;
        objetoRecogido.transform.SetParent(manoDerecha.transform);
        objetoRecogido.transform.localPosition = Vector3.zero;
        objetoRecogido.transform.localRotation = Quaternion.identity;
        objetoRecogido.GetComponent<Rigidbody>().useGravity = false;
        objetoRecogido.GetComponent<Rigidbody>().isKinematic = true;
        
    }

    void soltarObjeto (GameObject objeto)
    {
        objeto.transform.SetParent(null);
        objeto.GetComponent<Rigidbody>().useGravity = true;
        objeto.GetComponent<Rigidbody>().isKinematic = false;
    }
}
