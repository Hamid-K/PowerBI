using System;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;
using Microsoft.ReportingServices.Portal.ODataClient.V2;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x02000011 RID: 17
	public static class PbixDataSourceExtensions
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002D24 File Offset: 0x00000F24
		public static PbixDataSource GetPbixDataSourceFromODataClientV2DataSource(DataSource dataSource)
		{
			DataModelDataSource dataModelDataSource = dataSource.DataModelDataSource;
			if (dataSource == null)
			{
				return null;
			}
			string text = dataModelDataSource.Kind.ToString();
			SourceKind sourceKind;
			if (!Enum.TryParse<SourceKind>(text, true, out sourceKind))
			{
				throw new ArgumentOutOfRangeException("The ODataClient.V2 DataModelDataSourceKind: {0} does not have a representation in PbixDataSource.SourceKind", text);
			}
			string text2 = dataModelDataSource.Type.ToString();
			AccessType accessType;
			if (!Enum.TryParse<AccessType>(text2, true, out accessType))
			{
				throw new ArgumentOutOfRangeException("The ODataClient.V2 DataModelDataSourceType: {0} does not have a representation in PbixDataSource.AccessType", text2);
			}
			string text3 = dataModelDataSource.AuthType.ToString();
			AuthorizationType authorizationType;
			if (!Enum.TryParse<AuthorizationType>(text3, true, out authorizationType))
			{
				throw new ArgumentOutOfRangeException("The ODataClient.V2 DataModelDataSourceType: {0} does not have a representation in PbixDataSource.AuthorizationType", text3);
			}
			return new PbixDataSource
			{
				AuthType = authorizationType,
				ConnectionString = dataSource.ConnectionString,
				DataSourceIdentifier = dataModelDataSource.ModelConnectionName,
				Kind = sourceKind,
				Secret = dataModelDataSource.Secret,
				Type = accessType,
				Username = dataModelDataSource.Username
			};
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E18 File Offset: 0x00001018
		public static DataSource ToOdataV2DataSource(this PbixDataSource pbixDataSource)
		{
			string text = pbixDataSource.Kind.ToString();
			DataModelDataSourceKind dataModelDataSourceKind;
			if (!Enum.TryParse<DataModelDataSourceKind>(text, true, out dataModelDataSourceKind))
			{
				throw new ArgumentOutOfRangeException("The PbixDataSource.SourceKind: {0} does not have a representation in ODataClient.V2 DataModelDataSourceKind", text);
			}
			string text2 = pbixDataSource.Type.ToString();
			DataModelDataSourceType dataModelDataSourceType;
			if (!Enum.TryParse<DataModelDataSourceType>(text2, true, out dataModelDataSourceType))
			{
				throw new ArgumentOutOfRangeException("The PbixDataSource.AccessType: {0} does not have a representation in ODataClient.V2 DataModelDataSourceType", text2);
			}
			string text3 = pbixDataSource.AuthType.ToString();
			DataModelDataSourceAuthType dataModelDataSourceAuthType;
			if (!Enum.TryParse<DataModelDataSourceAuthType>(text3, true, out dataModelDataSourceAuthType))
			{
				throw new ArgumentOutOfRangeException("The PbixDataSource.AuthType: {0} does not have a representation in ODataClient.V2 DataModelDataSourceAuthType", text3);
			}
			return new DataSource
			{
				ConnectionString = pbixDataSource.ConnectionString,
				DataModelDataSource = new DataModelDataSource
				{
					Kind = dataModelDataSourceKind,
					Type = dataModelDataSourceType,
					AuthType = dataModelDataSourceAuthType,
					Username = pbixDataSource.Username,
					Secret = pbixDataSource.Secret,
					ModelConnectionName = pbixDataSource.DataSourceIdentifier
				}
			};
		}
	}
}
