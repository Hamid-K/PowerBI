using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200004D RID: 77
	public sealed class ContractConceptualSchemaStatistics
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00004188 File Offset: 0x00002388
		public ContractConceptualSchemaStatistics(IEnumerable<ContractConceptualEntitySetStatistics> tableStats, int telemetryIdentifier, int activeRelationshipCount, int inactiveRelationshipCount, int islandSubgraphCount, int inflectionPointsCount, int podCount)
		{
			this.TableStats = tableStats.AsReadOnlyCollection<ContractConceptualEntitySetStatistics>();
			this.ConceptualSchemaUniqueIdentifier = telemetryIdentifier;
			this.ActiveRelationshipCount = activeRelationshipCount;
			this.InactiveRelationshipCount = inactiveRelationshipCount;
			this.IslandSubgraphCount = islandSubgraphCount;
			this.InflectionPointsCount = inflectionPointsCount;
			this.PodCount = podCount;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000041D5 File Offset: 0x000023D5
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000041EC File Offset: 0x000023EC
		public int TableCount
		{
			get
			{
				if (this.TableStats != null)
				{
					return this.TableStats.Count;
				}
				return 0;
			}
			private set
			{
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000041EE File Offset: 0x000023EE
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000041F6 File Offset: 0x000023F6
		public ReadOnlyCollection<ContractConceptualEntitySetStatistics> TableStats { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000041FF File Offset: 0x000023FF
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00004207 File Offset: 0x00002407
		public int ConceptualSchemaUniqueIdentifier { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00004210 File Offset: 0x00002410
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00004218 File Offset: 0x00002418
		public int ActiveRelationshipCount { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00004221 File Offset: 0x00002421
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00004229 File Offset: 0x00002429
		public int InactiveRelationshipCount { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004232 File Offset: 0x00002432
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000423A File Offset: 0x0000243A
		public int IslandSubgraphCount { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004243 File Offset: 0x00002443
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000424B File Offset: 0x0000244B
		public int InflectionPointsCount { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004254 File Offset: 0x00002454
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000425C File Offset: 0x0000245C
		public int PodCount { get; private set; }

		// Token: 0x06000159 RID: 345 RVA: 0x00004265 File Offset: 0x00002465
		public string ToJsonString(Formatting formatting = Formatting.None)
		{
			return JsonConvert.SerializeObject(this, formatting);
		}
	}
}
