using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200005D RID: 93
	[DebuggerDisplay("ExternalItemPath: {m_value}")]
	public class ExternalItemPath : ItemPathBase
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00009F2E File Offset: 0x0000812E
		public string NativeCatalogPath
		{
			get
			{
				if (this.m_cachedLocalPath == null)
				{
					this.m_cachedLocalPath = ItemPathBase.GetLocalPath(this.m_value);
				}
				return this.m_cachedLocalPath;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00009F4F File Offset: 0x0000814F
		public CatalogItemPath NativeCatalogItemPath
		{
			get
			{
				return new CatalogItemPath(this.NativeCatalogPath, base.EditSessionID);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009F62 File Offset: 0x00008162
		public static ExternalItemPath ConstructFromEditSessionPath(string path)
		{
			return new ExternalItemPath(path, 0);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009F6B File Offset: 0x0000816B
		private ExternalItemPath(string editSessionPath, int notUsed)
			: base(editSessionPath)
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009F74 File Offset: 0x00008174
		public static ExternalItemPath CreateWithoutChecks(string value, string editSessionID)
		{
			return new ExternalItemPath(value, editSessionID);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009F7D File Offset: 0x0000817D
		public ExternalItemPath(string value, string editSessionID)
			: base(value, editSessionID)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009F87 File Offset: 0x00008187
		public ExternalItemPath(string value)
			: this(value, ItemPathBase.GetEditSessionID(value))
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009F96 File Offset: 0x00008196
		public CatalogItemPath ConvertToCatalogPath(IPathTranslator pathTrans)
		{
			if (pathTrans == null)
			{
				return new CatalogItemPath(this.NativeCatalogPath, base.EditSessionID);
			}
			return new CatalogItemPath(pathTrans.ExternalToCatalog(base.Value), base.EditSessionID);
		}

		// Token: 0x04000158 RID: 344
		private string m_cachedLocalPath;

		// Token: 0x04000159 RID: 345
		public static readonly ExternalItemPath Empty = new ExternalItemPath(string.Empty);
	}
}
