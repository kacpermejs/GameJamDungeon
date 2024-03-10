using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class WallTile : RuleTile<WallTile.Neighbor> {

  public class Neighbor : RuleTile.TilingRule.Neighbor {
    public const int IsFloor = 3;
    // public const int NotNull = 4;
  }

  bool IsFloor(TileBase tile) {

    FloorTile floorTile = tile as FloorTile;
    return floorTile != null;
  }

  public override bool RuleMatch(int neighbor, TileBase tile) {
    switch (neighbor) {
      case Neighbor.IsFloor: return IsFloor(tile);
    }
    return base.RuleMatch(neighbor, tile);
  }
}