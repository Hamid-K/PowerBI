using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200001D RID: 29
	internal static class ConceptualColumnGroupingBuilder
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00004798 File Offset: 0x00002998
		internal static ConceptualColumnGrouping BuildGrouping(ConceptualColumn column, IReadOnlyList<IConceptualColumn> groupByColumns, IReadOnlyList<IConceptualColumn> orderByColumns, bool isDefaultLabel, bool isDefaultImage, IReadOnlyList<IConceptualColumn> entityKeyColumns, bool entityKeyIsStable)
		{
			IReadOnlyList<IConceptualColumn> readOnlyList = ConceptualColumnGroupingBuilder.ExcludeInvalidGroupingColumns(groupByColumns);
			GroupingIdentity groupingIdentity = ConceptualColumnGroupingBuilder.CalculateDefaultIdentity(column, isDefaultLabel, isDefaultImage, entityKeyIsStable);
			IReadOnlyList<IConceptualColumn> readOnlyList2 = ConceptualColumnGroupingBuilder.CalculateIdentityColumns(column, groupingIdentity, readOnlyList, entityKeyColumns);
			GroupingIdentity groupingIdentity2 = ConceptualColumnGroupingBuilder.CalculateIdentity(column, groupingIdentity, readOnlyList2, entityKeyColumns);
			IReadOnlyList<IConceptualColumn> readOnlyList3;
			bool flag;
			if (groupingIdentity2 == GroupingIdentity.EntityKey)
			{
				readOnlyList2 = entityKeyColumns;
				readOnlyList3 = readOnlyList2;
				if (!entityKeyIsStable)
				{
					readOnlyList3 = Util.EmptyReadOnlyCollection<IConceptualColumn>();
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				readOnlyList3 = ConceptualColumnGroupingBuilder.CalculateQueryGroupColumns(column, readOnlyList2, orderByColumns);
				flag = entityKeyColumns.Count > 0 && readOnlyList3.IsSupersetOf(entityKeyColumns);
			}
			return new ConceptualColumnGrouping(groupingIdentity2, readOnlyList2, readOnlyList3, groupByColumns, flag);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004814 File Offset: 0x00002A14
		private static IReadOnlyList<IConceptualColumn> ExcludeInvalidGroupingColumns(IReadOnlyList<IConceptualColumn> groupByColumns)
		{
			if (groupByColumns.Count == 0)
			{
				return Util.EmptyReadOnlyCollection<IConceptualColumn>();
			}
			if (groupByColumns.Any((IConceptualColumn c) => !ConceptualColumnGroupingBuilder.CanGroupOnValue(c)))
			{
				return Util.EmptyReadOnlyCollection<IConceptualColumn>();
			}
			return groupByColumns.Distinct<IConceptualColumn>().ToList<IConceptualColumn>();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004867 File Offset: 0x00002A67
		private static GroupingIdentity CalculateDefaultIdentity(ConceptualColumn column, bool isDefaultLabel, bool isDefaultImage, bool entityKeyIsStable)
		{
			if (!ConceptualColumnGroupingBuilder.CanGroupOnValue(column))
			{
				return GroupingIdentity.EntityKey;
			}
			if (!entityKeyIsStable)
			{
				return GroupingIdentity.Value;
			}
			if (entityKeyIsStable && (isDefaultLabel || isDefaultImage))
			{
				return GroupingIdentity.EntityKey;
			}
			if (column.KeepUniqueRows)
			{
				return GroupingIdentity.EntityKey;
			}
			return GroupingIdentity.Value;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000488D File Offset: 0x00002A8D
		private static IReadOnlyList<IConceptualColumn> CalculateIdentityColumns(ConceptualColumn column, GroupingIdentity defaultIdentity, IReadOnlyList<IConceptualColumn> modelGroupBy, IReadOnlyList<IConceptualColumn> entityKey)
		{
			if (defaultIdentity == GroupingIdentity.EntityKey)
			{
				return entityKey;
			}
			if (modelGroupBy != null && modelGroupBy.Count > 0)
			{
				return modelGroupBy;
			}
			return Array.AsReadOnly<ConceptualColumn>(new ConceptualColumn[] { column });
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000048B2 File Offset: 0x00002AB2
		private static GroupingIdentity CalculateIdentity(ConceptualColumn column, GroupingIdentity defaultIdentity, IReadOnlyList<IConceptualColumn> identityColumns, IReadOnlyList<IConceptualColumn> entityKey)
		{
			if (ConceptualColumnGroupingBuilder.SetsEqual<IConceptualColumn>(identityColumns, entityKey))
			{
				return GroupingIdentity.EntityKey;
			}
			if (identityColumns.Count != 1 || identityColumns[0] != column)
			{
				return GroupingIdentity.Properties;
			}
			return defaultIdentity;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000048D8 File Offset: 0x00002AD8
		private static IReadOnlyList<IConceptualColumn> CalculateQueryGroupColumns(ConceptualColumn column, IReadOnlyList<IConceptualColumn> identityColumns, IReadOnlyList<IConceptualColumn> modelOrderBy)
		{
			IReadOnlyList<IConceptualColumn> readOnlyList = identityColumns;
			IEnumerable<IConceptualColumn> enumerable = null;
			if (modelOrderBy.Count > 0)
			{
				enumerable = modelOrderBy;
				if (identityColumns.Concat(column).IsSupersetOf(enumerable))
				{
					enumerable = null;
				}
			}
			if (enumerable != null)
			{
				readOnlyList = readOnlyList.Union(enumerable).ToList<IConceptualColumn>();
			}
			return readOnlyList;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004916 File Offset: 0x00002B16
		private static bool CanGroupOnValue(IConceptualColumn column)
		{
			return column.ConceptualDataCategory != ConceptualDataCategory.Image && column.ConceptualDataType != ConceptualPrimitiveType.Binary;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004930 File Offset: 0x00002B30
		private static bool SetsEqual<T>(IReadOnlyList<T> x, IReadOnlyList<T> y)
		{
			if (x.Count != y.Count)
			{
				return false;
			}
			if (x.Count == 0 && y.Count == 0)
			{
				return true;
			}
			if (x.Count == 1 && y.Count == 1)
			{
				T t = x[0];
				return t.Equals(y[0]);
			}
			return new HashSet<T>(x).SetEquals(y);
		}
	}
}
