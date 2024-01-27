using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Meme Card")]
public class SOMemeCard : ScriptableObject
{
    public Sprite MemeSprite;
    public short[] Scores;
}
