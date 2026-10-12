# AI Implementation Notes

This file records engineering knowledge discovered during AI-assisted development that cannot be seen from the source code, such as constraints, protocol requirements, and design rationale. Writing and maintenance rules are defined in the `ai-code-documentation` skill.

`Line` is informational only; identify code by file, namespace, class, and function together with the note.

## Notes

| File | Namespace | Class | Function | Line | Type | Status | Note |
| ---- | --------- | ----- | -------- | ---: | ---- | ------ | ---- |

## Example

Format illustration only. Do not copy this example when creating a new file:

| File | Namespace | Class | Function | Line | Type | Status | Note |
| ---- | --------- | ----- | -------- | ---: | ---- | ------ | ---- |
| `src/RobotController.cs` | `RobotControl` | `RobotController` | `MoveTo` | 42 | Protocol | Verified | Robot commands use millimeters while the internal geometry model uses meters. Conversion is required at the communication boundary. |
