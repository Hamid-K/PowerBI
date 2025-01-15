using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000104 RID: 260
	public static class ResourceKind
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x00005590 File Offset: 0x00003790
		public static bool IsUriKind(this IEngine engine, string kind)
		{
			ResourceKindInfo resourceKindInfo;
			return engine.TryLookupResourceKind(kind, out resourceKindInfo) && resourceKindInfo.IsUri;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000055B0 File Offset: 0x000037B0
		public static bool IsDatabaseKind(this IEngine engine, string kind)
		{
			ResourceKindInfo resourceKindInfo;
			return engine.TryLookupResourceKind(kind, out resourceKindInfo) && resourceKindInfo.IsDatabase;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000055D0 File Offset: 0x000037D0
		public static bool SupportsEncryptedConnection(this IEngine engine, string kind)
		{
			ResourceKindInfo resourceKindInfo;
			return engine.TryLookupResourceKind(kind, out resourceKindInfo) && resourceKindInfo.SupportsEncryptedConnection;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000055F0 File Offset: 0x000037F0
		public static bool SupportsConnectionString(this IEngine engine, string kind)
		{
			ResourceKindInfo resourceKindInfo;
			return engine.TryLookupResourceKind(kind, out resourceKindInfo) && resourceKindInfo.SupportsConnectionString;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00005610 File Offset: 0x00003810
		public static bool SupportsConnectionStringProperty(this IEngine engine, string kind, string property = null)
		{
			ResourceKindInfo resourceKindInfo;
			if (!engine.TryLookupResourceKind(kind, out resourceKindInfo))
			{
				return false;
			}
			if (property == null)
			{
				return resourceKindInfo.ConnectionStringProperties.Count != 0;
			}
			return resourceKindInfo.ConnectionStringProperties.Contains(property);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00005648 File Offset: 0x00003848
		public static bool SupportsNativeQuery(this IEngine engine, string kind)
		{
			ResourceKindInfo resourceKindInfo;
			return engine.TryLookupResourceKind(kind, out resourceKindInfo) && resourceKindInfo.SupportsNativeQuery;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00005668 File Offset: 0x00003868
		public static bool IsRecognizedResourceKind(this IEngine engine, string resourceKind)
		{
			ResourceKindInfo resourceKindInfo;
			return engine.TryLookupResourceKind(resourceKind, out resourceKindInfo);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00002139 File Offset: 0x00000339
		[Obsolete("Hasn't been relevant since Power BI v1")]
		public static bool IsShareableResourceKind(this IEngine engine, string resourceKind)
		{
			return true;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00005680 File Offset: 0x00003880
		public static bool IsSingletonKind(this IEngine engine, string resourceKind)
		{
			ResourceKindInfo resourceKindInfo;
			return engine.TryLookupResourceKind(resourceKind, out resourceKindInfo) && resourceKindInfo.IsSingleton;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000056A0 File Offset: 0x000038A0
		public static bool ValidateResource(this IEngine engine, string resourceKind, string resourcePath, out IResource resource, out string errorMessage)
		{
			if (resourcePath == null)
			{
				resource = null;
				errorMessage = "Null value for resourcePath";
				return false;
			}
			ResourceKindInfo resourceKindInfo;
			if (!engine.TryLookupResourceKind(resourceKind, out resourceKindInfo))
			{
				resource = null;
				errorMessage = "Unknown resource type";
				return false;
			}
			return resourceKindInfo.Validate(resourcePath, out resource, out errorMessage);
		}

		// Token: 0x0400023F RID: 575
		public const string ActiveDirectory = "ActiveDirectory";

		// Token: 0x04000240 RID: 576
		public const string Access = "Access";

		// Token: 0x04000241 RID: 577
		public const string AdobeAnalytics = "AdobeAnalytics";

		// Token: 0x04000242 RID: 578
		public const string AnalysisServices = "AnalysisServices";

		// Token: 0x04000243 RID: 579
		public const string Cdpa = "CDPA";

		// Token: 0x04000244 RID: 580
		public const string Essbase = "Essbase";

		// Token: 0x04000245 RID: 581
		public const string Exchange = "Exchange";

		// Token: 0x04000246 RID: 582
		[Obsolete]
		public const string Snowflake = "Snowflake";

		// Token: 0x04000247 RID: 583
		[Obsolete]
		public const string GoogleBigQuery = "GoogleBigQuery";

		// Token: 0x04000248 RID: 584
		[Obsolete]
		public const string Facebook = "Facebook";

		// Token: 0x04000249 RID: 585
		public const string File = "File";

		// Token: 0x0400024A RID: 586
		public const string Folder = "Folder";

		// Token: 0x0400024B RID: 587
		public const string Ftp = "Ftp";

		// Token: 0x0400024C RID: 588
		public const string GoogleAnalytics = "GoogleAnalytics";

		// Token: 0x0400024D RID: 589
		public const string Hdfs = "Hdfs";

		// Token: 0x0400024E RID: 590
		public const string OData = "OData";

		// Token: 0x0400024F RID: 591
		public const string Odbc = "Odbc";

		// Token: 0x04000250 RID: 592
		public const string SapHana = "SapHana";

		// Token: 0x04000251 RID: 593
		public const string SapBusinessWarehouse = "SapBusinessWarehouse";

		// Token: 0x04000252 RID: 594
		public const string OleDb = "OleDb";

		// Token: 0x04000253 RID: 595
		public const string AdoDotNet = "AdoDotNet";

		// Token: 0x04000254 RID: 596
		public const string SharePoint = "SharePoint";

		// Token: 0x04000255 RID: 597
		public const string Sql = "SQL";

		// Token: 0x04000256 RID: 598
		public const string MySql = "MySql";

		// Token: 0x04000257 RID: 599
		public const string Oracle = "Oracle";

		// Token: 0x04000258 RID: 600
		public const string DB2 = "DB2";

		// Token: 0x04000259 RID: 601
		public const string Teradata = "Teradata";

		// Token: 0x0400025A RID: 602
		public const string PostgreSQL = "PostgreSQL";

		// Token: 0x0400025B RID: 603
		public const string Sybase = "Sybase";

		// Token: 0x0400025C RID: 604
		public const string Web = "Web";

		// Token: 0x0400025D RID: 605
		public const string CurrentWorkbook = "CurrentWorkbook";

		// Token: 0x0400025E RID: 606
		public const string HDInsight = "HDInsight";

		// Token: 0x0400025F RID: 607
		public const string AzureBlobs = "AzureBlobs";

		// Token: 0x04000260 RID: 608
		public const string AzureTables = "AzureTables";

		// Token: 0x04000261 RID: 609
		public const string Salesforce = "Salesforce";

		// Token: 0x04000262 RID: 610
		public const string Spark = "Spark";

		// Token: 0x04000263 RID: 611
		public const string R = "R";

		// Token: 0x04000264 RID: 612
		public const string Python = "Python";

		// Token: 0x04000265 RID: 613
		public const string Informix = "Informix";

		// Token: 0x04000266 RID: 614
		public const string MQ = "MQ";

		// Token: 0x04000267 RID: 615
		public const string Environment = "Environment";

		// Token: 0x04000268 RID: 616
		public const string AzureDataLakeStorage = "AzureDataLakeStorage";

		// Token: 0x04000269 RID: 617
		public const string PowerBIDataflow = "PowerBI";

		// Token: 0x0400026A RID: 618
		public const string PowerBIDatamarts = "PowerBIDatamarts";

		// Token: 0x0400026B RID: 619
		public const string MicrosoftInformationProtection = "MicrosoftInformationProtection";

		// Token: 0x0400026C RID: 620
		public const string Lakehouse = "Lakehouse";

		// Token: 0x0400026D RID: 621
		public const string MashupCacheManager = "MashupCacheManager";
	}
}
