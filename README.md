# skills

**繁體中文** | [English](README.en.md)

Skills for vibe coding：給 Claude Code 等 AI 程式助手使用的 skill 集合。

## Skill 清單

| Skill | 說明 |
| ----- | ---- |
| [`ai-code-documentation`](ai-code-documentation/README.md) | 避免 AI 把解釋性註解塞進正式原始碼，並把非顯而易見的工程知識集中記錄到 `docs/AI_NOTES.md`。筆記支援繁體中文（預設）與英文。 |

## 安裝

每個 skill 都是一個獨立的子資料夾。先 clone 這個 repo，再把需要的 skill 資料夾複製到 Claude Code 的 skills 目錄：

| 位置 | 適用範圍 |
| ---- | -------- |
| `~/.claude/skills/<skill 名稱>/` | 個人所有專案 |
| `<專案>/.claude/skills/<skill 名稱>/` | 單一專案（可隨 repo 版控，團隊共用） |

macOS / Linux / Git Bash：

```bash
git clone https://github.com/ShawnTYHSU/skills.git
mkdir -p ~/.claude/skills
cp -r skills/ai-code-documentation ~/.claude/skills/
```

Windows PowerShell：

```powershell
git clone https://github.com/ShawnTYHSU/skills.git
New-Item -ItemType Directory -Force "$HOME\.claude\skills" | Out-Null
Copy-Item -Recurse skills\ai-code-documentation "$HOME\.claude\skills\"
```

安裝後重新啟動 Claude Code。各 skill 的詳細用法見該資料夾的 README。

**更新**：在 clone 下來的 `skills` 資料夾執行 `git pull`，再重新複製一次。

## 授權

[MIT License](LICENSE)
