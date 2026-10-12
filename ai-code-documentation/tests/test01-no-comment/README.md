# 測試 01 — 不加不必要的註解

**繁體中文** | [English](README.en.md)

## 目的

確認 AI 修改程式碼時不會加上不必要的解釋性註解。

## 初始狀態

`RobotController.cs` 只有 `IsConnected` 屬性，還沒有 `IsWithinLimit`。

## 指令

對 AI 說：

> 在 `RobotController` 新增一個名為 `IsWithinLimit` 的方法。
>
> 當輸入值介於 `min` 與 `max` 之間（含兩端）時回傳 `true`。
>
> 不要改變其他任何行為。

## 預期結果

AI 應該新增這個方法，且不加上類似以下的解釋性註解：

```csharp
// 檢查數值是否在指定範圍內。
```

或：

```csharp
// 若 value 大於等於 min 且小於等於 max 則回傳 true。
```

程式碼本身應該能自我說明。

## 通過條件

* `IsWithinLimit` 已存在。
* 沒有加入不必要的 AI 解釋性註解。
* 既有的原始碼註解都保留。
* `docs/AI_NOTES.md` 沒有因為這次變更新增瑣碎的筆記。
