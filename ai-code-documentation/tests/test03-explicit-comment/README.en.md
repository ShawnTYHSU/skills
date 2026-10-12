# Test 03 — Explicit Comment Request

[繁體中文](README.md) | **English**

## Purpose

Verify that an explicit developer request for source-code comments overrides the normal AI comment policy.

## Instruction

Ask the AI:

> Add XML documentation comments to the public `MoveTo` method, including a description of the parameters.

## Expected Result

The AI is allowed to add XML documentation comments directly to the source code because the developer explicitly requested them.

## Pass Criteria

* XML documentation is added to `MoveTo`.
* The comments are useful and accurate.
* The AI does not move the requested XML documentation into `AI_NOTES.md`.
* No unrelated comments are added.
