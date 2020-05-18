using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Gradient")]
public class Gradient : BaseMeshEffect
{
    public Color32 topColor = Color.white;
    public Color32 bottomColor = Color.black;
    public bool IsLeftToRight;

    public override void ModifyMesh(VertexHelper helper)
    {
        if (!IsActive() || helper.currentVertCount == 0)
            return;

        List<UIVertex> vertices = new List<UIVertex>();
        helper.GetUIVertexStream(vertices);

        float bottomY = IsLeftToRight ? vertices[0].position.x : vertices[0].position.y;
        float topY = IsLeftToRight ? vertices[0].position.x : vertices[0].position.y;

        for (int i = 1; i < vertices.Count; i++)
        {
            float y = IsLeftToRight ? vertices[0].position.x : vertices[i].position.y;
            if (y > topY)
            {
                topY = y;
            }
            else if (y < bottomY)
            {
                bottomY = y;
            }
        }

        float uiElementHeight = topY - bottomY;

        UIVertex v = new UIVertex();

        for (int i = 0; i < helper.currentVertCount; i++)
        {
            helper.PopulateUIVertex(ref v, i);
            v.color = Color32.Lerp(bottomColor, topColor, (IsLeftToRight ? v.position.x : v.position.y - bottomY) / uiElementHeight);
            helper.SetUIVertex(v, i);
        }
    }
}