# MainMenu Scene Documentation

**File:** `Assets/Scenes/MainMenu.unity`  
**Unity Version:** 2022.3.62f3  
**Mục đích:** Màn hình chính của game - điểm vào đầu tiên khi khởi động game

---

## Tổng quan

MainMenu là scene khởi động đầu tiên trong Geometry Dash. Scene này cung cấp giao diện menu chính với các nút chức năng để điều hướng đến các phần khác của game.

**Vị trí trong Build Settings:**  
Scene đầu tiên được enable (sau SampleScene.disabled), được cấu hình trong `ProjectSettings/EditorBuildSettings.asset`.

---

## Cấu trúc GameObjects

### 1. Canvas & UI System

#### Canvas_Background (GameObject)
- **Type:** Canvas (Render Mode: Screen Space - Camera)
- **Camera:** Reference đến Main Camera
- **Plane Distance:** 10
- **Components:**
  - `Canvas` (sortingOrder: 0)
  - `CanvasScaler` (Reference Resolution: 1280x720, Scale With Screen Size)
  - `GraphicRaycaster`
- **Mục đích:** Canvas nền cho các UI elements, quản lý scaling và rendering.

#### Canvas_UI (GameObject)
- **Type:** Canvas (Render Mode: Screen Space - Overlay)
- **Sorting Order:** 10 (hiển thị trên Canvas_Background)
- **Components:**
  - `Canvas`
  - `CanvasScaler` (Reference Resolution: 1280x720, Match Width/Height: 0.5)
  - `GraphicRaycaster`
- **Mục đích:** Canvas chính chứa các UI controls (buttons, panels).

---

### 2. Panels

#### Panel_TopButtons
- **Component:** `Horizontal Layout Group`
- **Padding:** Left/Right/Top: 0, Bottom: 0
- **Spacing:** 28
- **Child Alignment:** Middle Center
- **Children (3 buttons):**
  - Btn_Play (fileID: 813305230)
  - Btn_MoreGames (fileID: 855163668)
  - Btn_Character (fileID: 1749002219)
- **Vị trí:** Con của Canvas_UI RectTransform
- **Mục đích:** Panel chứa các nút chức năng chính ở phía trên.

#### Panel_BottomBar
- **Component:** `Horizontal Layout Group`
- **Padding:** Left/Right/Top: 0, Bottom: 14
- **Spacing:** 20
- **Child Alignment:** Middle Center
- **Children (3 buttons):**
  - Btn_Share (fileID: 100696074)
  - Btn_Leaderboard (fileID: 1486683980)
  - Btn_Rate (fileID: 1675061578)
- **Vị trí:** Con của Canvas_UI RectTransform
- **Mục đích:** Panel chứa các nút phụ ở phía dưới.

---

### 3. Buttons (Tất cả có cấu trúc giống nhau)

**Common Structure mỗi Button GameObject:**
```
[ButtonName] (GameObject)
├── RectTransform
├── Button (MonoBehaviour)
│   ├── Transition: Color Tint + Sprite Swap
│   ├── Colors: Normal, Highlighted, Pressed, Disabled
│   ├── SpriteState: HighlightedSprite, PressedSprite
│   ├── AnimationTriggers: Normal, Highlighted, Pressed, Selected, Disabled
│   ├── Interactable: true
│   ├── TargetGraphic: Image component
│   └── OnClick: [Event Persistent Calls]
└── Image (MonoBehaviour) - SpriteRenderer
    └── Sprite: [Asset reference]
```

**Chi tiết từng nút:**

#### Btn_Play (Tên: "Btn_Play")
- **Kích thước:** 148x148
- **Sprite:** GUID `ba1bdfc879bb70a4782d3b3761ff3188`
- **OnClick:** Gọi `MenuController.OnPlayClick()` (MenuController, Assembly-CSharp)
- **Mục đích:** Chuyển đến scene chọn level (SelectLevel)
- **Position:** Con của Panel_TopButtons

#### Btn_MoreGames (Tên: "Btn_MoreGames")
- **Kích thước:** 120x120
- **Sprite:** GUID `ede58087fda566d43b862148f8f51863`
- **OnClick:** Chưa được gán (empty calls)
- **Mục đích:** Dự kiến mở màn hình More Games

#### Btn_Character (Tên: "Btn_Character")
- **Kích thước:** 120x120
- **Sprite:** GUID `744781669d7a2bf459fa875c74a36c6d`
- **OnClick:** Chưa được gán
- **Mục đích:** Dự kiến mở character selection

#### Btn_Share (Tên: "Btn_Share")
- **Kích thước:** 100x100
- **Sprite:** GUID `833eb43eda416f54eb89cae2947f66a2`
- **OnClick:** Chưa được gán
- **Mục đích:** Dự kiến share achievements

#### Btn_Leaderboard (Tên: "Btn_Leaderboard")
- **Kích thước:** 100x100
- **Sprite:** GUID `0f42632ea1c48414683a695af92f2eec`
- **OnClick:** Chưa được gán
- **Mục đích:** Dự kiến hiển thị leaderboard

#### Btn_Rate (Tên: "Btn_Rate")
- **Kích thước:** 100x100
- **Sprite:** GUID `29e6084c3c0dbee44857e4c13cb16562`
- **OnClick:** Chưa được gán
- **Mục đích:** Dự kiến mở màn hình rate game

---

### 4. Backgrounds (BG_Floor)

Có 4 background objects được đặt ở các vị trí khác nhau để tạo hiệu ứng scrolling/parallax:

| GameObject | Position (X, Y) | Sprite GUID |
|------------|----------------|-------------|
| BG_Floor (1) | (-824, 0) | `d4a6f61d34c97ce479d301ee5dfa5f41` |
| BG_Floor (3) | (-454.44, 0) | `d4a6f61d34c97ce479d301ee5dfa5f41` |
| BG_Floor (4) | (284.68, 0) | `d4a6f61d34c97ce479d301ee5dfa5f41` |
| BG_Floor | (639.73, 0) | `d4a6f61d34c97ce479d301ee5dfa5f41` |

- **Layer:** 5 (UI layer)
- **Component:** Image với Fill Method = Radial360 (dùng cho hiệu ứng rotation/scrolling)
- **Mục đích:** Tạo background động cho menu.

---

### 5. Managers & Systems

#### MenuManager (Empty GameObject)
- **Position:** (447.65964, 192.14706, -1.9292402)
- **Component:** `MenuController` (Script GUID: `b556234725cfee0409e1aab0862615ee`)
- **Mục đích:** Quản lý logic menu chính, xử lý button click events.
- **Public Fields (Inspector):** (không có gì được expose trong scene này)
- **Methods:**
  - `OnPlayClick()` - được gán cho Btn_Play OnClick
  - `OnCharacterClick()` - chưa wired
  - `OnShareClick()` - chưa wired

#### SFXManager (GameObject)
- **Name:** "SFXManager"
- **Position:** (433.40445, 186.1215, -2.319902)
- **Layer:** 0 (Default)
- **Components:**
  - `SFXManager` (Script GUID: `4b89ae4a3d005cb479a75adb750f8dbc`)
    - `sfxSource`: AudioSource (component cùng GameObject)
    - `clickSound`: AudioClip (GUID: `14cd335dba1dc464fa1784900d284051`)
    - `explodeSound`: AudioClip (GUID: `a0513500ca2ae4343ba98fc72490b005`)
    - `jumpSound`: AudioClip (GUID: `1af1d23a35056b148a0bf898cdf22c38`)
    - `winSound`: AudioClip (GUID: `455a409fea71efc40846b30172fbc93a`)
  - `AudioSource` (component)
    - `PlayOnAwake: false`
    - `Volume: 1`
    - `Pitch: 1`
    - `Spatial Blend: 2D (0)`
- **Mục đích:** Quản lý âm thanh toàn cục, singleton pattern.

#### EventSystem
- **Components:**
  - `InputSystemUIInputModule` (GUID: `4f231c4fb786f3946a6b90b886c48677`)
    - Horizontal/Vertical axes, Submit/Cancel buttons
  - `StandaloneInputModule` (GUID: `76c392e42b5098c458856cdf6ecaaaa1`) - fallback
  - `EventSystem` (Transform)
- **Mục đích:** Xử lý input người dùng cho UI.

---

### 6. Camera

#### Main Camera
- **Components:**
  - `Camera`
  - `AudioListener`
- **Clear Flags:** Skybox (default)
- **Projection:** Perspective
- **Field of View:** 60
- **Near/Far Clip:** 0.3 / 1000
- **Depth:** -1
- **Mục đích:** Render scene, cung cấp audio listener.

---

## Rendering & Environment Settings

Scene-level settings (được lưu ở top-level YAML blocks):

- **OcclusionCullingSettings:** mặc định, occlusion baking disabled.
- **RenderSettings:**
  - Fog: disabled
  - Ambient Mode: Trilight
  - Skybox: none
  - Halo Strength: 0.5
  - Flare Strength: 1
- **LightmapSettings:** Baked lightmaps disabled, realtime lightmaps disabled.
- **NavMeshSettings:** NavMesh disabled (agentTypeID: 0).

---

## Asset References (GUIDs)

Các asset được tham chiếu trong scene (lưu ý: GUIDs có thể thay đổi nếu asset được reimport):

| Asset Type | GUID | File Path (estimated) |
|------------|------|----------------------|
| Btn_Play Sprite | `ba1bdfc879bb70a4782d3b3761ff3188` | Assets/Sprites/btn_play.png (?) |
| Btn_MoreGames Sprite | `ede58087fda566d43b862148f8f51863` | Assets/Sprites/btn_moregames.png |
| Btn_Character Sprite | `744781669d7a2bf459fa875c74a36c6d` | Assets/Sprites/btn_character.png |
| Btn_Share Sprite | `833eb43eda416f54eb89cae2947f66a2` | Assets/Sprites/btn_share.png |
| Btn_Leaderboard Sprite | `0f42632ea1c48414683a695af92f2eec` | Assets/Sprites/btn_leaderboard.png |
| Btn_Rate Sprite | `29e6084c3c0dbee44857e4c13cb16562` | Assets/Sprites/btn_rate.png |
| BG_Floor Sprite | `d4a6f61d34c97ce479d301ee5dfa5f41` | Assets/Sprites/bg_floor.png |
| Click Sound | `14cd335dba1dc464fa1784900d284051` | Assets/Audio/click.mp3 |
| Explode Sound | `a0513500ca2ae4343ba98fc72490b005` | Assets/Audio/explode.mp3 |
| Jump Sound | `1af1d23a35056b148a0bf898cdf22c38` | Assets/Audio/jump.mp3 |
| Win Sound | `455a409fea71efc40846b30172fbc93a` | Assets/Audio/win.mp3 |
| MenuController Script | `b556234725cfee0409e1aab0862615ee` | Assets/Scripts/Menu/MenuController.cs |
| SFXManager Script | `4b89ae4a3d005cb479a75adb750f8dbc` | Assets/Scripts/Manager/SFXManager.cs |

---

## Scripting & Logic Flow

### MenuController (Assets/Scripts/Menu/MenuController.cs)

**Purpose:** Xử lý các sự kiện click trên menu chính.

**Exposed Methods (có thể gán từ UnityEvents):**
- `OnPlayClick()` → `SceneManager.LoadScene("SelectLevel")`
- `OnCharacterClick()` → chưaImplement
- `OnShareClick()` → chưaImplement

**State:** Không có serialized fields; stateless.

### SFXManager (Assets/Scripts/Manager/SFXManager.cs)

**Purpose:** Singleton quản lý phát âm thanh effect.

**Pattern:**
```csharp
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    public AudioSource sfxSource;
    public AudioClip clickSound, explodeSound, jumpSound, winSound;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    public void PlayClick() => sfxSource.PlayOneShot(clickSound);
    public void PlayExplode() => sfxSource.PlayOneShot(explodeSound);
    public void PlayJump() => sfxSource.PlayOneShot(jumpSound);
    public void PlayWin() => sfxSource.PlayOneShot(winSound);
}
```

**Usage trong MainMenu:** Bất kỳ button nào cần âm thanh click sẽ gọi `SFXManager.Instance?.PlayClick()` (có thể được gán qua code hoặc UnityEvent).

---

## UI Layout & Resolution

- **Reference Resolution:** 1280 x 720 (drawn CanvasScaler)
- **Canvas Render Mode:** Mixed (Background canvas dùng Screen Space - Camera, UI canvas dùng Overlay)
- **Layout:** Các button được sắp xếp bằng Horizontal Layout Group trong 2 panels (top và bottom).
- **Scaling:** CanvasScaler sử dụng "Scale With Screen Size" để tự động scale trên các độ phân giải khác nhau.

---

## Các thành phần chưa hoàn thiện

Một số button chưa có OnClick event được wiring:
- Btn_MoreGames
- Btn_Character
- Btn_Share
- Btn_Leaderboard
- Btn_Rate

Các method tương ứng trong `MenuController` (OnCharacterClick, OnShareClick) có thể chưa được implement hoặc chưa kết nối.

---

## Notes & Gotchas

- **Scene Order:** MainMenu là scene startup, được ensure trong `EditorBuildSettings.asset` với index 1 (sau SampleScene).
- **Singleton SFXManager:** Nếu có vấn đề với âm thanh, kiểm tra Awake() chain và đảm bảo SFXManager không bị duplicate qua scene loads.
- **Asset GUIDs:** Các sprite references dựa trên GUIDs; nếu asset bị move/rename, GUIDs thay đổi và scene có thể mất reference. Kiểm tra .meta files để đảm bảo consistency.
- **UI Layers:** Buttons nằm trên Layer 5 (UI). Canvas cũng dùng layer UI.
- **EventSystem:** Có cả `InputSystemUIInputModule` và `StandaloneInputModule` - cần đảm bảo Input System package đã được import nếu dùng InputSystem.

---

## Quick Reference

| Component | File Path | Role |
|-----------|-----------|------|
| MenuController | `Assets/Scripts/Menu/MenuController.cs` | Main menu logic |
| SFXManager | `Assets/Scripts/Manager/SFXManager.cs` | Audio singleton |
| Scene File | `Assets/Scenes/MainMenu.unity` | Main menu scene |
| Build Setting | `ProjectSettings/EditorBuildSettings.asset` | Startup scene config |

---

**Last Updated:** 2026-04-06 (via init-deep documentation generation)
