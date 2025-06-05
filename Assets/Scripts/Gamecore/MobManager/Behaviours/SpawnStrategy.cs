using Gamecore.MobManager.Interface;
using UnityEngine;

namespace Gamecore.MobManager.Behaviours
{
    public class SpawnStrategy : ISpawnPositionStrategy
    {
        private readonly Camera _mainCamera = Camera.main;

        public Vector3 GetSpawnPosition(Vector3 originPosition)
        {
            var spawnPos = originPosition;
            var attempts = 0;
            while (attempts < 10)
            {
                attempts++;
                var angle = Random.Range(0f, 360f);
                var direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad));
                spawnPos += originPosition + direction * 10;
                var viewportPoint = _mainCamera.WorldToViewportPoint(spawnPos);
                var isVisible = viewportPoint is {z: > 0, x: > 0 and < 1, y: > 0 and < 1};
                if (isVisible) continue; 
                spawnPos.y = originPosition.y;
                return spawnPos;
            }
            return spawnPos;
        }
        public Vector3Int GetSpawnPosition(Vector2Int horizontalLimits, Vector2Int verticalLimits)
        {
            var horizontalPosition = Random.Range(horizontalLimits.x, horizontalLimits.y);
            var verticalPosition = Random.Range(verticalLimits.x, verticalLimits.y);
            return new Vector3Int(horizontalPosition, 0, verticalPosition);
        }
    }
}
