using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UserData",menuName ="ScriptableObjects/UserData")]
public class UserSettingsSO : ScriptableObject
{
    private string _nickName;

    public string NickName 
    { 
        get { return _nickName; }
    }



}
