# AI Code Documentation Rules

> English reference translation of [`SKILL.md`](SKILL.md). AI assistants load `SKILL.md` only; this file is for human readers. When changing the rules, update both files.

Keep production source code clean and prevent AI-generated explanatory comments from accumulating in the repo. AI-generated explanations, assumptions, and design rationale go in `docs/AI_NOTES.md`, not in source code.

When the developer explicitly asks for source comments (e.g. "add XML doc comments to this method"), do as asked.

## Rule 1 — Do not add unnecessary comments to source code

- Do not add comments that explain obvious behavior, such as `// Initialize variables`, `// Check whether connected`, `// Iterate over all items`, `// Return the result`, `// This method is responsible for...`.
- Do not write lengthy AI explanations in source code, and do not add comments merely because "it is easier to follow with an explanation".
- Prefer naming, methods, classes, types, and structure that explain themselves.
- Exception: safety- or hardware-critical warnings may stay in source, condensed to 1–2 lines stating "what must not be done, or else what happens"; put the details in AI_NOTES.

## Rule 2 — Preserve existing comments

Treat every existing comment as human-written; it may contain important engineering knowledge. Do not remove, rewrite, or replace it to comply with these rules.

Modify an existing comment only when:

- The developer explicitly asks, including asking for a comment cleanup (see "Cleaning up and reviewing existing comments" below)
- The comment is actually wrong and the fix is necessary

## Rule 3 — Record only valuable knowledge in AI_NOTES

Do not write a note for every function you change. Record only information that cannot be seen by reading the source and has real engineering value, for example:

- Non-obvious implementation decisions, reasons for an unusual approach
- Constraints of communication protocols, hardware, or external systems
- Compatibility requirements, workarounds
- Assumptions, side effects
- Undocumented behavior discovered during development or through reverse engineering

| No value | Has value |
| -------- | --------- |
| This function checks whether the robot is connected. | The robot controller requires coordinates in millimeters even though the internal calculation layer uses meters. |

Do not automatically add such explanations as source comments.

## Rule 4 — Maintain existing notes

- Before changing source code, check `docs/AI_NOTES.md` for notes that are affected.
- When a note already covers the same issue, update it instead of creating a duplicate.
- When a constraint or approach changes, update the note and its `Status`; mark notes that no longer apply as `Deprecated`.
- Do not record temporary AI reasoning or conversational explanations.

## Rule 5 — Comments do not record change history

Comments describe what the code is and why it is that way, not how it came to be. Change history belongs in git commit messages. Do not write:

- Date stamps: `added 2026-09-18`, `(2026-09)`
- Change narratives: "changed this to…", "originally… then…", "previously misjudged…", "fixed", "was missing before"
- Stacked status markers: `⚠️ 2026-09-18 …`, `✅ verified`
- Commit hashes or references to conversations/sessions: "see today's discussion", "last round"

When a status such as "not yet verified" needs to be recorded, put it in the `Status` column of AI_NOTES.

## Cleaning up and reviewing existing comments

- Only when the developer **explicitly asks** to clean up existing comments (e.g. "remove the changelog-style comments", "trim the comments"), read this skill's `references/cleanup.md` first and follow its procedure. Do not clean up on your own initiative.
- When the developer asks to find redundant AI comments (report only, no changes), review according to this skill's `agents/review-ai-comments.md`.

---

# AI_NOTES.md Format

## Language Setting

Supports `zh-TW` (Traditional Chinese, default) and `en` (English). Use the first one that applies:

1. An explicit instruction from the developer in the conversation, e.g. "Write AI_NOTES in English".
2. `AI_NOTES_LANGUAGE: en` or `AI_NOTES_LANGUAGE: zh-TW` in the project instruction file (`CLAUDE.md`, `AGENTS.md`, etc.).
3. The title of an existing `docs/AI_NOTES.md`: `# AI Implementation Notes` means English, `# AI 開發筆記` means Traditional Chinese.
4. Default: Traditional Chinese.

- The whole file (title, header, types, statuses, notes) uses one language; code identifiers (file paths, namespaces, classes, functions, RPC/command names, setting keys) stay as they appear in the source.
- If the language from items 1 or 2 differs from the existing file, ask the developer whether to convert the whole file first; do not mix languages.
- Review reports use the same language.

## Creating the file

If `docs/AI_NOTES.md` does not exist, create it from this skill's template: `docs/AI_NOTES.md` for Traditional Chinese, `docs/AI_NOTES.en.md` for English, dropping the final "範例" / "Example" section when copying. The file name is `docs/AI_NOTES.md` for both languages.

## Columns

| Language | Header |
| -------- | ------ |
| Traditional Chinese | `檔案 \| 命名空間 \| 類別 \| 函式 \| 行號 \| 類型 \| 狀態 \| 說明` |
| English | `File \| Namespace \| Class \| Function \| Line \| Type \| Status \| Note` |

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
| `已驗證` | `Verified` | Confirmed on real hardware or in a real environment; unit tests alone do not count |
| `未驗證` | `Unverified` | Not yet confirmed on real hardware or in a real environment |
| `暫時` | `Temporary` | A stopgap that should later be replaced by a proper solution |

- `Line` is the line of the declaration (not a call site) and is informational only; identify code by file, namespace, class, and function together with the note.
- When there is no corresponding function (e.g. a class or record itself), put `—` in the `Function` column. Several notes that all apply to a class as a whole may be merged into one.
- Non-code files (XAML, config files, etc.): put `—` in `Namespace` and the resource key (e.g. `x:Key` or `TargetType`) in `Class`; use `—` for both when a note covers the whole file.

## Grouping when there are many notes

When there are more than about 20 notes, group them under the "Notes" section by project (or module), each group with a `###` heading and its own table, sorted by file path within a group. Add new notes to the matching group instead of appending them at the end of the file.

```markdown
## Notes

### MyApp.Core

| File | Namespace | Class | Function | Line | Type | Status | Note |
| ---- | --------- | ----- | -------- | ---: | ---- | ------ | ---- |
| ... |

### MyApp.Protocols
...
```

## Writing the Note column

- One note covers one topic; two independent findings on the same method become two notes.
- When time context helps, write only what is useful to the reader (e.g. "cross-checked with 9 poses"), not change history (same as Rule 5).
- When citing external documentation, give the document name and section so it can be traced.
