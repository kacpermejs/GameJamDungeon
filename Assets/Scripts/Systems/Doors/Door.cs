using UnityEngine;

public class Door : MonoBehaviour {

  [SerializeField]
  private Room _from;

  [SerializeField]
  private Room _into;

  [SerializeField]
  private bool isLocked = false;

  [SerializeField]
  private bool isOpen = false;

  public bool IsOpen {
    get { return isOpen; }
    private set { isOpen = value; }
  }

  public void Unlock() {
    isLocked = false;
  }
  public void Lock() {
    isLocked = true;
  }
  public bool TryOpen() {
    if (isLocked) {
      return false;
    } 

    isOpen = true;
    return true;
    
  }
}
