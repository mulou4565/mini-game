using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MachineShow : Machine
{
    // Start is called before the first frame update

    SpriteRenderer renderers;
    Tilemap tilemap; 
    public bool isTilemap;
    public Collider2D[] colliders;
    public bool isDisplay;
    public float speed;
    public float waitTime;
    Coroutine Change;
    private void Awake()
    { 
        Change = null;
        tilemap = GetComponent<Tilemap>(); 
         renderers = GetComponent<SpriteRenderer>(); 
        
        if (tilemap == null) { isTilemap = false; }
        if (renderers == null) { isTilemap = true; }
        if (isDisplay)
        {
            if (isTilemap) { tilemap.color = new Vector4(tilemap.color.r, tilemap.color.g, tilemap.color.b, 1); }
            else { renderers.color = new Vector4(renderers.color.r, renderers.color.g, renderers.color.b, 1); }
            foreach (var item in colliders)
            {
                item.enabled = true;
            }
        }
        else {
            if (isTilemap) { tilemap.color = new Vector4(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0); }
            else { renderers.color = new Vector4(renderers.color.r, renderers.color.g, renderers.color.b, 0); }
            foreach (var item in colliders)
            {
                item.enabled = false;
            }
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q)) { Run(); }
    }
    public override void Run()
    {
        if (tilemap == null && renderers == null) { return; }
        if (Change == null)
        {
            if (isDisplay) { Change = StartCoroutine(Hide()); }
            else { Change = StartCoroutine(Show()); }
        
        
        }
    }
    IEnumerator Show()
    {   
        if (isTilemap)
        {   
            float nowa = 0;
            while (tilemap.color.a < 1)
            {
                nowa += speed;
                tilemap.color = new Vector4(tilemap.color.r, tilemap.color.g, tilemap.color.b,nowa);
                yield return new WaitForSeconds(waitTime);
                
            }
        }
        else
        {
            float nowa = 0;
            while (renderers.color.a < 1)
            {
                nowa += speed;
                renderers.color = new Vector4(renderers.color.r, renderers.color.g, renderers.color.b, nowa);
                yield return new WaitForSeconds(waitTime);

            }
        }
        isDisplay = true;
        foreach (var item in colliders)
        {
            item.enabled = true;
        }
        Change = null;
    }


    IEnumerator Hide()
    {

        foreach (var item in colliders)
        {
            item.enabled = false;
        }
        if (isTilemap)
        {
            float nowa = 1;
            while (tilemap.color.a >0)
            {
                nowa -= speed;
                tilemap.color = new Vector4(tilemap.color.r, tilemap.color.g, tilemap.color.b, nowa);
                yield return new WaitForSeconds(waitTime);

            }
        }
        else
        {
            float nowa = 1;
            while (renderers.color.a > 0)
            {
                nowa -= speed;
                renderers.color = new Vector4(renderers.color.r, renderers.color.g, renderers.color.b, nowa);
                yield return new WaitForSeconds(waitTime);

            }
        }
        isDisplay = false;
        Change = null;
    }

}
