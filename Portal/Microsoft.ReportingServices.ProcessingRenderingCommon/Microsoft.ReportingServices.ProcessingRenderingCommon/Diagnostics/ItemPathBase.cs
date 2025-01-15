using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200005B RID: 91
	public abstract class ItemPathBase
	{
		// Token: 0x06000280 RID: 640 RVA: 0x00009CF4 File Offset: 0x00007EF4
		protected ItemPathBase(string itemPath)
		{
			if (!ItemPathBase.ParseInternalItemPathParts(itemPath, out this.m_editSessionID, out this.m_value))
			{
				this.m_value = ((itemPath != null) ? itemPath.Trim() : null);
				this.m_editSessionID = null;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009D29 File Offset: 0x00007F29
		protected ItemPathBase(string itemPath, string editSessionID)
		{
			this.m_value = itemPath;
			this.m_editSessionID = editSessionID;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00009D3F File Offset: 0x00007F3F
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00009D47 File Offset: 0x00007F47
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00009D4F File Offset: 0x00007F4F
		public string EditSessionID
		{
			get
			{
				return this.m_editSessionID;
			}
			set
			{
				this.m_editSessionID = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009D58 File Offset: 0x00007F58
		public bool IsEditSession
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_editSessionID);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00009D68 File Offset: 0x00007F68
		public virtual string FullEditSessionIdentifier
		{
			get
			{
				if (!this.IsEditSession)
				{
					return this.Value;
				}
				if (this.Value.Contains("|") || this.EditSessionID.Contains("|"))
				{
					throw new InternalCatalogException("Unexpected character in ItemPath or EditSessionID");
				}
				return string.Format(CultureInfo.InvariantCulture, "|{0}|@|{1}|", this.EditSessionID, this.Value);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009DCE File Offset: 0x00007FCE
		public static string SafeValue(ItemPathBase path)
		{
			if (path == null)
			{
				return null;
			}
			return path.Value;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009DDB File Offset: 0x00007FDB
		public static string SafeEditSessionID(ItemPathBase path)
		{
			if (path == null)
			{
				return null;
			}
			return path.EditSessionID;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public static bool IsNullOrEmpty(ItemPathBase path)
		{
			return path == null || string.IsNullOrEmpty(path.Value);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009DFA File Offset: 0x00007FFA
		public override string ToString()
		{
			return this.m_value;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009E02 File Offset: 0x00008002
		public static string GetLocalPath(string path)
		{
			return path;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009E08 File Offset: 0x00008008
		public static int CatalogCompare(ItemPathBase a, string b)
		{
			int num = Localization.CatalogCultureCompare(ItemPathBase.SafeValue(a), b);
			if (num == 0 && a != null && a.IsEditSession)
			{
				return 1;
			}
			return num;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009E34 File Offset: 0x00008034
		public static int CatalogCompare(ItemPathBase a, ItemPathBase b)
		{
			int num = Localization.CatalogCultureCompare(ItemPathBase.SafeValue(a), ItemPathBase.SafeValue(b));
			if (num == 0)
			{
				return string.CompareOrdinal(ItemPathBase.SafeEditSessionID(a), ItemPathBase.SafeEditSessionID(b));
			}
			return num;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009E69 File Offset: 0x00008069
		public static string GetParentPathForSharePoint(string path)
		{
			return path;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009E6C File Offset: 0x0000806C
		public static string GetEditSessionID(string path)
		{
			string text = null;
			string text2 = null;
			ItemPathBase.ParseInternalItemPathParts(path, out text, out text2);
			return text;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009E8C File Offset: 0x0000808C
		public static bool ParseInternalItemPathParts(string path, out string editSessionID, out string itemPath)
		{
			if (!string.IsNullOrEmpty(path))
			{
				Match match = Regex.Match(path, "  ^\\|\r\n        ([a-z0-9]{24})\r\n    \\|\r\n    @\r\n    \\|\r\n        ([^\\|]+)\r\n    \\|$", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
				if (match.Success)
				{
					editSessionID = match.Groups[1].Value.Trim();
					itemPath = match.Groups[2].Value.Trim();
					return true;
				}
			}
			editSessionID = null;
			itemPath = null;
			return false;
		}

		// Token: 0x04000154 RID: 340
		protected readonly string m_value;

		// Token: 0x04000155 RID: 341
		protected string m_editSessionID;
	}
}
