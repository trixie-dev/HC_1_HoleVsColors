using UnityEngine;

[CreateAssetMenu(fileName = "ColorSetup", menuName = "ColorSetup", order = 0)]
public class ColorSetup : ScriptableObject
{
    [Header ("Ground")]
    public Color groundColor;
    public Color bordersColor;
    public Color sideColor;

    [Header ("Objects & Obstacles")]
    public Color objectColor;
    public Color obstacleColor;

    [Header ("UI (progress)")]
    public Color progressFillColor;

    [Header ("Background")]
    public Color cameraColor;
    public Color fadeColor;
}
