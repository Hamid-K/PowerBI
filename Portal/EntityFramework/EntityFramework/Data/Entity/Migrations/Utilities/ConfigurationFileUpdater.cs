using System;
using System.Data.Entity.Utilities;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x020000A2 RID: 162
	internal class ConfigurationFileUpdater
	{
		// Token: 0x06000EB9 RID: 3769 RVA: 0x0001F118 File Offset: 0x0001D318
		static ConfigurationFileUpdater()
		{
			AssemblyName name = typeof(ConfigurationFileUpdater).Assembly().GetName();
			ConfigurationFileUpdater._dependentAssemblyElement = new XElement(ConfigurationFileUpdater._asm + "dependentAssembly", new object[]
			{
				new XElement(ConfigurationFileUpdater._asm + "assemblyIdentity", new object[]
				{
					new XAttribute("name", "EntityFramework"),
					new XAttribute("culture", "neutral"),
					new XAttribute("publicKeyToken", "b77a5c561934e089")
				}),
				new XElement(ConfigurationFileUpdater._asm + "codeBase", new object[]
				{
					new XAttribute("version", name.Version.ToString()),
					new XAttribute("href", name.CodeBase)
				})
			});
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0001F21C File Offset: 0x0001D41C
		public virtual string Update(string configurationFile)
		{
			bool flag = !string.IsNullOrWhiteSpace(configurationFile) && File.Exists(configurationFile);
			XDocument xdocument = (flag ? XDocument.Load(configurationFile) : new XDocument());
			xdocument.GetOrAddElement("configuration").GetOrAddElement("runtime").GetOrAddElement(ConfigurationFileUpdater._asm + "assemblyBinding")
				.Add(ConfigurationFileUpdater._dependentAssemblyElement);
			string text = Path.GetTempFileName();
			if (flag)
			{
				File.Delete(text);
				text = Path.Combine(Path.GetDirectoryName(configurationFile), Path.GetFileName(text));
			}
			xdocument.Save(text);
			return text;
		}

		// Token: 0x04000831 RID: 2097
		private static readonly XNamespace _asm = "urn:schemas-microsoft-com:asm.v1";

		// Token: 0x04000832 RID: 2098
		private static readonly XElement _dependentAssemblyElement;
	}
}
