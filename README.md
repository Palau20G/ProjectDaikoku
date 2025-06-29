<h1 align="center">
  🏮 Project Daikoku 🏮  
</h1>

<p align="center">
  <b><i>A Modular Malware Research Framework in C#</i></b><br>
  For educational and research use only.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Language-CSharp-purple?style=flat-square"/>
  <img src="https://img.shields.io/badge/Status-Active-brightgreen?style=flat-square"/>
  <img src="https://img.shields.io/badge/License-Educational-lightgrey?style=flat-square"/>
</p>

---

## 📌 Overview

**Project Daikoku** is a C#-based framework for building, simulating, and analyzing malware components in a safe and modular way. Inspired by real-world malware and red team tooling, it's designed to educate and empower security researchers through hands-on experience with malware techniques.

> ⚠️ **This project is for educational and research purposes only. Do not use on any network or system you do not own or have explicit permission to test.**

---

## 🧱 Architecture

The framework is built around clean, extensible interfaces and modules:

ProjectDaikoku/
├── Interfaces/ # Core abstractions (ICommand, IModule, etc.)
├── Core/ # Command router, loader, main loop
├── Modules/ # Individual functionality (ReverseShell, Keylogger, etc.)
├── Common/ # Utilities, crypto, config
├── Docs/ # Thesis notes, design documentation
├── Scripts/ # Listeners, analysis tools
└── Daikoku.sln # Visual Studio solution


---

## ⚙️ Features

| Module         | Description                            | Status   |
|----------------|----------------------------------------|----------|
| 🔁 ReverseShell  | TCP-based reverse shell for command execution | 🔄 In Dev|
| 🖥️ Keylogger     | Simulated keylogger (for research only)       | 🔄 In Dev  |
| 🧠 Persistence   | Adds registry startup keys                 | 🔄 In Dev  |
| 🌐 HTTP C2       | Polling C2 via HTTP requests              | 🧪 Experimental |
| 🎭 Obfuscation   | Base64 + simple XOR for evasion           | 🔄 In Dev  |
| 🧪 SandboxEvasion| Detects sandbox/debugger environments     | 🔄 Planned |

---

## 🧠 Goals

- ✅ Learn core malware development techniques in a safe lab
- ✅ Understand how persistence, evasion, and communication work
- ✅ Reverse engineer your own modules for practice
- ✅ Generate thesis material from real code and experiments

---

## 🚀 Getting Started

### 🔧 Requirements
- Windows 10 (VM recommended)
- .NET SDK 6.0+
- Visual Studio or `dotnet` CLI

### 🖥️ Build & Run
```bash
git clone https://github.com/youruser/project-daikoku.git
cd project-daikoku
dotnet build

Run only inside isolated VMs. Never test on a live machine.

🎓 For Self study Use
The project includes:

📝 Module design notes

📊 Behavior logs (sandbox output, screenshots)

📖 Documented case studies for each module

📁 Safe test samples and simulation logic

🚨 Do not use this code for illegal or unauthorized purposes.

🤝 Acknowledgments
"Practical Malware Analysis" by Sikorski & Honig

Malware Unicorn's RE 101

The open-source infosec community

All the defenders out there fighting the good fight

### 🖕 BIG FUCKING DISCLAIMER
## I will not be held responsible if you use this sht for illegal purposes. I did this for self learning, no other than that.


<p align="center"> <i>May Daikoku bring fortune to your research — and reveal the shadows within the system.</i><br> 🏮🧠💻 </p> """