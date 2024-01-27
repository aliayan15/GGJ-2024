using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemeManager : MonoBehaviour
{
    public static MemeManager Instance { get; private set; }

    [SerializeField] private List<SOMemeCard> memeList;

    private List<SOMemeCard> _usedMemeList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Error", this);
    }

    public SOMemeCard GetRandomMeme()
    {
        if (memeList.Count == 0) return null;

        SOMemeCard meme = memeList[Random.Range(0, memeList.Count)];
        memeList.Remove(meme);
        return meme;
    }
    public void AddUsedMeme(SOMemeCard meme)
    {
        _usedMemeList.Add(meme);
    }
}
