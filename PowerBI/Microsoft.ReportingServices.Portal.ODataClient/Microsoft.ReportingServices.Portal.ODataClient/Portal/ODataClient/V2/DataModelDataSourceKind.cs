using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000097 RID: 151
	[OriginalName("DataModelDataSourceKind")]
	public enum DataModelDataSourceKind
	{
		// Token: 0x040002FF RID: 767
		[OriginalName("UnknownFunction")]
		UnknownFunction,
		// Token: 0x04000300 RID: 768
		[OriginalName("ActiveDirectory")]
		ActiveDirectory,
		// Token: 0x04000301 RID: 769
		[OriginalName("AnalysisServices")]
		AnalysisServices,
		// Token: 0x04000302 RID: 770
		[OriginalName("AzureBlobs")]
		AzureBlobs,
		// Token: 0x04000303 RID: 771
		[OriginalName("AzureTables")]
		AzureTables,
		// Token: 0x04000304 RID: 772
		[OriginalName("CurrentWorkbook")]
		CurrentWorkbook,
		// Token: 0x04000305 RID: 773
		[OriginalName("DataMarket")]
		DataMarket,
		// Token: 0x04000306 RID: 774
		[OriginalName("DB2")]
		DB2,
		// Token: 0x04000307 RID: 775
		[OriginalName("Exchange")]
		Exchange,
		// Token: 0x04000308 RID: 776
		[OriginalName("Facebook")]
		Facebook,
		// Token: 0x04000309 RID: 777
		[OriginalName("File")]
		File,
		// Token: 0x0400030A RID: 778
		[OriginalName("Folder")]
		Folder,
		// Token: 0x0400030B RID: 779
		[OriginalName("GoogleAnalytics")]
		GoogleAnalytics,
		// Token: 0x0400030C RID: 780
		[OriginalName("Hdfs")]
		Hdfs,
		// Token: 0x0400030D RID: 781
		[OriginalName("HDInsight")]
		HDInsight,
		// Token: 0x0400030E RID: 782
		[OriginalName("Informix")]
		Informix,
		// Token: 0x0400030F RID: 783
		[OriginalName("MQ")]
		MQ,
		// Token: 0x04000310 RID: 784
		[OriginalName("MySql")]
		MySql,
		// Token: 0x04000311 RID: 785
		[OriginalName("OData")]
		OData,
		// Token: 0x04000312 RID: 786
		[OriginalName("Odbc")]
		Odbc,
		// Token: 0x04000313 RID: 787
		[OriginalName("OleDb")]
		OleDb,
		// Token: 0x04000314 RID: 788
		[OriginalName("Oracle")]
		Oracle,
		// Token: 0x04000315 RID: 789
		[OriginalName("PostgreSQL")]
		PostgreSQL,
		// Token: 0x04000316 RID: 790
		[OriginalName("Salesforce")]
		Salesforce,
		// Token: 0x04000317 RID: 791
		[OriginalName("SapBusinessObjects")]
		SapBusinessObjects,
		// Token: 0x04000318 RID: 792
		[OriginalName("SapBusinessWarehouse")]
		SapBusinessWarehouse,
		// Token: 0x04000319 RID: 793
		[OriginalName("SapHana")]
		SapHana,
		// Token: 0x0400031A RID: 794
		[OriginalName("SharePoint")]
		SharePoint,
		// Token: 0x0400031B RID: 795
		[OriginalName("SQL")]
		SQL,
		// Token: 0x0400031C RID: 796
		[OriginalName("Sybase")]
		Sybase,
		// Token: 0x0400031D RID: 797
		[OriginalName("Teradata")]
		Teradata,
		// Token: 0x0400031E RID: 798
		[OriginalName("Web")]
		Web
	}
}
