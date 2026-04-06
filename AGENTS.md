# PROJECT KNOWLEDGE BASE

**Generated:** 2026-04-06
**Commit:** (current)
**Branch:** (current)

## OVERVIEW
Unity 2022.3 Geometry Dash clone. Small C# codebase (~10 scripts) with JSON-driven level data. 2D arcade gameplay with menu, level select, and progressive difficulty.

## STRUCTURE
```
./
├── Assets/
│   ├── Scripts/          # All C# gameplay code (feature folders)
│   │   ├── Manager/      # GameManager, SFXManager
│   │   ├── Player/       # PlayerController
│   │   ├── Level/        # LevelData, LevelLoader
│   │   ├── UI/           # HUDManager, PauseManager
│   │   └── Menu/         # MenuController, SelectLevelManager, MusicToggle
│   ├── Levels/           # JSON level definitions (level1.json … levelN.json)
│   ├── Scenes/           # Unity scenes: MainMenu.unity, SelectLevel.unity, GameScene.unity
│   ├── Audio/            # SFX and music
│   ├── Sprites/          # Game artwork
│   └── TextMesh Pro/     # TMP assets (also uses legacy BMFont)
├── ProjectSettings/      # 23 Unity config assets
└── Packages/manifest.json # Dependency manifest
```

## WHERE TO LOOK
| Task | Location | Notes |
|------|----------|-------|
| Game lifecycle | `Assets/Scripts/Manager/GameManager.cs` | Singleton, level progression, scene transitions |
| Player control | `Assets/Scripts/Player/PlayerController.cs` | Movement, jumping, collision |
| Level loading | `Assets/Scripts/Level/LevelLoader.cs` | JSON parsing, procedural level build |
| Level data model | `Assets/Scripts/Level/LevelData.cs` | Serializable classes for JSON |
| Menu flow | `Assets/Scripts/Menu/MenuController.cs` | Main menu button handlers |
| Level selection | `Assets/Scripts/Menu/SelectLevelManager.cs` | UI logic for choosing levels |
| Audio | `Assets/Scripts/Manager/SFXManager.cs` | Singleton sound effects |
| UI updates | `Assets/Scripts/UI/HUDManager.cs`, `PauseManager.cs` | HUD, pause panel |
| Start scene | `Assets/Scenes/MainMenu.unity` | First scene loaded on startup |
| Level data files | `Assets/Levels/level*.json` | Data-driven level definitions |

## CODE MAP
| Symbol | Type | Location | Role |
|--------|------|----------|------|
| `GameManager` | MonoBehaviour | Manager/GameManager.cs | Core singleton, orchestrates gameplay |
| `PlayerController` | MonoBehaviour | Player/PlayerController.cs | Player physics and input |
| `LevelLoader` | MonoBehaviour | Level/LevelLoader.cs | Loads JSON, spawns level objects |
| `LevelData` | serializable class | Level/LevelData.cs | JSON data model |
| `SFXManager` | MonoBehaviour | Manager/SFXManager.cs | Audio singleton |
| `MenuController` | MonoBehaviour | Menu/MenuController.cs | Main menu UI events |
| `SelectLevelManager` | MonoBehaviour | Menu/SelectLevelManager.cs | Level select UI |
| `HUDManager` | MonoBehaviour | UI/HUDManager.cs | HUD updates |
| `PauseManager` | MonoBehaviour | UI/PauseManager.cs | Pause menu logic |

## CONVENTIONS
- **Private fields**: underscore prefix (`_rb`, `_isGrounded`, `_levelIndex`)
- **UI fields**: type prefixes (`imgLevelIcon`, `txtPercent`, `dots`)
- **Inspector grouping**: `[Header("...")]` attributes (no `#region` blocks)
- **Serialization**: Public fields for inspector; `[System.Serializable]` data classes use lowercase field names for JSON mapping
- **Singleton pattern**: `public static Instance` with `Awake()` guard (DontDestroyOnLoad in SFXManager)
- **MonoBehaviour event order**: `Awake()` → `Start()` → `Update()` (standard)

## ANTI-PATTERNS (THIS PROJECT)
No in-code markers found: no `TODO`, `FIXME`, `DO NOT`, `NEVER`, `ALWAYS`, `DEPRECATED` in Assets/Scripts/**/*.cs.

## UNIQUE STYLES
- Data-driven level design: levels stored as JSON, loaded and built at runtime (not Unity scenes)
- Minimal scenes: only 3 gameplay-related scenes (MainMenu, SelectLevel, GameScene)
- Font strategy: mixes TextMeshPro and legacy BMFont assets (`Assets/Fonts/BMFont/`)
- No assembly definitions: all scripts compile into default Assembly-CSharp
- No test infrastructure present

## COMMANDS
```bash
# Open project in Unity (from project root)
open GeometryDash.sln  # or use Unity Hub

# Build from Unity Editor: File → Build Settings → Build
# No custom build scripts in repo
```

## NOTES
- Build Settings: `Assets/Scenes/MainMenu.unity` is the startup scene (enabled first in EditorBuildSettings.asset)
- Level progression: `GameManager` reads `PlayerPrefs` keys (`CurrentLevel`, `LevelX_Completed`, `LevelX_BestPercent`)
- Level JSON expected in `StreamingAssets/level{index}.json` (see `LevelLoader.LoadLevel`)
- Physics: 2D (`Rigidbody2D`, `PolygonCollider2D`, `Physics2D.OverlapCircle`)
- Layers: "Ground", "Spike", "Win" must exist in TagManager (confirm in ProjectSettings/TagManager.asset)