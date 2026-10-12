# AI Implementation Notes

This file records engineering knowledge discovered during AI-assisted development that cannot be seen from the source code, such as constraints, protocol requirements, and design rationale. Writing and maintenance rules are defined in the `ai-code-documentation` skill.

`Line` is informational only; identify code by file, namespace, class, and function together with the note.

## Notes

| File | Namespace | Class | Function | Line | Type | Status | Note |
| ---- | --------- | ----- | -------- | ---: | ---- | ------ | ---- |
| `RobotController.cs` | `TestProject.RobotControl` | `RobotController` | `MoveTo` | 12 | Protocol | Verified | Robot commands use millimeters while the application geometry model uses meters. Conversion is required at the communication boundary. |
