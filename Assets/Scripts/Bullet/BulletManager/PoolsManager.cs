using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    [SerializeField] private ObjectPool _bulletsPools;

    #region Delegates
    public delegate ObjectPool GetBulletPoolEvent();
    public static GetBulletPoolEvent GetBulletPoolHandle;
    #endregion

    private void OnEnable()
    {
        GetBulletPoolHandle += GetBulletPool;
    }

    private void OnDisable()
    {
        GetBulletPoolHandle -= GetBulletPool;
    }

    #region GettersMethods
    private ObjectPool GetBulletPool()
    {
        return _bulletsPools;
    }
    #endregion
}
