using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200008C RID: 140
	[NullableContext(2)]
	[Nullable(0)]
	[DataContract]
	public class TridentContextDetails
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00005148 File Offset: 0x00003348
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00005150 File Offset: 0x00003350
		[DataMember(IsRequired = false, Name = "artifactId")]
		public string ArtifactId { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00005159 File Offset: 0x00003359
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x00005161 File Offset: 0x00003361
		[DataMember(IsRequired = false, Name = "artifactKind")]
		public string ArtifactKind { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000516A File Offset: 0x0000336A
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x00005172 File Offset: 0x00003372
		[DataMember(IsRequired = false, Name = "artifactName")]
		public string ArtifactName { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000517B File Offset: 0x0000337B
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x00005183 File Offset: 0x00003383
		[Nullable(1)]
		[DataMember(IsRequired = true, Name = "capacityId")]
		public string CapacityId
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			set;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000518C File Offset: 0x0000338C
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00005194 File Offset: 0x00003394
		[Nullable(1)]
		[DataMember(IsRequired = true, Name = "mwcRolloutBaseUri")]
		public string MwcRolloutBaseUri
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			set;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000519D File Offset: 0x0000339D
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x000051A5 File Offset: 0x000033A5
		[DataMember(IsRequired = false, Name = "tenantId")]
		public Guid TenantId { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x000051AE File Offset: 0x000033AE
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x000051B6 File Offset: 0x000033B6
		[DataMember(IsRequired = false, Name = "workloadId")]
		public string WorkloadId { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x000051BF File Offset: 0x000033BF
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x000051C7 File Offset: 0x000033C7
		[DataMember(IsRequired = false, Name = "workloadResourceMoniker")]
		public string WorkloadResourceMoniker { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x000051D0 File Offset: 0x000033D0
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x000051D8 File Offset: 0x000033D8
		[DataMember(IsRequired = false, Name = "workloadType")]
		public string WorkloadType { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000051E1 File Offset: 0x000033E1
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x000051E9 File Offset: 0x000033E9
		[DataMember(IsRequired = false, Name = "workspaceId")]
		public string WorkspaceId { get; set; }
	}
}
