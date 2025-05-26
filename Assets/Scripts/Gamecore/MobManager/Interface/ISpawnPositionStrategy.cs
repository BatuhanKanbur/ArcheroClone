using UnityEngine;

namespace Gamecore.MobManager.Interface
{
    public interface ISpawnPositionStrategy
    {
        Vector3 GetSpawnPosition(Vector2 horizontalLimits, Vector2 verticalLimits);
    }
}
