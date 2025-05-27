using Gamecore.MobManager.Interface;
using UnityEngine;

namespace Gamecore.MobManager.Behaviours
{
    public class SpawnStrategy : ISpawnPositionStrategy
    {
        public Vector3Int GetSpawnPosition(Vector2Int horizontalLimits, Vector2Int verticalLimits)
        {
            var horizontalPosition = Random.Range(horizontalLimits.x, horizontalLimits.y);
            var verticalPosition = Random.Range(verticalLimits.x, verticalLimits.y);
            return new Vector3Int(horizontalPosition, 0, verticalPosition);
        }
    }
}
