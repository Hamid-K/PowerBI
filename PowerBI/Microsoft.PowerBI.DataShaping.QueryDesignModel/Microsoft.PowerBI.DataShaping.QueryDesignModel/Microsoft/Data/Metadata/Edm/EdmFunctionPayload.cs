using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000081 RID: 129
	internal struct EdmFunctionPayload
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x000172F2 File Offset: 0x000154F2
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x000172FA File Offset: 0x000154FA
		public string Name { readonly get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00017303 File Offset: 0x00015503
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0001730B File Offset: 0x0001550B
		public string NamespaceName { readonly get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00017314 File Offset: 0x00015514
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0001731C File Offset: 0x0001551C
		public string Schema { readonly get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00017325 File Offset: 0x00015525
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0001732D File Offset: 0x0001552D
		public string StoreFunctionName { readonly get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00017336 File Offset: 0x00015536
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0001733E File Offset: 0x0001553E
		public string CommandText { readonly get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00017347 File Offset: 0x00015547
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x0001734F File Offset: 0x0001554F
		public EntitySet EntitySet { readonly get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00017358 File Offset: 0x00015558
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00017360 File Offset: 0x00015560
		public bool? IsAggregate { readonly get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00017369 File Offset: 0x00015569
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00017371 File Offset: 0x00015571
		public bool? IsBuiltIn { readonly get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0001737A File Offset: 0x0001557A
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x00017382 File Offset: 0x00015582
		public bool? IsNiladic { readonly get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0001738B File Offset: 0x0001558B
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00017393 File Offset: 0x00015593
		public bool? IsComposable { readonly get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001739C File Offset: 0x0001559C
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x000173A4 File Offset: 0x000155A4
		public bool? IsFromProviderManifest { readonly get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x000173AD File Offset: 0x000155AD
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x000173B5 File Offset: 0x000155B5
		public bool? IsCachedStoreFunction { readonly get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x000173BE File Offset: 0x000155BE
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x000173C6 File Offset: 0x000155C6
		public FunctionParameter ReturnParameter { readonly get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000173CF File Offset: 0x000155CF
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x000173D7 File Offset: 0x000155D7
		public ParameterTypeSemantics? ParameterTypeSemantics { readonly get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x000173E0 File Offset: 0x000155E0
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x000173E8 File Offset: 0x000155E8
		public FunctionParameter[] Parameters { readonly get; set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x000173F1 File Offset: 0x000155F1
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x000173F9 File Offset: 0x000155F9
		public DataSpace DataSpace { readonly get; set; }
	}
}
