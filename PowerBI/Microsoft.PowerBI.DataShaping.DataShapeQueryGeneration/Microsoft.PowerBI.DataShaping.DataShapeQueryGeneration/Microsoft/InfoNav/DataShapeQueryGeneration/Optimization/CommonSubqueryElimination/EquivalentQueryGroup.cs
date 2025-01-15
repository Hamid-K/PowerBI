using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization.CommonSubqueryElimination
{
	// Token: 0x02000100 RID: 256
	internal sealed class EquivalentQueryGroup
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x00021D13 File Offset: 0x0001FF13
		public EquivalentQueryGroup(ResolvedQueryDefinition exemplar)
		{
			this.Exemplar = exemplar;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00021D22 File Offset: 0x0001FF22
		public EquivalentQueryGroup(ResolvedQueryDefinition exemplar, ResolvedQueryDefinition equivalentQuery)
		{
			this.Exemplar = exemplar;
			this.AddEquivalentQuery(equivalentQuery);
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00021D38 File Offset: 0x0001FF38
		public ResolvedQueryDefinition Exemplar { get; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00021D40 File Offset: 0x0001FF40
		public IReadOnlyList<ResolvedQueryDefinition> EquivalentQueries
		{
			get
			{
				return this._equivalentQueries;
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00021D48 File Offset: 0x0001FF48
		public void AddEquivalentQuery(ResolvedQueryDefinition otherQuery)
		{
			if (this._equivalentQueries == null)
			{
				this._equivalentQueries = new List<ResolvedQueryDefinition>();
			}
			this._equivalentQueries.Add(otherQuery);
		}

		// Token: 0x04000459 RID: 1113
		private List<ResolvedQueryDefinition> _equivalentQueries;
	}
}
