using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureNormalizer : MonoBehaviour
{

    public float scale = 1F;

    void Update()
    {
        Material m = GetComponent<Renderer>().material;
        m.mainTextureScale = new Vector2(transform.lossyScale.x, transform.lossyScale.z) * scale;
        Vector3 localPos = Quaternion.Inverse(transform.rotation) * transform.position;
        Vector2 offsetPos = -new Vector2(localPos.x, localPos.z) / 10;
        m.mainTextureOffset = offsetPos - m.mainTextureScale * 0.5F;
    }
}
