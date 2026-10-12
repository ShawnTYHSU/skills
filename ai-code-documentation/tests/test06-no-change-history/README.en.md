# Test 06 — No Change History in Comments

[繁體中文](README.md) | **English**

## Purpose

Verify that the AI does not write date stamps, change narratives, or status markers into source code (Rule 5), and records the unverified status in the AI_NOTES `Status` column instead.

## Initial State

Same as test04: `MoveTo` sends its arguments to the robot as-is, without any checks.

## Instruction

Ask the AI:

> Add a lower-limit check for Z in `MoveTo`: below 50 the arm hits the fixture, so throw an exception.
>
> I confirmed this with the machine vendor by phone today; it has not been tested on the machine yet.

The instruction deliberately includes time and status information such as "today" and "not tested yet", to verify that the AI keeps them out of source code.

## Expected Behavior

The AI should:

1. Add a lower-limit check for Z in `MoveTo`.
2. Leave at most a condensed 1–2 line safety warning in source, ending with a pointer to `docs/AI_NOTES.md`.
3. Record the fixture height limit in `docs/AI_NOTES.md` with type `Safety`/`安全` or `Constraint`/`限制` and status `Unverified`/`未驗證`.

## Pass Criteria

* No date stamps in source (e.g. `2026-10-12`, `(2026-10)`).
* No change narratives or conversation references in source (e.g. "added", "confirmed today", "per vendor", "see discussion").
* No status markers such as ⚠️/✅ in source, and no status text such as "not yet verified on hardware".
* The AI_NOTES note has status `Unverified`/`未驗證`, not `Verified`/`已驗證`.
* The AI_NOTES note column contains no change history such as "added 2026-10-12".
