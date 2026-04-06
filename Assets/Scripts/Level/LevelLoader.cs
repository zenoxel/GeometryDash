using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [Header("Scale — 1 Box2D unit = worldScale Unity unit")]
    public float worldScale = 0.5f;

    [Header("Sprite Mapping")]
    public SpriteMapping[] spriteMappings;

    private LevelData _data;
    private Dictionary<string, Sprite> _spriteDict = new();
    private float _levelEndX = 0f;

    public float LevelEndX => _levelEndX;

    public void LoadLevel(int levelIndex)
    {
        string path = Path.Combine(
            Application.streamingAssetsPath, $"level{levelIndex}.json");

        if (!File.Exists(path))
        {
            Debug.LogError($"Level file not found: {path}");
            return;
        }

        string json = File.ReadAllText(path);
        _data = JsonUtility.FromJson<LevelData>(json);

        BuildSpriteDict();
        SpawnLevel();
    }

    void BuildSpriteDict()
    {
        _spriteDict.Clear();
        foreach (var m in spriteMappings)
            if (m.sprite != null)
                _spriteDict[m.key.ToLower()] = m.sprite;
    }

    void SpawnLevel()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        _levelEndX = 0f;

        for (int i = 0; i < _data.body.Count; i++)
        {
            BodyData b    = _data.body[i];
            string   role = GetRole(b);
            SpawnBody(b, role, i);

            if (role == "Win")
                _levelEndX = b.position.x * worldScale;
        }

        GameManager.Instance?.SetLevelEndX(_levelEndX);
    }

    void SpawnBody(BodyData b, string role, int index)
    {
        var go = new GameObject($"{role}_{index}");
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(
            b.position.x * worldScale,
            b.position.y * worldScale, 0);
        go.transform.rotation =
            Quaternion.Euler(0, 0, b.angle * Mathf.Rad2Deg);

        switch (role)
        {
            case "Wall":
            case "Roof":
                go.layer = LayerMask.NameToLayer("Ground");
                go.tag   = "Ground";
                break;
            case "Spike":
                go.layer = LayerMask.NameToLayer("Spike");
                go.tag   = "Spike";
                break;
            case "Win":
                go.layer = LayerMask.NameToLayer("Win");
                go.tag   = "Win";
                break;
        }

        if (b.fixture != null && b.fixture.Count > 0)
        {
            var verts = b.fixture[0].polygon?.vertices;
            if (verts != null && verts.x != null && verts.x.Count >= 3)
            {
                var poly   = go.AddComponent<PolygonCollider2D>();
                var points = new Vector2[verts.x.Count];
                for (int j = 0; j < verts.x.Count; j++)
                    points[j] = new Vector2(
                        verts.x[j] * worldScale,
                        verts.y[j] * worldScale);
                poly.SetPath(0, points);
                poly.isTrigger = (role == "Spike" || role == "Win");
            }
        }

        var rb      = go.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        AttachSprite(go, index);
    }

    void AttachSprite(GameObject go, int bodyIndex)
    {
        if (_data.image == null) return;
        foreach (var img in _data.image)
        {
            if (img.body != bodyIndex) continue;
            string key = Path.GetFileNameWithoutExtension(img.file).ToLower();
            if (!_spriteDict.TryGetValue(key, out Sprite spr)) continue;

            var sr    = go.AddComponent<SpriteRenderer>();
            sr.sprite = spr;
            sr.color  = new Color(1, 1, 1, img.opacity > 0 ? img.opacity : 1f);

            float s = img.scale * worldScale;
            go.transform.localScale = new Vector3(s, s, 1);
            break;
        }
    }

    string GetRole(BodyData b)
    {
        if (b.customProperties == null) return "Wall";
        foreach (var p in b.customProperties)
            if (p.name == "RoleInGame") return p.@string;
        return "Wall";
    }
}

[System.Serializable]
public class SpriteMapping
{
    public string key;
    public Sprite sprite;
}