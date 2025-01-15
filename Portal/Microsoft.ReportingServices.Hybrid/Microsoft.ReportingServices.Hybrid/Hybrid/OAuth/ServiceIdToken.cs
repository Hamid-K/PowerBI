using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x02000011 RID: 17
	[DataContract]
	public sealed class ServiceIdToken
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002CF1 File Offset: 0x00000EF1
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002CF9 File Offset: 0x00000EF9
		[DataMember(Name = "aud")]
		public string Audience { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D02 File Offset: 0x00000F02
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002D0A File Offset: 0x00000F0A
		[DataMember(Name = "iss")]
		public string TokenIssuer { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002D13 File Offset: 0x00000F13
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002D1B File Offset: 0x00000F1B
		[DataMember(Name = "iat")]
		public long TokenIssuedAt { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002D24 File Offset: 0x00000F24
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002D2C File Offset: 0x00000F2C
		[DataMember(Name = "nbf")]
		public long NotBefore { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002D35 File Offset: 0x00000F35
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002D3D File Offset: 0x00000F3D
		[DataMember(Name = "exp")]
		public long Expiration { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002D46 File Offset: 0x00000F46
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002D4E File Offset: 0x00000F4E
		[DataMember(Name = "ver")]
		public string Version { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002D57 File Offset: 0x00000F57
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002D5F File Offset: 0x00000F5F
		[DataMember(Name = "tid")]
		public Guid TenantId { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D68 File Offset: 0x00000F68
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002D70 File Offset: 0x00000F70
		[DataMember(Name = "oid")]
		public Guid ObjectId { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D79 File Offset: 0x00000F79
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002D81 File Offset: 0x00000F81
		[DataMember(Name = "upn")]
		public string UserPrincipalName { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002D8A File Offset: 0x00000F8A
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002D92 File Offset: 0x00000F92
		[DataMember(Name = "unique_name")]
		public string UniqueName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002D9B File Offset: 0x00000F9B
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002DA3 File Offset: 0x00000FA3
		[DataMember(Name = "sub")]
		public string Subject { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002DAC File Offset: 0x00000FAC
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002DB4 File Offset: 0x00000FB4
		[DataMember(Name = "family_name")]
		public string FamilyName { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002DBD File Offset: 0x00000FBD
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002DC5 File Offset: 0x00000FC5
		[DataMember(Name = "given_name")]
		public string GivenName { get; set; }
	}
}
