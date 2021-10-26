using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingFilter : MonoBehaviour
{
    private Shader paintingShader;
    private SnapshotFilter filter;

    // Start is called before the first frame update
    void Start()
    {
        paintingShader = Shader.Find("Snapshot/Painting");
        filter = new BaseFilter("Painting", Color.white, paintingShader);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        filter.OnRenderImage(src, dst);
    }
}
