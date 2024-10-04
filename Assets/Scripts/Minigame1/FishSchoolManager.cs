using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchoolManager : MonoBehaviour
{
    public GameObject[] units;
    public GameObject unitPrefab;
    public int unitAmount = 25;
    public Vector3 offsetRange = new Vector3(0.9f, 1f, 1f);
    public float offsetBehindLeadFish = 0f;


    void Awake()
    {
        units = new GameObject[unitAmount];

        if (unitPrefab == null)
        {
            Debug.LogError("Unit Prefab missing");
            return;
        }

        for (int i = 0; i < unitAmount; i++)
        {
            Vector3 unitOffset = new Vector3(Random.Range(-offsetRange.x, offsetRange.x), Random.Range(-offsetRange.y, offsetRange.y) - offsetBehindLeadFish, 0);

            units[i] = Instantiate(unitPrefab, this.transform.position + unitOffset, Quaternion.identity, this.transform) as GameObject;
            units[i].GetComponent<FishSchoolUnit>().manager = this.gameObject;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 gizmoOffset = new Vector3(this.transform.position.x, this.transform.position.y - offsetBehindLeadFish, this.transform.position.z);
        Gizmos.DrawWireCube(gizmoOffset, offsetRange * 2);
    }
}
