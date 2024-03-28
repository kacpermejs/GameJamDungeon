using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Hero {


  public class RoomNode {
    private int _accumulatedPriority = 0;
    private Room _currentRoom;

    private Dictionary<WayPoint, int> _wayPoints = new Dictionary<WayPoint, int>();
    private List<Exit> _exits = new List<Exit>();

    public RoomNode(Room room) {
      _currentRoom = room;

      var roomWayPoints = room.WayPoints;

      foreach (var wayPoint in roomWayPoints) {
        this.AddWayPoint(wayPoint, 0);
        if (wayPoint is Exit exit) {
          _exits.Add(exit);
        }
      }
    }

    public void AddWayPoint(WayPoint wayPoint, int priorityOffset) {
      _wayPoints.Add(wayPoint, priorityOffset);
      _accumulatedPriority += wayPoint.Priority + priorityOffset;
    }

    public void ModifyPriorityOffset(WayPoint wayPoint, int priorityOffset) {
      int previous = _wayPoints[wayPoint];
      _wayPoints[wayPoint] = priorityOffset;
      _accumulatedPriority -= previous;
      _accumulatedPriority += priorityOffset;
    }

    public WayPoint GetHighestPriorityWayPoint() {
      var sortedWayPoints = _wayPoints.OrderByDescending(pair => pair.Value + pair.Key.Priority);

      var highestValueWayPoint = sortedWayPoints.FirstOrDefault();

      if (highestValueWayPoint.Key.Priority + highestValueWayPoint.Value <= 0) {
        return null;
      }

      return highestValueWayPoint.Key;
    }

  }

  public class DungeonGraph {

    private Dictionary<Room, RoomNode> _nodes = new Dictionary<Room, RoomNode>();

    public void AddRoom(Room room) {
      RoomNode newNode = new RoomNode(room);
      _nodes.Add(room, newNode);
    }

    public RoomNode GetByRoom(Room room) {
      return _nodes[room];
    }
    
  }


  public class MapMemory: MonoBehaviour {
    public DungeonGraph dungeonGraph = new DungeonGraph();

    [SerializeField]
    private Room _currentRoom;

    public Room CurrentRoom {
      get { return _currentRoom; }
      set {
        _currentRoom = value;
        OnRoomChanged();
      }
    }

    private void Start() {
      dungeonGraph.AddRoom(CurrentRoom);
    }

    public WayPoint SelectNextWaypoint(Room currentRoom) {
      return dungeonGraph.GetByRoom(currentRoom).GetHighestPriorityWayPoint();
    }

    public void MarkAsVisited(WayPoint wayPoint) {
      dungeonGraph.GetByRoom(CurrentRoom).ModifyPriorityOffset(wayPoint, -wayPoint.Priority);
    }

    private void OnRoomChanged() {
      Debug.Log("MapMemory: OnRoomChanged()");
      // TODO discover room if new
      //dungeonGraph.AddRoom(CurrentRoom);
    }
  }
}
