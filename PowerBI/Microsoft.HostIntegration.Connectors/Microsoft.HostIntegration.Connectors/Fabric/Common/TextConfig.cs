using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200041F RID: 1055
	internal class TextConfig
	{
		// Token: 0x060024C5 RID: 9413 RVA: 0x000708F5 File Offset: 0x0006EAF5
		public TextConfig(string fileName)
		{
			this.m_sections = TextConfig.Load(fileName);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x0007090C File Offset: 0x0006EB0C
		private static Dictionary<string, Dictionary<string, string>> Load(string fileName)
		{
			Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();
			using (StreamReader streamReader = new StreamReader(fileName))
			{
				Dictionary<string, string> dictionary2 = null;
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					text = text.Trim();
					if (text.StartsWith("[", StringComparison.Ordinal) && text.EndsWith("]", StringComparison.Ordinal))
					{
						string text2 = text.Substring(1, text.Length - 2);
						dictionary2 = new Dictionary<string, string>();
						dictionary[text2] = dictionary2;
					}
					else if (dictionary2 != null && !text.StartsWith(";", StringComparison.Ordinal))
					{
						int num = text.IndexOf('=');
						if (num > 0)
						{
							string text3 = text.Substring(0, num);
							string text4 = text.Substring(num + 1);
							dictionary2[text3.Trim()] = text4.Trim();
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000709E8 File Offset: 0x0006EBE8
		public string GetValue(string sectionName, string key)
		{
			Dictionary<string, string> section = this.GetSection(sectionName);
			if (section == null)
			{
				return null;
			}
			string text;
			section.TryGetValue(key, out text);
			return text;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x00070A10 File Offset: 0x0006EC10
		public Dictionary<string, string> GetSection(string sectionName)
		{
			if (sectionName == null)
			{
				throw new ArgumentNullException("sectionName");
			}
			Dictionary<string, string> dictionary;
			while (!this.m_sections.TryGetValue(sectionName, out dictionary))
			{
				int num = sectionName.LastIndexOf('.');
				if (num <= 0)
				{
					return null;
				}
				sectionName = sectionName.Substring(num);
			}
			return dictionary;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x00070A58 File Offset: 0x0006EC58
		public static TextConfig GetConfig(string fileName)
		{
			TextConfig textConfig;
			lock (TextConfig.s_configs)
			{
				if (!TextConfig.s_configs.TryGetValue(fileName, out textConfig))
				{
					textConfig = new TextConfig(fileName);
					TextConfig.s_configs.Add(fileName, textConfig);
				}
			}
			return textConfig;
		}

		// Token: 0x0400167C RID: 5756
		private Dictionary<string, Dictionary<string, string>> m_sections;

		// Token: 0x0400167D RID: 5757
		private static Dictionary<string, TextConfig> s_configs = new Dictionary<string, TextConfig>();
	}
}
