using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200004E RID: 78
	[DataContract]
	public sealed class GatewayClusterDetails
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00003D8E File Offset: 0x00001F8E
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00003D96 File Offset: 0x00001F96
		[DataMember(Name = "gatewayId", Order = 0)]
		public long GatewayId { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00003D9F File Offset: 0x00001F9F
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00003DA7 File Offset: 0x00001FA7
		[DataMember(Name = "objectId", Order = 10)]
		public Guid ObjectId { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00003DB0 File Offset: 0x00001FB0
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00003DB8 File Offset: 0x00001FB8
		[DataMember(Name = "name", Order = 20)]
		public string Name { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00003DC1 File Offset: 0x00001FC1
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00003DC9 File Offset: 0x00001FC9
		[DataMember(Name = "description", Order = 30)]
		public string Description { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00003DD2 File Offset: 0x00001FD2
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00003DDA File Offset: 0x00001FDA
		[DataMember(Name = "publicKey", Order = 40)]
		public string PublicKey { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00003DE3 File Offset: 0x00001FE3
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00003DEB File Offset: 0x00001FEB
		[DataMember(Name = "gateways", Order = 50, EmitDefaultValue = false)]
		public IList<GatewayClusterMemberDetails> Gateways { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00003DF4 File Offset: 0x00001FF4
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00003DFC File Offset: 0x00001FFC
		[DataMember(Name = "loadBalancingSettings", Order = 60)]
		public string LoadBalancingSettings { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00003E05 File Offset: 0x00002005
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00003E0D File Offset: 0x0000200D
		[DataMember(Name = "annotation", Order = 70)]
		public string Annotation { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00003E16 File Offset: 0x00002016
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00003E1E File Offset: 0x0000201E
		[DataMember(Name = "allowCloudDatasourceRefreshThroughGateway", Order = 80)]
		public bool AllowCloudDatasourceRefreshThroughGateway { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00003E27 File Offset: 0x00002027
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00003E2F File Offset: 0x0000202F
		[DataMember(Name = "allowableOptions", Order = 90)]
		public GatewayAllowableOptions AllowableOptions { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00003E38 File Offset: 0x00002038
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00003E40 File Offset: 0x00002040
		[DataMember(Name = "versionStatus", Order = 100)]
		public string VersionStatus { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00003E49 File Offset: 0x00002049
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00003E51 File Offset: 0x00002051
		[DataMember(Name = "expiryDate", Order = 110)]
		public DateTime? ExpiryDate { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00003E5A File Offset: 0x0000205A
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00003E62 File Offset: 0x00002062
		[DataMember(Name = "type", Order = 120)]
		public GatewayType Type { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00003E6B File Offset: 0x0000206B
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00003E73 File Offset: 0x00002073
		[DataMember(Name = "permission", Order = 130)]
		public UnifiedGatewayPrincipalEntryResponse Permission { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00003E7C File Offset: 0x0000207C
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00003E84 File Offset: 0x00002084
		[DataMember(Name = "userDatasourceLimitStatus", Order = 140)]
		public UserDatasourceLimitStatus UserDatasourceLimitStatus { get; set; }

		// Token: 0x0600024D RID: 589 RVA: 0x00003E90 File Offset: 0x00002090
		public static implicit operator GatewayClusterDetails(UnifiedGatewayClusterDetails gatewayClusterDetails)
		{
			GatewayClusterDetails gatewayClusterDetails2 = new GatewayClusterDetails();
			gatewayClusterDetails2.GatewayId = gatewayClusterDetails.GatewayId;
			gatewayClusterDetails2.ObjectId = gatewayClusterDetails.ObjectId;
			gatewayClusterDetails2.Name = gatewayClusterDetails.Name;
			gatewayClusterDetails2.Description = gatewayClusterDetails.Description;
			gatewayClusterDetails2.PublicKey = gatewayClusterDetails.PublicKey;
			gatewayClusterDetails2.Gateways = GatewayClusterDetails.ToServiceGatewayClusterMemberDetails(gatewayClusterDetails.Gateways);
			gatewayClusterDetails2.LoadBalancingSettings = gatewayClusterDetails.LoadBalancingSettings;
			gatewayClusterDetails2.Annotation = gatewayClusterDetails.Annotation;
			gatewayClusterDetails2.AllowableOptions = GatewayClusterDetails.ToServiceGatewayAllowableOptions(gatewayClusterDetails.AllowableOptions);
			gatewayClusterDetails2.VersionStatus = gatewayClusterDetails.VersionStatus;
			gatewayClusterDetails2.ExpiryDate = gatewayClusterDetails.ExpiryDate;
			gatewayClusterDetails2.Type = GatewayClusterDetails.ToServiceGatewayType(gatewayClusterDetails.Type);
			gatewayClusterDetails2.Permission = gatewayClusterDetails.Permission;
			gatewayClusterDetails2.UserDatasourceLimitStatus = gatewayClusterDetails.UserDatasourceLimitStatus;
			gatewayClusterDetails2.AllowCloudDatasourceRefreshThroughGateway = gatewayClusterDetails2.AllowableOptions.HasFlag(GatewayAllowableOptions.CloudDatasourceRefresh);
			return gatewayClusterDetails2;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00003F78 File Offset: 0x00002178
		private static GatewayType ToServiceGatewayType(string gatewayType)
		{
			GatewayType gatewayType2;
			if (!Enum.TryParse<GatewayType>(gatewayType, true, out gatewayType2))
			{
				throw new InvalidOperationException("Invalid GatewayType: input string " + gatewayType);
			}
			return gatewayType2;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00003FA4 File Offset: 0x000021A4
		private static GatewayAllowableOptions ToServiceGatewayAllowableOptions(IDictionary<string, bool> allowableOptions)
		{
			GatewayAllowableOptions gatewayAllowableOptions = GatewayAllowableOptions.None;
			foreach (KeyValuePair<string, bool> keyValuePair in allowableOptions)
			{
				GatewayAllowableOptions gatewayAllowableOptions2;
				if (Enum.TryParse<GatewayAllowableOptions>(keyValuePair.Key, true, out gatewayAllowableOptions2) && keyValuePair.Value)
				{
					gatewayAllowableOptions |= gatewayAllowableOptions2;
				}
			}
			return gatewayAllowableOptions;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00004008 File Offset: 0x00002208
		private static GatewayStatus ToServiceGatewayStatus(string gatewayStatus)
		{
			GatewayStatus gatewayStatus2;
			if (!Enum.TryParse<GatewayStatus>(gatewayStatus, true, out gatewayStatus2))
			{
				throw new InvalidOperationException("Invalid GatewayStatus: input string " + gatewayStatus);
			}
			return gatewayStatus2;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00004034 File Offset: 0x00002234
		private static ClusterMemberStatus ToServiceClusterMemberStatus(string clusterMemberStatus)
		{
			ClusterMemberStatus clusterMemberStatus2;
			if (!Enum.TryParse<ClusterMemberStatus>(clusterMemberStatus, true, out clusterMemberStatus2))
			{
				throw new InvalidOperationException("Invalid ClusterMemberStatus: input string " + clusterMemberStatus);
			}
			return clusterMemberStatus2;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000405E File Offset: 0x0000225E
		private static IList<GatewayClusterMemberDetails> ToServiceGatewayClusterMemberDetails(IEnumerable<UnifiedGatewayClusterMemberDetails> memberGateways)
		{
			return memberGateways.Select((UnifiedGatewayClusterMemberDetails mgw) => new GatewayClusterMemberDetails
			{
				GatewayId = mgw.GatewayId,
				GatewayObjectId = mgw.GatewayObjectId,
				GatewayName = mgw.GatewayName,
				GatewayAnnotation = mgw.GatewayAnnotation,
				GatewayStatus = GatewayClusterDetails.ToServiceGatewayStatus(mgw.GatewayStatus),
				IsAnchorGateway = mgw.IsAnchorGateway,
				GatewayClusterStatus = GatewayClusterDetails.ToServiceClusterMemberStatus(mgw.GatewayClusterStatus),
				GatewayLoadBalancingSettings = mgw.GatewayLoadBalancingSettings,
				GatewayPublicKey = mgw.GatewayPublicKey,
				GatewayVersionStatus = mgw.GatewayVersionStatus,
				ExpiryDate = mgw.ExpiryDate
			}).ToList<GatewayClusterMemberDetails>();
		}
	}
}
