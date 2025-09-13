using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Speed at which the player moves.
    public float speed = 5.0f; // Set a default speed
    // UI Text component to display the count of "Pickup" objects called 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // Rigidbody of the player
    private Rigidbody rb;
    // Variable to keep track of collected "Pickup" objects.
    private int count;
    // Movement along the X & Y axes.
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        SetCountText();
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = $"Count: {count}";
        if (count >= 8)
        {
            winTextObject.SetActive(true);

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Disable the player object instead of destroying it
            gameObject.SetActive(false);
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You LOSE!";
        }
    }
}