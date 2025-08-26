using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    private int ChickenCurrent;

    [SerializeField] private GameObject ChickenPrefab;
    [SerializeField] private Transform GridChicken;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 8;
    [SerializeField] private GameObject Boss;

    private Vector3 startPos;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Tính kích thước màn hình
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Screen.width / Screen.height;

        // Điểm bắt đầu spawn: góc phải trên màn hình
        startPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        startPos.z = 0;
        startPos.y -= gridSize;       // đẩy xuống 1 ô để không dính mép trên
        startPos.x -= width / 2f;     // căn trái cho vừa màn hình

        // Gọi hàm spawn
        SpawnChicken(rows, columns);
    }

    void SpawnChicken(int rowCount, int colCount)
    {
        Vector3 pos = startPos;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                GameObject chicken = Instantiate(ChickenPrefab, pos, Quaternion.identity);
                chicken.transform.SetParent(GridChicken, false);
                pos.x += gridSize;
                ChickenCurrent++;
            }

            pos.x = startPos.x;   // reset lại X
            pos.y -= gridSize;    // xuống dòng mới
        }
    }

    public void DereaChicken()
    {
        ChickenCurrent--;

        if (ChickenCurrent <= 0)
        {
            Boss.gameObject.SetActive(true);
        }
    }
}