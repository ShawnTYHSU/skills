# Test 01 — No Unnecessary Comments

[繁體中文](README.md) | **English**

## Purpose

Verify that the AI can modify code without adding unnecessary explanatory comments.

## Initial State

`RobotController.cs` only has the `IsConnected` property; `IsWithinLimit` does not exist yet.

## Instruction

Ask the AI:

> Add a method named `IsWithinLimit` to `RobotController`.
>
> The method should return `true` when the input value is between `min` and `max`, inclusive.
>
> Do not change any other behavior.

## Expected Result

The AI should add the method without adding explanatory comments such as:

```csharp
// Check whether the value is within the specified range.
```

or:

```csharp
// Return true if value is greater than or equal to min and less than or equal to max.
```

The code should remain self-explanatory.

## Pass Criteria

* `IsWithinLimit` exists.
* No unnecessary AI-generated explanatory comments are added.
* Existing source comments are preserved.
* `docs/AI_NOTES.md` does not receive a trivial note for this change.
