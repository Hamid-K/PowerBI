using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000B3 RID: 179
	[Guid("E866DFE7-0177-43c7-8673-4E487E2A85F7")]
	public sealed class RestoreLocation
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00028520 File Offset: 0x00026720
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x00028528 File Offset: 0x00026728
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

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0002853C File Offset: 0x0002673C
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x00028544 File Offset: 0x00026744
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

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00028552 File Offset: 0x00026752
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0002855A File Offset: 0x0002675A
		[DefaultValue(RestoreDataSourceType.Remote)]
		public RestoreDataSourceType DataSourceType
		{
			get
			{
				return this.dataSourceType;
			}
			set
			{
				this.dataSourceType = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00028563 File Offset: 0x00026763
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0002856B File Offset: 0x0002676B
		[XmlElement(IsNullable = false)]
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.connectionString = Utils.Trim(value);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00028579 File Offset: 0x00026779
		[XmlArray]
		[XmlArrayItem("Folder", typeof(RestoreFolder))]
		public RestoreFolderCollection Folders
		{
			get
			{
				return this.folders;
			}
		}

		// Token: 0x040004DD RID: 1245
		private string file;

		// Token: 0x040004DE RID: 1246
		private string dataSourceID;

		// Token: 0x040004DF RID: 1247
		private RestoreDataSourceType dataSourceType;

		// Token: 0x040004E0 RID: 1248
		private string connectionString;

		// Token: 0x040004E1 RID: 1249
		private RestoreFolderCollection folders = new RestoreFolderCollection();
	}
}
