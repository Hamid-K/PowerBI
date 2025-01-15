using System;
using Microsoft.Identity.Client.Cache;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000146 RID: 326
	public class AuthenticationResultMetadata
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x0003AED3 File Offset: 0x000390D3
		public AuthenticationResultMetadata(TokenSource tokenSource)
		{
			this.TokenSource = tokenSource;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0003AEE2 File Offset: 0x000390E2
		public TokenSource TokenSource { get; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0003AEEA File Offset: 0x000390EA
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x0003AEF2 File Offset: 0x000390F2
		public string TokenEndpoint { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0003AEFB File Offset: 0x000390FB
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x0003AF03 File Offset: 0x00039103
		public long DurationTotalInMs { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0003AF0C File Offset: 0x0003910C
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x0003AF14 File Offset: 0x00039114
		public long DurationInCacheInMs { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0003AF1D File Offset: 0x0003911D
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x0003AF25 File Offset: 0x00039125
		public long DurationInHttpInMs { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0003AF2E File Offset: 0x0003912E
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x0003AF36 File Offset: 0x00039136
		public DateTimeOffset? RefreshOn { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0003AF3F File Offset: 0x0003913F
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x0003AF47 File Offset: 0x00039147
		public CacheRefreshReason CacheRefreshReason { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0003AF50 File Offset: 0x00039150
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x0003AF58 File Offset: 0x00039158
		public CacheLevel CacheLevel { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0003AF61 File Offset: 0x00039161
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x0003AF69 File Offset: 0x00039169
		public RegionDetails RegionDetails { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0003AF72 File Offset: 0x00039172
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x0003AF7A File Offset: 0x0003917A
		public string Telemetry { get; set; }
	}
}
