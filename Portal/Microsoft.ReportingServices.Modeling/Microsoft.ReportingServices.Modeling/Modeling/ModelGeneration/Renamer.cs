using System;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000F7 RID: 247
	public sealed class Renamer : IXmlLoadable
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00029230 File Offset: 0x00027430
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x00029238 File Offset: 0x00027438
		public string MatchPattern
		{
			get
			{
				return this.m_matchPattern;
			}
			set
			{
				this.m_matchPattern = value ?? string.Empty;
				this.m_matchRegex = new Regex("^" + this.m_matchPattern + "$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0002926C File Offset: 0x0002746C
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x00029274 File Offset: 0x00027474
		public string ReplacePattern
		{
			get
			{
				return this.m_replacePattern;
			}
			set
			{
				this.m_replacePattern = value ?? string.Empty;
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00029286 File Offset: 0x00027486
		public bool Rename(string oldName, out string newName)
		{
			newName = oldName;
			if (this.m_matchRegex != null && this.m_matchRegex.IsMatch(oldName))
			{
				newName = this.m_matchRegex.Replace(oldName, this.m_replacePattern);
				return true;
			}
			return false;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000292B8 File Offset: 0x000274B8
		internal static Renamer FromReader(ModelingXmlReader xr)
		{
			Renamer renamer = new Renamer();
			xr.LoadObject("renamer", renamer);
			return renamer;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x000292D8 File Offset: 0x000274D8
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "match")
				{
					this.MatchPattern = xr.ReadValueAsString();
					return true;
				}
				if (localName == "replace")
				{
					this.ReplacePattern = xr.ReadValueAsString();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002932D File Offset: 0x0002752D
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x04000522 RID: 1314
		internal const string RenamerElem = "renamer";

		// Token: 0x04000523 RID: 1315
		private const string MatchAttr = "match";

		// Token: 0x04000524 RID: 1316
		private const string ReplaceAttr = "replace";

		// Token: 0x04000525 RID: 1317
		private string m_matchPattern = string.Empty;

		// Token: 0x04000526 RID: 1318
		private Regex m_matchRegex;

		// Token: 0x04000527 RID: 1319
		private string m_replacePattern = string.Empty;
	}
}
