#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SOMemeCard))]
public class SOMemeCardEditor : Editor
{
    //Here we grab a reference to our Weapon SO
    SOMemeCard weapon;

    private void OnEnable()
    {
        //target is by default available for you
        //because we inherite Editor
        weapon = target as SOMemeCard;
    }

    //Here is the meat of the script
    public override void OnInspectorGUI()
    {
        //Draw whatever we already have in SO definition
        base.OnInspectorGUI();
        //Guard clause
        if (weapon.MemeSprite == null)
            return;

        //Convert the weaponSprite (see SO script) to Texture
        Texture2D texture = AssetPreview.GetAssetPreview(weapon.MemeSprite);
        //We crate empty space 80x80 (you may need to tweak it to scale better your sprite
        //This allows us to place the image JUST UNDER our default inspector
        GUILayout.Label("", GUILayout.Height(160), GUILayout.Width(128));
        //Draws the texture where we have defined our Label (empty space)
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}

#endif
