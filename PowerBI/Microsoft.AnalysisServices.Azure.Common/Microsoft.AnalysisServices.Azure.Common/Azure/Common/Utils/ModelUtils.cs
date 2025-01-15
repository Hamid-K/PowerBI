using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.PowerBI.ContentProviders;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x02000144 RID: 324
	public static class ModelUtils
	{
		// Token: 0x06001161 RID: 4449 RVA: 0x00046BB0 File Offset: 0x00044DB0
		public static string GetConnectionString(DataSourceMapping dataSourceMapping)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Data Source={0};".FormatWithInvariantCulture(new object[] { dataSourceMapping.ProviderEndPoint }));
			stringBuilder.Append("Identity={0};DataSet={1};".FormatWithInvariantCulture(new object[]
			{
				dataSourceMapping.DataAccessInfo.SID,
				dataSourceMapping.DataAccessInfo.DSID
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00046C1C File Offset: 0x00044E1C
		public static DataSourceMapping GetDataSourceMappingById(IEnumerable<DataSourceMapping> dataSources, string dataSourceId)
		{
			if (dataSources == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(dataSourceId))
			{
				return null;
			}
			foreach (DataSourceMapping dataSourceMapping in dataSources)
			{
				if (dataSourceId.Equals(dataSourceMapping.ASDataSource))
				{
					return dataSourceMapping;
				}
			}
			return null;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00046C84 File Offset: 0x00044E84
		public static bool IsTabularModel(string xmlaDbSchema)
		{
			return !string.IsNullOrWhiteSpace(xmlaDbSchema) && xmlaDbSchema.Contains(ModelUtils.STORAGE_ENGINE_MODE);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00046C9B File Offset: 0x00044E9B
		public static bool IsModelXmlaBased(PowerBIModelProvisioningInfo modelInfo)
		{
			return !string.IsNullOrEmpty(modelInfo.XmlaSchema) || !string.IsNullOrEmpty(modelInfo.TabularJsonSchema);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00046CBA File Offset: 0x00044EBA
		public static string NormalizePartitionIdAsQueryName(string input)
		{
			return "V1" + Convert.ToBase64String(Encoding.UTF8.GetBytes(input)).Replace("=", "");
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00046CE5 File Offset: 0x00044EE5
		public static string GenerateProcessFullXmla(string databaseId, bool isTabularModel)
		{
			if (isTabularModel)
			{
				return "<Batch Transaction=\"false\" xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n            <Refresh xmlns=\"http://schemas.microsoft.com/analysisservices/2014/engine\">\r\n            <DatabaseID>__PowerBI_Database_Identifier__</DatabaseID>\r\n            <Model>\r\n              <!-- Begin Refresh Model schema -->\r\n              <xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:sql=\"urn:schemas-microsoft-com:xml-sql\">\r\n                <xs:element>\r\n                  <xs:complexType>\r\n                    <xs:sequence>\r\n                      <xs:element type=\"row\" />\r\n                    </xs:sequence>\r\n                  </xs:complexType>\r\n                </xs:element>\r\n                <xs:complexType name=\"row\">\r\n                  <xs:sequence>\r\n                    <xs:element name=\"RefreshType\" type=\"xs:long\" sql:field=\"RefreshType\" minOccurs=\"0\" />\r\n                  </xs:sequence>\r\n                </xs:complexType>\r\n              </xs:schema>\r\n              <!-- End Refresh Model schema -->\r\n              <row xmlns=\"urn:schemas-microsoft-com:xml-analysis:rowset\">\r\n                <RefreshType>1</RefreshType>\r\n              </row>\r\n            </Model>          \r\n          </Refresh>           \r\n        </Batch>".Replace("__PowerBI_Database_Identifier__", databaseId);
			}
			return "<Process xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">\r\n                <Type>ProcessFull</Type>\r\n                <Object>\r\n                <DatabaseID>__PowerBI_Database_Identifier__</DatabaseID></Object></Process>".Replace("__PowerBI_Database_Identifier__", databaseId);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00046D0B File Offset: 0x00044F0B
		public static string GenerateIncrementalRefreshXmla(int maxParallel, DateTime incrementalRefreshEffectiveDate, string databaseId)
		{
			return "{\r\n                \"sequence\": \r\n                {\r\n                    \"maxParallelism\": {MAXPARALLELISM},\r\n                    \"operations\": \r\n                    [\r\n                        {\r\n                            \"refresh\": \r\n                            {\r\n                                \"type\": \"full\",\r\n                                \"applyRefreshPolicy\": true,\r\n                                \"effectiveDate\": \"{EFFECTIVEDATE}\",\r\n                                \"objects\": \r\n                                [\r\n                                    {\r\n                                        \"database\": \"{DATABASE}\"\r\n                                    }\r\n                                ]\r\n                            }\r\n                        }\r\n                    ]\r\n                }\r\n            } ".Replace("{MAXPARALLELISM}", maxParallel.ToString()).Replace("{EFFECTIVEDATE}", incrementalRefreshEffectiveDate.ToString("d", CultureInfo.InvariantCulture)).Replace("{DATABASE}", databaseId);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00046D4C File Offset: 0x00044F4C
		public static bool RequirePowerQueryTranslation(string managedProviderName, string connectionstring)
		{
			if (!string.IsNullOrEmpty(managedProviderName) && ModelUtils.NonPowerPivotProviders.Any((string e) => managedProviderName.StartsWith(e, StringComparison.InvariantCultureIgnoreCase)))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceInformation("RequirePowerQueryTranslation: providerName {0}, result: false".FormatWithInvariantCulture(new object[] { managedProviderName }));
				return false;
			}
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
			dbConnectionStringBuilder.ConnectionString = connectionstring;
			object obj;
			if (dbConnectionStringBuilder.TryGetValue("Provider", out obj))
			{
				string providerValue = obj as string;
				if (providerValue != null && ModelUtils.NonPowerPivotProviders.Any((string e) => providerValue.StartsWith(e, StringComparison.InvariantCultureIgnoreCase)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceInformation("RequirePowerQueryTranslation: providerValue {0}, result: false".FormatWithInvariantCulture(new object[] { providerValue }));
					return false;
				}
			}
			object obj2;
			if (dbConnectionStringBuilder.TryGetValue("Data Source", out obj2))
			{
				string text = obj2 as string;
				if (text != null && text.EndsWith("database.windows.net", StringComparison.OrdinalIgnoreCase))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceInformation("RequirePowerQueryTranslation: DataSource {0}, result: false".FormatWithInvariantCulture(new object[] { text }));
					return false;
				}
			}
			TraceSourceBase<ANCommonTrace>.Tracer.TraceInformation("RequirePowerQueryTranslation: providerName {0}, result: true".FormatWithInvariantCulture(new object[] { managedProviderName }));
			return true;
		}

		// Token: 0x040003EB RID: 1003
		private static string STORAGE_ENGINE_MODE = "<ddl200_200:StorageEngineUsed>TabularMetadata</ddl200_200:StorageEngineUsed>";

		// Token: 0x040003EC RID: 1004
		private const string SqlAzureDomainName = "database.windows.net";

		// Token: 0x040003ED RID: 1005
		private static readonly List<string> NonPowerPivotProviders = new List<string> { "Microsoft.Mashup.OleDb", "ManagedProviderForPowerBI" };
	}
}
