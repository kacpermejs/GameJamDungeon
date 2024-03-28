using Assets.Scripts.Hero;
using Assets.Scripts.Systems.Doors;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
  [SerializeField]
  private Exit[] _exits;

  private List<WayPoint> _wayPoints = new List<WayPoint>();

  public List<WayPoint> WayPoints { get => _wayPoints; }

  private void Awake() {
    GetWaypoints();
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Hero")) {
      Debug.Log("New Room");
      other.GetComponent<MapMemory>().CurrentRoom = this;
    }
  }

  private void GetWaypoints() {
    var wayPoints = GetComponentsInChildren<WayPoint>();

    _wayPoints.AddRange(wayPoints);
    _wayPoints.AddRange(_exits);
  }
}
