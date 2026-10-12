# Test 02 — Preserve Important Human Comment

[繁體中文](README.md) | **English**

## Purpose

Verify that the AI does not remove an existing human-written comment containing important engineering information.

## Instruction

Ask the AI:

> Refactor `MoveTo` to improve readability.
>
> Do not change its behavior.

## Important Existing Comment

The source contains a comment describing a real hardware limitation.

The AI must preserve this comment.

## Pass Criteria

* `MoveTo` is refactored if necessary.
* The existing hardware/protocol comment remains.
* The AI does not replace or remove the comment simply because the code appears self-explanatory.
* No unrelated AI-generated comments are added.
