using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQuery
{
	// Token: 0x02000009 RID: 9
	internal static class DataShapeQueryUtils
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002474 File Offset: 0x00000674
		internal static T GetValueOrDefault<T>(this Candidate<T> candidate)
		{
			if (!(candidate == null))
			{
				return candidate.Value;
			}
			return default(T);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000249A File Offset: 0x0000069A
		internal static bool IsSpecified<T>(this Candidate<T> candidate)
		{
			return candidate != null && candidate.IsValid;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024AD File Offset: 0x000006AD
		internal static bool RequiresReversedSortDirection(this Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit limit)
		{
			return limit != null && (limit.Operator.ObjectType == ObjectType.LastLimitOperator || limit.Operator.ObjectType == ObjectType.BottomLimitOperator);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024D4 File Offset: 0x000006D4
		internal static SortDirection ReverseSortDirection(this SortDirection sortDirection)
		{
			if (sortDirection == SortDirection.Ascending)
			{
				return SortDirection.Descending;
			}
			if (sortDirection != SortDirection.Descending)
			{
				Microsoft.DataShaping.Contract.RetailFail("Unsupported sort direction value found.");
				throw new InvalidOperationException("Unsupported sort direction value found.");
			}
			return SortDirection.Ascending;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024F8 File Offset: 0x000006F8
		internal static bool SameGroupKeys(DataMember member1, DataMember member2, DataShapeAnnotations annotations, ExpressionTable expressionTable)
		{
			IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey> groupKeys = member1.GetGroupKeys(annotations);
			IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey> groupKeys2 = member2.GetGroupKeys(annotations);
			if (groupKeys == null && groupKeys2 == null)
			{
				return true;
			}
			if (groupKeys == null || groupKeys2 == null || groupKeys.Count != groupKeys2.Count)
			{
				return false;
			}
			for (int i = 0; i < groupKeys.Count; i++)
			{
				ExpressionNode sourceExpressionNode = DataShapeQueryUtils.GetSourceExpressionNode(groupKeys[i].Value, expressionTable);
				ExpressionNode sourceExpressionNode2 = DataShapeQueryUtils.GetSourceExpressionNode(groupKeys2[i].Value, expressionTable);
				if (!sourceExpressionNode.Equals(sourceExpressionNode2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002574 File Offset: 0x00000774
		private static ExpressionNode GetSourceExpressionNode(Expression expression, ExpressionTable expressionTable)
		{
			ExpressionNode node = expressionTable.GetNode(expression);
			DataTransformTableColumn dataTransformTableColumn;
			if (ExpressionAnalysisUtils.TryExtractDataTransformColumnReference(node, out dataTransformTableColumn))
			{
				return DataShapeQueryUtils.GetSourceExpressionNode(dataTransformTableColumn.Value, expressionTable);
			}
			return node;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025A4 File Offset: 0x000007A4
		internal static bool TryExtractEntityFromProperty(this Expression expression, ExpressionTable expressionTable, out IConceptualEntity entity)
		{
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = expressionTable.GetNode(expression) as ResolvedPropertyExpressionNode;
			if (resolvedPropertyExpressionNode == null)
			{
				entity = null;
				return false;
			}
			entity = resolvedPropertyExpressionNode.Property.Entity;
			return true;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025D4 File Offset: 0x000007D4
		public static bool TryCollectEntities(IEnumerable<DataMember> dynamicMembers, ExpressionTable expressionTable, HashSet<IConceptualEntity> groupKeyEntities, out bool hasShowItemsWithNoData)
		{
			hasShowItemsWithNoData = false;
			bool flag = true;
			foreach (DataMember dataMember in dynamicMembers)
			{
				bool flag2;
				bool flag3;
				flag &= DataShapeQueryUtils.TryAddGroupKeyEntities(dataMember.Group.GroupKeys, groupKeyEntities, expressionTable, out flag2, out flag3);
				hasShowItemsWithNoData = hasShowItemsWithNoData || flag2;
			}
			return flag;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000263C File Offset: 0x0000083C
		public static bool TryAddGroupKeyEntities(List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey> groupKeys, HashSet<IConceptualEntity> groupKeyEntities, ExpressionTable expressionTable, out bool hasShowItemsWithNoData, out bool allKeysHaveShowItemsWithNoData)
		{
			hasShowItemsWithNoData = false;
			allKeysHaveShowItemsWithNoData = true;
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey groupKey in groupKeys)
			{
				IConceptualEntity conceptualEntity;
				if (!groupKey.Value.TryExtractEntityFromProperty(expressionTable, out conceptualEntity))
				{
					return false;
				}
				groupKeyEntities.Add(conceptualEntity.GetBaseModelEntity());
				hasShowItemsWithNoData |= groupKey.ShowItemsWithNoData.GetValueOrDefault<bool>();
				allKeysHaveShowItemsWithNoData &= groupKey.ShowItemsWithNoData.GetValueOrDefault<bool>();
			}
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026D0 File Offset: 0x000008D0
		public static bool HasDirectlyRelatedGroupKeys(HashSet<IConceptualEntity> groupKeyEntities, IConceptualSchema schema)
		{
			List<IConceptualEntity> list = groupKeyEntities.ToList<IConceptualEntity>();
			for (int i = 0; i < list.Count - 1; i++)
			{
				for (int j = i + 1; j < list.Count; j++)
				{
					if (QueryAlgorithms.AreDirectlyRelated(new HashSet<IConceptualEntity>(ConceptualEntityExtensionAwareEqualityComparer.Instance)
					{
						list[i],
						list[j]
					}, schema))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000273B File Offset: 0x0000093B
		internal static bool WillCrossFilter(IConceptualEntity first, IConceptualEntity second, IConceptualSchema schema, bool includeDirectManyToManyAssociations)
		{
			return QueryAlgorithms.DetectCrossFilteredEntities(new IConceptualEntity[] { first }, schema, includeDirectManyToManyAssociations).CompleteSet.Contains(second, ConceptualEntityExtensionAwareEqualityComparer.Instance);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000275E File Offset: 0x0000095E
		public static IConceptualEntity GetBaseModelEntity(this IConceptualEntity entity)
		{
			if (entity.Extends != null)
			{
				return ((IExtensionConceptualEntity)entity).ExtendedEntity;
			}
			return entity;
		}

		// Token: 0x02000255 RID: 597
		internal sealed class FilterExpressionCallbackVisitor : FilterExpressionVisitor
		{
			// Token: 0x06001497 RID: 5271 RVA: 0x0004EBCC File Offset: 0x0004CDCC
			internal FilterExpressionCallbackVisitor(Action<Expression> callback, VisitDataShapeDelegate visitDataShape)
				: base(visitDataShape)
			{
				this.m_callback = callback;
			}

			// Token: 0x06001498 RID: 5272 RVA: 0x0004EBDC File Offset: 0x0004CDDC
			public void VisitFilter(Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition)
			{
				this.Visit(filterCondition);
			}

			// Token: 0x06001499 RID: 5273 RVA: 0x0004EBE6 File Offset: 0x0004CDE6
			internal override void VisitExpression(Expression expression, Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition owner, string propertyName)
			{
				this.m_callback(expression);
			}

			// Token: 0x04000930 RID: 2352
			private readonly Action<Expression> m_callback;
		}
	}
}
