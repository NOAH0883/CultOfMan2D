//using NUnit.Framework;
using System.Collections.Generic;
//using System.Linq;
//using Unity.Mathematics;
using UnityEngine;
//using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject finalRoomPrefab;
    [SerializeField] GameObject startRoomPrefab;

    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;

    int roomWidth = 18;
    int roomHeight = 10;

    [SerializeField] int gridSizeX = 10;
    [SerializeField] int gridSizeY = 10;

    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private int roomCount;

    private bool generationComplete = false;


    [SerializeField] List<GameObject> roomsPrefab = new List<GameObject>();
            
    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initalRoomIndex = new Vector2Int(gridSizeX/2, gridSizeY/2);
        StartRoomGenerationFromRoom(initalRoomIndex);

    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();


            int gridX = roomIndex.x;
            int gridY = roomIndex.y;


            if (gridX > 0 && roomGrid[gridX - 1, gridY] == 0)
            {
                //No neighbor to the left
                TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            }
            if (gridX < gridSizeX - 1 && roomGrid[gridX + 1, gridY] == 0)
            {
                //No neighbor to the right
                TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            }
            if (gridY > 0 && roomGrid[gridX, gridY - 1] == 0)
            {
                //No neighbor below
                TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
            }
            if (gridY < gridSizeY - 1 && roomGrid[gridX, gridY + 1] == 0)
            {
                //No neighbor above
                TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            }
           
        }
        else if (roomCount < minRooms)
        {
            Debug.Log("not enough rooms, regenerate rooms");
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            Debug.Log($"Generation complete, {roomCount} rooms created!");
            generationComplete = true;
            BossRoom();
        }
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initailRoom = Instantiate(startRoomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initailRoom.name = $"Room-{roomCount}";
        initailRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initailRoom);
        initailRoom.transform.SetParent(this.transform);
    }


    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        
        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            return false;

        if (roomCount >= maxRooms)
            return false;

        if(Random.value < 0.5 && roomIndex != Vector2Int.zero)
            return false;

        //if (CountAdjacentRooms(roomIndex) > 1)
        //    return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        //random room generation
        int rndRoom = Random.Range(0, roomsPrefab.Count);
        

        var newRoom = Instantiate(roomsPrefab[rndRoom], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom);
        newRoom.transform.SetParent(this.transform);

        OpenDoors(newRoom, x, y);

        return true;
    }


    private void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }



    private void BossRoom()
    {
        if (roomObjects.Count == 0) return;

        // get reference to the last room generated - gameObject and script
        GameObject lastRoom = roomObjects[roomObjects.Count - 1];
        Room lastRoomScript = lastRoom.GetComponent<Room>();

        // get roomindex and position of the last room
        Vector2Int gridIndex = lastRoomScript.RoomIndex;
        Vector3 position = lastRoom.transform.position;

        //spawn the bos prefab at the last rooms position and change its name 
        GameObject bossRoom = Instantiate(finalRoomPrefab, position, Quaternion.identity);
        bossRoom.name = "Boss-Room";
        
        // get the boss rooms script and change its grid index to the last rooms grid index
        Room bossRoomScript = bossRoom.GetComponent<Room>();
        bossRoomScript.RoomIndex = gridIndex;

        //change the door layout based of the bossRoom roomindex
        OpenDoors(bossRoom, gridIndex.x, gridIndex.y);

        //spawn the last room for the boss room in list and destroy the last room 
        roomObjects[roomObjects.Count - 1] = bossRoom;
        bossRoom.transform.SetParent(this.transform);
        Destroy(lastRoom);
    }







    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        if(x > 0 && roomGrid[x - 1, y] != 0)
        {
            //left
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }
        if(x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            //right
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }
        if(y > 0 && roomGrid[x, y - 1] != 0)
        {
            //room below
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }
        if(y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            //room above
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
            return roomObject.GetComponent<Room>();
        return null;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX / 2),
            roomHeight * (gridY - gridSizeY / 2));
    }


    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (x > 0 && roomGrid[x - 1, y] != 0) count++; // left room
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++; // right room
        if (y > 0 && roomGrid[x, y - 1] != 0) count++; // bottom room
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++; // top room

        return count;
    }

    private void OnDrawGizmos()
    {
        Color gizmoColour = new Color(0, 1, 1, 0.5f);
        Gizmos.color = gizmoColour;

        for(int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y< gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }

}




