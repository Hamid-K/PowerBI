using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004A0 RID: 1184
	public sealed class Documentation : MetadataItem
	{
		// Token: 0x06003A23 RID: 14883 RVA: 0x000C04E0 File Offset: 0x000BE6E0
		internal Documentation()
		{
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000C04FE File Offset: 0x000BE6FE
		public Documentation(string summary, string longDescription)
		{
			this.Summary = summary;
			this.LongDescription = longDescription;
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000C052A File Offset: 0x000BE72A
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.Documentation;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000C052E File Offset: 0x000BE72E
		// (set) Token: 0x06003A27 RID: 14887 RVA: 0x000C0536 File Offset: 0x000BE736
		public string Summary
		{
			get
			{
				return this._summary;
			}
			internal set
			{
				if (value != null)
				{
					this._summary = value;
					return;
				}
				this._summary = "";
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000C054E File Offset: 0x000BE74E
		// (set) Token: 0x06003A29 RID: 14889 RVA: 0x000C0556 File Offset: 0x000BE756
		public string LongDescription
		{
			get
			{
				return this._longDescription;
			}
			internal set
			{
				if (value != null)
				{
					this._longDescription = value;
					return;
				}
				this._longDescription = "";
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x000C056E File Offset: 0x000BE76E
		internal override string Identity
		{
			get
			{
				return "Documentation";
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000C0575 File Offset: 0x000BE775
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this._summary) && string.IsNullOrEmpty(this._longDescription);
			}
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000C0594 File Offset: 0x000BE794
		public override string ToString()
		{
			return this._summary;
		}

		// Token: 0x04001369 RID: 4969
		private string _summary = "";

		// Token: 0x0400136A RID: 4970
		private string _longDescription = "";
	}
}
