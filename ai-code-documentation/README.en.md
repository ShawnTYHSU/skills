# AI Code Documentation Skill

[繁體中文](README.md) | **English**

Keeps AI coding assistants from **filling production source code with explanatory comments**, while collecting the engineering knowledge that is actually worth keeping in `docs/AI_NOTES.md`.

Works with Claude Code and other AI coding assistants that support the `SKILL.md` format. Notes are written in Traditional Chinese by default and can be switched to English (see [Language Setting](#language-setting)).

## Why this skill

AI coding assistants tend to add comments like these as they write code:

```csharp
// Check whether the robot is connected
if (!IsConnected) return;

// Iterate over all items
foreach (var item in items) { ... }
```

One of them is harmless. But every AI edit adds more, until the source is buried in comments that merely restate the code, and the important human-written comments become hard to find.

On the other hand, AI-assisted development also uncovers information that **is** worth keeping, for example:

- The controller expects millimeters, while internal calculations use meters
- An RPC's return format is undocumented and was found by testing on real hardware
- A workaround for a limitation in an external system

If this knowledge only lives in the chat history, it is lost when the conversation ends.

This skill's approach:

```text
AI modifies source code
   │
   ├── Obvious explanations ─────────────► Not written (naming and structure explain the code)
   │
   ├── Existing human comments ──────────► Preserved, not rewritten
   │
   └── Non-obvious engineering knowledge ► Recorded in docs/AI_NOTES.md
```

## Core Rules

1. **Do not add unnecessary comments to source code**: prefer naming, methods, types, and structure that explain themselves.
2. **Preserve existing human comments**: unless the developer asks, or the comment is actually wrong.
3. **Record valuable explanations separately**: write them to `docs/AI_NOTES.md`, not as source comments.
4. **Do not record trivial explanations**: only record information with real engineering value.
5. **Comments do not record change history**: no date stamps, no "originally… then changed to…", no commit hashes; history belongs in git, and states such as "unverified" go in the AI_NOTES `Status` column.

Exception: when the developer **explicitly asks** for source comments (e.g. "add XML doc comments to this method"), follow the request. Safety-critical or hardware-critical warnings may also stay in source code when necessary.

See [`SKILL.en.md`](SKILL.en.md) for the full rules in English. The AI loads [`SKILL.md`](SKILL.md) (written in Chinese), which it follows regardless of the language you work in; `SKILL.en.md` is a reference translation for human readers.

## Directory Structure

```text
ai-code-documentation/
├── SKILL.md                      # Main rules (what the AI loads, Chinese)
├── SKILL.en.md                   # English translation of the rules (for humans; not loaded by the AI)
├── README.md                     # Documentation (Traditional Chinese)
├── README.en.md                  # Documentation (English)
├── LICENSE                       # MIT License
├── openai.yaml                   # Display name and invocation settings (OpenAI/Codex format)
├── docs/
│   ├── AI_NOTES.md               # AI_NOTES.md template (Traditional Chinese, default)
│   └── AI_NOTES.en.md            # AI_NOTES.md template (English)
├── agents/
│   └── review-ai-comments.md     # Procedure for reviewing existing AI comments in a codebase
└── tests/
    ├── README.md / README.en.md  # Test matrix and procedure (Chinese / English)
    ├── test01-no-comment/        # No unnecessary comments in normal development
    ├── test02-important-comment/ # Preserve important human comments
    ├── test03-explicit-comment/  # Add comments when the developer explicitly asks
    ├── test04-ai-note/           # Record non-obvious knowledge in AI_NOTES.md
    └── test05-no-duplicate/      # Do not create duplicate notes
```

Every test folder has a `README.md` (Chinese) and a `README.en.md` (English).

## Installation

### Claude Code

This skill lives in the `ai-code-documentation/` subfolder of the [ShawnTYHSU/skills](https://github.com/ShawnTYHSU/skills) repo. Clone the repo, then copy this folder to one of these locations:

| Location | Scope |
| -------- | ----- |
| `~/.claude/skills/ai-code-documentation/` | All of your projects |
| `<project>/.claude/skills/ai-code-documentation/` | A single project (can be committed and shared with the team) |

macOS / Linux / Git Bash:

```bash
git clone https://github.com/ShawnTYHSU/skills.git
mkdir -p ~/.claude/skills
cp -r skills/ai-code-documentation ~/.claude/skills/
```

Windows PowerShell:

```powershell
git clone https://github.com/ShawnTYHSU/skills.git
New-Item -ItemType Directory -Force "$HOME\.claude\skills" | Out-Null
Copy-Item -Recurse skills\ai-code-documentation "$HOME\.claude\skills\"
```

Restart Claude Code after installing. If `/ai-code-documentation` shows up, the skill is installed.

**Updating**: run `git pull` in the cloned `skills` folder, then copy the folder again.

### Other AI coding assistants

If the tool supports `SKILL.md`-style skills, place the folder in that tool's skills directory. Otherwise, paste the contents of `SKILL.md` into the tool's project instruction file.

## Language Setting

`docs/AI_NOTES.md` and review reports support two languages. **The default is Traditional Chinese.**

| Value | Language | Template used |
| ----- | -------- | ------------- |
| `zh-TW` (default) | Traditional Chinese | `docs/AI_NOTES.md` |
| `en` | English | `docs/AI_NOTES.en.md` |

### Switch to English

1. Open (or create) the project instruction file in the project root: `CLAUDE.md` for Claude Code, or the tool's own instruction file for other tools (e.g. `AGENTS.md`).
2. Add this line:
   ```text
   AI_NOTES_LANGUAGE: en
   ```
3. Save the file. From then on, the AI uses the English template when creating or updating `docs/AI_NOTES.md`; headers, types, statuses, and notes are all in English.

To switch back to Chinese, change the value to `zh-TW` or remove the line.

### Specify for a single conversation

If you don't want to change project settings, just tell the AI in the conversation, e.g. "Write AI_NOTES in English this time." An instruction in the conversation takes priority over the project setting.

### Resolution order

The AI uses the first rule that applies:

1. An explicit instruction from the developer in the conversation
2. `AI_NOTES_LANGUAGE` in the project instruction file
3. The title of the existing `docs/AI_NOTES.md` (`# AI Implementation Notes` = English, `# AI 開發筆記` = Chinese)
4. Default: Traditional Chinese

> A single `AI_NOTES.md` never mixes languages. If the configured language differs from the existing file, the AI asks whether to convert the whole file instead of mixing languages on its own.

## Usage

### Invocation

Claude Code decides automatically from the `description` in the `SKILL.md` frontmatter and loads the skill when adding, modifying, refactoring, or debugging source code. You can also invoke it manually:

```text
/ai-code-documentation
```

To **only invoke it manually**, add this to the `SKILL.md` frontmatter:

```yaml
disable-model-invocation: true
```

> `allow_implicit_invocation: false` in `openai.yaml` is an OpenAI/Codex setting; Claude Code does not read it.

### Cleaning up existing comments (cleanup mode)

When a codebase has accumulated changelog-style comments, explicitly ask the AI to clean them up, for example:

```text
Clean up the changelog-style comments in MyProject following ai-code-documentation; keep only necessary descriptions and parameter definitions
```

The AI follows the "Cleanup Mode" procedure in `SKILL.md`: scan and confirm scope first, then batch by batch move valuable knowledge to `docs/AI_NOTES.md` and delete the narrative, verifying for each batch that only comments changed and that the build and tests pass, and finally report issues found but not handled (such as comments that contradict the code). Without an explicit request, the AI does not clean up existing comments.

### Reviewing an existing codebase

To find unnecessary AI comments that have already accumulated, ask the AI to review the codebase following [`agents/review-ai-comments.md`](agents/review-ai-comments.md). The procedure only produces a report and never deletes comments automatically; the report follows the [language setting](#language-setting).

> Files under `agents/` are procedure documents for the AI. They are not automatically registered as Claude Code subagents. To use one as a subagent, place it under `.claude/agents/` and add subagent frontmatter.

## AI_NOTES.md Format

The first time something needs to be recorded, the AI creates `docs/AI_NOTES.md` in your project from the template matching the language setting (the file name is the same in both languages). Code identifiers are always kept as they appear in the source.

| File | Namespace | Class | Function | Line | Type | Status | Note |
| ---- | --------- | ----- | -------- | ---: | ---- | ------ | ---- |
| `src/RobotController.cs` | `RobotControl` | `RobotController` | `MoveTo` | 42 | Protocol | Verified | Robot commands use millimeters while the internal geometry model uses meters. Conversion is required at the communication boundary. |

**Type**

| English | 繁體中文 | Purpose |
| ------- | -------- | ------- |
| `Constraint` | `限制` | Implementation or hardware constraint |
| `Protocol` | `協定` | Communication or protocol requirement |
| `Design` | `設計` | Design decision or architecture rationale |
| `Hardware` | `硬體` | Hardware-specific behavior |
| `Workaround` | `權宜做法` | Workaround for an external or legacy limitation |
| `Performance` | `效能` | Performance-related implementation detail |
| `Compatibility` | `相容性` | Compatibility requirement |
| `Calibration` | `校正` | Calibration or machine-specific information |
| `Safety` | `安全` | Safety-critical information |

**Status**

| English | 繁體中文 | Meaning |
| ------- | -------- | ------- |
| `Active` | `使用中` | Currently valid |
| `Deprecated` | `已棄用` | No longer applicable, kept for history |
| `Verified` | `已驗證` | Confirmed on real hardware or in a real environment (unit tests alone do not count) |
| `Unverified` | `未驗證` | Not yet confirmed on real hardware or in a real environment |
| `Temporary` | `暫時` | A stopgap that should later be replaced by a proper solution |

`Line` is informational only and may drift as code changes; identify code by file, namespace, class, and function together with the note.

## Tests

`tests/` contains **behavioral tests**, not unit tests: copy a test folder into a test repo, give its instruction to the AI, then check the source diff and `docs/AI_NOTES.md` against the pass criteria.

| Test | Scenario | Expected behavior |
| ---- | -------- | ----------------- |
| `test01-no-comment` | Normal code generation | No unnecessary comments |
| `test02-important-comment` | Existing engineering comment | Preserve the comment |
| `test03-explicit-comment` | Developer explicitly requests comments | Add the requested comments |
| `test04-ai-note` | Non-obvious engineering constraint | Record the knowledge in `AI_NOTES.md` |
| `test05-no-duplicate` | Existing note already documents the issue | Do not create a duplicate note |

To test English mode, add `AI_NOTES_LANGUAGE: en` to the test repo's `CLAUDE.md` and run the same tests. See [`tests/README.en.md`](tests/README.en.md) for the detailed procedure. Run each test in an isolated branch or test repository.

## Customization

- **Language**: switch with `AI_NOTES_LANGUAGE` (see [Language Setting](#language-setting)). To add another language, add a template `docs/AI_NOTES.<lang>.md` and update the "語言設定" (Language Setting) and "表格結構" (Table Structure) sections of `SKILL.md`.
- **Notes location**: defaults to `docs/AI_NOTES.md`. To change it, update every occurrence of the path in `SKILL.md` (and its translation `SKILL.en.md`).
- **Types / statuses**: can be added or removed in `SKILL.md`, `SKILL.en.md`, and both templates; keep all four consistent.

## License

[MIT License](LICENSE)
