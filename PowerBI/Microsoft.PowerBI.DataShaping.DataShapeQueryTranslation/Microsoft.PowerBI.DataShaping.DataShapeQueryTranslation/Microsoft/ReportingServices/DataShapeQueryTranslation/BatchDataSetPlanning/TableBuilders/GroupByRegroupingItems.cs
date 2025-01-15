using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001DA RID: 474
	internal sealed class GroupByRegroupingItems
	{
		// Token: 0x06001072 RID: 4210 RVA: 0x000444B0 File Offset: 0x000426B0
		internal GroupByRegroupingItems(IReadOnlyList<DataMember> dataMembers, IReadOnlyList<Calculation> detailsCalculations, IReadOnlyList<Calculation> aggregateCalculations, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "SortKey", "SuggestedName" })] IReadOnlyList<global::System.ValueTuple<SortKey, string>> aggregateSortKeys, IScope innermostScope, IReadOnlyList<DataTransformTableColumn> dataTransformTableColumns)
		{
			this.DynamicDataMembers = dataMembers;
			this.DetailCalculations = detailsCalculations;
			this.AggregateCalculations = aggregateCalculations;
			this.AggregateSortKeys = aggregateSortKeys;
			this.InnermostScope = innermostScope;
			this.DataTransformTableColumns = dataTransformTableColumns;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x000444E5 File Offset: 0x000426E5
		internal IReadOnlyList<DataMember> DynamicDataMembers { get; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x000444ED File Offset: 0x000426ED
		internal IReadOnlyList<Calculation> DetailCalculations { get; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x000444F5 File Offset: 0x000426F5
		internal IReadOnlyList<Calculation> AggregateCalculations { get; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x000444FD File Offset: 0x000426FD
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "SortKey", "SuggestedName" })]
		internal IReadOnlyList<global::System.ValueTuple<SortKey, string>> AggregateSortKeys
		{
			[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "SortKey", "SuggestedName" })]
			get;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00044505 File Offset: 0x00042705
		internal IScope InnermostScope { get; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0004450D File Offset: 0x0004270D
		internal IReadOnlyList<DataTransformTableColumn> DataTransformTableColumns { get; }
	}
}
