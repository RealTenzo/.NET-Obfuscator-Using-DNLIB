# Tenzo X Obfuscator


**Tenzo X Obfuscator** is a .NET obfuscation tool designed to protect your assemblies from reverse engineering by obfuscating the names of types, methods, fields, and properties, as well as applying various anti-debugging and anti-tampering techniques. This tool is useful for developers who want to secure their .NET applications from unwanted analysis and decompilation.

---

## Features

- **Obfuscates Class and Method Names**: Randomizes class and method names to make decompiled code harder to understand.
- **Junk Attributes**: Adds junk attributes to the assembly, making reverse engineering more difficult.
- **String Encryption**: Encrypts string literals to prevent string analysis.
- **Anti-De4Dot Protection**: Implements techniques to thwart De4Dot deobfuscation tool.
- **Protects Assemblies from Common Reverse Engineering Tools**: Prevents tampering and analysis by various .NET decompilers and debuggers.

---

## Installation Of Source

1. Download Source

2. Open the project in **Visual Studio**.

3. Ensure that the required NuGet packages are installed

4. After you can modify!


## Installation Of Exe For Normal Usage



1. At first Download The Exe from release
2. After run the exe :D thats it
3. make sure you have .net drivers



---



### Steps to Obfuscate an Assembly:

1. **Open the application**:
   Launch **Tenzo X Obfuscator**.

2. **Select the input assembly**:
   Click on the button to choose an executable (.exe) or DLL file you want to obfuscate.

3. **Configure Obfuscation Settings**:
   - **Junk Attributes**: Enter the number of junk attributes you want to add.
   - **Enable Encryption**: Check the option to encrypt strings.
   - **Enable Anti-De4Dot**: Check to add anti-De4Dot protections.

4. **Start Obfuscation**:
   Click the "Start Obfuscation" button to begin the obfuscation process. The obfuscated assembly will be saved in the same directory as the input assembly.

---

## Code Overview

### Key Functions:

- **ObfuscateAssembly**: This function reads the input assembly and applies the obfuscation methods to rename types and methods, add junk attributes, encrypt strings, and apply anti-deobfuscation techniques.
  
- **junkattrib**: Adds junk attributes (fake types) to the assembly, making it harder to analyze.

- **encryptString**: Encrypts string literals in the assembly to protect sensitive data.

- **antiDe4Dot**: Adds protections to prevent the use of De4Dot deobfuscator on the assembly.

---
![Tenzo X Obfuscator Logo](https://cdn.discordapp.com/attachments/1314356036328488990/1319596627882807316/image.png?ex=676689a1&is=67653821&hm=0b2a2ddf5f98e04c1fb5771d0245c43e36a9f8749ef1d146011a525652202eb3&)


## Contributing

Contributions are welcome! If you'd like to contribute, please fork the repository, create a new branch, and submit a pull request with your changes.

Make sure to follow these guidelines:

- Keep code clean and well-documented.
- Ensure your code is tested and works as expected.
- Follow the [.NET coding guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/).

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## Acknowledgements

- Thanks to the authors of **Dn Lib** and other open-source libraries used in this project.

---

## Contact

If you have any questions or feedback, feel free to open an issue on GitHub or contact me at [https://fbi.bio/tenzo.vs].

---

> **Disclaimer**: This tool is intended for legitimate use cases such as protecting your own assemblies. **Tenzo X Obfuscator** should not be used for malicious purposes. Please use it responsibly.
