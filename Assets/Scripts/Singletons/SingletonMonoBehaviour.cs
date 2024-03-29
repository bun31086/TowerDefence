// ---------------------------------------------------------  
// SingletonMonoBehaviour.cs  
// シングルトン
// 作成日:  3/13
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

    #region 変数,プロパティ 

    private static T _instance;
    public static T Instance {
        get {
            if (_instance == null) {
                Type type = typeof(T);

                _instance = (T)FindObjectOfType(type);
            }
            return _instance;
        }
    }
    #endregion
}
