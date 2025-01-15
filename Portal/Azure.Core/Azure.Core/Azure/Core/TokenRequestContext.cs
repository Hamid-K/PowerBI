using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000067 RID: 103
	[NullableContext(2)]
	[Nullable(0)]
	public readonly struct TokenRequestContext
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000A450 File Offset: 0x00008650
		[NullableContext(1)]
		public TokenRequestContext(string[] scopes, [Nullable(2)] string parentRequestId)
		{
			this.IsCaeEnabled = false;
			this.Scopes = scopes;
			this.ParentRequestId = parentRequestId;
			this.Claims = null;
			this.TenantId = null;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A475 File Offset: 0x00008675
		public TokenRequestContext([Nullable(1)] string[] scopes, string parentRequestId, string claims)
		{
			this.IsCaeEnabled = false;
			this.Scopes = scopes;
			this.ParentRequestId = parentRequestId;
			this.Claims = claims;
			this.TenantId = null;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000A49A File Offset: 0x0000869A
		public TokenRequestContext([Nullable(1)] string[] scopes, string parentRequestId, string claims, string tenantId)
		{
			this.IsCaeEnabled = false;
			this.Scopes = scopes;
			this.ParentRequestId = parentRequestId;
			this.Claims = claims;
			this.TenantId = tenantId;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public TokenRequestContext([Nullable(1)] string[] scopes, string parentRequestId = null, string claims = null, string tenantId = null, bool isCaeEnabled = false)
		{
			this.Scopes = scopes;
			this.ParentRequestId = parentRequestId;
			this.Claims = claims;
			this.TenantId = tenantId;
			this.IsCaeEnabled = isCaeEnabled;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000A4E7 File Offset: 0x000086E7
		[Nullable(1)]
		public string[] Scopes
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000A4EF File Offset: 0x000086EF
		public string ParentRequestId { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000A4F7 File Offset: 0x000086F7
		public string Claims { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000A4FF File Offset: 0x000086FF
		public string TenantId { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000A507 File Offset: 0x00008707
		public bool IsCaeEnabled { get; }
	}
}
