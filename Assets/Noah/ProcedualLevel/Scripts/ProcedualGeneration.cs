using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProcedualGeneration
{

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);

        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomList.Add(room);
                    }
                }
                else
                {
                   
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minHeight, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomList.Add(room);
                    }
                }
            }
        }

        return roomList;

    }

    private static void SplitVertically(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.min.y, room.min.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit,room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}
