using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Sprite[] animationSprites;

    public float animationTime = 1.0f;


    private SpriteRenderer _spriteRenderer;

    private int _animationFrame;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSpite), this.animationTime, this.animationTime);
    }

    private void AnimateSpite()
    {
        _animationFrame++;

        if (_animationFrame >= this.animationSprites.Length) {
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }
    
}
