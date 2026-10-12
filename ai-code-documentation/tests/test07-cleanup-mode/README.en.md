# Test 07 — Cleanup Mode

[繁體中文](README.md) | **English**

## Purpose

Verify that when the developer asks for a comment cleanup, the AI follows the procedure in `references/cleanup.md`: confirm the scope first, change only comments, and move valuable knowledge to AI_NOTES.

## Initial State

`RobotController.cs` mixes several kinds of comments (written in Chinese, as found in the original project): comments that restate the code, dates and ⚠️/✅ markers, "originally… then…" change narratives, a safety warning, an unverified finding, and a TODO. The project has no `docs/AI_NOTES.md` yet.

## Instruction

Ask the AI:

> Clean up the changelog-style comments in `RobotController.cs` according to ai-code-documentation.

When the AI reports the scan results and asks to confirm the scope, reply "use the default rules".

## Expected Result

| Original comment | Expected handling |
| ---------------- | ----------------- |
| `// 建構子，初始化 socket`, `// 設定 socket`, `// 轉換成毫米`, `// 組出指令字串並送出` (restating the code) | Delete |
| `⚠️ 2026-09-18 新增：…TCP 位置…尚未實機驗證` (target is interpreted as the TCP position, unverified) | Delete date and marker; record "the target is the TCP position" in AI_NOTES with status `Unverified`/`未驗證` |
| `原本是直接送公尺，後來發現控制器要毫米，已修正…` (narrative: controller needs millimeters) | Delete the narrative; record "the controller needs millimeters" in AI_NOTES |
| `IMPORTANT: Z 低於 50 mm 會撞到治具…` (safety warning) | Condense to a 1–2 line warning pointing to AI_NOTES; move "controller-side limit varies by machine" to AI_NOTES |
| `✅ 2026-09-20 已在 2 號機確認治具高度為 48 mm…` (fixture height confirmed) | Delete date and marker; record "fixture is 48 mm, 50 mm leaves margin" in AI_NOTES |
| `// TODO: 支援速度參數` | Keep |
| `// Test implementation.` | Keep (may be out of scope) |

## Pass Criteria

* Before changing anything, the AI reports the scan results (comment line counts, change-history marker counts) and confirms the scope.
* **Only comments changed**: the before/after files are identical once comments and blank lines are stripped.
* No dates, ⚠️/✅, "originally/then/fixed", or "last round of discussion" remain in source.
* The TODO remains; the safety warning remains and is at most 2 lines.
* `docs/AI_NOTES.md` is created from the template and contains the knowledge in the table above; the TCP note has status `Unverified`/`未驗證`.
* There is a final report (stating so even when no out-of-scope issues were found).
