using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000030 RID: 48
	[ImmutableObject(true)]
	public sealed class ConceptualSchemaStatistics
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x0000295A File Offset: 0x00000B5A
		internal ConceptualSchemaStatistics(int activeRelationshipCount, int inactiveRelationshipCount, int islandSubgraphCount, int inflectionPointsCount)
		{
			this._activeRelationshipCount = activeRelationshipCount;
			this._inactiveRelationshipCount = inactiveRelationshipCount;
			this._islandSubgraphCount = islandSubgraphCount;
			this._inflectionPointsCount = inflectionPointsCount;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000297F File Offset: 0x00000B7F
		internal int ActiveRelationshipCount
		{
			get
			{
				return this._activeRelationshipCount;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002987 File Offset: 0x00000B87
		internal int InactiveRelationshipCount
		{
			get
			{
				return this._inactiveRelationshipCount;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000298F File Offset: 0x00000B8F
		internal int IslandSubgraphCount
		{
			get
			{
				return this._islandSubgraphCount;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002997 File Offset: 0x00000B97
		internal int InflectionPointsCount
		{
			get
			{
				return this._inflectionPointsCount;
			}
		}

		// Token: 0x040000DB RID: 219
		private readonly int _activeRelationshipCount;

		// Token: 0x040000DC RID: 220
		private readonly int _inactiveRelationshipCount;

		// Token: 0x040000DD RID: 221
		private readonly int _islandSubgraphCount;

		// Token: 0x040000DE RID: 222
		private readonly int _inflectionPointsCount;
	}
}
