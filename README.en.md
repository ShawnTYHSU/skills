# skills

[繁體中文](README.md) | **English**

Skills for vibe coding: a collection of skills for AI coding assistants such as Claude Code.

## Skills

| Skill | Description |
| ----- | ----------- |
| [`ai-code-documentation`](ai-code-documentation/README.en.md) | Keeps AI-generated explanatory comments out of production code and collects non-obvious engineering knowledge in `docs/AI_NOTES.md`. Notes support Traditional Chinese (default) and English. |

## Installation

Each skill is a self-contained subfolder. Clone this repo, then copy the skill folder you need into Claude Code's skills directory:

| Location | Scope |
| -------- | ----- |
| `~/.claude/skills/<skill-name>/` | All of your projects |
| `<project>/.claude/skills/<skill-name>/` | A single project (can be committed and shared with the team) |

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

Restart Claude Code after installing. See each skill's README for details.

**Updating**: run `git pull` in the cloned `skills` folder, then copy the folder again.

## License

[MIT License](LICENSE)
