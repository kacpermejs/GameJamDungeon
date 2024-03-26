using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Assets.Scripts.Systems.Doors {

  public enum DoorState {
    Closed = 0,
    Open = 1,
    Locked = 2
  }

  public class Door : InteractiveBehaviour {

    [SerializeField]
    private Exit _exitNorth;

    [SerializeField]
    private Exit _exitSouth;
    
    [SerializeField]
    private Exit _exitWest;

    [SerializeField]
    private Exit _exitEast;

    [SerializeField]
    private bool _requiresKey = false;

    [SerializeField]
    private DoorState _defaultState = DoorState.Closed;

    private DoorState _doorState;
    public UnityEvent<DoorState> onDoorStateChanged;

    private NavMeshObstacle[] _obstacles;

    private void Awake() {
      _obstacles = GetComponentsInChildren<NavMeshObstacle>();

      _doorState = _defaultState;
    }

    private void UpdateColliders() {
      foreach (var obstacle in _obstacles) {
        obstacle.GetComponent<Collider2D>().enabled = !IsOpen; // Is closed or locked
      }
    }
    
    private void UpdateObstacles() {
      foreach (var obstacle in _obstacles) {
        obstacle.carving = !IsOpen; // Is closed or locked
        obstacle.enabled = !IsOpen;
      }
    }

    private void ChangeState(DoorState state) {
      if (_doorState != state) {
        _doorState = state;
        onDoorStateChanged.Invoke(_doorState);
      }
      UpdateColliders();
      UpdateObstacles();

    }

    public DoorState CurrentState { get { return _doorState; } }

    public bool IsClosed {
      get { return CurrentState == DoorState.Closed; }
    }

    public bool IsOpen {
      get { return CurrentState == DoorState.Open; }
    }

    public bool IsLocked {
      get { return CurrentState == DoorState.Locked; }
    }

    private void Open() {
      ChangeState(DoorState.Open);

    }

    public void Close() {
      ChangeState(DoorState.Closed);
    }

    public void Lock() {
      ChangeState(DoorState.Locked);
    }

    public void Unlock() {
      Open();
    }

    public override bool CanInteract(IActor actor) {
      if (_requiresKey) {
        if (IsLocked || IsOpen && !HasKey(actor)) {
          return false;
        }
      } else { //door locked my lever for example
        if (IsLocked) {
          return false;
        }
      }

      return true;
    }

    public override void Interact(IActor actor) {
      base.Interact(actor);
    }

    protected override void ActionSuccess(IActor actor) {
      if (IsClosed) {
        Open();
      } else if (IsLocked) {
        Unlock();
      } else if (IsOpen) {
        if (_requiresKey)
          Lock();
        else
          Close();
      }
    }

    protected override void ActionFailure(IActor actor) {
      if (IsClosed) {
        OpeningFail();
      } else if (IsLocked) {
        UnlockFail();
      } else if (IsOpen) {
        LockFail();
      }
    }

    private void LockFail() {
    }

    private void UnlockFail() {
    }

    private void OpeningFail() {
    }

    private bool HasKey(IActor actor) {
      return true; //TODO
    }

  }
}
