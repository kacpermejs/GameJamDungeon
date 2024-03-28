using UnityEngine;

public class Exit : WayPoint {
  [SerializeField]
  private Room _room;

  [SerializeField]
  private Exit _otherSide;

  public Exit OtherSide { get => _otherSide; }
}
