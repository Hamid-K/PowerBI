using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000414 RID: 1044
	public sealed class ObjectContextOptions
	{
		// Token: 0x060031F6 RID: 12790 RVA: 0x000A0C2C File Offset: 0x0009EE2C
		internal ObjectContextOptions()
		{
			this.ProxyCreationEnabled = true;
			this.EnsureTransactionsForFunctionsAndCommands = true;
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x000A0C42 File Offset: 0x0009EE42
		// (set) Token: 0x060031F8 RID: 12792 RVA: 0x000A0C4A File Offset: 0x0009EE4A
		public bool EnsureTransactionsForFunctionsAndCommands { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000A0C53 File Offset: 0x0009EE53
		// (set) Token: 0x060031FA RID: 12794 RVA: 0x000A0C5B File Offset: 0x0009EE5B
		public bool LazyLoadingEnabled { get; set; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x000A0C64 File Offset: 0x0009EE64
		// (set) Token: 0x060031FC RID: 12796 RVA: 0x000A0C6C File Offset: 0x0009EE6C
		public bool ProxyCreationEnabled { get; set; }

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x000A0C75 File Offset: 0x0009EE75
		// (set) Token: 0x060031FE RID: 12798 RVA: 0x000A0C7D File Offset: 0x0009EE7D
		public bool UseLegacyPreserveChangesBehavior { get; set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x000A0C86 File Offset: 0x0009EE86
		// (set) Token: 0x06003200 RID: 12800 RVA: 0x000A0C8E File Offset: 0x0009EE8E
		public bool UseConsistentNullReferenceBehavior { get; set; }

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x000A0C97 File Offset: 0x0009EE97
		// (set) Token: 0x06003202 RID: 12802 RVA: 0x000A0C9F File Offset: 0x0009EE9F
		public bool UseCSharpNullComparisonBehavior { get; set; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000A0CA8 File Offset: 0x0009EEA8
		// (set) Token: 0x06003204 RID: 12804 RVA: 0x000A0CB0 File Offset: 0x0009EEB0
		public bool DisableFilterOverProjectionSimplificationForCustomFunctions { get; set; }
	}
}
