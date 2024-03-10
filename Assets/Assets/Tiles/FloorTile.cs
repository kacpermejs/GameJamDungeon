using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class FloorTile : RuleTile<FloorTile.Neighbor> {

  public class Neighbor : RuleTile.TilingRule.Neighbor {
    public const int IsWall = 3;
  }
  bool IsWall(TileBase tile) {

    WallTile wallTile = tile as WallTile;
    return wallTile != null;
  }

  public override bool RuleMatch(int neighbor, TileBase tile) {
    switch (neighbor) {
      case Neighbor.IsWall: return IsWall(tile);
    }
    return base.RuleMatch(neighbor, tile);
  }
}