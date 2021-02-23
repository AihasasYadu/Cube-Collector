using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private Camera cam;

    private float maxDrag = 10;
    private float minDrag = 0;
    private CollectibleType lastCollectibleType;
    private bool isOnGround;
    private int streak;
    private Rigidbody rb;
    private void Start()
    {
        isOnGround = false;
        lastCollectibleType = CollectibleType.None;
        streak = 1;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(isOnGround)
            Movement();
    }

    private void LateUpdate()
    {
        cam.transform.localPosition = gameObject.transform.localPosition + new Vector3(0, 20, 0);
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);

        if(moveHorizontal == 0 && moveVertical == 0)
        {
            rb.drag = maxDrag;
            rb.sleepThreshold = 0.01f;
        }
        else
        {
            rb.drag = minDrag;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        CollectibleController temp = collision.gameObject.GetComponent<CollectibleController>();
        if(temp != null)
        {
            if(temp.GetCollectibleType.Equals(lastCollectibleType))
            {
                streak++;
                EventManager.Instance.StreakEvent(streak);
            }
            else
            {
                lastCollectibleType = temp.GetCollectibleType;
                streak = 1;
                EventManager.Instance.StreakEvent(streak);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<MeshCollider>())
        {
            isOnGround = true;
        }
    }
}
