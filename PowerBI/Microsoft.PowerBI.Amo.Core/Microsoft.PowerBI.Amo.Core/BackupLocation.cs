using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000075 RID: 117
	[Guid("D7468114-DC2B-464c-83F8-DD33513DEE47")]
	public sealed class BackupLocation
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x00023026 File Offset: 0x00021226
		public BackupLocation()
		{
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0002302E File Offset: 0x0002122E
		public BackupLocation(string file, string dataSourceId)
		{
			this.File = file;
			this.DataSourceID = dataSourceId;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00023044 File Offset: 0x00021244
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x0002304C File Offset: 0x0002124C
		[XmlElement(IsNullable = false)]
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				Utils.CheckValidPath(value);
				this.file = Utils.Trim(value);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00023060 File Offset: 0x00021260
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00023068 File Offset: 0x00021268
		[XmlElement(IsNullable = false)]
		public string DataSourceID
		{
			get
			{
				return this.dataSourceID;
			}
			set
			{
				this.dataSourceID = Utils.Trim(value);
			}
		}

		// Token: 0x0400041D RID: 1053
		private string file;

		// Token: 0x0400041E RID: 1054
		private string dataSourceID;
	}
}
