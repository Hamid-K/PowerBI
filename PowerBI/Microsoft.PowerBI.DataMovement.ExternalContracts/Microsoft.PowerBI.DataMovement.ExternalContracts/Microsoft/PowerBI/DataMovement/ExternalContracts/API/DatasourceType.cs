using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000043 RID: 67
	public enum DatasourceType
	{
		// Token: 0x0400014D RID: 333
		[EnumMember]
		Sql,
		// Token: 0x0400014E RID: 334
		[EnumMember]
		AnalysisServices,
		// Token: 0x0400014F RID: 335
		[EnumMember]
		SAPHana,
		// Token: 0x04000150 RID: 336
		[EnumMember]
		File,
		// Token: 0x04000151 RID: 337
		[EnumMember]
		Folder,
		// Token: 0x04000152 RID: 338
		[EnumMember]
		Oracle,
		// Token: 0x04000153 RID: 339
		[EnumMember]
		Teradata,
		// Token: 0x04000154 RID: 340
		[EnumMember]
		SharePointList,
		// Token: 0x04000155 RID: 341
		[EnumMember]
		Web,
		// Token: 0x04000156 RID: 342
		[EnumMember]
		OData,
		// Token: 0x04000157 RID: 343
		[EnumMember]
		DB2,
		// Token: 0x04000158 RID: 344
		[EnumMember]
		MySql,
		// Token: 0x04000159 RID: 345
		[EnumMember]
		PostgreSql,
		// Token: 0x0400015A RID: 346
		[EnumMember]
		Sybase,
		// Token: 0x0400015B RID: 347
		[EnumMember]
		Extension,
		// Token: 0x0400015C RID: 348
		[EnumMember]
		SAPBW,
		// Token: 0x0400015D RID: 349
		[EnumMember]
		AzureTables,
		// Token: 0x0400015E RID: 350
		[EnumMember]
		AzureBlobs,
		// Token: 0x0400015F RID: 351
		[EnumMember]
		Informix,
		// Token: 0x04000160 RID: 352
		[EnumMember]
		ODBC,
		// Token: 0x04000161 RID: 353
		[EnumMember]
		Excel,
		// Token: 0x04000162 RID: 354
		[EnumMember]
		SharePoint,
		// Token: 0x04000163 RID: 355
		[EnumMember]
		PubNub,
		// Token: 0x04000164 RID: 356
		[EnumMember]
		MQ,
		// Token: 0x04000165 RID: 357
		[EnumMember]
		BizTalk,
		// Token: 0x04000166 RID: 358
		[EnumMember]
		GoogleAnalytics,
		// Token: 0x04000167 RID: 359
		[EnumMember]
		CustomHttpApi,
		// Token: 0x04000168 RID: 360
		[EnumMember]
		Exchange,
		// Token: 0x04000169 RID: 361
		[EnumMember]
		Facebook,
		// Token: 0x0400016A RID: 362
		[EnumMember]
		HDInsight,
		// Token: 0x0400016B RID: 363
		[EnumMember]
		AzureMarketplace,
		// Token: 0x0400016C RID: 364
		[EnumMember]
		ActiveDirectory,
		// Token: 0x0400016D RID: 365
		[EnumMember]
		Hdfs,
		// Token: 0x0400016E RID: 366
		[EnumMember]
		SharePointDocLib,
		// Token: 0x0400016F RID: 367
		[EnumMember]
		PowerQueryMashup,
		// Token: 0x04000170 RID: 368
		[EnumMember]
		OleDb,
		// Token: 0x04000171 RID: 369
		[EnumMember]
		AdoDotNet,
		// Token: 0x04000172 RID: 370
		[EnumMember]
		R,
		// Token: 0x04000173 RID: 371
		[EnumMember]
		LOB,
		// Token: 0x04000174 RID: 372
		[EnumMember]
		Salesforce,
		// Token: 0x04000175 RID: 373
		[EnumMember]
		CustomConnector,
		// Token: 0x04000176 RID: 374
		[EnumMember]
		SAPBWMessageServer,
		// Token: 0x04000177 RID: 375
		[EnumMember]
		AdobeAnalytics,
		// Token: 0x04000178 RID: 376
		[EnumMember]
		Essbase,
		// Token: 0x04000179 RID: 377
		[EnumMember]
		AzureDataLakeStorage,
		// Token: 0x0400017A RID: 378
		[EnumMember]
		SapErp,
		// Token: 0x0400017B RID: 379
		[EnumMember]
		UIFlow,
		// Token: 0x0400017C RID: 380
		[EnumMember]
		CDPA,
		// Token: 0x0400017D RID: 381
		[EnumMember]
		EventHub
	}
}
