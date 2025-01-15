using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200007E RID: 126
	public sealed class Documentation : MetadataItem
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x00016DC4 File Offset: 0x00014FC4
		internal Documentation()
		{
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00016DE2 File Offset: 0x00014FE2
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.Documentation;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00016DE6 File Offset: 0x00014FE6
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x00016DEE File Offset: 0x00014FEE
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

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00016E06 File Offset: 0x00015006
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x00016E0E File Offset: 0x0001500E
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

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00016E26 File Offset: 0x00015026
		internal override string Identity
		{
			get
			{
				return "Documentation";
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00016E2D File Offset: 0x0001502D
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this._summary) && string.IsNullOrEmpty(this._longDescription);
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00016E4C File Offset: 0x0001504C
		public override string ToString()
		{
			return this._summary;
		}

		// Token: 0x0400076B RID: 1899
		private string _summary = "";

		// Token: 0x0400076C RID: 1900
		private string _longDescription = "";
	}
}
