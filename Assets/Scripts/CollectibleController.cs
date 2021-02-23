using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private const int PLAYER_LAYER = 8;
    private Color color;
    private int points;
    private CollectibleType currentCollectibleType;

    public CollectibleType GetCollectibleType { get { return currentCollectibleType; } }
    public void Initialize(CollectibleScriptableObject sc)
    {
        color = sc.color;
        points = sc.points;
        currentCollectibleType = sc.type;
    }

    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer.Equals(PLAYER_LAYER))
        {
            EventManager.Instance.CollectEvent(points);
            Destroy(gameObject);
            CollectibleService.Instance.RemoveDestroyed(this);
        }
    }
}
