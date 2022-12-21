﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceRadius);

        int nearbyResourceAmount = 0;
        foreach (Collider2D collider in colliderArray)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType) { nearbyResourceAmount++; }

            }
        }
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }
    private float timer;
    private float timerMax;
    private ResourceGeneratorData resourceGeneratorData;
    

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
    }
    private void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);

        if (nearbyResourceAmount == 0) { enabled = false; }
        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) + 
                resourceGeneratorData.timerMax * (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount); 
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer += timerMax;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSe()
    {
        return 1 / timerMax;
    }
}
