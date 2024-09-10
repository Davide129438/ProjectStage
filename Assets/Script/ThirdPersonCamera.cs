using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Il personaggio che la telecamera seguirà
    public float distance = 5.0f; // Distanza dal personaggio
    public float height = 2.0f; // Altezza della camera rispetto al personaggio
    public float rotationSpeed = 200.0f; // Sensibilità di rotazione della telecamera

    private float currentX = 0.0f;
    private float currentY = 45.0f; // Imposta un angolo iniziale più alto
    private float minYAngle = 20.0f; // Imposta un limite minimo per mantenere la telecamera dall'alto
    private float maxYAngle = 80.0f; // Imposta un limite massimo per evitare un angolo troppo ripido

    private bool isPlayerMoving = false; // Flag per controllare se il player si sta muovendo
    private Vector3 initialCameraPosition; // Posizione iniziale della camera

    void Start()
    {
        // Salva la posizione iniziale della camera
        initialCameraPosition = transform.position;

        // Blocca la rotazione iniziale della camera in base alla posizione del player
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion initialRotation = Quaternion.LookRotation(direction);
        transform.rotation = initialRotation;
    }

    void Update()
    {
        // Verifica se il player sta premendo i tasti di movimento
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isPlayerMoving = true; // Il giocatore si sta muovendo
        }

        // Solo se il player si sta muovendo, la telecamera può ruotare
        if (isPlayerMoving)
        {
            currentX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            // Limita l'angolo verticale della camera per mantenere la visuale dall'alto
            currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
        }
    }

    void LateUpdate()
    {
        if (isPlayerMoving)
        {
            // Calcola la rotazione della telecamera in base agli input
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

            // Posiziona la telecamera dietro il personaggio mantenendo la distanza e l'altezza
            Vector3 position = target.position - (rotation * Vector3.forward * distance + Vector3.up * height);
            transform.position = position;

            // La telecamera guarda leggermente sopra il centro del target
            transform.LookAt(target.position + Vector3.up * height * 0.5f);
        }
        else
        {
            // Mantieni la posizione e rotazione iniziale fino a quando il player non si muove
            transform.position = initialCameraPosition;
        }
    }
}
