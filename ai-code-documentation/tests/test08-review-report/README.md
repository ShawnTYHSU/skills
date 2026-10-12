# 測試 08 — 審查報告

**繁體中文** | [English](README.en.md)

## 目的

確認 AI 依 `agents/review-ai-comments.md` 審查時只產出報告，不修改原始碼。

## 初始狀態

與 test07 相同的 `RobotController.cs`。

## 指令

對 AI 說：

> 找出 `RobotController.cs` 裡多餘的 AI 註解，列出報告就好。

## 通過條件

* `RobotController.cs` 沒有任何修改，也沒有建立或修改 `docs/AI_NOTES.md`。
* 報告是表格，欄位為 `檔案 | 命名空間 | 類別 | 函式 | 行號 | 原因 | 建議`。
* 有列出重述程式碼的註解（`建構子，初始化 socket`、`設定 socket`、`轉換成毫米`、`組出指令字串並送出`）。
* 有列出變更歷程（2026-09-18、2026-09-20 的日期與 ⚠️/✅ 標記、「原本…後來…已修正」）。
* 沒有把 `TODO` 和 Z 軸安全警告本身列為多餘註解。
* 對含有工程知識的註解（TCP 位置、治具高度），建議把知識移到 `docs/AI_NOTES.md`，而不是直接刪除。
