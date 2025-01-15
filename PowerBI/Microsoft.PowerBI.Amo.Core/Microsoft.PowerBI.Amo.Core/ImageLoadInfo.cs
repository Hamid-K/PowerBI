using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200008A RID: 138
	[Guid("9ECB4262-D3DF-4d08-8C17-E29DD49A42F6")]
	public sealed class ImageLoadInfo
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x00024687 File Offset: 0x00022887
		public ImageLoadInfo()
			: this(null, null, null, ReadWriteMode.ReadWrite)
		{
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00024693 File Offset: 0x00022893
		public ImageLoadInfo(string databaseId, string databaseName, Stream sourceDbStream, ReadWriteMode readWriteMode)
		{
			this.DatabaseId = databaseId;
			this.DatabaseName = databaseName;
			this.DatabaseReadWriteMode = readWriteMode;
			this.SourceDbStream = sourceDbStream;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x000246B8 File Offset: 0x000228B8
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x000246C0 File Offset: 0x000228C0
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

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x000246FA File Offset: 0x000228FA
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00024704 File Offset: 0x00022904
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
			set
			{
				value = Utils.Trim(value);
				string text;
				if (value != null && !Utils.IsSyntacticallyValidName(value, typeof(Database), out text))
				{
					throw new ArgumentException(text);
				}
				this.databaseName = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0002473E File Offset: 0x0002293E
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x00024746 File Offset: 0x00022946
		public ReadWriteMode DatabaseReadWriteMode
		{
			get
			{
				return this.readWriteMode;
			}
			set
			{
				this.readWriteMode = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0002474F File Offset: 0x0002294F
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x00024757 File Offset: 0x00022957
		public Stream SourceDbStream
		{
			get
			{
				return this.sourceDbStream;
			}
			set
			{
				this.sourceDbStream = value;
			}
		}

		// Token: 0x04000464 RID: 1124
		private string databaseId;

		// Token: 0x04000465 RID: 1125
		private string databaseName;

		// Token: 0x04000466 RID: 1126
		private ReadWriteMode readWriteMode;

		// Token: 0x04000467 RID: 1127
		private Stream sourceDbStream;
	}
}
