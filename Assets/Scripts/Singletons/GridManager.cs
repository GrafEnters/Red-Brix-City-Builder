using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public Vector2 gridSize; // ������ ����
    public bool isReloadingOnRButton = true;

    public Transform BuildingsHolder, CellsHolder;
    public Transform IslandCube;    
    public GridCell CellPrefab;       
    public Building BuildingPrefab;

    Dictionary<Vector3Int, GridCell> cells;
    Grid Grid;
    bool isSelectedBuildingToConstruct;
    BuildingSO curConstructingBuilding; // ���������, ��������� ��� �������������
    

    private void Awake()
    {
        instance = this;
        Grid = GetComponent<Grid>();
    }

    private void Start()
    {
        GenerateField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isReloadingOnRButton) //��� ������� �� R - ������������� ����� 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // ���������� ������ ���� � ��������� �������
    public void GenerateField()
    {
        GenerateIsland();
        cells = new Dictionary<Vector3Int, GridCell>();
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3Int cellIndex = new Vector3Int(x, 0, y);
                Vector3 cellPosition = Grid.CellToLocal(cellIndex);
                GridCell cell = Instantiate(CellPrefab, cellPosition, Quaternion.identity, CellsHolder);
                cell.GenerateRandomLandshaft();
                cells.Add(cellIndex, cell);
            }
        }

    }

    //�������� ������ ������� �������������� ������� ����
    void GenerateIsland()
    {
        IslandCube.localScale = new Vector3(gridSize.x, IslandCube.localScale.y, gridSize.y);
        IslandCube.localPosition = new Vector3(gridSize.x / 2 - Grid.cellSize.x / 2, IslandCube.localPosition.y, gridSize.y / 2 - Grid.cellSize.y / 2);
    }

    //������� ��� ������, �� ������� ���������
    public void ClearField()
    {
        foreach (var index in cells.Keys)
        {
            Destroy(cells[index].gameObject);
        }
        cells = null;
    }

    public void StartConstructing(BuildingSO building)
    {
        UI.instance.SetStopConstrictingButtonActive(true);
        isSelectedBuildingToConstruct = true;
        curConstructingBuilding = building;
    }

    public void StopConstructing()
    {
        UI.instance.SetStopConstrictingButtonActive(false);
        isSelectedBuildingToConstruct = false;
        curConstructingBuilding = null;
    }

    //���������� �������� � ����� ����� ����� ����������
    //��������� ������������� ��������� ��������� � ������, �� ������� ����� �����
    //���� ������ ���� ������� ��� ������ ��� ������ - ������ �� ������
    //����� ���� ���������� ����� �������
    public void ConstructBuildingAtThisPointOfScreen()
    {
        if (isSelectedBuildingToConstruct)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                Vector3 halfCellSize = Grid.cellSize / 2;
                halfCellSize.y = 0;
                Vector3Int cellIndex = Grid.WorldToCell(hit.point + halfCellSize);

                if (cells.ContainsKey(cellIndex))
                {
                    if (cells[cellIndex].IsEmpty())
                        StartConstructingBuildingAtCell(curConstructingBuilding, cellIndex);
                    else
                        Debug.Log("This cell is already occupied!");
                }

            }
        }
    }

     // ���������� ��������� � ��������� ������ � �������������� �
    public void StartConstructingBuildingAtCell(BuildingSO buildingSO, Vector3Int cellIndex)
    {
        ResourceManager.instance.ChangeWorker(false);
        Building building = Instantiate(BuildingPrefab, cells[cellIndex].transform.position,Quaternion.identity,BuildingsHolder.transform);
        cells[cellIndex].SetHasBuilding(true);
        building.Init(buildingSO);
        ResourceManager.instance.ChangeResources(-buildingSO.price);
        StopConstructing();
    }
}
