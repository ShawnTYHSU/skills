# AI Code Documentation Skill Tests

[繁體中文](README.md) | **English**

These tests validate the behavior of the `ai-code-documentation` skill.

## Test Matrix

| Test | Scenario | Expected behavior |
| ---- | -------- | ----------------- |
| `test01-no-comment` | Normal code generation | No unnecessary comments |
| `test02-important-comment` | Existing engineering comment | Preserve the comment |
| `test03-explicit-comment` | Developer explicitly requests comments | Add the requested comments |
| `test04-ai-note` | Non-obvious engineering constraint | Record the knowledge in `AI_NOTES.md` |
| `test05-no-duplicate` | Existing note already documents the issue | Do not create a duplicate note |
| `test06-no-change-history` | Instruction mentions dates and verification status | No change history in source; status goes in AI_NOTES |
| `test07-cleanup-mode` | Developer asks to clean up changelog-style comments | Confirm scope, change only comments, move knowledge to AI_NOTES |
| `test08-review-report` | Developer asks for a review of redundant comments | Report only, no file changes |

## Recommended Test Procedure

Run each test in an isolated Git branch or test repository.

For each test:

1. Copy the contents of the test folder into the test repo (`RobotController.cs`; test05 also includes `docs/AI_NOTES.md`).
2. Install/enable the AI Code Documentation Skill.
3. Give the AI the instruction from the test's README.
4. Review the source-code diff.
5. Review `docs/AI_NOTES.md`.
6. Compare the result against the test's pass criteria.

Pass criterion shared by all tests: new content written to `docs/AI_NOTES.md` must follow the "Language Setting" in `SKILL.md` (Traditional Chinese by default; code identifiers excepted), and types/statuses must use the values for that language.

### Testing English mode

To test English mode, add this line to the `CLAUDE.md` (or the tool's project instruction file) in the root of the test repo:

```text
AI_NOTES_LANGUAGE: en
```

Then run the same tests. `docs/AI_NOTES.md` is expected to be created from the English template, with the header, types, statuses, and notes all in English.

For test05 in English mode, copy `docs/AI_NOTES.en.md` into the test repo as `docs/AI_NOTES.md` to use it as the English initial notes.

Additional check: in English mode, if the Chinese initial notes are used instead, the AI should first ask whether to convert the whole file rather than mixing the two languages.

## Important

These are behavioral tests, not unit tests.

The goal is not only to verify that the generated code compiles.

The goal is to verify that AI-assisted development follows the project's documentation policy:

```text
Production source
        │
        ├── Necessary source comments
        │
        └── Implementation code
                 │
                 │
                 ▼
        Non-obvious engineering knowledge
                 │
                 ▼
          docs/AI_NOTES.md
```

The desired result is a clean production codebase that still retains the useful engineering knowledge discovered during AI-assisted development.
