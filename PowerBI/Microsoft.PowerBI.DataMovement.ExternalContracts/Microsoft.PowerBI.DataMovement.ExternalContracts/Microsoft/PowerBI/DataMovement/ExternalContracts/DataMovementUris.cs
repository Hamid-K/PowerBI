using System;
using System.Globalization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts
{
	// Token: 0x0200000A RID: 10
	public static class DataMovementUris
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020F0 File Offset: 0x000002F0
		public static string GetGatewaysUrl(GatewayType type = GatewayType.Resource)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways?type={0}", type.ToString());
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000210E File Offset: 0x0000030E
		public static string GetUnifiedGatewaysUrl(GatewayType type = GatewayType.Resource)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gateways?type={0}", type.ToString());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		public static string GetGatewayUrl<T>(T gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}", gatewayObjectId);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002143 File Offset: 0x00000343
		public static string GetOnPremMigrationAllowedUrl()
		{
			return "/metadata/datamovement/onpremmigrationallowed";
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000214A File Offset: 0x0000034A
		public static string GetUnifiedGatewayUrl(Guid gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gateways/{0}", gatewayObjectId);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002161 File Offset: 0x00000361
		public static string GetGatewayStatusUrl<T>(T gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/status", gatewayObjectId);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002178 File Offset: 0x00000378
		public static string GetUnifiedGatewayStatusUrl(Guid gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gateways/{0}/status", gatewayObjectId);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000218F File Offset: 0x0000038F
		public static string GetGatewayPrincipalsUrl<T>(T gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/principals", gatewayObjectId);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021A6 File Offset: 0x000003A6
		public static string GetDeleteGatewayPrincipalsUrl(Guid gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/principals/delete", gatewayObjectId);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021BD File Offset: 0x000003BD
		public static string GetUnifiedGatewayPrincipalsUrl(Guid gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gateways/{0}/principals", gatewayObjectId);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021D4 File Offset: 0x000003D4
		public static string GetUnifiedGatewayPrincipalsWithOptionsUrl(Guid gatewayObjectId, GetPrincipalOptions options)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gateways/{0}/principals?options={1}", gatewayObjectId, options);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021F1 File Offset: 0x000003F1
		public static string GetDatasourceUrl<T>(T gatewayObjectId, T datasourceObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/datasources/{1}", gatewayObjectId, datasourceObjectId);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000220E File Offset: 0x0000040E
		public static string GetDatasourcesUrl<T>(T gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/datasources", gatewayObjectId);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002225 File Offset: 0x00000425
		public static string GetDatasourceStatusUrl<T>(T gatewayObjectId, T datasourceObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/datasources/{1}/status", gatewayObjectId, datasourceObjectId);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002242 File Offset: 0x00000442
		public static string GetDatasourceStatusUrl(Guid gatewayId, Guid datasourceId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/datasources/{1}/status", gatewayId, datasourceId);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000225F File Offset: 0x0000045F
		public static string GetModelDatasourcesUrl(string modelId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/models/{0}/datasources", modelId);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002271 File Offset: 0x00000471
		public static string GetDatasourceCredentialsUrl<T>(T gatewayObjectId, T datasourceObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/datasources/{1}/credentials", gatewayObjectId, datasourceObjectId);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000228E File Offset: 0x0000048E
		public static string GetDatasourceCredentialsUrl(Guid gatewayId, Guid datasourceId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/datasources/{1}/credentials", gatewayId, datasourceId);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022AB File Offset: 0x000004AB
		public static string GetMonikersMonikerNameUrl(string gatewayId, string datasourceId, string monikerName)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/monikers/{2}", gatewayId, datasourceId, monikerName);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022BF File Offset: 0x000004BF
		public static string GetDeleteMonikersMonikerUrl(string tenantObjectId, string userObjectId, string monikerObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/monikers/{2}", tenantObjectId, userObjectId, monikerObjectId);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022D3 File Offset: 0x000004D3
		public static string GetMonikersBindUrl(string gatewayId, string datasourceId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/monikers/bind", gatewayId, datasourceId);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022E6 File Offset: 0x000004E6
		public static string GetMonikersDiscoverUrl(string tenantObjectId, string userObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/monikers/discover", tenantObjectId, userObjectId);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022F9 File Offset: 0x000004F9
		public static string GetDataSourcesAndGatewaysDiscoverUrl(string tenantObjectId, string userObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/datasourcesAndGateways/discover", tenantObjectId, userObjectId);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000230C File Offset: 0x0000050C
		public static string GetDiscoverDatasourcesUrl(string gatewayId, string datasourceId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/datasources/discover", gatewayId, datasourceId);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000231F File Offset: 0x0000051F
		public static string GetGatewaysDatasourcesCredentialsUrl<T>(string tenantObjectid, string userObjectid, T gatewayObjectId, T datasourceObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/{2}/datasources/{3}/credentials", new object[] { tenantObjectid, userObjectid, gatewayObjectId, datasourceObjectId });
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002350 File Offset: 0x00000550
		public static string GetDatasourceSystemWithTemporaryMonikerUrl<T>(string tenantObjectid, string userObjectid, T gatewayObjectId, T datasourceObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/{2}/datasources/{3}/fullDiscoverWithTemporaryMoniker", new object[] { tenantObjectid, userObjectid, gatewayObjectId, datasourceObjectId });
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002381 File Offset: 0x00000581
		public static string GetDiscoverGatewaysDatasourcesStatusUrl<T>(string tenantObjectid, string userObjectid, T gatewayObjectId, T datasourceObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/{2}/datasources/{3}/status", new object[] { tenantObjectid, userObjectid, gatewayObjectId, datasourceObjectId });
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023B2 File Offset: 0x000005B2
		public static string GetDatasourceGatewaysStatusUrl<T>(string tenantObjectid, string userObjectid, T gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/{2}/status", tenantObjectid, userObjectid, gatewayObjectId);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023CB File Offset: 0x000005CB
		public static string GetGatewayClustersStatusUrl<T>(string tenantObjectid, string userObjectid, T anchorGatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/gatewayClusters/{2}/status", tenantObjectid, userObjectid, anchorGatewayObjectId);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023E4 File Offset: 0x000005E4
		public static string GetDatasourceStatusUrl(string tenantObjectid, string userObjectid)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}", tenantObjectid, userObjectid);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023F7 File Offset: 0x000005F7
		public static string GetUserGroupsUrl(string tenantObjectid, string userObjectid)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/groups", tenantObjectid, userObjectid);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000240A File Offset: 0x0000060A
		public static string GetDatasourceGatewaysDefaultStatusUrl(string tenantObjectid, string userObjectid)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/default", tenantObjectid, userObjectid);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000241D File Offset: 0x0000061D
		public static string GetDiscoverGatewaysUrl(string tenantObjectid, string userObjectid)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways", tenantObjectid, userObjectid);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002430 File Offset: 0x00000630
		public static string GetMonikerFullDiscoverSystemUrl(string tenantObjectid, string userObjectid, string moniker)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/dmts/tenants/{0}/users/{1}/monikers/{2}/fullDiscover", tenantObjectid, userObjectid, moniker);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002444 File Offset: 0x00000644
		public static string GetEntropyUrl()
		{
			return "/metadata/datamovement/entropy";
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000244B File Offset: 0x0000064B
		public static string GetDiscoverDatasourceFeaturesUrl()
		{
			return "/metadata/datamovement/dataSourceFeatures";
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002452 File Offset: 0x00000652
		public static string GetDatasourcePrincipalsUrl<T>(T gatewayObjectId, T datasourceObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/datasources/{1}/principals", gatewayObjectId, datasourceObjectId);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000246F File Offset: 0x0000066F
		public static string GetDeleteDatasourcePrincipalsUrl(Guid gatewayId, Guid datasourceId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gateways/{0}/datasources/{1}/principals/delete", gatewayId, datasourceId);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000248C File Offset: 0x0000068C
		public static string GetBindModelUrl(string modelId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/models/{0}/bind", modelId);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000249E File Offset: 0x0000069E
		public static string GetTakeOverMonikerDatasourcesUrl(long modelId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/models/{0}/takeover", modelId);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024B5 File Offset: 0x000006B5
		public static string GetValidateUpdatedParametersUrl(long modelId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/datasources/validateUpdatedParameters/{0}", modelId);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024CC File Offset: 0x000006CC
		public static string GetValidateParametersUrl(string contentProviderKey)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/datasources/validateParameters/{0}", contentProviderKey);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000024DE File Offset: 0x000006DE
		public static string CreateUnifiedGatewayGatewayWithAppsUrl()
		{
			return "/unifiedgateway/gateways/CreateGatewayWithApps";
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000024E5 File Offset: 0x000006E5
		public static string GetMigrateUserDatasourcesUrl()
		{
			return "/metadata/datamovement/migratePGW";
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000024EC File Offset: 0x000006EC
		public static string GetUnifiedGatewayClustersRequireAdminUrl(string keyword = null)
		{
			string text = ((keyword == null) ? string.Empty : string.Format(CultureInfo.InvariantCulture, "?keyword={0}", keyword));
			return "/unifiedgateway/gatewayclusters" + text;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000251F File Offset: 0x0000071F
		public static string GetGatewayClustersRequireAdminUrl(string keyword = null)
		{
			return "/metadata/datamovement/gatewayclusters";
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002526 File Offset: 0x00000726
		public static string GetUpdateAnchorOrAddUpdateFellowGatewayUrl(Guid anchorGatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gatewayclusters/{0}", anchorGatewayObjectId);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000253D File Offset: 0x0000073D
		public static string GetClusterGatewayStatusByObjectIdUrl(Guid anchorGatewayObjectId, Guid gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gatewayclusters/{0}/gateways/{1}/status", anchorGatewayObjectId, gatewayObjectId);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000255A File Offset: 0x0000075A
		public static string GetUpdateClusterGatewayUrl(Guid anchorGatewayObjectId, Guid gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gatewayclusters/{0}/gateways/{1}", anchorGatewayObjectId, gatewayObjectId);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002577 File Offset: 0x00000777
		public static string GetDeleteClusterGatewayUrl(Guid anchorGatewayObjectId, Guid gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gatewayclusters/{0}/gateways/{1}", anchorGatewayObjectId, gatewayObjectId);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002594 File Offset: 0x00000794
		public static string GetClusterDatasourceStatusUrl<T>(T anchorGatewayId, T datasourceId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gatewayclusters/{0}/datasources/{1}/status", anchorGatewayId, datasourceId);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000025B1 File Offset: 0x000007B1
		public static string GetUpdateGatewayClusterUrl(Guid anchorGatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gatewayclusters/{0}", anchorGatewayObjectId);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000025C8 File Offset: 0x000007C8
		public static string GetUpdateUnifiedGatewayClusterUrl(Guid clusterObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gatewayclusters/{0}", clusterObjectId);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000025DF File Offset: 0x000007DF
		public static string GetDiscoverClusterStatusUrl<T>(T anchorGatewayId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gatewayclusters/{0}/status", anchorGatewayId);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000025F6 File Offset: 0x000007F6
		public static string GetPublishDatasourceToClusterUrl<T>(T anchorGatewayId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gatewayclusters/{0}/datasources", anchorGatewayId);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000260D File Offset: 0x0000080D
		public static string GetUpdateClusterDatasourceCredentialsUrl<T>(T anchorGatewayId, T datasourceId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/metadata/datamovement/gatewayclusters/{0}/datasources/{1}/credentials", anchorGatewayId, datasourceId);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000262A File Offset: 0x0000082A
		public static string GetEncryptClusterCredentialsUrl(Guid anchorGatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gatewayclusters/{0}/datasources/EncryptCredentials", anchorGatewayObjectId);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002641 File Offset: 0x00000841
		public static string GetClusterGatewayServiceBusRelaysInfoUrl(Guid anchorGatewayObjectId, Guid gatewayObjectId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gatewayclusters/{0}/gateways/{1}/serviceBusRelaysInfo", anchorGatewayObjectId, gatewayObjectId);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000265E File Offset: 0x0000085E
		public static string GetReEncryptClusterDatasourceCredentialsUrl(Guid anchorGatewayId, Guid datasourceId)
		{
			return string.Format(CultureInfo.InvariantCulture, "/unifiedgateway/gatewayclusters/{0}/datasources/{1}/ReEncryptCredential", anchorGatewayId, datasourceId);
		}

		// Token: 0x0400002C RID: 44
		private const string c_metadataDM = "/metadata/datamovement";

		// Token: 0x0400002D RID: 45
		private const string c_unifiedGateway = "/unifiedgateway";

		// Token: 0x0400002E RID: 46
		private const string c_gateways = "/metadata/datamovement/gateways";

		// Token: 0x0400002F RID: 47
		private const string c_unifiedGateways = "/unifiedgateway/gateways";

		// Token: 0x04000030 RID: 48
		private const string c_gatewayFormat = "/metadata/datamovement/gateways/{0}";

		// Token: 0x04000031 RID: 49
		private const string c_gatewayStatusFormat = "/metadata/datamovement/gateways/{0}/status";

		// Token: 0x04000032 RID: 50
		private const string c_unifiedGatewayFormat = "/unifiedgateway/gateways/{0}";

		// Token: 0x04000033 RID: 51
		private const string c_unifiedgatewayStatusFormat = "/unifiedgateway/gateways/{0}/status";

		// Token: 0x04000034 RID: 52
		private const string c_gatewayPrincipalsFormat = "/metadata/datamovement/gateways/{0}/principals";

		// Token: 0x04000035 RID: 53
		private const string c_deleteGatewayPrincipalsFormat = "/metadata/datamovement/gateways/{0}/principals/delete";

		// Token: 0x04000036 RID: 54
		private const string c_unifiedGatewayPrincipalsFormat = "/unifiedgateway/gateways/{0}/principals";

		// Token: 0x04000037 RID: 55
		private const string c_unifiedGatewayPrincipalsWithOptionsFormat = "/unifiedgateway/gateways/{0}/principals?options={1}";

		// Token: 0x04000038 RID: 56
		private const string c_datasources = "/metadata/datamovement/gateways/{0}/datasources";

		// Token: 0x04000039 RID: 57
		private const string c_datasourcesFormat = "/metadata/datamovement/gateways/{0}/datasources/{1}";

		// Token: 0x0400003A RID: 58
		private const string c_datasourcesStatusFormat = "/metadata/datamovement/gateways/{0}/datasources/{1}/status";

		// Token: 0x0400003B RID: 59
		private const string c_datasourcesCredentialsFormat = "/metadata/datamovement/gateways/{0}/datasources/{1}/credentials";

		// Token: 0x0400003C RID: 60
		private const string c_clusterDatasourcesStatusFormat = "/metadata/datamovement/gatewayclusters/{0}/datasources/{1}/status";

		// Token: 0x0400003D RID: 61
		private const string c_entropy = "/metadata/datamovement/entropy";

		// Token: 0x0400003E RID: 62
		private const string c_datasourcePrincipalsFormat = "/metadata/datamovement/gateways/{0}/datasources/{1}/principals";

		// Token: 0x0400003F RID: 63
		private const string c_deleteDatasourcePrincipalsFormat = "/metadata/datamovement/gateways/{0}/datasources/{1}/principals/delete";

		// Token: 0x04000040 RID: 64
		private const string c_modelFormat = "/metadata/datamovement/models/{0}";

		// Token: 0x04000041 RID: 65
		private const string c_modelDatasourcesFormat = "/metadata/datamovement/models/{0}/datasources";

		// Token: 0x04000042 RID: 66
		private const string c_modelBindFormat = "/metadata/datamovement/models/{0}/bind";

		// Token: 0x04000043 RID: 67
		private const string c_monikerDatasourcesTakeOverFormat = "/metadata/datamovement/models/{0}/takeover";

		// Token: 0x04000044 RID: 68
		private const string c_discoverDatasourceFeatures = "/metadata/datamovement/dataSourceFeatures";

		// Token: 0x04000045 RID: 69
		private const string c_createUnifiedGatewayWithApps = "/unifiedgateway/gateways/CreateGatewayWithApps";

		// Token: 0x04000046 RID: 70
		private const string c_validateParametersFormat = "/metadata/datamovement/datasources";

		// Token: 0x04000047 RID: 71
		private const string c_validateParametersOnUpdateFormat = "/metadata/datamovement/datasources/validateUpdatedParameters/{0}";

		// Token: 0x04000048 RID: 72
		private const string c_validateParametersOnImportFormat = "/metadata/datamovement/datasources/validateParameters/{0}";

		// Token: 0x04000049 RID: 73
		private const string c_migrateUserDatasources = "/metadata/datamovement/migratePGW";

		// Token: 0x0400004A RID: 74
		private const string c_onPremMigrationAllowed = "/metadata/datamovement/onpremmigrationallowed";

		// Token: 0x0400004B RID: 75
		private const string c_gatewayClusters = "/metadata/datamovement/gatewayclusters";

		// Token: 0x0400004C RID: 76
		private const string c_unifiedGatewayClusters = "/unifiedgateway/gatewayclusters";

		// Token: 0x0400004D RID: 77
		private const string c_gatewayClustersGateways = "/unifiedgateway/gatewayclusters/{0}/gateways/{1}";

		// Token: 0x0400004E RID: 78
		private const string c_updateAnchorOrAddUpdateFellowGateway = "/unifiedgateway/gatewayclusters/{0}";

		// Token: 0x0400004F RID: 79
		private const string c_getClusterGatewayStatusByObjectId = "/unifiedgateway/gatewayclusters/{0}/gateways/{1}/status";

		// Token: 0x04000050 RID: 80
		private const string c_updateGatewayCluster = "/metadata/datamovement/gatewayclusters/{0}";

		// Token: 0x04000051 RID: 81
		private const string c_updateUnifiedGatewayCluster = "/unifiedgateway/gatewayclusters/{0}";

		// Token: 0x04000052 RID: 82
		private const string c_getDiscoverClusterStatus = "/metadata/datamovement/gatewayclusters/{0}/status";

		// Token: 0x04000053 RID: 83
		private const string c_clusterDatasources = "/metadata/datamovement/gatewayclusters/{0}/datasources";

		// Token: 0x04000054 RID: 84
		private const string c_updateClusterDatasourceCredentials = "/metadata/datamovement/gatewayclusters/{0}/datasources/{1}/credentials";

		// Token: 0x04000055 RID: 85
		private const string c_reencryptClusterDatasourceCredentials = "/unifiedgateway/gatewayclusters/{0}/datasources/{1}/ReEncryptCredential";

		// Token: 0x04000056 RID: 86
		private const string c_encryptClusterCredentials = "/unifiedgateway/gatewayclusters/{0}/datasources/EncryptCredentials";

		// Token: 0x04000057 RID: 87
		private const string c_unifiedGatewayDmtsTenants = "/unifiedgateway/dmts/tenants";

		// Token: 0x04000058 RID: 88
		private const string c_ugwTenantUsersFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}";

		// Token: 0x04000059 RID: 89
		private const string c_ugwUsersMonikerFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/monikers";

		// Token: 0x0400005A RID: 90
		private const string c_ugwTenantsUsersMonikersBindFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/monikers/bind";

		// Token: 0x0400005B RID: 91
		private const string c_ugwTenantsUsersMonikersDiscoverFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/monikers/discover";

		// Token: 0x0400005C RID: 92
		private const string c_ugwUsersMonikersFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/monikers/{2}";

		// Token: 0x0400005D RID: 93
		private const string c_ugwTenantsUsersMonikerFullDiscoverFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/monikers/{2}/fullDiscover";

		// Token: 0x0400005E RID: 94
		private const string c_ugwTenantUsersDatasourcesDiscoverFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/datasources/discover";

		// Token: 0x0400005F RID: 95
		private const string c_ugwTenantUsersDSAndGatewaysDiscoverFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/datasourcesAndGateways/discover";

		// Token: 0x04000060 RID: 96
		private const string c_ugwTenantUsersGatewaysFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways";

		// Token: 0x04000061 RID: 97
		private const string c_ugwTenantUsersGatewaysDefaultFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/default";

		// Token: 0x04000062 RID: 98
		private const string c_ugwTenantUsersGatewaysStatusFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/{2}/status";

		// Token: 0x04000063 RID: 99
		private const string c_ugwUsersGatewaysDataSourcesFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/{2}/datasources";

		// Token: 0x04000064 RID: 100
		private const string c_ugwUsersGatewaysDataSourcesStatusFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/{2}/datasources/{3}/status";

		// Token: 0x04000065 RID: 101
		private const string c_ugwUsersGatewaysDataSourcesCredsFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/{2}/datasources/{3}/credentials";

		// Token: 0x04000066 RID: 102
		private const string c_ugwUsersGatewaysDataSourcesFullDiscoverFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/gateways/{2}/datasources/{3}/fullDiscoverWithTemporaryMoniker";

		// Token: 0x04000067 RID: 103
		private const string c_ugwUsersGatewayClustersStatusFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/gatewayClusters/{2}/status";

		// Token: 0x04000068 RID: 104
		private const string c_ugwUsersGroupsFormat = "/unifiedgateway/dmts/tenants/{0}/users/{1}/groups";

		// Token: 0x04000069 RID: 105
		private const string c_clusterGatewayServiceBusRelayInfo = "/unifiedgateway/gatewayclusters/{0}/gateways/{1}/serviceBusRelaysInfo";
	}
}
