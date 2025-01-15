using System;

namespace Microsoft.ReportingServices.CatalogAccess.DataAccessObject
{
	// Token: 0x02000018 RID: 24
	public class DataModelDataSourceEntity
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002657 File Offset: 0x00000857
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000265F File Offset: 0x0000085F
		public long DSID { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002668 File Offset: 0x00000868
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002670 File Offset: 0x00000870
		public Guid ItemId { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002679 File Offset: 0x00000879
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002681 File Offset: 0x00000881
		public DataModelDataSourceEntity.DataModelDataSourceType DSType { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000268A File Offset: 0x0000088A
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002692 File Offset: 0x00000892
		public DataModelDataSourceEntity.DataModelDataSourceKind DSKind { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000269B File Offset: 0x0000089B
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000026A3 File Offset: 0x000008A3
		public DataModelDataSourceEntity.DataModelDataSourceAuthType AuthType { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000026AC File Offset: 0x000008AC
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000026B4 File Offset: 0x000008B4
		public byte[] ConnectionString { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000026BD File Offset: 0x000008BD
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000026C5 File Offset: 0x000008C5
		public byte[] Username { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000026CE File Offset: 0x000008CE
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000026D6 File Offset: 0x000008D6
		public byte[] Password { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000026DF File Offset: 0x000008DF
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000026E7 File Offset: 0x000008E7
		public string CreatedBy { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000026F0 File Offset: 0x000008F0
		// (set) Token: 0x06000103 RID: 259 RVA: 0x000026F8 File Offset: 0x000008F8
		public DateTime CreatedDate { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002701 File Offset: 0x00000901
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00002709 File Offset: 0x00000909
		public string ModifiedBy { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00002712 File Offset: 0x00000912
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000271A File Offset: 0x0000091A
		public DateTime ModifiedDate { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00002723 File Offset: 0x00000923
		// (set) Token: 0x06000109 RID: 265 RVA: 0x0000272B File Offset: 0x0000092B
		public Guid DataSourceId { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002734 File Offset: 0x00000934
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000273C File Offset: 0x0000093C
		public string ModelConnectionName { get; set; }

		// Token: 0x02000029 RID: 41
		public enum DataModelDataSourceAuthType
		{
			// Token: 0x040000EC RID: 236
			Unknown,
			// Token: 0x040000ED RID: 237
			Anonymous,
			// Token: 0x040000EE RID: 238
			Integrated,
			// Token: 0x040000EF RID: 239
			Windows,
			// Token: 0x040000F0 RID: 240
			UsernamePassword,
			// Token: 0x040000F1 RID: 241
			Key,
			// Token: 0x040000F2 RID: 242
			Impersonate
		}

		// Token: 0x0200002A RID: 42
		public enum DataModelDataSourceKind
		{
			// Token: 0x040000F4 RID: 244
			UnknownFunction,
			// Token: 0x040000F5 RID: 245
			ActiveDirectory,
			// Token: 0x040000F6 RID: 246
			AnalysisServices,
			// Token: 0x040000F7 RID: 247
			AzureBlobs,
			// Token: 0x040000F8 RID: 248
			AzureTables,
			// Token: 0x040000F9 RID: 249
			CurrentWorkbook,
			// Token: 0x040000FA RID: 250
			DataMarket,
			// Token: 0x040000FB RID: 251
			DB2,
			// Token: 0x040000FC RID: 252
			Exchange,
			// Token: 0x040000FD RID: 253
			Facebook,
			// Token: 0x040000FE RID: 254
			File,
			// Token: 0x040000FF RID: 255
			Folder,
			// Token: 0x04000100 RID: 256
			GoogleAnalytics,
			// Token: 0x04000101 RID: 257
			Hdfs,
			// Token: 0x04000102 RID: 258
			HDInsight,
			// Token: 0x04000103 RID: 259
			Informix,
			// Token: 0x04000104 RID: 260
			MQ,
			// Token: 0x04000105 RID: 261
			MySql,
			// Token: 0x04000106 RID: 262
			OData,
			// Token: 0x04000107 RID: 263
			Odbc,
			// Token: 0x04000108 RID: 264
			OleDb,
			// Token: 0x04000109 RID: 265
			Oracle,
			// Token: 0x0400010A RID: 266
			PostgreSQL,
			// Token: 0x0400010B RID: 267
			Salesforce,
			// Token: 0x0400010C RID: 268
			SapBusinessObjects,
			// Token: 0x0400010D RID: 269
			SapBusinessWarehouse,
			// Token: 0x0400010E RID: 270
			SapHana,
			// Token: 0x0400010F RID: 271
			SharePoint,
			// Token: 0x04000110 RID: 272
			SQL,
			// Token: 0x04000111 RID: 273
			Sybase,
			// Token: 0x04000112 RID: 274
			Teradata,
			// Token: 0x04000113 RID: 275
			Web
		}

		// Token: 0x0200002B RID: 43
		public enum DataModelDataSourceType
		{
			// Token: 0x04000115 RID: 277
			Unknown,
			// Token: 0x04000116 RID: 278
			Live,
			// Token: 0x04000117 RID: 279
			DirectQuery,
			// Token: 0x04000118 RID: 280
			Import
		}
	}
}
