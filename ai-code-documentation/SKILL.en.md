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
