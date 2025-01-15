using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000DC RID: 220
	internal sealed class IntermediateQueryTransformResolver : IIntermediateQueryTransformResolver
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		internal IntermediateQueryTransformResolver()
		{
			this._columnMap = new Dictionary<ResolvedQueryTransformTableColumn, IntermediateQueryTransformTableColumn>(ReferenceEqualityComparer<ResolvedQueryTransformTableColumn>.Instance);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001CBD8 File Offset: 0x0001ADD8
		internal void Clear()
		{
			this._columnMap.Clear();
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001CBE5 File Offset: 0x0001ADE5
		internal void RegisterColumn(ResolvedQueryTransformTableColumn column, IntermediateQueryTransformTableColumn intermediateColumn)
		{
			this._columnMap.Add(column, intermediateColumn);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
		internal void RegisterColumns(IReadOnlyList<ResolvedQueryTransformTableColumn> queryColumns, IReadOnlyList<IntermediateQueryTransformTableColumn> intermediateColumns)
		{
			for (int i = 0; i < queryColumns.Count; i++)
			{
				this.RegisterColumn(queryColumns[i], intermediateColumns[i]);
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001CC26 File Offset: 0x0001AE26
		public bool TryResolveColumn(ResolvedQueryTransformTableColumn column, out IntermediateQueryTransformTableColumn intermediateColumn)
		{
			return this._columnMap.TryGetValue(column, out intermediateColumn);
		}

		// Token: 0x040003FB RID: 1019
		private readonly Dictionary<ResolvedQueryTransformTableColumn, IntermediateQueryTransformTableColumn> _columnMap;

		// Token: 0x040003FC RID: 1020
		internal static readonly IIntermediateQueryTransformResolver Instance = new IntermediateQueryTransformResolver();
	}
}
