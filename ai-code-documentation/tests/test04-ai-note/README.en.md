# Test 04 — Record Non-Obvious Engineering Knowledge

[繁體中文](README.md) | **English**

## Purpose

Verify that the AI records useful non-obvious engineering knowledge in `docs/AI_NOTES.md` instead of adding explanatory comments to production code.

## Initial State

`MoveTo` in `RobotController.cs` sends its arguments to the robot as-is, without any unit conversion.

## Instruction

Ask the AI:

> Update `MoveTo` so that the robot receives coordinates in millimeters.
>
> The application's internal coordinate system uses meters.
>
> Keep the production code clean and document any non-obvious protocol requirement according to the project's AI documentation rules.

## Expected Behavior

The AI should:

1. Convert meters to millimeters at the communication boundary.
2. Avoid adding a verbose explanatory source comment.
3. Add a useful entry to `docs/AI_NOTES.md`.
4. Classify the requirement as `Protocol`/`協定` or `Constraint`/`限制`.
5. Avoid duplicating an existing note if the same information already exists.

## Expected AI Note

The note should communicate this important engineering fact:

> Robot commands use millimeters while the application's internal geometry model uses meters. Conversion is required at the communication boundary.

The exact wording may differ, but the language must follow the language setting (Traditional Chinese by default).

## Pass Criteria

* The conversion exists in production code.
* No unnecessary explanatory comment is added to `MoveTo`.
* `docs/AI_NOTES.md` contains a corresponding non-obvious engineering note.
* The note identifies the relevant file/class/function.
* The note does not merely describe what the code obviously does.
* The note follows the language setting, and type/status use the values for that language.
