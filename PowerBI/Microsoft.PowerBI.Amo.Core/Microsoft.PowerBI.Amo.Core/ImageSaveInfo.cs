using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200008B RID: 139
	[Guid("C30C4ED9-32DA-4a46-9366-C05D8A1D8796")]
	public sealed class ImageSaveInfo
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x00024760 File Offset: 0x00022960
		public ImageSaveInfo()
		{
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00024768 File Offset: 0x00022968
		public ImageSaveInfo(string databaseId, Stream targetDbStream)
		{
			this.DatabaseId = databaseId;
			this.TargetDbStream = targetDbStream;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0002477E File Offset: 0x0002297E
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00024788 File Offset: 0x00022988
		public string DatabaseId
		{
			get
			{
				return this.databaseId;
			}
			set
			{
				value = Utils.Trim(value);
				string text;
				if (value != null && !Utils.IsSyntacticallyValidID(value, typeof(Database), out text))
				{
					throw new ArgumentException(text);
				}
				this.databaseId = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x000247C2 File Offset: 0x000229C2
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x000247CA File Offset: 0x000229CA
		public Stream TargetDbStream
		{
			get
			{
				return this.targetDbStream;
			}
			set
			{
				this.targetDbStream = value;
			}
		}

		// Token: 0x04000468 RID: 1128
		private string databaseId;

		// Token: 0x04000469 RID: 1129
		private Stream targetDbStream;
	}
}
