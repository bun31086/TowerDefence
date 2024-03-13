// ---------------------------------------------------------  
// SingletonMonoBehaviour.cs  
//   
// 作成日:  3/13
// 作成者:  
// ---------------------------------------------------------  
using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

    #region 変数とプロパティ 

    private static T _instance;
    public static T Instance {
        get {
            if (_instance == null) {
                Type type = typeof(T);

                _instance = (T)FindObjectOfType(type);
                //if (_instance == null) {
                //    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                //}
            }

            return _instance;
        }
    }

    #endregion
}
