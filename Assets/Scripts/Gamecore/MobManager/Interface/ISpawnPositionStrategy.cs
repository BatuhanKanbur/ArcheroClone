using UnityEngine;

namespace Gamecore.MobManager.Interface
{
    public interface ISpawnPositionStrategy
    {
        public Vector3Int GetSpawnPosition(Vector2Int horizontalLimits, Vector2Int verticalLimits);
    }
}
