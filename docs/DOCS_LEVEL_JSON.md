# Level JSON Structure Documentation

## Overview

Level files (`level1.json` - `level8.json`) nằm trong `Assets/Levels/`. Đây là **Box2D Physics Export** từ Unity Physics2D Editor, không phải custom game level format.

## File Format

```json
{
    "allowSleep": true,
    "autoClearForces": true,
    "body": [
        {
            "angle": 0,
            "angularVelocity": 0,
            "awake": true,
            "customProperties": [
                {
                    "name": "RoleInGame",
                    "string": "Spike"
                }
            ],
            "fixture": [
                {
                    "density": 1,
                    "friction": 0.2,
                    "name": "fixture1",
                    "polygon": {
                        "vertices": {
                            "x": [1, 0.0, -1],
                            "y": [-3, 3, -3]
                        }
                    }
                }
            ],
            "linearVelocity": 0,
            "name": "body1",
            "position": {
                "x": 184.0,
                "y": 12.0
            },
            "type": 0
        }
    ],
    "image": [
        {
            "file": "spike.png",
            "body": 0,
            "center": { "x": 0, "y": 0 },
            "scale": 1,
            "opacity": 1
        }
    ]
}
```

## Data Structures

### Root: `LevelData`

| Field   | Type               | Description                    |
|---------|--------------------|---------------------------------|
| `body`  | `List<BodyData>`   | Physics bodies (obstacles)     |
| `image` | `List<ImageData>`  | Sprite visual attachments      |

### Body: `BodyData`

| Field              | Type                    | Description                          |
|--------------------|------------------------|--------------------------------------|
| `name`             | `string`               | Body identifier (e.g., "body1")      |
| `position`         | `PositionData`         | World position (x, y)                |
| `angle`            | `float`                | Rotation in radians                 |
| `customProperties` | `List<CustomProperty>` | Metadata (RoleInGame)              |
| `fixture`          | `List<FixtureData>`    | Collider shapes                      |

### Position: `PositionData`

| Field | Type  | Description        |
|-------|-------|--------------------|
| `x`   | `float` | X coordinate      |
| `y`   | `float` | Y coordinate      |

### CustomProperty

| Field   | Type    | Description                    |
|---------|---------|--------------------------------|
| `name`  | `string` | Property name                  |
| `string`| `string` | Property value (RoleInGame)   |

**RoleInGame Values:**

| Value   | Unity Layer | Unity Tag | Meaning              |
|---------|-------------|-----------|----------------------|
| `Spike` | Spike       | Spike     | Deadly obstacle      |
| `Wall`  | Ground      | Ground    | Floor/wall platform  |
| `Roof`  | Ground      | Ground    | Ceiling              |
| `Win`   | Win         | Win       | Level end trigger   |

### Fixture: `FixtureData`

| Field    | Type          | Description              |
|----------|---------------|--------------------------|
| `polygon`| `PolygonData` | Polygon collider shape  |

### Polygon: `PolygonData`

| Field     | Type           | Description              |
|-----------|----------------|--------------------------|
| `vertices`| `VerticesData`| Polygon vertex points   |

### Vertices: `VerticesData`

| Field | Type          | Description                    |
|-------|---------------|--------------------------------|
| `x`   | `List<float>` | X coordinates of vertices     |
| `y`   | `List<float>` | Y coordinates of vertices     |

### Image: `ImageData`

| Field    | Type         | Description                              |
|----------|--------------|-------------------------------------------|
| `file`   | `string`     | Sprite filename (e.g., "spike.png")       |
| `body`   | `int`        | Index of body to attach sprite to         |
| `center` | `CenterData`| Sprite center offset                      |
| `scale`  | `float`      | Sprite scale multiplier                  |
| `opacity`| `float`      | Sprite opacity (0-1)                      |

## How LevelLoader Works

### Loading Flow

```
1. LoadLevel(levelIndex)
   └─> Read: StreamingAssets/level{index}.json
   
2. JsonUtility.FromJson<LevelData>(json)
   └─> Parse to LevelData object
   
3. BuildSpriteDict()
   └─> Load SpriteMapping[] from Inspector into dictionary
   
4. SpawnLevel()
   └─> For each body in _data.body:
       ├─> Create GameObject
       ├─> Set position (x * worldScale, y * worldScale)
       ├─> Set rotation (angle * Rad2Deg)
       ├─> Add PolygonCollider2D from fixture vertices
       ├─> Add Rigidbody2D (Static)
       ├─> Set Layer/Tag based on RoleInGame
       └─> Attach sprite from image[] (if exists)
```

### Sprite Mapping System

Sprites được gán qua `LevelLoader` Inspector - `SpriteMapping[]` array:

```csharp
[System.Serializable]
public class SpriteMapping {
    public string key;    // e.g., "spike", "wall", "win"
    public Sprite sprite; // Sprite asset reference
}
```

**Required Mappings:**

| Key      | Usage                           |
|----------|---------------------------------|
| `spike`  | Bodies with RoleInGame = "Spike"|
| `wall`   | Bodies with RoleInGame = "Wall" |
| `roof`   | Bodies with RoleInGame = "Roof" |
| `win`    | Bodies with RoleInGame = "Win"  |

### World Scale

`LevelLoader.worldScale = 0.5f` (default)

- Box2D units → Unity units conversion
- Level position (x: 184) → Unity world position (x: 92)

## Integration Steps

### Step 1: Setup Sprites

Trong Unity Inspector, gán sprites vào `LevelLoader` component:

```
LevelLoader (Script)
├── World Scale: 0.5
└── Sprite Mappings:
    ├── Element 0: key="spike",  sprite=spike.png
    ├── Element 1: key="wall",   sprite=wall.png
    ├── Element 2: key="roof",   sprite=roof.png
    └── Element 3: key="win",    sprite=win.png
```

### Step 2: Ensure Layers Exist

Trong Unity Project Settings → Tags and Layers:

| Layer Name | Usage          |
|------------|----------------|
| `Ground`   | Wall, Roof     |
| `Spike`    | Spike obstacles|
| `Win`      | Level end      |

### Step 3: Place JSON Files

```
Assets/
└── StreamingAssets/
    ├── level1.json
    ├── level2.json
    └── ...
```

**Note:** Unity yêu cầu files trong `StreamingAssets/` để read qua `Application.streamingAssetsPath`.

## Asset Reference

### Available Sprites (trong Assets/Sprites/)

| Category    | Path                              | Potential Use        |
|-------------|------------------------------------|---------------------|
| Effects/rube| .../Effects/rube/spike.png        | Spike sprite        |
| Effects/rube| .../Effects/rube/floor*.png       | Wall/Roof sprites   |
| Effects/rube| .../Effects/rube/roof*.png        | Alternative roofs   |
| Player      | .../Player/fang*.png              | Alternative sprites |

### Level Files

| File        | Bodies | Description        |
|-------------|--------|--------------------|
| level1.json | ~200+  | Tutorial level     |
| level2.json | ...    | Level 2            |
| level3.json | ...    | Level 3            |
| level4.json | ...    | Level 4            |
| level5.json | ...    | Level 5            |
| level6.json | ...    | Level 6            |
| level7.json | ...    | Level 7            |
| level8.json | ...    | Level 8            |

## Code Reference

- **LevelData.cs** - `Assets/Scripts/Level/LevelData.cs` (lines 1-54)
- **LevelLoader.cs** - `Assets/Scripts/Level/LevelLoader.cs` (lines 1-147)

## Notes

1. JSON sử dụng lowercase field names để JsonUtility parse đúng
2. Tất cả bodies đều là Static (không di chuyển)
3. Spike và Win colliders là Triggers (isTrigger = true)
4. Image array map sprite vào body theo index - nếu không có image entry thì không có sprite hiển thị
5. File JSON rất dài (~20000 lines) vì export toàn bộ Box2D world state