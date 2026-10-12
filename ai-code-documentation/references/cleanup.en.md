# Cleaning Up Existing Comments (Cleanup Mode)

> English reference translation of [`cleanup.md`](cleanup.md). AI assistants load `cleanup.md` only; this file is for human readers. When changing the rules, update both files.

Use this procedure only when the developer **explicitly asks** to clean up existing comments. This is the exception to Rule 2 in `SKILL.md`. Do not clean up on your own initiative.

## How to handle each comment

| Action | Applies to |
| ------ | ---------- |
| **Keep** | One-line description; mapping notes (e.g. the corresponding legacy function or RPC name); units, ranges, meaning of `null`; `<exception>`; TODO/FIXME; license headers |
| **Condense and keep** | Safety-critical warnings: shorten to 1–2 lines stating "what must not be done, or else what happens"; move details to AI_NOTES and end the warning with "see docs/AI_NOTES.md" |
| **Move to AI_NOTES, then delete** | Design rationale, protocol or hardware findings, unverified assumptions, incident lessons, differences from external documentation, reasons for deliberately not doing something |
| **Delete** | Change history listed in Rule 5; restating the code; conversational explanations; descriptions that are outdated or contradict the code (confirm with the developer when it is a contradiction) |

A single comment often mixes several kinds of content and must be split: e.g. "⚠️ Added 2026-09-18: the controller interprets the target as TCP, not yet verified on hardware" → delete the date, record "the controller interprets the target as TCP" in AI_NOTES (status `Unverified`), and keep only a one-line description in source.

## Project rules take precedence

Information that the project instruction file (e.g. `CLAUDE.md`) explicitly requires in source stays, minus dates and narrative. For example, if the project requires "hardware constants must state whether a value is official or estimated", keep short labels such as "(official spec)" or "(calibrated on hardware)" and move the calibration story to AI_NOTES.

Mapping descriptions that the project convention keeps in resource files (e.g. what a palette color is used for, region headings per consuming page) are necessary descriptions and stay.

## Procedure

1. **Scan**: list each file's comment line count and change-history markers (dates, ⚠️/✅, commit hashes, "originally/then/changed to/fixed", etc.), and confirm scope and retention rules with the developer first. A solution file (e.g. `.sln`) may reference projects outside the repository (e.g. a sibling repo); process only files inside the repository and exclude warnings from those external projects during verification. Do not scan tools or skills copied into the repository (e.g. `.claude/skills/`).
2. **Establish the test baseline**: build and run the tests on the unmodified version (e.g. check out main in a separate worktree). Record tests that already fail as known failures in AI_NOTES and report them at the end.
3. **Batch**: process by project or layer, splitting large files further; under version control, create a new branch and make one commit per batch.
4. **For each batch**: record the knowledge worth keeping in AI_NOTES first, then delete the source comments; **change only comments, never program behavior**.
5. **Verify each batch** and commit only if everything passes (do not chain verification and commit with `;`, or a failed verification will still commit; every step of a verification script, including regenerating AI_NOTES, must abort on failure):
   - **Only comments changed**: strip comments and blank lines from the before/after versions of each file; they must be identical.
   - **Build and tests** pass; only the known failures from step 2 are allowed, and any other failure stops the cleanup.
   - **References in comments still resolve**: confirm that every reference you added or changed (e.g. `<see cref>`) points to an existing name.
   - **Comment syntax is still valid**: when deleting a whole paragraph, do not leave a blank line inside an XML doc comment, or the doc comment breaks (CS1570 in C#).
   - **AI_NOTES line numbers**: line numbers shift a lot during cleanup, so recompute them at the end of each batch for notes that have one.
6. If the project instruction file says "details are in the source comments", update it to point to `docs/AI_NOTES.md`.
7. **Report**: list issues found during cleanup that were out of scope and therefore not fixed, such as comments that contradict the code or outdated statements in the project instruction file.

## Common situations

- **Outdated or contradictory comments**: the comment describes behavior that does not match the code (e.g. says "no parameters" while the method has parameters). Delete clearly outdated ones; when behavior is in question (e.g. two places contradict each other and it is unclear which is right), rewrite to match the code and list it in the report for the developer to confirm.
- **References between comments**: e.g. "same reason as the comment on X", where X's comment has been deleted or condensed. Rewrite the referring comment so it stands on its own. References to classes in another repository (e.g. "same reason as IFoo in OtherRepo") cannot be followed by readers; make the source comment self-contained and record the cross-repository relationship in AI_NOTES.
- **Contradictory verification status**: e.g. one place says "never tested on hardware" while another records a hardware test result. Remove such status statements from source, record the status once in the AI_NOTES `Status` column, and list it in the report.
- **Existing AI_NOTES in an older format**: keep every note, convert the title, header, and sections to the current template, and do not start a new file. Map `Type` and `Status` values that are not in the allowed lists (e.g. "Assumption", "Valid") to the closest allowed value; something confirmed only by reflection or documentation, never run in a real environment, must not be marked `Verified`.
- **Misplaced doc comments**: e.g. two doc comment blocks stacked on one member, one of which actually describes the next method. Move it to the correct member (move comments only, do not change code).

## .NET / C# projects

### Retention standard for XML doc comments

- `<summary>`: 1–2 lines describing what it does; include mapping notes here.
- `<param>`, `<returns>`: only when units, coordinate frames, ranges, or the meaning of `null`/defaults are not obvious. Documenting only some parameters is intentional; complete them only if the project enables documentation generation (`GenerateDocumentationFile`) and treats CS1573 as an error.
- `<exception>`: keep.
- `<remarks>`: generally move to AI_NOTES; only safety-critical warnings may stay in condensed form.

### Other

- **Checking `<see cref>`**: projects without documentation generation do not warn about broken crefs; build temporarily with documentation generation to check: `dotnet build -p:GenerateDocumentationFile=true -p:NoWarn=CS1591` (CS1591 is "missing XML comment" and is unrelated to crefs).
- **Crefs to upper-layer or external types**: a lower-layer project (e.g. Abstractions) cannot reference types in an upper layer (e.g. Core) or another repository, so such `<see cref>` never resolves; use `<c>TypeName</c>` instead.
- **Members created by source generators**: e.g. `[ObservableProperty]` generates property `InitialValue` from field `initialValue`; put the field name that actually exists in source in the AI_NOTES `Function` column.
