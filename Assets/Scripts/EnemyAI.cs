using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;

    // ชื่อของเลน (สำหรับ NavMesh Area)
    private string[] laneNames = { "LaneLeft", "LaneMiddle", "LaneRight" };

    // *** เพิ่ม: ตำแหน่งแกน X ของแต่ละเลน (ตามที่คุณวาง Plane ไว้ที่ -3, 0, 3) ***
    private float[] laneXPositions = { -3f, 0f, 3f };

    private int currentLaneIndex = 1; // เริ่มที่เลนกลาง (Index 1)

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(SwitchLaneRoutine());
    }

    void Update()
    {
        // คำนวณจุดหมายปลายทางใหม่
        // แกน X: ดึงมาจากค่าตำแหน่งกลางของเลนปัจจุบัน (laneXPositions)
        // แกน Z: เอาตำแหน่งปัจจุบันของศัตรู + ไปข้างหน้า 10 เมตร
        Vector3 targetPosition = new Vector3(
            laneXPositions[currentLaneIndex],
            transform.position.y,
            transform.position.z + 10f
        );

        agent.SetDestination(targetPosition);
    }

    IEnumerator SwitchLaneRoutine()
    {
        while (true)
        {
            // 1. รอ 5 วินาที (ให้วิ่งในเลนเดิมไปก่อน)
            yield return new WaitForSeconds(5f);

            // 2. คำนวณเลนถัดไป
            currentLaneIndex = (currentLaneIndex + 1) % laneNames.Length;

            // 3. เปลี่ยน Area Mask ตามกติกา Lab (เพื่อให้ NavMesh รู้ว่าตอนนี้อยู่เลนไหน)
            string laneName = laneNames[currentLaneIndex];
            int laneID = NavMesh.GetAreaFromName(laneName);
            agent.areaMask = 1 << laneID;

            // 4. *** ส่วนที่เพิ่มมา: สั่งวาร์ป (Instant Teleport) ***
            // สร้างตำแหน่งใหม่: เอา X ของเลนใหม่ + Y และ Z เดิมของตัวศัตรู
            Vector3 warpPosition = new Vector3(
                laneXPositions[currentLaneIndex],
                transform.position.y,
                transform.position.z
            );

            agent.Warp(warpPosition); // ย้ายร่างทันที!
        }
    }
}
