using UnityEngine;

[CreateAssetMenu(fileName = "APIStaticData", menuName = "StaticData/APIStaticData")]
public class APIStaticData : ScriptableObject
{
    public string Url;
    public string Token;
}
