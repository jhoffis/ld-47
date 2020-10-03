using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public TreeState treeState = TreeState.Sapling;
    public Sprite[] treeSprites;

    private SpriteRenderer _spriteRenderer;

    
    
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = treeSprites[0];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.realtimeSinceStartup > 10 && treeState == TreeState.Sapling)
        {
            _spriteRenderer.sprite = treeSprites[1];
            treeState = TreeState.Grown;
        }
    }
}

public enum TreeState
{
    Sapling, Grown, ChoppedDown
}
