# AI Code Documentation Rules

> English reference translation of [`SKILL.md`](SKILL.md). AI assistants load `SKILL.md` only; this file is for human readers. When changing the rules, update both files.

## Purpose

Keep production source code clean and prevent AI-generated explanatory comments from accumulating in the repo.

AI-generated explanations, implementation notes, assumptions, and design rationale should be recorded in:

`docs/AI_NOTES.md`

instead of being written directly into production source code.

These rules apply to all source code unless the developer explicitly asks for source comments.

---

# Core Rules

## Rule 1 — Do not add unnecessary comments to source code

When adding or modifying source code:

- Do not add comments just to explain obvious code behavior.
- Do not add comments like:
  - `// Initialize variables`
  - `// Check whether connected`
  - `// Iterate over all items`
  - `// Return the result`
  - `// This method is responsible for...`
- Do not generate lengthy AI explanations in source code.
- Do not add comments merely because "AI-generated code is easier to follow with explanations".

Prefer making these self-explanatory instead of relying on explanatory comments:

- Naming
- Methods
- Classes
- Types
- Structure

---

# Rule 2 — Preserve existing human comments

Do not remove, rewrite, or replace existing comments to comply with these rules.

Existing comments may be human-written and may contain important engineering knowledge.

Modify an existing comment only when:

- The developer explicitly asks
- The comment is actually wrong and the fix is necessary
- The developer asks for a documentation cleanup

---

# Rule 3 — Record valuable explanations separately

Some information is valuable but does not need to be embedded in source code.

For example:

- Non-obvious implementation decisions
- Communication protocol constraints
- Hardware constraints
- Compatibility requirements
- Workarounds
- Assumptions
- Side effects
- Reasons for an unusual implementation
- Undocumented behavior discovered during development
- Behavior obtained through reverse engineering
- Limitations of external systems

Record these in:

`docs/AI_NOTES.md`

Do not automatically add such explanations as source comments.

---

# Rule 4 — Do not record trivial explanations

Do not create an AI note for every function you change.

Create an AI note only when the information has meaningful engineering value.

For example, this has **no** value:

> This function checks whether the robot is connected.

But this **does** have value:

> The robot controller requires coordinates in millimeters even though the internal calculation layer uses meters.

---

# Rule 5 — Comments do not record change history

Comments describe what the code is and why it is that way, not how it came to be. Change history belongs in git commit messages.

When adding or modifying source code, do not write:

- Date stamps: `added 2026-09-18`, `(2026-09)`
- Change narratives: "changed this to…", "originally… then…", "previously misjudged…", "fixed", "was missing before"
- Stacked status markers: `⚠️ 2026-09-18 …`, `✅ verified`
- Commit hashes or references to conversations/sessions: "see today's discussion", "last round"

When a status such as "not yet verified" needs to be recorded, put it in the `Status` column of `docs/AI_NOTES.md`, not as a date or emoji in source code.

---

# Cleaning Up Existing Comments (Cleanup Mode)

Only when the developer **explicitly asks** to clean up existing comments (e.g. "remove the changelog-style comments", "trim the comments") may existing comments be modified according to this section. This is the exception to Rule 2. Do not clean up on your own initiative.

## How to handle each comment

| Action | Applies to |
| ------ | ---------- |
| **Keep** | One-line description; mapping notes (e.g. the corresponding legacy function or RPC name); units, ranges, meaning of `null`; `<exception>`; TODO/FIXME; license headers |
| **Condense and keep** | Safety-critical warnings: shorten to 1–2 lines stating "what must not be done, or else what happens"; move details to AI_NOTES |
| **Move to AI_NOTES, then delete** | Design rationale, protocol or hardware findings, unverified assumptions, incident lessons, differences from external documentation, reasons for deliberately not doing something |
| **Delete** | Change history listed in Rule 5; restating the code; conversational explanations; descriptions that are outdated or contradict the code (confirm with the developer when it is a contradiction) |

A single comment often mixes several kinds of content and must be split: e.g. "⚠️ Added 2026-09-18: the controller interprets the target as TCP, not yet verified on hardware" → delete the date, record "the controller interprets the target as TCP" in AI_NOTES (status `Unverified`), and keep only a one-line description in source.

## Retention standard for XML doc comments

- `<summary>`: 1–2 lines describing what it does; include mapping notes here.
- `<param>`, `<returns>`: only when units, coordinate frames, ranges, or the meaning of `null`/defaults are not obvious. Documenting only some parameters is intentional; complete them only if the project enables documentation generation (`GenerateDocumentationFile`) and treats CS1573 as an error.
- `<exception>`: keep.
- `<remarks>`: generally move to AI_NOTES; only safety-critical warnings may stay in condensed form.

## Project rules take precedence

Information that the project instruction file (e.g. `CLAUDE.md`) explicitly requires in source stays, minus dates and narrative. For example, if the project requires "hardware constants must state whether a value is official or estimated", keep short labels such as "(official spec)" or "(calibrated on hardware)" and move the calibration story to AI_NOTES.

## Procedure

1. Scan: list each file's comment line count and change-history markers (dates, ⚠️/✅, commit hashes, "originally/then/changed to/fixed", etc.), and confirm scope and retention rules with the developer first.
2. Batch: process by project or layer, splitting large files further; under version control, create a new branch and make one commit per batch.
3. For each batch: record the knowledge worth keeping in AI_NOTES first, then delete the source comments; **change only comments, never program behavior**.
4. Verify each batch:
   - **Only comments changed**: strip comments and blank lines from the before/after versions of each file; they must be identical.
   - **Build and tests** pass.
   - **References in comments still resolve**: confirm that every `<see cref>` or similar reference you added or changed points to an existing name. Projects without documentation generation do not warn about broken crefs; build temporarily with documentation generation to check (e.g. `dotnet build -p:GenerateDocumentationFile=true`).
5. If the project instruction file says "details are in the source comments", update it to point to `docs/AI_NOTES.md`.
6. Report: list issues found during cleanup that were out of scope and therefore not fixed, such as comments that contradict the code or outdated statements in the project instruction file.

## Common situations

- **Outdated or contradictory comments**: the comment describes behavior that does not match the code (e.g. says "no parameters" while the method has parameters). Delete clearly outdated ones; when behavior is in question (e.g. two places contradict each other and it is unclear which is right), rewrite to match the code and list it in the report for the developer to confirm.
- **Misplaced doc comments**: e.g. two `<summary>` blocks stacked on one member, one of which actually describes the next method. Move it to the correct member (move comments only, do not change code).
- **AI_NOTES line numbers**: take the line of the declaration (not a call site); line numbers shift a lot during cleanup, so recompute them at the end of each batch.

---

# AI_NOTES.md Format

## Language Setting

AI_NOTES.md supports two languages:

| Value | Language | Template |
| ----- | -------- | -------- |
| `zh-TW` (default) | Traditional Chinese | this skill's `docs/AI_NOTES.md` |
| `en` | English | this skill's `docs/AI_NOTES.en.md` |

Determine the language in this order and use the first one that applies:

1. **Explicit instruction from the developer in the conversation**, e.g. "Write AI_NOTES in English".
2. **Project instruction file setting**: the project's `CLAUDE.md` (or `AGENTS.md`, or whichever project instruction file the tool uses) contains the line:
   ```text
   AI_NOTES_LANGUAGE: en
   ```
   The value is `zh-TW` or `en`.
3. **Existing `docs/AI_NOTES.md`**: a title of `# AI Implementation Notes` means English; `# AI 開發筆記` means Traditional Chinese.
4. **Default**: Traditional Chinese (`zh-TW`).

Rules:

- The chosen language applies to all of AI_NOTES.md: title, rules, table header, types, statuses, and notes.
- Code identifiers always stay as they appear in the source, e.g. file paths, namespaces, classes, functions, RPC/command names, and setting keys.
- A single AI_NOTES.md must not mix languages. If the setting (items 1 and 2) differs from the language of the existing file, ask the developer whether to convert the whole file first; do not mix languages on your own.
- Review reports produced by `agents/review-ai-comments.md` use the same language.

## Creating the file

If `docs/AI_NOTES.md` does not exist, create it from the template matching the language setting (drop the final "範例" / "Example" section when copying). The file name is `docs/AI_NOTES.md` for both languages.

## Table Structure

| Field | Traditional Chinese | English |
| ----- | ------------------- | ------- |
| Header | `檔案 \| 命名空間 \| 類別 \| 函式 \| 行號 \| 類型 \| 狀態 \| 說明` | `File \| Namespace \| Class \| Function \| Line \| Type \| Status \| Note` |

`Type` values:

| Traditional Chinese | English | Purpose |
| ------------------- | ------- | ------- |
| `限制` | `Constraint` | Implementation or hardware constraint |
| `協定` | `Protocol` | Communication or protocol requirement |
| `設計` | `Design` | Design decision or architecture rationale |
| `硬體` | `Hardware` | Hardware-specific behavior |
| `權宜做法` | `Workaround` | Workaround for an external or legacy limitation |
| `效能` | `Performance` | Performance-related implementation detail |
| `相容性` | `Compatibility` | Compatibility requirement |
| `校正` | `Calibration` | Calibration or machine-specific information |
| `安全` | `Safety` | Safety-critical information |

`Status` values:

| Traditional Chinese | English | Meaning |
| ------------------- | ------- | ------- |
| `使用中` | `Active` | Currently valid |
| `已棄用` | `Deprecated` | No longer applicable, kept for history |
| `已驗證` | `Verified` | Confirmed on real hardware or in a real environment |
| `未驗證` | `Unverified` | Not yet confirmed on real hardware or in a real environment |
| `暫時` | `Temporary` | A stopgap that should later be replaced by a proper solution |

- Use `已驗證`/`Verified` only for items confirmed on real hardware or in a real environment; unit tests alone do not count.
- When there is no corresponding function (e.g. a class or record itself), put `—` in the `函式`/`Function` column.
- When a class has several notes that all apply to the class as a whole, they may be merged into one note with `—` in the `Function` column.

## Grouping when there are many notes

When there are more than about 20 notes, group them under the "Notes" section by project (or module), each group with a `###` heading and its own table, for example:

```markdown
## Notes

### MyApp.Core

| File | Namespace | Class | Function | Line | Type | Status | Note |
| ---- | --------- | ----- | -------- | ---: | ---- | ------ | ---- |
| ... |

### MyApp.Protocols

| File | ... |
```

Sort by file path within a group. Add new notes to the matching group instead of appending them at the end of the file.

## Writing the Note column

- State facts and reasons, not change history (same as Rule 5). When time context helps the reader, write only things like "confirmed with 9 poses", not "added 2026-09-18".
- One note covers one topic; two independent findings on the same method become two notes.
- When citing external documentation, give the document name and section so it can be traced.
