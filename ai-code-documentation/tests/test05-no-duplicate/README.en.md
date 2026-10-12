# Test 05 — Avoid Duplicate Notes

[繁體中文](README.md) | **English**

## Purpose

Verify that the AI checks existing `AI_NOTES.md` entries before creating a new note.

## Initial State

`MoveTo` in `RobotController.cs` already converts meters to millimeters, and `docs/AI_NOTES.md` (included in this folder) already contains:

| 檔案 | 命名空間 | 類別 | 函式 | 行號 | 類型 | 狀態 | 說明 |
| ---- | -------- | ---- | ---- | ---: | ---- | ---- | ---- |
| `RobotController.cs` | `TestProject.RobotControl` | `RobotController` | `MoveTo` | 12 | 協定 | 已驗證 | 機器手臂指令以毫米為單位，應用程式幾何模型用公尺，必須在通訊邊界轉換。 |

In English mode, use `docs/AI_NOTES.en.md` instead (copy it as `docs/AI_NOTES.md`), which contains:

| File | Namespace | Class | Function | Line | Type | Status | Note |
| ---- | --------- | ----- | -------- | ---: | ---- | ------ | ---- |
| `RobotController.cs` | `TestProject.RobotControl` | `RobotController` | `MoveTo` | 12 | Protocol | Verified | Robot commands use millimeters while the application geometry model uses meters. Conversion is required at the communication boundary. |

## Instruction

Ask the AI:

> Refactor `MoveTo` while preserving the existing meter-to-millimeter conversion.

## Expected Behavior

The AI should recognize that the protocol requirement is already documented.

It should not create another identical note.

The existing note may be updated if the implementation changed, but unnecessary duplication should be avoided.

## Pass Criteria

* No duplicate `MoveTo` protocol note is created.
* Existing documentation remains valid.
* If the implementation changed the constraint, the existing note is updated rather than duplicated.
