using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.x += Time.deltaTime / 20f;
        mat.mainTextureOffset = offset;





        float targetWidth = 1920f;
        float targetHeight = 1080f;
        float scaleFactorWidth = Screen.width / targetWidth;
        float scaleFactorHeight = Screen.height / targetHeight;
        float scaleFactor = Mathf.Min(scaleFactorWidth, scaleFactorHeight);
        float targetZ = -10f * scaleFactor; 
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        transform.position = new Vector3(1f * scaleFactor, -506f * scaleFactor, targetZ);









    }

}
