using UnityEngine;

public class Controller : MonoBehaviour {
        [SerializeField] private float salto = 5f;
        [SerializeField] private float velocidadPersonaje = 2f;
        [SerializeField] private float velocidadCamara = 3f;
        [SerializeField] private float gravedad = 9.81f;
        [SerializeField] private CharacterController controller;
        [SerializeField] private Vector3 movimientoPersonaje = Vector3.zero;
        [SerializeField] private Vector3 movimientoCamaraX = Vector3.zero;
        [SerializeField] private Vector3 movimientoCamaraY = Vector3.zero;
        [SerializeField] private GameObject camara;
        [SerializeField] private bool camaraInvertida = false;
        [SerializeField] private float rotacionX = 0f;
    
        void Update()
        {
            moverse();
        }
    
        void moverse()
        {
            if (controller.isGrounded)
            {
                movimientoPersonaje = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                movimientoPersonaje = transform.TransformDirection(movimientoPersonaje);
                movimientoPersonaje *= velocidadPersonaje;
                if (Input.GetButton("Jump"))
                {
                    movimientoPersonaje.y = salto;
                }
            }
            movimientoPersonaje.y -= gravedad * Time.deltaTime;
            controller.Move(movimientoPersonaje * Time.deltaTime);
    
            movimientoCamaraX = new Vector3(0, Input.GetAxis("Mouse X"), 0);
            movimientoCamaraX *= velocidadCamara;
            transform.Rotate(movimientoCamaraX);
    
            float mouseIndexY = Input.GetAxis("Mouse Y") * velocidadCamara;
            if (camaraInvertida)
            {
                rotacionX += mouseIndexY;
            }
            else
            {
                rotacionX -= mouseIndexY;
            }
            // Limitamos la rotación vertical entre -90 y 90 grados (o -85 y 85 para evitar problemas de gimbal lock)
            rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);
    
            // Aplicamos la rotación local a la cámara
            camara.transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        }
}
