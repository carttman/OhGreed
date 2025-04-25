using UnityEngine;
using UnityEditor;
using System.IO;

public class SpriteRendererBatchExporter : MonoBehaviour
{
    [MenuItem("Tools/Export All Selected SpriteRenderers to PNG")]
    static void ExportSelectedSpriteRenderers()
    {
        var objs = Selection.GetFiltered<GameObject>(SelectionMode.Deep);

        string folder = "Assets/ExportedFromScene";
        Directory.CreateDirectory(folder);
        int count = 0;

        foreach (var obj in objs)
        {
            var sr = obj.GetComponent<SpriteRenderer>();
            if (sr == null || sr.sprite == null) continue;

            Sprite sprite = sr.sprite;
            float pixelsPerUnit = sprite.pixelsPerUnit;
            float widthUnits = sprite.rect.width / pixelsPerUnit;
            float heightUnits = sprite.rect.height / pixelsPerUnit;

            int texWidth = Mathf.CeilToInt(sprite.rect.width);
            int texHeight = Mathf.CeilToInt(sprite.rect.height);

            // 카메라 생성
            var camGO = new GameObject("TempCamera");
            var cam = camGO.AddComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = heightUnits / 2f;
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0, 0, 0, 0); // 투명
            cam.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -10f);

            // 렌더 텍스처 생성
            var rt = new RenderTexture(texWidth, texHeight, 24);
            cam.targetTexture = rt;
            cam.Render();

            RenderTexture.active = rt;
            Texture2D tex = new Texture2D(texWidth, texHeight, TextureFormat.ARGB32, false);
            tex.ReadPixels(new Rect(0, 0, texWidth, texHeight), 0, 0);
            tex.Apply();

            // 저장
            byte[] png = tex.EncodeToPNG();
            string path = $"{folder}/{obj.name}_export.png";
            File.WriteAllBytes(path, png);
            count++;

            // 정리
            RenderTexture.active = null;
            cam.targetTexture = null;
            Object.DestroyImmediate(rt);
            Object.DestroyImmediate(camGO);
            Object.DestroyImmediate(tex);
        }

        AssetDatabase.Refresh();
        Debug.Log($"{count}개의 SpriteRenderer 오브젝트를 PNG로 저장했습니다.");
    }
}
