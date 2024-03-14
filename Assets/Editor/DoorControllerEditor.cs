using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DoorOrientation))]
public class DoorControllerEditor : Editor {
  public override void OnInspectorGUI() {
    // Draw the default inspector
    DrawDefaultInspector();

    // Cast the target to DoorController
    DoorOrientation doorController = (DoorOrientation)target;

    // Check if the orientation has changed
    if (GUI.changed) {
      // Update the orientation of the door
      doorController.UpdateOrientation();
    }
  }
}
