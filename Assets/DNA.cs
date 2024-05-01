using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DNA : MonoBehaviour
{
    // gene for color
    public float Red { get => red; private set => red = value; }
    public float Green { get => green; private set => green = value; }
    public float Blue { get => blue; private set => blue = value; }

    public float Size { get => size; private set => size = value; }


    private bool dead = false;

    public float TimeToDie { get => timeToDie; private set => timeToDie = value; }

    private SpriteRenderer spriteRenderer;
    private Collider2D sCollider;

    [SerializeField] private float red;
    [SerializeField] private float green;
    [SerializeField] private float blue;
    [SerializeField] private float timeToDie;
    [SerializeField] private float size;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();

    }


    private void Start()
    {
        spriteRenderer.color = new Color(Red, Green, Blue);
        transform.localScale = new Vector3(size, size, size);   
    }
    private void OnMouseDown()
    {
        dead = true;
        TimeToDie = PopulationManager.elapsed;
        Debug.Log("Dead At: " + TimeToDie);
        spriteRenderer.enabled = false;
        sCollider.enabled = false;
    }

    public void SetRed(float value)
    {
        Red = value;
    }

    public void SetGreen(float value)
    {
        Green = value;
    }

    public void SetBlue(float value)
    {
        Blue = value;
    }

    public void SetSize(float value)
    { size = value; }


}



