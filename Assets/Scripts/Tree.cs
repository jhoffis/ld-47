using UnityEngine;
using Random = UnityEngine.Random;

public class Tree : MonoBehaviour, IResource
{
    public TreeState treeState = TreeState.Sapling;
    public Sprite[] treeSprites;

    private SpriteRenderer _spriteRenderer;

    private float _time;

    private float _growTime;

    private float _deathClock;

    // Start is called before the first frame update
    private void Start()
    {
        _growTime = Random.Range(5f, 100f);
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = treeSprites[0];
        _time = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > _growTime && treeState == TreeState.Sapling)
        {
            _spriteRenderer.sprite = treeSprites[1];
            treeState = TreeState.Grown;
        }


        if (treeState == TreeState.ChoppedDown)
        {
            _deathClock += Time.deltaTime;
            if (_deathClock > 5)
            {
                Debug.Log("Destroy self");
                Destroy(gameObject);
            }
        }
    }

    public ResourceType GetType()
    {
        return ResourceType.Timber;
    }


    public int Collect(int gatherSpeed)
    {
        if (treeState != TreeState.Grown) return 0;
        treeState = TreeState.ChoppedDown;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        _spriteRenderer.sprite = treeSprites[2];
        return gatherSpeed + Random.Range(0, 2);
    }
}

public enum TreeState
{
    Sapling,
    Grown,
    ChoppedDown
}