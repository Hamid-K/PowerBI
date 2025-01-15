using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200001F RID: 31
	[DataContract]
	internal sealed class OAuthToken
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000052C8 File Offset: 0x000034C8
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000052D0 File Offset: 0x000034D0
		[DataMember(Name = "access_token")]
		public string AccessToken { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000052D9 File Offset: 0x000034D9
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000052E1 File Offset: 0x000034E1
		[DataMember(Name = "expires")]
		public string Expires { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000052EA File Offset: 0x000034EA
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x000052F2 File Offset: 0x000034F2
		[DataMember(Name = "expires_in")]
		public string ExpiresIn { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000052FB File Offset: 0x000034FB
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005303 File Offset: 0x00003503
		[DataMember(Name = "refresh_token")]
		public string RefreshToken { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000530C File Offset: 0x0000350C
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005314 File Offset: 0x00003514
		[DataMember(Name = "scope")]
		public string Scope { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000531D File Offset: 0x0000351D
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00005325 File Offset: 0x00003525
		[DataMember(Name = "instance_url")]
		public string InstanceUrl { get; set; }
	}
}
