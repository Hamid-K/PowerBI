using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x0200003F RID: 63
	public sealed class ServiceState
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00002D29 File Offset: 0x00000F29
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00002D31 File Offset: 0x00000F31
		public bool IsAvailable { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00002D3A File Offset: 0x00000F3A
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00002D42 File Offset: 0x00000F42
		public IEnumerable<string> RestrictedFeatures { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00002D4B File Offset: 0x00000F4B
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00002D53 File Offset: 0x00000F53
		public IEnumerable<string> AllowedSystemActions { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00002D5C File Offset: 0x00000F5C
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00002D64 File Offset: 0x00000F64
		public string TimeZone { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00002D6D File Offset: 0x00000F6D
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00002D75 File Offset: 0x00000F75
		public bool UserHasFavorites { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00002D7E File Offset: 0x00000F7E
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00002D86 File Offset: 0x00000F86
		public string AcceptLanguage { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00002D8F File Offset: 0x00000F8F
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00002D97 File Offset: 0x00000F97
		public bool RequireIntune { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00002DA0 File Offset: 0x00000FA0
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public ProductType ProductType { get; set; }
	}
}
