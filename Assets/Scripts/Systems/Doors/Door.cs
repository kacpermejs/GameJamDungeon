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
    private Room _from;

    [SerializeField]
    private Room _into;

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

      foreach (var obstacle in _obstacles) {
        obstacle.carveOnlyStationary = false;
        obstacle.carving = !IsOpen;
        obstacle.enabled = !IsOpen;
      }
    }

    private void DisableCollider() {
      foreach (var obstacle in _obstacles) {
        obstacle.GetComponent<Collider2D>().enabled = false;
      }
    }

    private void EnableCollider() {
      foreach (var obstacle in _obstacles) {
        obstacle.GetComponent<Collider2D>().enabled = true;
      }
    }
    
    private void DisableObstacle() {
      foreach (var obstacle in _obstacles) {
        obstacle.enabled = false;
      }
    }

    private void EnableObstacle() {
      foreach (var obstacle in _obstacles) {
        obstacle.enabled = true;
      }
    }

    private void ChangeState(DoorState state) {
      if (_doorState != state) {
        _doorState = state;
        onDoorStateChanged.Invoke(_doorState);
      }

      foreach (var obstacle in _obstacles) {
        obstacle.carving = IsOpen;
        obstacle.enabled = IsOpen;
      }

      if (CurrentState == DoorState.Open) {
        DisableCollider();
        DisableObstacle();
      } else {
        EnableCollider();
        EnableObstacle();
      }
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
