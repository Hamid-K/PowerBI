using System;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001144 RID: 4420
	public class TableType
	{
		// Token: 0x060073C6 RID: 29638 RVA: 0x0018E37B File Offset: 0x0018C57B
		public TableType(string serverType, string linkKind)
		{
			this.serverType = serverType;
			this.kind = linkKind;
			this.linkKind = linkKind;
			this.hasPrimaryKey = true;
			this.hasForeignKeys = true;
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x0018E3A6 File Offset: 0x0018C5A6
		public TableType(string serverType, string kind, string linkKind, bool hasPrimaryKey, bool hasForeignKey)
		{
			this.serverType = serverType;
			this.kind = kind;
			this.linkKind = linkKind;
			this.hasPrimaryKey = hasPrimaryKey;
			this.hasForeignKeys = hasForeignKey;
		}

		// Token: 0x17002044 RID: 8260
		// (get) Token: 0x060073C8 RID: 29640 RVA: 0x0018E3D3 File Offset: 0x0018C5D3
		public string ServerType
		{
			get
			{
				return this.serverType;
			}
		}

		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x060073C9 RID: 29641 RVA: 0x0018E3DB File Offset: 0x0018C5DB
		public string Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x060073CA RID: 29642 RVA: 0x0018E3E3 File Offset: 0x0018C5E3
		public string LinkKind
		{
			get
			{
				return this.linkKind;
			}
		}

		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x060073CB RID: 29643 RVA: 0x0018E3EB File Offset: 0x0018C5EB
		public bool HasPrimaryKey
		{
			get
			{
				return this.hasPrimaryKey;
			}
		}

		// Token: 0x17002048 RID: 8264
		// (get) Token: 0x060073CC RID: 29644 RVA: 0x0018E3F3 File Offset: 0x0018C5F3
		public bool HasForeignKeys
		{
			get
			{
				return this.hasForeignKeys;
			}
		}

		// Token: 0x04003FBF RID: 16319
		private readonly string serverType;

		// Token: 0x04003FC0 RID: 16320
		private readonly string kind;

		// Token: 0x04003FC1 RID: 16321
		private readonly string linkKind;

		// Token: 0x04003FC2 RID: 16322
		private readonly bool hasPrimaryKey;

		// Token: 0x04003FC3 RID: 16323
		private readonly bool hasForeignKeys;
	}
}
