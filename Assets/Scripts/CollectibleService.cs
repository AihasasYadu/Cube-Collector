using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleService : MonoSingletonGeneric<CollectibleService>
{
    [SerializeField] private CollectibleController collectiblePrefab;
    [SerializeField] private List<CollectibleScriptableObject> collectibleProp;

    private List<CollectibleController> collectiblesList;
    private void Start()
    {
        collectiblesList = new List<CollectibleController>();
        collectiblesList.Clear();
        InitializeCollectibles();
        EventManager.GameOver += GameOver;
    }

    private void InitializeCollectibles()
    {
        int enumCount = 0;
        foreach (CollectibleType type in Enum.GetValues(typeof(CollectibleType)))
        {
            enumCount++;
        }

        int randomIndex;
        for (int i = 0; i < 10; i++)
        {
            randomIndex = UnityEngine.Random.Range(0, enumCount - 1);
            CollectibleController instance = Instantiate(collectiblePrefab);
            instance.Initialize(collectibleProp[randomIndex]);
            Transform temp = instance.transform;
            temp = GameManager.Instance.GetRandomCoordinates();
            instance.transform.position = temp.position;
            collectiblesList.Add(instance);
        }
    }

    public void RemoveDestroyed(CollectibleController cube)
    {
        for(int i = 0; i < collectiblesList.Count; i++)
        {
            if(collectiblesList[i] == cube)
            {
                collectiblesList.RemoveAt(i);
            }
        }
        if(collectiblesList.Count == 0)
        {
            EventManager.Instance.GameOverEvent();
        }
    }

    private void GameOver()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventManager.GameOver -= GameOver;
    }
}
