using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Gamecore.AssetManager
{
    public static class AssetManager<T> where T : Object
    {
        #region Initilzation

        private static bool IsInit;
        private static void Init()
        {
            if (IsInit) return;
            SceneManager.activeSceneChanged += OnSceneChanged;
            IsInit = true;
        }
        private static void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            AssetCache.Clear();
        }

        #endregion

        private static readonly Dictionary<object, object> AssetCache = new();

        public static async UniTask<T> LoadObject(object assetReference)
        {
            await Addressables.InitializeAsync(false);
            Init();
            if (AssetCache.TryGetValue(assetReference, out var assetObject))
                return (T) assetObject;
            var handle = Addressables.LoadAssetAsync<T>(assetReference);
            await handle.ToUniTask();
            if (handle.Status != AsyncOperationStatus.Succeeded) throw new Exception("Failed to load asset.");
            AssetCache[assetReference] = handle.Result;
            return handle.Result;
        }

        public static async UniTask<List<T>> LoadObjects(object assetReference)
        {
            await Addressables.InitializeAsync(false);
            Init();
            if (AssetCache.TryGetValue(assetReference, out var assetObject))
                return (List<T>) assetObject;
            var handle = Addressables.LoadAssetsAsync<T>(assetReference, null);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var newAssetList = new List<T>(handle.Result);
                AssetCache[assetReference] = newAssetList;
                return newAssetList;
            }
            Debug.LogError(handle.OperationException);
            return new List<T>();
        }

        public static void ReleaseAsset(List<object> assetReference)
        {
            if (assetReference is not {Count: > 0}) return;
            foreach (var reference in assetReference)
            {
                if (!AssetCache.TryGetValue(reference, out var assetObject))
                {
                    Debug.LogError("Asset not found in cache.");
                    return;
                }
                Addressables.Release(assetObject);
                AssetCache.Remove(reference);
            }
        }
    }
    public static class AssetReferenceExtensions
    {
        public static bool IsAssigned(this AssetReference assetReference)
        {
            return assetReference != null && !string.IsNullOrEmpty(assetReference.AssetGUID);
        }
    }
}
