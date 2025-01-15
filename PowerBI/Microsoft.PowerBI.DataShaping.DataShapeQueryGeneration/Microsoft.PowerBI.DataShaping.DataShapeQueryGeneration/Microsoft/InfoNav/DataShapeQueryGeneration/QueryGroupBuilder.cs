using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000A7 RID: 167
	internal sealed class QueryGroupBuilder
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x000178DC File Offset: 0x00015ADC
		internal QueryGroupBuilder(DsqExpressionGenerator expressionGenerator, DataShapeGenerationErrorContext errorContext, SubtotalType subtotal, QueryGroupBuilderOptions options, bool isContextOnly)
		{
			this._errorContext = errorContext;
			this._expressionGenerator = expressionGenerator;
			this._subtotal = subtotal;
			this._keys = new List<QueryGroupKeyBuilder>();
			this._sortKeys = new List<QueryGroupSortKey>();
			this._valueBuilders = new List<QueryGroupValueBuilder>();
			this._isNonAggregatable = false;
			this._options = options;
			this._isSubtotalContextOnly = isContextOnly;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0001793C File Offset: 0x00015B3C
		internal IReadOnlyList<QueryGroupKeyBuilder> Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00017944 File Offset: 0x00015B44
		internal IReadOnlyList<QueryGroupValueBuilder> ValueBuilders
		{
			get
			{
				return this._valueBuilders;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001794C File Offset: 0x00015B4C
		internal SubtotalType Subtotal
		{
			get
			{
				return this._subtotal;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00017954 File Offset: 0x00015B54
		internal bool IsNonAggregatable
		{
			get
			{
				return this._isNonAggregatable;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001795C File Offset: 0x00015B5C
		internal bool HasSubtotal
		{
			get
			{
				return this._subtotal > SubtotalType.None;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00017967 File Offset: 0x00015B67
		internal bool HasDetailIdentity
		{
			get
			{
				return this._detailGroupIdentity != null;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00017972 File Offset: 0x00015B72
		internal void AddCalculatedGroupProjection<TColumn>(int selectIndex, string nativeReferenceName, ICalculatedGroupProjection calculatedGroupProjection, TColumn identityColumn, QueryColumnAdapter<TColumn> adapter, bool showItemsWithNoData)
		{
			this.AddIntervalProjection<TColumn>(selectIndex, nativeReferenceName, calculatedGroupProjection as IntervalGroupProjection, identityColumn, adapter, showItemsWithNoData);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00017988 File Offset: 0x00015B88
		private void AddIntervalProjection<TColumn>(int selectIndex, string nativeReferenceName, IntervalGroupProjection interval, TColumn identityColumn, QueryColumnAdapter<TColumn> adapter, bool showItemsWithNoData)
		{
			if (!this._options.SuppressAutomaticGroupSorts)
			{
				this.AddToSortKeys<TColumn>(interval.MinColumn, identityColumn, adapter, false, new int?(selectIndex));
				this.AddToSortKeys<TColumn>(interval.MaxColumn, identityColumn, adapter, false, new int?(selectIndex));
			}
			this.AddKey<TColumn>(interval.MinColumn, showItemsWithNoData, false, identityColumn, adapter, new int?(selectIndex), false);
			this.AddKey<TColumn>(interval.MaxColumn, showItemsWithNoData, false, identityColumn, adapter, new int?(selectIndex), false);
			this.AddToIntervalValues<TColumn>(selectIndex, nativeReferenceName, interval, identityColumn, adapter);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00017A14 File Offset: 0x00015C14
		internal void AddProjectedColumn<TColumn>(int selectIndex, string nativeReferenceName, TColumn column, QueryColumnAdapter<TColumn> adapter, bool showItemsWithNoData)
		{
			IConceptualColumn conceptualColumn = adapter.GetConceptualColumn(column);
			IReadOnlyList<IConceptualColumn> readOnlyList = ((conceptualColumn != null) ? conceptualColumn.Grouping.IdentityColumns : null);
			bool flag = this._options.ProjectIdentityOnly && (conceptualColumn == null || !conceptualColumn.IsSelfIdentity());
			if (!this._options.SuppressModelGrouping)
			{
				this.AddToIdentityValues<TColumn>(new int?(selectIndex), column, adapter, readOnlyList, flag);
			}
			if (this._options.TrackGroupKeysAndSortKeysForReferencing && !this._options.OmitModelOrderBy)
			{
				IReadOnlyList<IConceptualColumn> readOnlyList2 = ((conceptualColumn != null) ? conceptualColumn.OrderByColumns : null);
				this.AddToOrderByGroupingValues<TColumn>(column, adapter, readOnlyList2, flag);
			}
			if (!flag)
			{
				this.AddToProjectedValues<TColumn>(selectIndex, nativeReferenceName, column, adapter);
			}
			this.AddToSortKeys<TColumn>(selectIndex, column, adapter, flag);
			IReadOnlyList<IConceptualColumn> readOnlyList3 = this.DetermineKeyColumns(conceptualColumn);
			this.AddToKeys<TColumn>(readOnlyList3, showItemsWithNoData, readOnlyList, column, adapter, new int?(selectIndex), flag);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00017AE4 File Offset: 0x00015CE4
		private IReadOnlyList<IConceptualColumn> DetermineKeyColumns(IConceptualColumn conceptualColumn)
		{
			if (conceptualColumn == null)
			{
				return null;
			}
			if (this._options.SuppressModelGrouping)
			{
				return new List<IConceptualColumn> { conceptualColumn };
			}
			if (this._options.OmitModelOrderBy)
			{
				return conceptualColumn.Grouping.IdentityColumns;
			}
			return conceptualColumn.Grouping.QueryGroupColumns;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00017B34 File Offset: 0x00015D34
		internal bool TryAddGroupBy(ResolvedQueryExpression expr, int groupByIndex)
		{
			ResolvedQuerySourceRefExpression resolvedQuerySourceRefExpression = expr as ResolvedQuerySourceRefExpression;
			if (!(resolvedQuerySourceRefExpression != null))
			{
				return false;
			}
			IConceptualEntity sourceEntity = resolvedQuerySourceRefExpression.SourceEntity;
			if (this._detailGroupIdentity == null)
			{
				this._detailGroupIdentity = new QueryDetailGroupIdentity(sourceEntity, sourceEntity.DsqExpression(), groupByIndex);
				if (!this._options.SuppressModelGrouping && sourceEntity.KeyColumns != null && sourceEntity.KeyColumns.Count > 0)
				{
					this.AddToIdentityValues<IConceptualColumn>(null, null, QueryConceptualColumnAdapter.Instance, sourceEntity.KeyColumns, false);
					this.AddToKeys(sourceEntity.KeyColumns, false, sourceEntity.KeyColumns, null, false);
					this.AddToSortKeys(sourceEntity.KeyColumns, false);
				}
				return true;
			}
			this._errorContext.Register(DataShapeGenerationMessages.UnsupportedMultipleGroupByEntitiesInSemanticQuery(EngineMessageSeverity.Error, groupByIndex));
			return false;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00017BF8 File Offset: 0x00015DF8
		internal bool TryAddEntitySet(IConceptualEntity entity, int selectIndex, string nativeReferenceName, bool showItemsWithNoData)
		{
			if (entity.KeyColumns != null && entity.KeyColumns.Count > 0)
			{
				this.AddToIdentityValues<IConceptualColumn>(new int?(selectIndex), null, QueryConceptualColumnAdapter.Instance, entity.KeyColumns, false);
				this.AddToKeys(entity.KeyColumns, showItemsWithNoData, entity.KeyColumns, new int?(selectIndex), false);
				if (entity.DefaultLabelColumn != null)
				{
					this.AddFieldToGroup(selectIndex, nativeReferenceName, entity.DefaultLabelColumn);
				}
				else
				{
					foreach (IConceptualColumn conceptualColumn in entity.KeyColumns)
					{
						this.AddFieldToGroup(selectIndex, nativeReferenceName, conceptualColumn);
					}
				}
				this.AddToSortKeys(entity.KeyColumns, false);
				return true;
			}
			IConceptualColumn conceptualColumn2 = entity.Properties.FirstOrDefault((IConceptualProperty f) => !f.IsHidden && !f.EdmName.ToUpperInvariant().Contains("ID")) as IConceptualColumn;
			if (conceptualColumn2 != null)
			{
				this._errorContext.Register(DataShapeGenerationMessages.NoStableKeyForSelectedEntity(EngineMessageSeverity.Warning, conceptualColumn2.EdmName, entity.EdmName));
				this.AddToIdentityValues<IConceptualColumn>(new int?(selectIndex), null, QueryConceptualColumnAdapter.Instance, conceptualColumn2.Grouping.IdentityColumns, false);
				this.AddFieldToGroup(selectIndex, nativeReferenceName, conceptualColumn2);
				this.AddToKeys(conceptualColumn2.Grouping.QueryGroupColumns, showItemsWithNoData, conceptualColumn2.Grouping.IdentityColumns, new int?(selectIndex), false);
				return true;
			}
			this._errorContext.Register(DataShapeGenerationMessages.NoStableKeyAndNoVisibleFieldsForSelectedEntity(EngineMessageSeverity.Error, entity.EdmName));
			return false;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00017D7C File Offset: 0x00015F7C
		private void AddToSortKeys<TColumn>(int selectIndex, TColumn column, QueryColumnAdapter<TColumn> adapter, bool shouldProjectIdentityOnly)
		{
			IConceptualColumn conceptualColumn = adapter.GetConceptualColumn(column);
			if (this._options.SuppressAutomaticGroupSorts)
			{
				return;
			}
			if (!this._options.OmitModelOrderBy)
			{
				this.AddToSortKeys<TColumn>((conceptualColumn != null) ? conceptualColumn.OrderByColumns : null, column, adapter, false, new int?(selectIndex));
			}
			if (shouldProjectIdentityOnly)
			{
				this.AddToSortKeys<TColumn>((conceptualColumn != null) ? conceptualColumn.Grouping.IdentityColumns : null, column, adapter, true, null);
				return;
			}
			this.AddToSortKeys(adapter.ToDsqExpression(column), new int?(selectIndex));
			IReadOnlyList<IConceptualColumn> readOnlyList = this.DetermineKeyColumns(conceptualColumn);
			this.AddToSortKeys<TColumn>(readOnlyList, column, adapter, false, null);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00017E20 File Offset: 0x00016020
		private void AddFieldToGroup(int selectIndex, string nativeReferenceName, IConceptualColumn fieldInstance)
		{
			QueryConceptualColumnAdapter instance = QueryConceptualColumnAdapter.Instance;
			instance.GetConceptualColumn(fieldInstance);
			this.AddToProjectedValues<IConceptualColumn>(selectIndex, nativeReferenceName, fieldInstance, instance);
			this.AddToSortKeys<IConceptualColumn>(selectIndex, fieldInstance, QueryConceptualColumnAdapter.Instance, false);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00017E54 File Offset: 0x00016054
		private void AddToProjectedValues<TColumn>(int selectIndex, string nativeReferenceName, TColumn column, QueryColumnAdapter<TColumn> adapter)
		{
			ExpressionNode expressionNode = adapter.ToDsqExpression(column);
			string formatString = adapter.GetFormatString(column);
			IConceptualColumn conceptualColumn = adapter.GetConceptualColumn(column);
			ProjectedDsqExpressionValue value = new ProjectedDsqExpressionValue(expressionNode, formatString, conceptualColumn);
			QueryGroupValueBuilder queryGroupValueBuilder = this._valueBuilders.FirstOrDefault((QueryGroupValueBuilder b) => b.MatchesSingleExpression(value.DsqExpression));
			int? num = null;
			if (selectIndex != -1)
			{
				num = new int?(selectIndex);
			}
			bool? flag = null;
			if (conceptualColumn != null)
			{
				flag = new bool?(conceptualColumn.ConceptualDataType.IsScalar());
			}
			ProjectedDsqExpression projectedDsqExpression = new ProjectedDsqExpression(num, value, false, flag, nativeReferenceName, false);
			if (queryGroupValueBuilder == null)
			{
				this._valueBuilders.Add(new QueryGroupValueBuilder(projectedDsqExpression, adapter.GetConceptualColumn(column), true, false, false));
				return;
			}
			if (!queryGroupValueBuilder.TryPromoteToProjected(projectedDsqExpression))
			{
				queryGroupValueBuilder.AddAdditionalSemanticQuerySelectIndex(selectIndex);
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00017F24 File Offset: 0x00016124
		private void AddToIdentityValues<TColumn>(int? correspondingSelectIndex, TColumn existingColumn, QueryColumnAdapter<TColumn> adapter, IReadOnlyList<IConceptualColumn> identityColumns, bool propagateRoleAndOmitFromOutput)
		{
			if (identityColumns.IsNullOrEmpty<IConceptualColumn>())
			{
				this.AddIdentityValue<TColumn>(correspondingSelectIndex, adapter, existingColumn);
				return;
			}
			foreach (IConceptualColumn conceptualColumn in identityColumns)
			{
				this.AddIdentityValue<TColumn>(correspondingSelectIndex, existingColumn, adapter, propagateRoleAndOmitFromOutput, conceptualColumn);
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00017F88 File Offset: 0x00016188
		private void AddToOrderByGroupingValues<TColumn>(TColumn existingColumn, QueryColumnAdapter<TColumn> adapter, IReadOnlyList<IConceptualColumn> orderByKeyColumns, bool propagateRoleAndOmitFromOutput)
		{
			if (orderByKeyColumns.IsNullOrEmpty<IConceptualColumn>())
			{
				return;
			}
			foreach (IConceptualColumn conceptualColumn in orderByKeyColumns)
			{
				this.AddOrderByKeyValue<TColumn>(existingColumn, adapter, propagateRoleAndOmitFromOutput, conceptualColumn);
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00017FE0 File Offset: 0x000161E0
		private void AddToIntervalValues<TColumn>(int selectIndex, string nativeReferenceName, IntervalGroupProjection interval, TColumn identityColumn, QueryColumnAdapter<TColumn> adapter)
		{
			ProjectedDsqExpression projectedDsqExpression = this.CreateIntervalPart<TColumn>(selectIndex, nativeReferenceName, interval.MinColumn, identityColumn, adapter);
			ProjectedDsqExpression projectedDsqExpression2 = this.CreateIntervalPart<TColumn>(selectIndex, nativeReferenceName, interval.MaxColumn, identityColumn, adapter);
			QueryGroupValueBuilder queryGroupValueBuilder = new QueryGroupValueBuilder(projectedDsqExpression, projectedDsqExpression2, adapter.GetConceptualColumn(identityColumn));
			this._valueBuilders.Add(queryGroupValueBuilder);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00018030 File Offset: 0x00016230
		private ProjectedDsqExpression CreateIntervalPart<TColumn>(int selectIndex, string nativeReferenceName, IConceptualColumn column, TColumn identityColumn, QueryColumnAdapter<TColumn> adapter)
		{
			TColumn orCreateColumn = adapter.GetOrCreateColumn(column, identityColumn, this._expressionGenerator, false);
			ExpressionNode expressionNode = adapter.ToDsqExpression(orCreateColumn);
			return new ProjectedDsqExpression(new int?(selectIndex), new ProjectedDsqExpressionValue(expressionNode, null, null), false, new bool?(column.ConceptualDataType.IsScalar()), nativeReferenceName, false);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00018080 File Offset: 0x00016280
		private void AddIdentityValue<TColumn>(int? correspondingSelectIndex, TColumn existingColumn, QueryColumnAdapter<TColumn> adapter, bool propagateRoleAndOmitFromOutput, IConceptualColumn identityColumn)
		{
			TColumn orCreateColumn = adapter.GetOrCreateColumn(identityColumn, existingColumn, this._expressionGenerator, propagateRoleAndOmitFromOutput);
			this.AddIdentityValue<TColumn>(correspondingSelectIndex, adapter, orCreateColumn);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000180A8 File Offset: 0x000162A8
		private void AddIdentityValue<TColumn>(int? correspondingSelectIndex, QueryColumnAdapter<TColumn> adapter, TColumn identityColumn)
		{
			ExpressionNode expr = adapter.ToDsqExpression(identityColumn);
			QueryGroupValueBuilder queryGroupValueBuilder = this._valueBuilders.FirstOrDefault((QueryGroupValueBuilder b) => b.MatchesSingleExpression(expr));
			if (queryGroupValueBuilder == null)
			{
				IConceptualColumn conceptualColumn = adapter.GetConceptualColumn(identityColumn);
				queryGroupValueBuilder = new QueryGroupValueBuilder(new ProjectedDsqExpression(null, new ProjectedDsqExpressionValue(expr, (conceptualColumn != null) ? conceptualColumn.FormatString : null, conceptualColumn), false, (conceptualColumn != null) ? new bool?(conceptualColumn.ConceptualDataType.IsScalar()) : null, null, false), conceptualColumn, false, true, false);
				this._valueBuilders.Add(queryGroupValueBuilder);
			}
			else
			{
				queryGroupValueBuilder.SetIsIdentity();
			}
			if (correspondingSelectIndex != null)
			{
				queryGroupValueBuilder.AddSelectIndexWithThisIdentity(correspondingSelectIndex.Value);
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00018168 File Offset: 0x00016368
		private void AddOrderByKeyValue<TColumn>(TColumn existingColumn, QueryColumnAdapter<TColumn> adapter, bool propagateRoleAndOmitFromOutput, IConceptualColumn orderByColumn)
		{
			TColumn orCreateColumn = adapter.GetOrCreateColumn(orderByColumn, existingColumn, this._expressionGenerator, propagateRoleAndOmitFromOutput);
			ExpressionNode expr = adapter.ToDsqExpression(orCreateColumn);
			QueryGroupValueBuilder queryGroupValueBuilder = this._valueBuilders.FirstOrDefault((QueryGroupValueBuilder b) => b.MatchesSingleExpression(expr));
			if (queryGroupValueBuilder == null)
			{
				IConceptualColumn conceptualColumn = adapter.GetConceptualColumn(orCreateColumn);
				queryGroupValueBuilder = new QueryGroupValueBuilder(new ProjectedDsqExpression(null, new ProjectedDsqExpressionValue(expr, (conceptualColumn != null) ? conceptualColumn.FormatString : null, conceptualColumn), false, (conceptualColumn != null) ? new bool?(conceptualColumn.ConceptualDataType.IsScalar()) : null, null, false), conceptualColumn, false, false, true);
				this._valueBuilders.Add(queryGroupValueBuilder);
				return;
			}
			queryGroupValueBuilder.SetIsOrderByKey();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00018222 File Offset: 0x00016422
		private void AddToKeys(IReadOnlyList<IConceptualColumn> columns, bool showItemsWithNoData, IReadOnlyList<IConceptualProperty> identityFields, int? correspondingProjectIndex, bool hasOmittedValue)
		{
			this.AddToKeys<IConceptualColumn>(columns, showItemsWithNoData, identityFields, null, QueryConceptualColumnAdapter.Instance, correspondingProjectIndex, hasOmittedValue);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00018238 File Offset: 0x00016438
		private void AddToKeys<TColumn>(IReadOnlyList<IConceptualColumn> newColumns, bool showItemsWithNoData, IReadOnlyList<IConceptualProperty> identityFields, TColumn existingColumn, QueryColumnAdapter<TColumn> adapter, int? correspondingProjectIndex, bool shouldPropagateRoleForIdentityOnly)
		{
			if (newColumns.IsNullOrEmpty<IConceptualColumn>())
			{
				this.AddKey<TColumn>(existingColumn, showItemsWithNoData, true, adapter, correspondingProjectIndex);
				return;
			}
			foreach (IConceptualColumn conceptualColumn in newColumns)
			{
				bool flag = identityFields.Contains(conceptualColumn);
				bool flag2 = shouldPropagateRoleForIdentityOnly && flag;
				this.AddKey<TColumn>(conceptualColumn, showItemsWithNoData, flag, existingColumn, adapter, correspondingProjectIndex, flag2);
				if (conceptualColumn != null && conceptualColumn.AggregateBehavior == AggregateBehavior.DiscourageAcrossGroups)
				{
					this._isNonAggregatable = true;
				}
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000182C0 File Offset: 0x000164C0
		private void AddKey<TColumn>(IConceptualColumn newConceptualColumn, bool showItemsWithNoData, bool isIdentityKey, TColumn existingColumn, QueryColumnAdapter<TColumn> adapter, int? correspondingSelectIndex, bool propagateRoleAndOmitFromOutput)
		{
			TColumn orCreateColumn = adapter.GetOrCreateColumn(newConceptualColumn, existingColumn, this._expressionGenerator, propagateRoleAndOmitFromOutput);
			this.AddKey<TColumn>(orCreateColumn, showItemsWithNoData, isIdentityKey, adapter, correspondingSelectIndex);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000182F0 File Offset: 0x000164F0
		private void AddKey<TColumn>(TColumn column, bool showItemsWithNoData, bool isIdentityKey, QueryColumnAdapter<TColumn> adapter, int? correspondingSelectIndex)
		{
			ExpressionNode columnExpr = adapter.ToDsqExpression(column);
			QueryGroupKeyBuilder queryGroupKeyBuilder = this._keys.FirstOrDefault((QueryGroupKeyBuilder b) => b.Expression.Equals(columnExpr));
			if (queryGroupKeyBuilder == null)
			{
				queryGroupKeyBuilder = new QueryGroupKeyBuilder(columnExpr, adapter.GetConceptualColumn(column), correspondingSelectIndex);
				this._keys.Add(queryGroupKeyBuilder);
			}
			queryGroupKeyBuilder.ShowItemsWithNoData = showItemsWithNoData;
			queryGroupKeyBuilder.IsIdentityKey = isIdentityKey;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001836C File Offset: 0x0001656C
		private void AddToSortKeys(IReadOnlyList<IConceptualColumn> fields, bool propagateRoleAndOmitFromOutput)
		{
			this.AddToSortKeys<IConceptualColumn>(fields, null, QueryConceptualColumnAdapter.Instance, propagateRoleAndOmitFromOutput, null);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00018390 File Offset: 0x00016590
		private void AddToSortKeys<TColumn>(IEnumerable<IConceptualColumn> conceptualColumns, TColumn existingColumn, QueryColumnAdapter<TColumn> adapter, bool propagateRoleAndOmitFromOutput, int? modelSortSourceSelectId = null)
		{
			if (conceptualColumns == null)
			{
				this.AddToSortKeys<TColumn>(existingColumn, adapter, modelSortSourceSelectId);
				return;
			}
			foreach (IConceptualColumn conceptualColumn in conceptualColumns)
			{
				this.AddToSortKeys<TColumn>(conceptualColumn, existingColumn, adapter, propagateRoleAndOmitFromOutput, modelSortSourceSelectId);
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000183EC File Offset: 0x000165EC
		private void AddToSortKeys<TColumn>(IConceptualColumn conceptualColumn, TColumn existingColumn, QueryColumnAdapter<TColumn> adapter, bool propagateRoleAndOmitFromOutput, int? modelSortSourceSelectId)
		{
			TColumn orCreateColumn = adapter.GetOrCreateColumn(conceptualColumn, existingColumn, this._expressionGenerator, propagateRoleAndOmitFromOutput);
			this.AddToSortKeys<TColumn>(orCreateColumn, adapter, modelSortSourceSelectId);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00018414 File Offset: 0x00016614
		private void AddToSortKeys<TColumn>(TColumn column, QueryColumnAdapter<TColumn> adapter, int? modelSortSourceSelectId)
		{
			ExpressionNode expressionNode = adapter.ToDsqExpression(column);
			this.AddToSortKeys(expressionNode, null);
			if (modelSortSourceSelectId != null)
			{
				this.AddToSelectModelSorts(modelSortSourceSelectId.Value, expressionNode, adapter.GetConceptualColumn(column));
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00018458 File Offset: 0x00016658
		private void AddToSelectModelSorts(int modelSortSourceSelectId, ExpressionNode columnExpr, IConceptualColumn field)
		{
			if (this._options.TrackGroupKeysAndSortKeysForReferencing)
			{
				if (this._modelSortToSourceSelects == null)
				{
					this._modelSortToSourceSelects = new Dictionary<ExpressionNode, global::System.ValueTuple<IList<int>, IConceptualColumn>>();
					this._modelSortToSourceSelects.Add(columnExpr, new global::System.ValueTuple<IList<int>, IConceptualColumn>(new List<int> { modelSortSourceSelectId }, field));
					return;
				}
				global::System.ValueTuple<IList<int>, IConceptualColumn> valueTuple;
				if (this._modelSortToSourceSelects.TryGetValue(columnExpr, out valueTuple))
				{
					valueTuple.Item1.Add(modelSortSourceSelectId);
					return;
				}
				this._modelSortToSourceSelects.Add(columnExpr, new global::System.ValueTuple<IList<int>, IConceptualColumn>(new List<int> { modelSortSourceSelectId }, field));
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000184E0 File Offset: 0x000166E0
		private void AddToSortKeys(ExpressionNode expression, int? selectIndex)
		{
			QueryGroupSortKey queryGroupSortKey = this._sortKeys.Find((QueryGroupSortKey sk) => sk.Expression.Equals(expression));
			if (queryGroupSortKey == null)
			{
				this._sortKeys.Add(new QueryGroupSortKey(expression, selectIndex));
				return;
			}
			queryGroupSortKey.UpdateSelectIndex(selectIndex);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00018534 File Offset: 0x00016734
		internal void SuppressSubtotals()
		{
			this._subtotal = SubtotalType.None;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001853D File Offset: 0x0001673D
		internal void SuppressSortByMeasureRollup()
		{
			this._suppressSortByMeasureRollup = true;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00018546 File Offset: 0x00016746
		internal void SetIsSubtotalContextOnly()
		{
			this._isSubtotalContextOnly = true;
			this._subtotal = SubtotalType.After;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00018558 File Offset: 0x00016758
		internal bool TryAddGroupKeyExpressions(HashSet<ExpressionNode> uniqueExpressions)
		{
			bool flag = false;
			foreach (QueryGroupKeyBuilder queryGroupKeyBuilder in this._keys)
			{
				flag |= uniqueExpressions.Add(queryGroupKeyBuilder.Expression);
			}
			return flag;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000185B8 File Offset: 0x000167B8
		internal void AddNonGroupKeyExpressions(HashSet<ExpressionNode> uniqueExpressions)
		{
			foreach (QueryGroupSortKey queryGroupSortKey in this._sortKeys)
			{
				uniqueExpressions.Add(queryGroupSortKey.Expression);
			}
			foreach (QueryGroupValueBuilder queryGroupValueBuilder in this._valueBuilders)
			{
				uniqueExpressions.Add(queryGroupValueBuilder.GetDsqExpressionNode());
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001865C File Offset: 0x0001685C
		internal QueryGroup ToGroup(bool allowCustomMeasureSorts, QuerySortGenerator sortGenerator, out List<QueryGroupValue> values)
		{
			values = this._valueBuilders.Select((QueryGroupValueBuilder b) => b.ToGroupValue()).ToList<QueryGroupValue>();
			List<QueryGroupKey> list = this._keys.Select((QueryGroupKeyBuilder b) => b.ToKey()).ToList<QueryGroupKey>();
			bool flag = allowCustomMeasureSorts && !this._suppressSortByMeasureRollup;
			IReadOnlyList<DsqSortKey> readOnlyList = sortGenerator.DetermineGroupSortKeys(list, values, this._sortKeys, flag, this._options.SuppressAutomaticGroupSorts);
			QueryGroupBindingHints queryGroupBindingHints = this.CreateBindingHints(readOnlyList);
			return new QueryGroup(list, readOnlyList, this._detailGroupIdentity, this._subtotal, queryGroupBindingHints, this._suppressSortByMeasureRollup, this._isSubtotalContextOnly);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00018720 File Offset: 0x00016920
		private QueryGroupBindingHints CreateBindingHints(IReadOnlyList<DsqSortKey> sortKeys)
		{
			if (!this._options.GenerateRestartIdentities && !this._options.TrackGroupKeysAndSortKeysForReferencing)
			{
				return null;
			}
			Dictionary<DsqSortKey, ModelSortBindingInfo> dictionary = new Dictionary<DsqSortKey, ModelSortBindingInfo>();
			HashSet<DsqSortKey> hashSet = new HashSet<DsqSortKey>();
			foreach (DsqSortKey dsqSortKey in sortKeys)
			{
				if (!dsqSortKey.IsMeasure)
				{
					global::System.ValueTuple<IList<int>, IConceptualColumn> valueTuple;
					if (this._modelSortToSourceSelects != null && this._modelSortToSourceSelects.TryGetValue(dsqSortKey.Expression, out valueTuple))
					{
						dictionary.Add(dsqSortKey, new ModelSortBindingInfo(valueTuple.Item2, valueTuple.Item1.AsReadOnlyList<int>()));
					}
					if (this._options.GenerateRestartIdentities)
					{
						hashSet.Add(dsqSortKey);
					}
				}
			}
			return new QueryGroupBindingHints(dictionary, hashSet, this._options.TrackGroupKeysAndSortKeysForReferencing);
		}

		// Token: 0x0400034A RID: 842
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x0400034B RID: 843
		private readonly DsqExpressionGenerator _expressionGenerator;

		// Token: 0x0400034C RID: 844
		private readonly List<QueryGroupKeyBuilder> _keys;

		// Token: 0x0400034D RID: 845
		private readonly List<QueryGroupSortKey> _sortKeys;

		// Token: 0x0400034E RID: 846
		private readonly List<QueryGroupValueBuilder> _valueBuilders;

		// Token: 0x0400034F RID: 847
		private readonly QueryGroupBuilderOptions _options;

		// Token: 0x04000350 RID: 848
		private SubtotalType _subtotal;

		// Token: 0x04000351 RID: 849
		private QueryDetailGroupIdentity _detailGroupIdentity;

		// Token: 0x04000352 RID: 850
		private bool _isNonAggregatable;

		// Token: 0x04000353 RID: 851
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "SourceSelects", "Field" })]
		private Dictionary<ExpressionNode, global::System.ValueTuple<IList<int>, IConceptualColumn>> _modelSortToSourceSelects;

		// Token: 0x04000354 RID: 852
		private bool _suppressSortByMeasureRollup;

		// Token: 0x04000355 RID: 853
		private bool _isSubtotalContextOnly;
	}
}
