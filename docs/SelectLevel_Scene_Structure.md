# SelectLevel Scene Documentation

**File:** `Assets/Scenes/SelectLevel.unity`  
**Unity Version:** 2022.3.62f3  
**Mục đích:** Màn hình chọn level - cho phép người chơi chọn level cụ thể và xem progress

---

## Tổng quan

SelectLevel là scene cho phép người chơi duyệt qua các level, xem tiến độ và best percentage, đồng thời bắt đầu chơi level được chọn. Scene này là bước trung gian giữa MainMenu và GameScene.

**Vị trí trong Build Settings:**  
Scene thứ 2 trong build order (được enable sau MainMenu).

---

## Cấu trúc GameObjects

### 1. Canvas & UI System

#### Canvas_Background (GameObject)
- **Name:** `'Canvas_Background '` (có khoảng trắng)
- **Type:** Canvas (Render Mode: Screen Space - Camera)
- **Camera:** Reference đến Main Camera (fileID: 1189902338)
- **Plane Distance:** 100
- **Components:**
  - `Canvas` (sortingOrder: 0)
  - `CanvasScaler` (Reference Resolution: 1280x720, Scale With Screen Size, Match: 0)
  - `GraphicRaycaster`
- **Mục đích:** Canvas nền cho UI elements.

#### Canvas_UI (GameObject)
- **Name:** `Canvas_UI`
- **Type:** Canvas (Render Mode: Screen Space - Overlay)
- **Sorting Order:** 10
- **Components:**
  - `Canvas`
  - `CanvasScaler` (Reference Resolution: 1280x720, Match Width/Height: 0)
  - `GraphicRaycaster`
- **Mục đích:** Canvas chính chứa tất cả UI controls.

---

### 2. Panels & Layout

#### Panel_LevelCard (GameObject)
- **Vị trí:** Con của Canvas_UI RectTransform (AnchoredPosition: (0, 49))
- **Kích thước:** 750 x 240
- **Màu nền:** RGB(74, 74, 30) = `rgba(0.29, 0.29, 0.12, 1)` (màu olive xanh)
- **Components:**
  - `RectTransform`
  - `Image` (Sprite: none, màu solid)
  - `Button` (Navigation: Automatic)
    - **OnClick:** Gọi `SelectLevelManager.OnPlayLevel()`
  - `CanvasRenderer`
- **Children:**
  - `Img_LevelIcon` (Image)
  - `Img_Star` (Image)
  - `Img_NextLevelText` (Image) - Sprite "NEXT LEVEL" (GUID: `6106aa36ead047147a7b4338ec1da0f2`)
- **Mục đích:** Card hiển thị level hiện tại và cho phép chọn để chơi.

#### Panel_ProgressBar (GameObject)
- **Vị trí:** Con của Canvas_UI RectTransform (AnchoredPosition: (0, -165))
- **Kích thước:** 560 x 45
- **Children:**
  - `Img_BarBG` (Image - background của progress bar)
  - `Img_BarFill` (Image - fill amount hiển thị progress)
  - `Txt_Percent` (TMP_Text - hiển thị phần trăm)
- **Mục đích:** Hiển thị best percentage của level hiện tại.

#### Panel_Dots (GameObject)
- **Vị trí:** Con của Canvas_UI RectTransform (AnchoredPosition: (0, 0))
- **Kích thước:** 300 x 30
- **Components:** `Horizontal Layout Group` (Spacing: 12, Child Force Expand: Width/Height)
- **Children:**
  - `Img_DotActive` (Image - dot đang được chọn)
- **Mục đích:** Hiển thị các dot đánh dấu level hiện tại trong số 8 level. Có thể sẽ có thêm 7 dot inactive nhưng chỉ thấy active trong scene.

---

### 3. Buttons

#### Btn_Back (GameObject)
- **Vị trí:** Con của Canvas_UI RectTransform, Anchor phía trên bên trái (AnchoredPosition: (60, -55))
- **Kích thước:** ~87.67 x 80
- **Sprite:** GUID `c8e269f3c5cacbb41878666aa0acf142`
- **Transition:** Color Tint ( không dùng Sprite Swap)
- **OnClick:** Gọi `SelectLevelManager.OnBackClick()` → `SceneManager.LoadScene("MainMenu")`
- **Mục đích:** Quay về MainMenu.

#### Btn_Prev (GameObject)
- **Vị trí:** Con của Panel_LevelCard? (AnchoredPosition: (-1626, 11))
- **Kích thước:** 71 x 138
- **Sprite:** GUID `dad0676379fffa14baa02e2849790453`
- **Transition:** Sprite Swap
- **OnClick:** Gọi `SelectLevelManager.OnPrevClick()` → giảm `_current`
- **Scale X:** -1 (lật ngang - có thể để tạo hiệu ứng mirror)
- **Mục đích:** Chuyển sang level trước đó.

#### Btn_Next (GameObject)
- **Vị trí:** Con của Panel_LevelCard? (AnchoredPosition: (-17, 0))
- **Kích thước:** 71 x 138
- **Sprite:** GUID `dad0676379fffa14baa02e2849790453` (cùng sprite với Prev)
- **Transition:** Sprite Swap
- **OnClick:** Gọi `SelectLevelManager.OnNextClick()` → tăng `_current`
- **Mục đích:** Chuyển sang level tiếp theo.

#### Btn_Leaderboard (GameObject)
- **Vị trí:** Con của Canvas_UI (AnchoredPosition: (-50.12, -55))
- **Kích thước:** ~89.88 x 93.83
- **Sprite:** GUID `0f42632ea1c48414683a695af92f2eec`
- **OnClick:** Chưa được gán
- **Mục đích:** Dự kiến mở màn hình leaderboard.

---

### 4. Backgrounds

#### BG_Floor (GameObject)
- **Vị trí:** Con của Canvas_Background (AnchoredPosition: (0, 0))
- **Kích thước:** width = 0 (stretched), height = 160
- **Sprite:** GUID `d4a6f61d34c97ce479d301ee5dfa5f41` (cùng sprite BG_Floor với MainMenu)
- **Fill Method:** Radial360
- **Mục đích:** Background dạng scrolling/parallax.

#### BG_Color (GameObject)
- **Vị trí:** Con của Canvas_Background (AnchoredPosition: (0, 0))
- **Màu:** RGB(184, 196, 0) = `rgba(0.721, 0.768, 0.0, 1)` (màu xanh lá sáng)
- **Mục đích:** Nền màu cho toàn scene.

#### Img_LeftBG (GameObject)
- **Vị trí:** Con của Canvas_UI RectTransform (AnchoredPosition: (0, 2))
- **Kích thước:** 256.11 x 217.5
- **Sprite:** GUID `6234f35f71ca6d84ca77fb9719d6d951`
- **Mục đích:** Background bên trái (có thể decor).

#### Img_RightBG (GameObject)
- **Vị trí:** Con của Canvas_UI RectTransform, flipped Y (Rotate: (0, 1, 0, 0) tức lật 180 độ)
- **Kích thước:** 256.11 x ~201.9
- **Sprite:** GUID `6234f35f71ca6d84ca77fb9719d6d951` (cùng sprite với LeftBG)
- **Mục đích:** Background bên phải mirror của LeftBG.

---

### 5. Informational UI

#### Txt_Percent (GameObject)
- **Name:** `Txt_Percent`
- **Component:** `TextMeshProUGUI`
- **Text mặc định:** "33%"
- **Font Asset:** LiberationSans SDF (GUID: `8f586378b4e144a9851e7b34d9b748ee`)
- **Font Size:** 36
- **Alignment:** Middle Center
- **Anchor:** (0.5, 0.5) relative to Panel_ProgressBar (AnchoredPosition: (40.5, 0))
- **Mục đích:** Hiển thị best percentage.

#### Txt_StarCount (GameObject)
- **Name:** `Txt_StarCount`
- **Component:** `TextMeshProUGUI`
- **Text mặc định:** "1"
- **Font Asset:** LiberationSans SDF (GUID: `8f586378b4e144a9851e7b34d9b748ee`)
- **Font Size:** 36
- **Anchor:** (1, 1) relative to Panel_LevelCard (AnchoredPosition: (120, -10))
- **Mục đích:** Hiển thị số sao đã đạt được cho level hiện tại.

---

### 6. Managers & Scripts

#### LevelSelectManager (GameObject)
- **Name:** `LevelSelectManager`
- **Position:** (445.53912, 223.949, -0.3408012)
- **Layer:** 0 (Default)
- **Component:** `SelectLevelManager` (Script GUID: `1d0b32e33c2788e4bb8b9b87e3e557f2`)
- **Serialized Fields (Inspector):**
  - `levelIcons`: Sprite[8] - tất cả cùng GUID `15bea2c3a5844f546ac10898a4888dae` (có thể placeholder)
  - `levelNameSprites`: Sprite[8] - 8 GUIDs khác nhau (từ index 0-7)
  - `starCounts`: int[8] = [1,1,2,2,3,3,4,5] (sao mặc định mỗi level)
  - `imgLevelIcon`: Reference đến `Img_LevelIcon` Image component
  - `imgLevelName`: Reference đến `Img_LevelName` Image component
  - `txtStarCount`: Reference đến `Txt_StarCount` TMP_Text
  - `imgBarFill`: Reference đến `Img_BarFill` Image
  - `txtPercent`: Reference đến `Txt_Percent` TMP_Text
  - `dots`: Array (có thể chứa các dot controls)
- **Methods (UnityEvent wired):**
  - `OnNextClick()` → wired đến Btn_Next OnClick
  - `OnPrevClick()` → wired đến Btn_Prev OnClick
  - `OnBackClick()` → wired đến Btn_Back OnClick
- **Logic:**
  - Start() loads `LastSelectedLevel` from PlayerPrefs (default 0)
  - Refresh() cập nhật UI: icon, name, star count (saved or default), progress bar (best percent), dots highlight
- **Mục đích:** Quản lý logic chọn level, navigation giữa các level, persistence.

#### LevelLoader (GameObject)
- **Name:** `LevelLoader`
- **Position:** (445.53912, 223.949, -0.3408012) - cùng position với LevelSelectManager
- **Component:** `LevelLoader` (Script GUID: `a6d088e5ac6574e46a6dedb82c0f4f33`)
- **Serialized Fields:**
  - `worldScale`: 0.5
  - `spriteMappings`: Array với 12 keys (spike, floor, roof1-11) và sprite GUIDs tương ứng
  - `groundLayerName`: "Ground"
  - `spikeLayerName`: "Spike"
  - `winLayerName`: "Win"
- **Mục đích:** Load và spawn level từ JSON file khi bắt đầu gameplay (được gọi từ GameManager).

---

### 7. System Components

#### EventSystem
- **Components:**
  - `InputSystemUIInputModule` (Horizontal/Vertical axes, Submit/Cancel)
  - `StandaloneInputModule` (fallback)
  - `EventSystem`
- **Mục đích:** Xử lý input cho UI.

#### Main Camera
- **Components:** `Camera` + `AudioListener`
- **Clear Flags:** Skybox (màu xanh đậm `rgba(0.192, 0.302, 0.475, 0)`)
- **Projection:** Orthographic (orthographic size: 5)
- **Depth:** -1
- **Position:** (0, 0, -10)
- **Mục đích:** Render scene, cung cấp audio listener.

---

## Asset References (GUIDs)

| Asset Type | GUID |File Path (estimated) |
|------------|------|---------------------|
| Btn_Back Sprite | `c8e269f3c5cacbb41878666aa0acf142` | Assets/Sprites/btn_back.png |
| Btn_Prev/Next Sprite | `dad0676379fffa14baa02e2849790453` | Assets/Sprites/btn_arrow.png |
| Btn_Leaderboard Sprite | `0f42632ea1c48414683a695af92f2eec` | Assets/Sprites/btn_leaderboard.png |
| Img_NextLevelText Sprite | `6106aa36ead047147a7b4338ec1da0f2` | Assets/Sprites/text_next_level.png |
| Img_LevelIcon Sprite (placeholder) | `15bea2c3a5844f546ac10898a4888dae` | Assets/Sprites/level_icon.png |
| Img_LevelName Sprites (8) | `62a286ac9a076ce4b817bd3093c61793` đến `5e506ec3b132e3b4cadedf1573ae85e3` | Assets/Sprites/levelname_01-08.png |
| Img_Star Sprite | `aaea6abaded14bd44844d7a7640caac6` | Assets/Sprites/star.png |
| Img_DotActive Sprite | `86029b271ad595c43add005910c7c01a` | Assets/Sprites/dot_active.png |
| Img_BarFill Sprite | `8e70b74f2e298964b96be7f8b5bdea96` | Assets/Sprites/bar_fill.png |
| Img_BarBG Sprite | `51886f565e2337e4f88d4f90b366e210` | Assets/Sprites/bar_bg.png |
| Img_Left/RightBG Sprite | `6234f35f71ca6d84ca77fb9719d6d951` | Assets/Sprites/bg_side.png |
| TMP Font (LiberationSans) | `8f586378b4e144a9851e7b34d9b748ee` | Assets/TextMesh Pro/Resources/LiberationSans SDF.asset |
| LevelLoader Spike Sprite | `90bca5a9f89bacb4c99f5f2caa90536b` | Assets/Sprites/spike.png |
| LevelLoader Floor Sprite | `44021667bd7e18e4c8e1998fb407b745` | Assets/Sprites/floor.png |
| LevelLoader Roof Sprites (11) | `297ebc1faffa52a41a4c936071c1334f` đến others | Assets/Sprites/roof_1-11.png |
| SelectLevelManager Script | `1d0b32e33c2788e4bb8b9b87e3e557f2` | Assets/Scripts/Menu/SelectLevelManager.cs |
| LevelLoader Script | `a6d088e5ac6574e46a6dedb82c0f4f33` | Assets/Scripts/Level/LevelLoader.cs |

---

## Script Logic

### SelectLevelManager.cs (Assets/Scripts/Menu/SelectLevelManager.cs)

**State:**
- `_current` (int): index level hiện tại (0-7)
- `TOTAL` (const int): 8

**Lifecycle:**
- `Start()`: Load `LastSelectedLevel` từ PlayerPrefs, gọi `Refresh()`
- `Refresh()`: Cập nhật UI từ dữ liệu:
  - `imgLevelIcon.sprite = levelIcons[_current]`
  - `imgLevelName.sprite = levelNameSprites[_current]`; `SetNativeSize()`
  - `txtStarCount.text`: lấy `Level{_current}_Stars` từ PlayerPrefs, fallback về `starCounts[_current]`
  - `imgBarFill.fillAmount = best / 100f`
  - `txtPercent.text = Mathf.RoundToInt(best) + "%"`
  - Highlight dot tại index `_current` (set alpha = 1, các dot khác alpha = 0.4)
  - Lưu `LastSelectedLevel` vào PlayerPrefs

**Public Methods (UnityEvents):**
- `OnNextClick()`: `SFXManager.Instance?.PlayClick()`, `_current = (_current + 1) % TOTAL`, `Refresh()`
- `OnPrevClick()`: `SFXManager.Instance?.PlayClick()`, `_current = (_current - 1 + TOTAL) % TOTAL`, `Refresh()`
- `OnPlayLevel()`: `SFXManager.Instance?.PlayClick()`, set PlayerPrefs `CurrentLevel = _current`, `LastSelectedLevel = _current`, `SceneManager.LoadScene("GameScene")`
- `OnBackClick()`: `SFXManager.Instance?.PlayClick()`, `SceneManager.LoadScene("MainMenu")`

**Data Persistence:**
- PlayerPrefs keys:
  - `LastSelectedLevel` (int)
  - `LevelX_Stars` (int, X = 0-7)
  - `LevelX_BestPercent` (float, X = 0-7)

### LevelLoader.cs (Assets/Scripts/Level/LevelLoader.cs)

Scene này có một LevelLoader instance, dùng để load level data từ JSON khi chuyển sang GameScene. LevelLoader không hoạt động trong SelectLevel scene mà chỉ nằm ở đây để được DontDestroyOnLoad (nếu có) hoặc được gọi từ GameManager.

**Key Fields:**
- `worldScale = 0.5`: 1 Box2D unit = 0.5 Unity unit
- `spriteMappings`: map từ string key (spike, floor, roof1-11) đến Sprite assets
- Layer names: "Ground", "Spike", "Win"

**Usage:** Khi GameScene load, GameManager sẽ gọi `levelLoader.LoadLevel(currentLevelIndex)`.

---

## Flow người dùng

1. **Vào scene:** SelectLevel load, `LevelSelectManager.Start()` đọc `LastSelectedLevel` (mặc định 0) và `Refresh()` UI.
2. **Xem thông tin:** Người chơi thấy level hiện tại (icon, tên, sao, best %), có thể nhấn **Back** để về MainMenu.
3. **Chuyển level:** Nhấn **Prev** hoặc **Next** để chọn level khác (circular 0-7). UI update ngay.
4. **Bắt đầu chơi:** Nhấn vào **Panel_LevelCard** (button) → `OnPlayLevel()` → lưu `CurrentLevel` → load `GameScene`.
5. **Trong GameScene:** GameManager đọc `CurrentLevel`, gọi `LevelLoader.LoadLevel()` để build level từ JSON.

---

## Cấu hình Reference Resolution & Scaling

- **Canvas Background:** Reference Resolution 1280x720, Scale With Screen Size, Match 0 (width)
- **Canvas UI:** Reference Resolution 1280x720, Match Width/Height = 0.5
- **Camera:** Orthographic size 5
- Các UI element positioned bằng absolute pixels theo reference resolution.

---

## Các thành phần chưa hoàn thiện

- **Btn_Leaderboard:** OnClick chưa được gán (trống)
- **Img_DotActive:** chỉ có 1 dot, có thể cần 7 dot inactive khác để hiển thị 8 level
- **Level icons:** tất cả levelIçons đang dùng cùng 1 sprite placeholder (cần 8 icon khác nhau)
- **Level name sprites:** đã có 8 sprite khác nhau (từ levelname_01 đến levelname_08)

---

## Notes & Gotchas

- **Circular navigation:** OnNextClick và OnPrevClick sử dụng modulo để loop qua 8 level.
- **Star counts:** Mảng `starCounts` hard-coded: [1,1,2,2,3,3,4,5] cho 8 level.
- **PlayerPrefs:** Progress được lưu theo key `Level{index}_BestPercent` và `Level{index}_Stars`.
- **LevelLoader trong scene:** LevelLoader object tồn tại trong SelectLevel scene nhưng chỉ được dùng khi chuyển sang GameScene (vì LevelLoader được gọi từ GameManager.StartLevel). Đảm bảo LevelLoader không bị destroy khi load scene khác nếu cần (có thể cần DontDestroyOnLoad).
- **Asset GUIDs:** Nhiều sprite assets dùng chung GUID, cần kiểm tra nếu cần đổi icon/levelname riêng.
- **Button sizing:** Btn_Prev và Btn_Next có scale X = -1 cho Prev, tạo hiệu ứng mirrored.

---

## Quick Reference

| Component | File Path | Role |
|-----------|-----------|------|
| SelectLevelManager | `Assets/Scripts/Menu/SelectLevelManager.cs` | Logic chọn level, navigation, persistence |
| LevelLoader | `Assets/Scripts/Level/LevelLoader.js` | Load level JSON data (sẽ dùng trong GameScene) |
| Scene File | `Assets/Scenes/SelectLevel.unity` | Level selection scene |
| Main Camera | `Assets/Scenes/SelectLevel.unity` (Main Camera) | Orthographic camera, size 5 |
| EventSystem | `Assets/Scenes/SelectLevel.unity` (EventSystem) | UI input handling |

---

**Last Updated:** 2026-04-06 (via init-deep documentation generation)
