# AI Implementation Notes

This file stores non-obvious implementation knowledge discovered or generated during AI-assisted development.

The purpose of this file is to keep production source code focused on implementation while preserving useful engineering context that may otherwise be lost during AI-assisted development.

## Rules

* Write all notes in English. Keep code identifiers (files, namespaces, classes, functions, RPC/command names) as they appear in the source.
* Do not record trivial or self-evident code behavior.
* Do not duplicate information already obvious from the source code.
* Preserve existing human-written source comments.
* Use this file for non-obvious implementation details, constraints, protocol assumptions, design decisions, and engineering context.
* `Line` is informational only and must not be treated as a stable identifier.
* Update an existing note instead of creating a duplicate when the same issue already exists.
* Notes should describe facts, constraints, or implementation rationale rather than generic AI explanations.
* Safety-critical or hardware-critical warnings may remain directly in source code when necessary.
* If a developer explicitly requests a source-code comment, follow that request even if the comment would normally belong here.

## Note Types

Common values for `Type`:

* `Constraint` — implementation or hardware constraint
* `Protocol` — communication or protocol requirement
* `Design` — design decision or architecture rationale
* `Hardware` — hardware-specific behavior
* `Workaround` — workaround for an external or legacy limitation
* `Performance` — performance-related implementation detail
* `Compatibility` — compatibility requirement
* `Calibration` — calibration or machine-specific information
* `Safety` — safety-critical information

## Status

Common values for `Status`:

* `Active` — currently valid
* `Deprecated` — no longer applicable, kept for history
* `Verified` — confirmed on real hardware or in a real environment
* `Unverified` — not yet confirmed on real hardware or in a real environment (unit tests alone count as unverified)
* `Temporary` — a stopgap that should later be replaced by a proper solution

## Notes

| File | Namespace | Class | Function | Line | Type | Status | Note |
| ---- | --------- | ----- | -------- | ---: | ---- | ------ | ---- |

## Maintenance Guidelines

When modifying source code:

1. Check whether an existing note is affected.
2. Update an existing note if the implementation constraint has changed.
3. Add a new note only when the information is useful and non-obvious.
4. Avoid recording temporary AI reasoning or conversational explanations.
5. Avoid recording implementation details that can be directly understood by reading the source code.
6. Do not use line numbers as the primary way of identifying code. Use file, namespace, class, and function together with the description.

## Example

Format illustration only. Do not copy this example when creating a new file:

| File | Namespace | Class | Function | Line | Type | Status | Note |
| ---- | --------- | ----- | -------- | ---: | ---- | ------ | ---- |
| `src/RobotController.cs` | `RobotControl` | `RobotController` | `MoveTo` | 42 | Protocol | Verified | Robot commands use millimeters while the internal geometry model uses meters. Conversion is required at the communication boundary. |
