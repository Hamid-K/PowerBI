using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.Reporting.Common.Internal;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000FF RID: 255
	internal sealed class QdmFilterPredicate
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x00027953 File Offset: 0x00025B53
		public QdmFilterPredicate(QueryExpression predicateExpression, ReadOnlyOrderedHashSet<IEdmFieldInstance> filterIdentityTargetFieldList, ReadOnlyOrderedHashSet<IConceptualColumn> filterIdentityTargetColumnList = null)
		{
			this.PredicateExpression = predicateExpression;
			this.FilterIdentityTargetFieldList = filterIdentityTargetFieldList;
			this.FilterIdentityTargetColumnList = filterIdentityTargetColumnList;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00027970 File Offset: 0x00025B70
		public static QdmFilterPredicate Create(FilterCondition condition, IDictionary<ReadOnlyOrderedHashSet<IEdmFieldInstance>, IList<EntitySet>> groupingFieldSetsToEntitiesMap, IDictionary<ReadOnlyOrderedHashSet<IConceptualColumn>, IList<IConceptualEntity>> groupingColumnSetsToEntitiesMap, bool useConceptualSchema)
		{
			QueryExpression queryExpression = condition.ToPredicate();
			if (useConceptualSchema)
			{
				IReadOnlyList<IConceptualColumn> filterIdentityTargetColumnList = QdmExpressionBuilder.GetReferencedIdentityColumns(queryExpression).EvaluateReadOnly<IConceptualColumn>();
				ReadOnlyOrderedHashSet<IConceptualColumn> readOnlyOrderedHashSet = groupingColumnSetsToEntitiesMap.Keys.FirstOrDefault((ReadOnlyOrderedHashSet<IConceptualColumn> columnSet) => columnSet.SetEquals(filterIdentityTargetColumnList));
				if (readOnlyOrderedHashSet == null)
				{
					readOnlyOrderedHashSet = ReadOnlyOrderedHashSet<IConceptualColumn>.CopyFrom(filterIdentityTargetColumnList);
					groupingColumnSetsToEntitiesMap[readOnlyOrderedHashSet] = readOnlyOrderedHashSet.Select((IConceptualColumn c) => c.Entity).Distinct<IConceptualEntity>().Evaluate<IConceptualEntity>();
				}
				return new QdmFilterPredicate(queryExpression, null, readOnlyOrderedHashSet);
			}
			IReadOnlyList<IEdmFieldInstance> filterIdentityTargetFieldList = QdmExpressionBuilder.GetReferencedIdentityFields(queryExpression).EvaluateReadOnly<IEdmFieldInstance>();
			ReadOnlyOrderedHashSet<IEdmFieldInstance> readOnlyOrderedHashSet2 = groupingFieldSetsToEntitiesMap.Keys.FirstOrDefault((ReadOnlyOrderedHashSet<IEdmFieldInstance> fieldSet) => fieldSet.SetEquals(filterIdentityTargetFieldList));
			if (readOnlyOrderedHashSet2 == null)
			{
				readOnlyOrderedHashSet2 = ReadOnlyOrderedHashSet<IEdmFieldInstance>.CopyFrom(filterIdentityTargetFieldList);
				groupingFieldSetsToEntitiesMap[readOnlyOrderedHashSet2] = readOnlyOrderedHashSet2.Select((IEdmFieldInstance f) => f.Entity).Distinct<EntitySet>().Evaluate<EntitySet>();
			}
			return new QdmFilterPredicate(queryExpression, readOnlyOrderedHashSet2, null);
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00027A86 File Offset: 0x00025C86
		public QueryExpression PredicateExpression { get; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00027A8E File Offset: 0x00025C8E
		public ReadOnlyOrderedHashSet<IEdmFieldInstance> FilterIdentityTargetFieldList { get; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00027A96 File Offset: 0x00025C96
		public ReadOnlyOrderedHashSet<IConceptualColumn> FilterIdentityTargetColumnList { get; }
	}
}
