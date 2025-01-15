using System;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BEF RID: 3055
	internal abstract class ExchangeSearchResult
	{
		// Token: 0x0600532D RID: 21293 RVA: 0x00119C30 File Offset: 0x00117E30
		public ExchangeSearchResult(string id, string folderPath)
			: this(id, folderPath, false)
		{
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x00119C3B File Offset: 0x00117E3B
		public ExchangeSearchResult(string id, string folderPath, bool isAttachment)
		{
			this.id = id;
			this.folderPath = folderPath;
			this.isAttachment = isAttachment;
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x0600532F RID: 21295 RVA: 0x00119C58 File Offset: 0x00117E58
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x00119C60 File Offset: 0x00117E60
		public string FolderPath
		{
			get
			{
				return this.folderPath;
			}
		}

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x06005331 RID: 21297 RVA: 0x00119C68 File Offset: 0x00117E68
		public bool IsAttachment
		{
			get
			{
				return this.isAttachment;
			}
		}

		// Token: 0x06005332 RID: 21298
		public abstract object GetColumnValue(ExchangeColumnInfo columnInfo);

		// Token: 0x04002DF5 RID: 11765
		private string id;

		// Token: 0x04002DF6 RID: 11766
		private string folderPath;

		// Token: 0x04002DF7 RID: 11767
		private bool isAttachment;
	}
}
