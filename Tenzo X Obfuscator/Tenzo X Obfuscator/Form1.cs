using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypeAttributes = dnlib.DotNet.TypeAttributes;

namespace Tenzo_X_Obfuscator
{
    public partial class Form1 : Form
    {
        string[] attrib = { "ObfuscatedByGoliath", "NineRays.Obfuscator.Evaluation", "NetGuard", "dotNetProtector", "YanoAttribute", "Xenocode.Client.Attributes.AssemblyAttributes.ProcessedByXenocode", "PoweredByAttribute", "DotNetPatcherPackerAttribute", "DotNetPatcherObfuscatorAttribute", "DotfuscatorAttribute", "CryptoObfuscator.ProtectedWithCryptoObfuscatorAttribute", "BabelObfuscatorAttribute", "BabelAttribute", "AssemblyInfoAttribute", "ZYXDNGuarder", "ConfusedByAttribute" };
        bool protectname = false;
        bool fakeobf = false;
        bool junkatrb = false;
        bool encryptstring = false;
        bool antide4dot = false;

        public Form1()
        {
            InitializeComponent();
        }

        public static byte[] encodeBytes(byte[] bytes, string pass)
        {
            byte[] TenzoBytes = Encoding.Unicode.GetBytes(pass);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] ^= TenzoBytes[i % 16];
            }
            return bytes;
        }

        public static byte[] decryptBytes(byte[] bytes, String pass)
        {
            byte[] TenzoBytes = Encoding.Unicode.GetBytes(pass);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] ^= TenzoBytes[i % 16];
            }
            return bytes;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void ProtectName(AssemblyDef assembly, ModuleDef mod)
        {
            foreach (TypeDef type in mod.Types)
            {
                mod.Name = "ObfuscatedByProjectTenzo";
                if (type.IsGlobalModuleType || type.IsRuntimeSpecialName || type.IsSpecialName || type.IsWindowsRuntime || type.IsInterface)
                {
                    continue;
                }
                else
                {
                    foreach (PropertyDef property in type.Properties)
                    {
                        if (property.IsRuntimeSpecialName) continue;
                        property.Name = RandomString(20) + "難読化ＰＲＯＪＥＣＴ-ＴＥＮＺＯ難読化";
                    }
                    foreach (FieldDef fields in type.Fields)
                    {
                        fields.Name = RandomString(20) + "難読化ＰＲＯＪＥＣＴ-ＴＥＮＺＯ難読化";
                    }
                    foreach (EventDef eventdef in type.Events)
                    {
                        eventdef.Name = RandomString(20) + "難読化ＰＲＯＪＥＣＴ-ＴＥＮＺＯ難読化";
                    }
                    foreach (MethodDef method in type.Methods)
                    {
                        if (method.IsConstructor || method.IsRuntimeSpecialName || method.IsRuntime || method.IsStaticConstructor || method.IsVirtual) continue;
                        method.Name = RandomString(20);
                    }
                }
            }
        }

        public void fakeobfuscation(ModuleDefMD module)
        {
            for (int i = 0; i < attrib.Length; i++)
            {
                var fakeattrib = new TypeDefUser(attrib[i], attrib[i], module.CorLibTypes.Object.TypeDefOrRef);
                fakeattrib.Attributes = TypeAttributes.Class | TypeAttributes.NotPublic | TypeAttributes.WindowsRuntime;
                module.Types.Add(fakeattrib);
            }
        }

        public void junkattrib(ModuleDefMD module)
        {
            int number = System.Convert.ToInt32(guna2TextBox2.Text);
            for (int i = 0; i < number; i++)
            {
                var junkatrb = new TypeDefUser("難読化ＰＲＯＪＥＣＴ-ＴＥＮＺＯ難読化" + RandomString(20), "難読化ＰＲＯＪＥＣＴ-ＴＥＮＺＯ難読化" + RandomString(20), module.CorLibTypes.Object.TypeDefOrRef);
                module.Types.Add(junkatrb);
            }
        }

        public void encryptString(ModuleDef module)
        {
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (method.Body == null) continue;
                    method.Body.SimplifyBranches();
                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                        {
                            string base64toencrypt = method.Body.Instructions[i].Operand.ToString();
                            string base64EncryptedString = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(base64toencrypt));
                            method.Body.Instructions[i].OpCode = OpCodes.Nop;
                            method.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, module.Import(typeof(System.Text.Encoding).GetMethod("get_UTF8", new Type[] { }))));
                            method.Body.Instructions.Insert(i + 2, new Instruction(OpCodes.Ldstr, base64EncryptedString));
                            method.Body.Instructions.Insert(i + 3, new Instruction(OpCodes.Call, module.Import(typeof(System.Convert).GetMethod("FromBase64String", new Type[] { typeof(string) }))));
                            method.Body.Instructions.Insert(i + 4, new Instruction(OpCodes.Callvirt, module.Import(typeof(System.Text.Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) }))));
                            i += 4;
                        }
                    }
                }
            }
        }

        public void antiDe4Dot(ModuleDefMD module)
        {
            Random rnd = new Random();
            InterfaceImpl Interface = new InterfaceImplUser(module.GlobalType);
            for (int i = 200; i < 300; i++)
            {
                TypeDef typedef = new TypeDefUser("", $"Form{i.ToString()}", module.CorLibTypes.GetTypeRef("System", "Attribute"));
                InterfaceImpl interface1 = new InterfaceImplUser(typedef);
                module.Types.Add(typedef);
                typedef.Interfaces.Add(interface1);
                typedef.Interfaces.Add(Interface);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guna2Transition1.Show(this);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "Executables | *.*";
            openfiledialog.ShowDialog();
            guna2TextBox1.Text = openfiledialog.FileName;
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            encryptstring = !encryptstring;
        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            junkatrb = !junkatrb;
        }

        private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            protectname = !protectname;
        }

        private void guna2CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            antide4dot = !antide4dot;
        }

        private void guna2CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            fakeobf = !fakeobf;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AssemblyDef assembly = AssemblyDef.Load(guna2TextBox1.Text);
            ModuleContext modCtx = ModuleDefMD.CreateModuleContext();
            ModuleDefMD module = ModuleDefMD.Load(guna2TextBox1.Text, modCtx);

            // Apply obfuscation methods based on checkbox states
            if (antide4dot)
            {
                antiDe4Dot(module);
            }

            if (protectname)
            {
                ProtectName(assembly, module);
            }

            if (fakeobf)
            {
                fakeobfuscation(module);
            }

            if (junkatrb)
            {
                junkattrib(module);
            }

            if (encryptstring)
            {
                encryptString(module);
            }

            // Check if the metadata spoofing flag is enabled
            if (spoofMetadata)
            {
                // Spoof metadata when the flag is true
                SpoofMetadata(module);
            }

            // Save the final module to the specified path
            module.Write(guna2TextBox3.Text + ".exe");

            // Display success message
            MessageBox.Show("Made By Tenzo :)", "Thank you for using the obfuscator :D", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        public void SpoofMetadata(ModuleDefMD module)
        {

            var assembly = module.Assembly;

            // Set AssemblyTitle to a C++ application name
            assembly.Name = "MyCPlusPlusApp";

            // Import the constructor for AssemblyCompanyAttribute
            var companyAttrCtor = module.Import(typeof(AssemblyCompanyAttribute).GetConstructor(new[] { typeof(string) }));
            if (companyAttrCtor != null)
            {
                var companyAttr = new CustomAttribute((ICustomAttributeType)companyAttrCtor);
                companyAttr.ConstructorArguments.Add(new CAArgument(module.CorLibTypes.String, "C++"));
                assembly.CustomAttributes.Add(companyAttr);
            }

            // Import the constructor for AssemblyProductAttribute
            var productAttrCtor = module.Import(typeof(AssemblyProductAttribute).GetConstructor(new[] { typeof(string) }));
            if (productAttrCtor != null)
            {
                var productAttr = new CustomAttribute((ICustomAttributeType)productAttrCtor);
                productAttr.ConstructorArguments.Add(new CAArgument(module.CorLibTypes.String, "C++ Application"));
                assembly.CustomAttributes.Add(productAttr);
            }

            // Import the constructor for AssemblyDescriptionAttribute
            var descriptionAttrCtor = module.Import(typeof(AssemblyDescriptionAttribute).GetConstructor(new[] { typeof(string) }));
            if (descriptionAttrCtor != null)
            {
                var descriptionAttr = new CustomAttribute((ICustomAttributeType)descriptionAttrCtor);
                descriptionAttr.ConstructorArguments.Add(new CAArgument(module.CorLibTypes.String, "C++ version of the application"));
                assembly.CustomAttributes.Add(descriptionAttr);
            }

            // You can optionally add more metadata attributes, just make sure to import their constructors properly


        }




        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }
        bool spoofMetadata = false;
        private void guna2CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle the state for metadata spoofing
            spoofMetadata = !spoofMetadata;
        }
    }
}
