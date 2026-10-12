# AI Code Documentation Skill

**繁體中文** | [English](README.en.md)

讓 AI 程式助手**不要把解釋性註解塞進正式原始碼**，同時把真正有價值的工程知識集中記錄到 `docs/AI_NOTES.md`。

適用於 Claude Code 等支援 `SKILL.md` 格式的 AI 程式助手。筆記預設以繁體中文撰寫，可切換成英文（見[語言設定](#語言設定)）。

## 為什麼需要這個 skill

AI 程式助手寫程式時，常會順手加上這類註解：

```csharp
// 檢查是否已連線
if (!IsConnected) return;

// 逐一走訪所有項目
foreach (var item in items) { ... }
```

單看一次無害，但每次 AI 修改程式碼都會累積更多，最後原始碼被大量「重述程式碼本身」的註解淹沒，真正重要的人工註解反而不容易被看見。

另一方面，AI 開發過程中也常發現**真的值得留下**的資訊，例如：

- 控制器要求毫米輸入，但內部計算用公尺
- 某個 RPC 回傳格式官方文件沒寫，是實測得知
- 為了繞過外部系統限制採取的權宜做法

這些資訊如果只留在對話紀錄，對話結束就消失了。

這個 skill 的做法：

```text
AI 修改原始碼
   │
   ├── 顯而易見的說明 ──────────► 不寫（靠命名與結構自我說明）
   │
   ├── 既有人工註解 ────────────► 保留，不改寫
   │
   └── 非顯而易見的工程知識 ────► 記錄到 docs/AI_NOTES.md
```

## 核心規則

1. **不要在原始碼加不必要的註解**：優先用命名、方法、型別、結構讓程式碼自我說明。
2. **保留既有的人工註解**：除非開發者要求，或內容確實有誤。
3. **有價值的說明另外記錄**：寫到 `docs/AI_NOTES.md`，不寫成原始碼註解。
4. **不記錄瑣碎說明**：只記有工程參考價值的資訊。

例外：開發者**明確要求**加原始碼註解時（例如「幫這個方法加 XML 文件註解」），照要求做。安全或硬體關鍵的警告，必要時也可以留在原始碼。

完整規則見 [`SKILL.md`](SKILL.md)（英文對照：[`SKILL.en.md`](SKILL.en.md)）。

## 目錄結構

```text
ai-code-documentation/
├── SKILL.md                      # 主規範（AI 載入的內容）
├── SKILL.en.md                   # 主規範英文對照（給人閱讀，AI 不會載入）
├── README.md                     # 說明文件（繁體中文）
├── README.en.md                  # 說明文件（English）
├── LICENSE                       # MIT 授權
├── openai.yaml                   # 顯示名稱與觸發設定（OpenAI/Codex 格式）
├── docs/
│   ├── AI_NOTES.md               # AI_NOTES.md 範本（繁體中文，預設）
│   └── AI_NOTES.en.md            # AI_NOTES.md 範本（English）
├── agents/
│   └── review-ai-comments.md     # 審查既有程式碼中多餘 AI 註解的流程
└── tests/
    ├── README.md / README.en.md  # 測試矩陣與測試流程（中／英）
    ├── test01-no-comment/        # 一般開發不加不必要註解
    ├── test02-important-comment/ # 保留重要的人工註解
    ├── test03-explicit-comment/  # 開發者明確要求時要加註解
    ├── test04-ai-note/           # 非顯而易見的知識寫進 AI_NOTES.md
    └── test05-no-duplicate/      # 不建立重複筆記
```

每個測試資料夾都有 `README.md`（中文）與 `README.en.md`（英文）。

## 安裝

### Claude Code

這個 skill 放在 [ShawnTYHSU/skills](https://github.com/ShawnTYHSU/skills) repo 的 `ai-code-documentation/` 子資料夾。先 clone 整個 repo，再把這個資料夾複製到下列其中一個位置：

| 位置 | 適用範圍 |
| ---- | -------- |
| `~/.claude/skills/ai-code-documentation/` | 個人所有專案 |
| `<專案>/.claude/skills/ai-code-documentation/` | 單一專案（可隨 repo 版控，團隊共用） |

macOS / Linux / Git Bash：

```bash
git clone https://github.com/ShawnTYHSU/skills.git
mkdir -p ~/.claude/skills
cp -r skills/ai-code-documentation ~/.claude/skills/
```

Windows PowerShell：

```powershell
git clone https://github.com/ShawnTYHSU/skills.git
New-Item -ItemType Directory -Force "$HOME\.claude\skills" | Out-Null
Copy-Item -Recurse skills\ai-code-documentation "$HOME\.claude\skills\"
```

安裝後重新啟動 Claude Code，輸入 `/ai-code-documentation` 能看到這個 skill 就代表安裝成功。

**更新**：在 clone 下來的 `skills` 資料夾執行 `git pull`，再重新複製一次。

### 其他 AI 程式助手

若工具支援 `SKILL.md` 格式的 skill，依該工具的 skills 目錄放置即可。不支援的工具，可以把 `SKILL.md` 的內容貼到該工具的專案指示檔中使用。

## 語言設定

`docs/AI_NOTES.md` 與審查報告支援兩種語言，**預設為繁體中文**。

| 設定值 | 語言 | 使用的範本 |
| ------ | ---- | ---------- |
| `zh-TW`（預設） | 繁體中文 | `docs/AI_NOTES.md` |
| `en` | English | `docs/AI_NOTES.en.md` |

### 切換成英文

1. 在專案根目錄開啟（或建立）專案指示檔：Claude Code 是 `CLAUDE.md`；其他工具用它自己的專案指示檔（例如 `AGENTS.md`）。
2. 加入一行：
   ```text
   AI_NOTES_LANGUAGE: en
   ```
3. 存檔。之後 AI 建立或更新 `docs/AI_NOTES.md` 時會改用英文範本，表頭、類型、狀態、說明都用英文。

要切回中文，把值改成 `zh-TW` 或直接刪掉這一行即可。

### 只在單次對話中指定

不想改專案設定時，也可以直接在對話中告訴 AI，例如「這次 AI_NOTES 用英文寫」。對話中的指定優先於專案設定。

### 判斷順序

AI 依下列順序決定語言，採用第一個符合的：

1. 開發者在對話中明確指定
2. 專案指示檔中的 `AI_NOTES_LANGUAGE`
3. 既有 `docs/AI_NOTES.md` 的標題（`# AI Implementation Notes` 為英文，`# AI 開發筆記` 為中文）
4. 預設：繁體中文

> 同一份 `AI_NOTES.md` 不會混用兩種語言。如果設定的語言跟既有檔案不同，AI 會先詢問是否要把整份檔案轉換，不會自行混寫。

## 使用方式

### 觸發

Claude Code 會依 `SKILL.md` frontmatter 的 `description` 自動判斷：新增、修改、重構或除錯原始碼時載入。也可以手動輸入：

```text
/ai-code-documentation
```

若希望**只在手動輸入時才觸發**，在 `SKILL.md` frontmatter 加上：

```yaml
disable-model-invocation: true
```

> `openai.yaml` 裡的 `allow_implicit_invocation: false` 是 OpenAI/Codex 的設定，Claude Code 不會讀取它。

### 審查既有程式碼

想找出程式碼庫裡已經累積的多餘 AI 註解，可以要求 AI 依 [`agents/review-ai-comments.md`](agents/review-ai-comments.md) 的流程審查。這個流程只產出報告，不會自動刪除註解；報告語言跟隨[語言設定](#語言設定)。

> `agents/` 底下的檔案是給 AI 參考的流程文件，不會自動註冊成 Claude Code subagent。若要當 subagent 使用，需另外放到 `.claude/agents/` 並補上 subagent frontmatter。

## AI_NOTES.md 格式

第一次需要記錄時，AI 會依語言設定，從對應的範本在專案中建立 `docs/AI_NOTES.md`（兩種語言的檔名都一樣）。程式碼識別名稱一律保留原文。

| 檔案 | 命名空間 | 類別 | 函式 | 行號 | 類型 | 狀態 | 說明 |
| ---- | -------- | ---- | ---- | ---: | ---- | ---- | ---- |
| `src/RobotController.cs` | `RobotControl` | `RobotController` | `MoveTo` | 42 | 協定 | 已驗證 | 機器手臂指令以毫米為單位，內部幾何模型用公尺，必須在通訊邊界轉換。 |

**類型**

| 繁體中文 | English | 用途 |
| -------- | ------- | ---- |
| `限制` | `Constraint` | 實作或硬體限制 |
| `協定` | `Protocol` | 通訊或協定要求 |
| `設計` | `Design` | 設計決策或架構理由 |
| `硬體` | `Hardware` | 硬體特有行為 |
| `權宜做法` | `Workaround` | 為了外部或舊系統限制採取的做法 |
| `效能` | `Performance` | 效能相關的實作細節 |
| `相容性` | `Compatibility` | 相容性要求 |
| `校正` | `Calibration` | 校正或機台特有資訊 |
| `安全` | `Safety` | 安全關鍵資訊 |

**狀態**

| 繁體中文 | English | 意義 |
| -------- | ------- | ---- |
| `使用中` | `Active` | 目前有效 |
| `已棄用` | `Deprecated` | 已不再適用，保留作為歷史紀錄 |
| `已驗證` | `Verified` | 已在實機或真實環境確認（只有單元測試不算） |
| `未驗證` | `Unverified` | 尚未在實機或真實環境確認 |
| `暫時` | `Temporary` | 權宜性質，之後應該被正式做法取代 |

`行號` 僅供參考，程式碼變動後可能偏移；辨識時以檔案、命名空間、類別、函式搭配說明為準。

## 測試

`tests/` 是**行為測試**，不是單元測試：把各測試資料夾的內容複製到測試 repo、把指令交給 AI，再檢查原始碼 diff 和 `docs/AI_NOTES.md` 是否符合通過條件。

| 測試 | 情境 | 預期行為 |
| ---- | ---- | -------- |
| `test01-no-comment` | 一般程式碼產生 | 不加不必要的註解 |
| `test02-important-comment` | 既有的工程註解 | 保留該註解 |
| `test03-explicit-comment` | 開發者明確要求加註解 | 加上要求的註解 |
| `test04-ai-note` | 非顯而易見的工程限制 | 把知識記錄到 `AI_NOTES.md` |
| `test05-no-duplicate` | 既有筆記已記錄同一問題 | 不建立重複筆記 |

詳細流程（含英文模式的測法）見 [`tests/README.md`](tests/README.md)。建議每個測試都在獨立的分支或測試 repo 中執行。

## 自訂

- **語言**：用 `AI_NOTES_LANGUAGE` 切換（見[語言設定](#語言設定)）。要新增其他語言，需新增對應範本 `docs/AI_NOTES.<語言>.md`，並更新 `SKILL.md` 的「語言設定」與「表格結構」。
- **筆記位置**：預設 `docs/AI_NOTES.md`。要改路徑，修改 `SKILL.md`（及英文對照 `SKILL.en.md`）中所有出現這個路徑的地方。
- **類型／狀態**：可在 `SKILL.md`、`SKILL.en.md` 與兩份範本中增減，四處要保持一致。

## 授權

[MIT License](LICENSE)
