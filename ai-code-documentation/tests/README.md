# AI Code Documentation Skill 測試

**繁體中文** | [English](README.en.md)

這些測試用來驗證 `ai-code-documentation` skill 的行為。

## 測試矩陣

| 測試 | 情境 | 預期行為 |
| ---- | ---- | -------- |
| `test01-no-comment` | 一般程式碼產生 | 不加不必要的註解 |
| `test02-important-comment` | 既有的工程註解 | 保留該註解 |
| `test03-explicit-comment` | 開發者明確要求加註解 | 加上要求的註解 |
| `test04-ai-note` | 非顯而易見的工程限制 | 把知識記錄到 `AI_NOTES.md` |
| `test05-no-duplicate` | 既有筆記已記錄同一問題 | 不建立重複筆記 |

## 建議測試流程

每個測試都在獨立的 Git 分支或測試 repo 中執行。

每個測試：

1. 把該測試資料夾的內容複製到測試 repo（`RobotController.cs`；test05 另附 `docs/AI_NOTES.md`）。
2. 安裝／啟用 AI Code Documentation Skill。
3. 把該測試 `README.md` 裡的指令交給 AI。
4. 檢查原始碼 diff。
5. 檢查 `docs/AI_NOTES.md`。
6. 對照該測試的通過條件比較結果。

所有測試共同的通過條件：`docs/AI_NOTES.md` 新寫入的內容必須符合 `SKILL.md` 的「語言設定」（預設繁體中文，程式碼識別名稱除外），類型、狀態使用該語言對應的值。

### 測試英文模式

要測英文模式時，在測試 repo 根目錄的 `CLAUDE.md`（或該工具使用的專案指示檔）加入一行：

```text
AI_NOTES_LANGUAGE: en
```

再執行同一組測試。預期 `docs/AI_NOTES.md` 依英文範本建立，表頭、類型、狀態、說明都是英文。

test05 在英文模式下，改把 `docs/AI_NOTES.en.md` 複製到測試 repo 並命名為 `docs/AI_NOTES.md`，作為英文的初始筆記。

額外檢查：英文模式下若仍使用中文的初始筆記，AI 應先詢問是否整份轉換，而不是混寫兩種語言。

## 重要

這些是行為測試，不是單元測試。

目的不只是確認產生的程式碼能編譯。

目的是確認 AI 輔助開發時有遵守專案的文件規範：

```text
正式原始碼
        │
        ├── 必要的原始碼註解
        │
        └── 實作程式碼
                 │
                 │
                 ▼
        非顯而易見的工程知識
                 │
                 ▼
          docs/AI_NOTES.md
```

期望結果是：正式程式碼保持乾淨，同時保留 AI 輔助開發過程中發現的有用工程知識。
