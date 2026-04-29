using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    public GameObject UI; // ลาก Canvas ที่มีปุ่ม Play มาใส่
    public Segment segmentScript;    // ลาก Object ที่ใส่สคริปต์ Segment มาใส่
    public MonoBehaviour playerControl; // ลากตัวละคร (สคริปต์วิ่ง) มาใส่
    
    public void StartGame()
    {
        // 1. ปิดหน้าจอเมนู
        UI.SetActive(false);

        // 2. เริ่มการสร้างด่าน
        segmentScript.enabled = true;

        PlayerController player = playerControl.GetComponent<PlayerController>();
        if (player != null) 
        {
            player.canMove = true; 
        }   
    }
}