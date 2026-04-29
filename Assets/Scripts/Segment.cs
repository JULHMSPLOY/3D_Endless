using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Segment : MonoBehaviour
{
    public GameObject[] segmentMap;  
    [SerializeField] private int zPos = 20;
    private List<GameObject> activeSegments = new List<GameObject>();
    public Transform player; 
    private int maxSegments = 5;  

    // ปิดการทำงานไว้ก่อนจนกว่าจะกด Play
    void Start()
    {
        this.enabled = false; 
    }

    void Update()
    {
        // แก้ไข Warning โดยการเอา boolean ที่ไม่ได้ใช้ออก หรือใช้เช็คระยะห่างแทน
        if (activeSegments.Count < maxSegments)
        {
            GenerateSegment();
        }

        RemovePassedSegments();
    }

    void GenerateSegment()
    {
        // สร้างด่านใหม่ทันที ไม่ต้องใช้ Coroutine เพื่อความแม่นยำของตำแหน่ง zPos
        GameObject newSegment = Instantiate(segmentMap[Random.Range(0, segmentMap.Length)], new Vector3(0, 0, zPos), Quaternion.identity);
        activeSegments.Add(newSegment);
        zPos += 20;
    }

    void RemovePassedSegments()
    {
        if (activeSegments.Count > 0 && activeSegments[0].transform.position.z < player.position.z - 20)
        {
            Destroy(activeSegments[0]);
            activeSegments.RemoveAt(0);
        }
    }
}