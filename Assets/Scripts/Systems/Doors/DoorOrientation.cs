using UnityEngine;

public class DoorOrientation : MonoBehaviour {
  public GameObject northSouthDoor;
  public GameObject westEastDoor;

  public bool isNorthSouth = true;

  void Start() {
    UpdateOrientation();
  }

  // Function to toggle orientation
  public void ToggleOrientation() {
    isNorthSouth = !isNorthSouth;
    UpdateOrientation();
  }

  // Update the orientation of the door based on the boolean variable
  public void UpdateOrientation() {
    northSouthDoor.SetActive(isNorthSouth);
    westEastDoor.SetActive(!isNorthSouth);
  }
}
