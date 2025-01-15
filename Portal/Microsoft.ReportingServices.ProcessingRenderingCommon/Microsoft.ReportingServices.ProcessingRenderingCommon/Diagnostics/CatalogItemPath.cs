using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200005C RID: 92
	[DebuggerDisplay("CatalogItemPath: {m_originalValue}")]
	public class CatalogItemPath : ItemPathBase
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009EF0 File Offset: 0x000080F0
		public string OriginalValue
		{
			get
			{
				return this.m_originalValue;
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009EF8 File Offset: 0x000080F8
		public CatalogItemPath(string value)
			: base(ItemPathBase.GetLocalPath(value))
		{
			this.m_originalValue = value;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009F0D File Offset: 0x0000810D
		public CatalogItemPath(string value, string editSessionID)
			: this(value)
		{
			this.m_editSessionID = editSessionID;
		}

		// Token: 0x04000156 RID: 342
		public static readonly CatalogItemPath Empty = new CatalogItemPath(string.Empty);

		// Token: 0x04000157 RID: 343
		private readonly string m_originalValue;
	}
}
