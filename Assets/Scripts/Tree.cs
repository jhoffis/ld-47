using UnityEngine;

public class Tree : MonoBehaviour
{
    public TreeState treeState = TreeState.Sapling;
    public Sprite[] treeSprites;

    private SpriteRenderer _spriteRenderer;

    private float _time;


    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = treeSprites[0];
        _time = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        _time += Time.deltaTime;
        if (!(_time > 10) || treeState != TreeState.Sapling) return;
        _spriteRenderer.sprite = treeSprites[1];
        treeState = TreeState.Grown;
    }
}

public enum TreeState
{
    Sapling,
    Grown,
    ChoppedDown
}