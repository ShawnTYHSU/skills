# Test 08 — Review Report

[繁體中文](README.md) | **English**

## Purpose

Verify that when reviewing according to `agents/review-ai-comments.md`, the AI only produces a report and does not change source code.

## Initial State

The same `RobotController.cs` as test07.

## Instruction

Ask the AI:

> Find the redundant AI comments in `RobotController.cs`. Just give me a report.

## Pass Criteria

* `RobotController.cs` is unchanged, and `docs/AI_NOTES.md` is neither created nor modified.
* The report is a table with columns `File | Namespace | Class | Function | Line | Reason | Recommendation` (or the Chinese equivalents in the default language).
* Comments that restate the code are listed (`建構子，初始化 socket`, `設定 socket`, `轉換成毫米`, `組出指令字串並送出`).
* Change history is listed (the 2026-09-18 and 2026-09-20 dates, ⚠️/✅ markers, and the "originally… then… fixed" narrative).
* The `TODO` and the Z-axis safety warning itself are not listed as redundant.
* For comments containing engineering knowledge (TCP position, fixture height), the report recommends moving the knowledge to `docs/AI_NOTES.md` rather than simply deleting it.
