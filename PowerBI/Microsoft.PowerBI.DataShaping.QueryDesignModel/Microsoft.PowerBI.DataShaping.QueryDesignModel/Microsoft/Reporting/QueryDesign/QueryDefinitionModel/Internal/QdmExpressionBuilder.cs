using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000FB RID: 251
	internal static class QdmExpressionBuilder
	{
		// Token: 0x06000E7A RID: 3706 RVA: 0x00026B34 File Offset: 0x00024D34
		public static QueryCalculateExpression Calculate(this QueryExpression expression, IConceptualModel model, IConceptualSchema schema, IEnumerable<FilterCondition> filters, DaxCapabilities daxCapabilities, IFeatureSwitchProvider featureSwitchProvider, IDataComparer comparer, CancellationToken cancellationToken)
		{
			return expression.Calculate(filters.QdmFilters(model, schema, daxCapabilities, featureSwitchProvider, comparer, cancellationToken, ScanKind.InheritFilterContextIncludeBlankRow, true));
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00026B59 File Offset: 0x00024D59
		public static QdmEntityPlaceholderExpression QdmReference(this EntitySet entitySet, IConceptualEntity entity)
		{
			return new QdmEntityPlaceholderExpression((entity != null) ? entity.GetExtensionAwareResultType().RowType : entitySet.ElementType.ConceptualType, entitySet, entity);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00026B7D File Offset: 0x00024D7D
		public static QdmEntityPlaceholderExpression QdmReference(this IConceptualEntity entity)
		{
			return new QdmEntityPlaceholderExpression(entity.GetExtensionAwareResultType().RowType, null, entity);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00026B94 File Offset: 0x00024D94
		public static IEnumerable<QueryFieldExpression> QdmKeyReferences(this EntitySet entitySet, IConceptualEntity entity)
		{
			QdmEntityPlaceholderExpression entityRef = entitySet.QdmReference(entity);
			return ((entity != null) ? entity.KeyColumns.Select((IConceptualColumn c) => entityRef.Field(c.ConceptualTypeColumn)) : null) ?? entitySet.ElementType.KeyFields.Select((EdmField f) => entityRef.Field(f, null));
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00026BF4 File Offset: 0x00024DF4
		public static IEnumerable<QueryFieldExpression> QdmKeyReferences(this IConceptualEntity entity)
		{
			QdmEntityPlaceholderExpression entityRef = entity.QdmReference();
			return entity.KeyColumns.Select((IConceptualColumn c) => entityRef.Field(c.ConceptualTypeColumn));
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00026C2A File Offset: 0x00024E2A
		public static QueryFieldExpression QdmReference(this EdmFieldInstance fieldInstance, IConceptualColumn column)
		{
			return fieldInstance.Entity.QdmReference((column != null) ? column.Entity : null).Field(fieldInstance.Field, column);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00026C51 File Offset: 0x00024E51
		public static QueryFieldExpression QdmScalarEntityReference(this EdmFieldInstance fieldInstance, IConceptualColumn column)
		{
			return fieldInstance.Entity.ScalarEntity((column != null) ? column.Entity : null).Field(fieldInstance.Field, column);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00026C78 File Offset: 0x00024E78
		public static QueryFieldExpression QdmReference(this IEdmFieldInstance fieldInstance)
		{
			return fieldInstance.Entity.QdmReference(null).Field(fieldInstance.Field, null);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00026C92 File Offset: 0x00024E92
		public static QueryFieldExpression QdmReference(this IConceptualColumn column)
		{
			return column.Entity.QdmReference().Field(column);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00026CA5 File Offset: 0x00024EA5
		public static QueryExpression QdmAggregateArgument(this IEdmFieldInstance fieldInstance, IConceptualColumn column)
		{
			return fieldInstance.Entity.QdmAggregateArgument(fieldInstance.Field, (column != null) ? column.Entity : null, column);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00026CC5 File Offset: 0x00024EC5
		public static QueryExpression QdmAggregateArgument(this IConceptualColumn column)
		{
			return QdmExpressionBuilder.QdmAggregateArgument((EntitySet)null, null, column.Entity, column);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00026CD5 File Offset: 0x00024ED5
		public static QueryExpression QdmAggregateArgument(this EntitySet entitySet, EdmField field, IConceptualEntity entity, IConceptualColumn column)
		{
			QueryExpressionBinding queryExpressionBinding = ((entity != null) ? entity.Scan(false).BindAs(entity.EdmName) : entitySet.Scan(false).BindAs(entitySet.Name));
			return queryExpressionBinding.Project(queryExpressionBinding.Variable.Field(field, column), ProjectSubsetStrategy.Default);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00026D13 File Offset: 0x00024F13
		public static QueryExpression QdmAggregateArgument(this EntitySet entitySet, string fieldName, IConceptualEntity entity)
		{
			QueryExpressionBinding queryExpressionBinding = ((entity != null) ? entity.Scan(false).BindAs(entity.EdmName) : entitySet.Scan(false).BindAs(entitySet.Name));
			return queryExpressionBinding.Project(queryExpressionBinding.Variable.Field(fieldName), ProjectSubsetStrategy.Default);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00026D50 File Offset: 0x00024F50
		internal static QueryExpression HasAnyRows(this QueryExpression argument, bool suppressCalculate = false)
		{
			QueryExpression queryExpression = argument.IsEmpty().Not();
			if (!suppressCalculate)
			{
				queryExpression = queryExpression.Calculate(Array.Empty<QueryExpression>());
			}
			return queryExpression;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00026D79 File Offset: 0x00024F79
		internal static QueryExpression Max(this QueryExpression left, QueryExpression right)
		{
			Func<QueryExpression, QueryExpression, QueryComparisonExpression> func;
			if ((func = QdmExpressionBuilder.<>O.<0>__GreaterThan) == null)
			{
				func = (QdmExpressionBuilder.<>O.<0>__GreaterThan = new Func<QueryExpression, QueryExpression, QueryComparisonExpression>(QueryExpressionBuilder.GreaterThan));
			}
			return QdmExpressionBuilder.EvaluateVersusThreshold(left, right, func);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00026D9D File Offset: 0x00024F9D
		internal static QueryExpression Min(this QueryExpression left, QueryExpression right)
		{
			Func<QueryExpression, QueryExpression, QueryComparisonExpression> func;
			if ((func = QdmExpressionBuilder.<>O.<1>__LessThan) == null)
			{
				func = (QdmExpressionBuilder.<>O.<1>__LessThan = new Func<QueryExpression, QueryExpression, QueryComparisonExpression>(QueryExpressionBuilder.LessThan));
			}
			return QdmExpressionBuilder.EvaluateVersusThreshold(left, right, func);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00026DC1 File Offset: 0x00024FC1
		private static QueryExpression EvaluateVersusThreshold(QueryExpression expr, QueryExpression threshold, Func<QueryExpression, QueryExpression, QueryComparisonExpression> comparison)
		{
			return comparison(expr, threshold).If(expr, threshold);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00026DD2 File Offset: 0x00024FD2
		internal static IEnumerable<IEdmFieldInstance> GetReferencedIdentityFields(QueryExpression expression)
		{
			return QdmExpressionBuilder.GetIdentityFields(expression.GetReferencedFields());
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00026DDF File Offset: 0x00024FDF
		internal static IEnumerable<IConceptualColumn> GetReferencedIdentityColumns(QueryExpression expression)
		{
			return QdmExpressionBuilder.GetIdentityColumns(expression.GetReferencedColumns());
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00026DEC File Offset: 0x00024FEC
		internal static IEnumerable<IEdmFieldInstance> GetIdentityFields(IEnumerable<IEdmFieldInstance> fields)
		{
			HashSet<IEdmFieldInstance> hashSet = new HashSet<IEdmFieldInstance>();
			foreach (IEdmFieldInstance edmFieldInstance in fields)
			{
				if (edmFieldInstance.IsValid)
				{
					hashSet.UnionWith(QdmExpressionBuilder.GetIdentityFields(edmFieldInstance));
				}
			}
			return hashSet;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00026E48 File Offset: 0x00025048
		internal static IEnumerable<IConceptualColumn> GetIdentityColumns(IEnumerable<IConceptualColumn> columns)
		{
			HashSet<IConceptualColumn> hashSet = new HashSet<IConceptualColumn>();
			foreach (IConceptualColumn conceptualColumn in columns)
			{
				hashSet.UnionWith(QdmExpressionBuilder.GetIdentityColumns(conceptualColumn));
			}
			return hashSet;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00026E9C File Offset: 0x0002509C
		internal static IEnumerable<IEdmFieldInstance> GetIdentityFields(IEdmFieldInstance field)
		{
			List<IEdmFieldInstance> list = field.Field.Grouping.IdentityFields.Select((EdmField groupByField) => field.Entity.FieldInstance(groupByField)).ToList<IEdmFieldInstance>();
			list.Add(field);
			return list;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00026EED File Offset: 0x000250ED
		internal static IEnumerable<IConceptualColumn> GetIdentityColumns(IConceptualColumn column)
		{
			List<IConceptualColumn> list = column.Grouping.IdentityColumns.ToList<IConceptualColumn>();
			list.Add(column);
			return list;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00026F06 File Offset: 0x00025106
		internal static IEnumerable<IEdmFieldInstance> GetIdentityFieldsWithDefaultValues(IEnumerable<IEdmFieldInstance> fields)
		{
			return from f in QdmExpressionBuilder.GetIdentityFields(fields)
				where f.IsValid && f.Field.DefaultMember != null
				select f;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00026F32 File Offset: 0x00025132
		internal static IEnumerable<IConceptualColumn> GetIdentityColumnsWithDefaultValues(IEnumerable<IConceptualColumn> columns)
		{
			return from c in QdmExpressionBuilder.GetIdentityColumns(columns)
				where c.DefaultValue != null
				select c;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00026F5E File Offset: 0x0002515E
		internal static IEnumerable<IEdmFieldInstance> GetIdentityFieldsWithDefaultValues(IEdmFieldInstance field)
		{
			return from f in QdmExpressionBuilder.GetIdentityFields(field)
				where f.IsValid && f.Field.DefaultMember != null
				select f;
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00026F8A File Offset: 0x0002518A
		internal static IEnumerable<IConceptualColumn> GetIdentityColumnsWithDefaultValues(IConceptualColumn column)
		{
			return from c in QdmExpressionBuilder.GetIdentityColumns(column)
				where c.DefaultValue != null
				select c;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00026FB8 File Offset: 0x000251B8
		internal static IEnumerable<IEdmFieldInstance> GetRelatedIdentityFieldsWithDefaultValues(IEnumerable<IEdmFieldInstance> fields)
		{
			return QdmExpressionBuilder.GetIdentityFieldsWithDefaultValues((from fieldInstance in fields
				from pathFieldInstance in fieldInstance.GetLowerRelationshipPath()
				select pathFieldInstance).Evaluate<IEdmFieldInstance>());
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00027014 File Offset: 0x00025214
		internal static QueryGroupByExpression QdmFilterGroupBy(this IEnumerable<IEdmFieldInstance> fields, IConceptualSchema schema, bool useConceptualSchema, IEnumerable<IConceptualColumn> columns, ScanKind scanKind = ScanKind.InheritFilterContextIncludeBlankRow)
		{
			IEnumerable<KeyValuePair<string, QueryExpression>> enumerable;
			if (!useConceptualSchema)
			{
				enumerable = fields.Select((IEdmFieldInstance f, int index) => f.QdmReference().As(f.Field.Name));
			}
			else
			{
				enumerable = columns.Select((IConceptualColumn c, int index) => c.QdmReference().As(c.EdmName));
			}
			return enumerable.QdmGroupBy(useConceptualSchema, schema, scanKind);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0002707C File Offset: 0x0002527C
		internal static QueryGroupByExpression QdmGroupBy(this IEnumerable<KeyValuePair<string, QueryExpression>> expressions, bool useConceptualSchema, IConceptualSchema schema, ScanKind scanKind = ScanKind.InheritFilterContextIncludeBlankRow)
		{
			IList<KeyValuePair<string, QueryExpression>> list = expressions.Where((KeyValuePair<string, QueryExpression> e) => e.Value.IsModelFieldReference()).Evaluate<KeyValuePair<string, QueryExpression>>();
			IList<KeyValuePair<string, QueryExpression>> list2 = expressions.Except(list).Evaluate<KeyValuePair<string, QueryExpression>>();
			QueryGroupExpressionBinding binding;
			if (useConceptualSchema)
			{
				IConceptualEntity supersetExtensionEntity = list.Select((KeyValuePair<string, QueryExpression> e) => QdmExpressionBuilder.GetSingleEntity(e.Value)).ToList<IConceptualEntity>().GetSupersetExtensionEntity();
				binding = supersetExtensionEntity.QdmScan(scanKind).GroupBindAs(supersetExtensionEntity.EdmName);
			}
			else
			{
				EntitySet entitySet = list.Select((KeyValuePair<string, QueryExpression> e) => QdmExpressionBuilder.GetSingleEntitySet(e.Value)).Distinct<EntitySet>().SingleOrDefault<EntitySet>();
				binding = entitySet.QdmScan(scanKind).GroupBindAs(entitySet.Name);
			}
			return binding.GroupBy(list.Select((KeyValuePair<string, QueryExpression> e) => e.Value.RewriteEntityPlaceholders(binding.Variable).As(e.Key)), list2);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00027180 File Offset: 0x00025380
		private static QueryExpression QdmScan(this EntitySet entitySet, ScanKind scanKind)
		{
			switch (scanKind)
			{
			case ScanKind.InheritFilterContextIncludeBlankRow:
				return entitySet.Scan(false);
			case ScanKind.InheritFilterContextExcludeBlankRow:
				return entitySet.Scan(true);
			case ScanKind.IndependentFilterContextIncludeBlankRow:
				return entitySet.All(null);
			default:
				Microsoft.DataShaping.Contract.RetailFail("Unexpected ScanKind: {0}", scanKind);
				throw new InvalidOperationException(Microsoft.Reporting.StringUtil.FormatInvariant("Unexpected ScanKind: {0}", new object[] { scanKind }));
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000271E8 File Offset: 0x000253E8
		private static QueryExpression QdmScan(this IConceptualEntity entity, ScanKind scanKind)
		{
			switch (scanKind)
			{
			case ScanKind.InheritFilterContextIncludeBlankRow:
				return entity.Scan(false);
			case ScanKind.InheritFilterContextExcludeBlankRow:
				return entity.Scan(true);
			case ScanKind.IndependentFilterContextIncludeBlankRow:
				return entity.All();
			default:
				Microsoft.DataShaping.Contract.RetailFail("Unexpected ScanKind: {0}", scanKind);
				throw new InvalidOperationException(Microsoft.Reporting.StringUtil.FormatInvariant("Unexpected ScanKind: {0}", new object[] { scanKind }));
			}
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0002724E File Offset: 0x0002544E
		internal static EntitySet GetSingleEntitySet(QueryExpression expression)
		{
			EntitySet entitySet = expression.FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.PlaceholdersOnly).SingleOrDefault<EntitySet>();
			ArgumentValidation.CheckCondition(entitySet != null, "expression", "The specified expression does not contain any entity placeholders.");
			return entitySet;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002726F File Offset: 0x0002546F
		internal static IConceptualEntity GetSingleEntity(QueryExpression expression)
		{
			return expression.FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.PlaceholdersOnly).SingleOrDefault<IConceptualEntity>();
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0002727D File Offset: 0x0002547D
		internal static QueryGroupByExpression AppendAggregates(this QueryGroupByExpression groupByExpression, IEnumerable<KeyValuePair<string, QueryExpression>> aggregates)
		{
			ArgumentValidation.CheckNotNull<QueryGroupByExpression>(groupByExpression, "groupByExpression");
			ArgumentValidation.CheckNotNullOrEmpty<KeyValuePair<string, QueryExpression>>(aggregates, "aggregates");
			return groupByExpression.Input.GroupBy(groupByExpression.GroupItems, groupByExpression.Aggregates.Concat(aggregates));
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000272B4 File Offset: 0x000254B4
		internal static QueryGroupAndJoinExpression QdmGroupAndJoin(IEnumerable<IGroupItem> groupItems, IEnumerable<QueryGroupAndJoinAdditionalColumn> additionalColumns, IEnumerable<QueryExpression> queryExpressions)
		{
			return QueryExpressionBuilder.GroupAndJoin(QdmExpressionBuilder.RewriteEntityPlaceholdersToScalarEntityReferences(groupItems), QdmExpressionBuilder.RewriteEntityPlaceholdersToScalarEntityReferences(additionalColumns), queryExpressions);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000272C8 File Offset: 0x000254C8
		public static bool IsModelFieldReference(this QueryExpression expression)
		{
			QueryFieldExpression queryFieldExpression = expression as QueryFieldExpression;
			if (queryFieldExpression != null)
			{
				QdmEntityPlaceholderExpression qdmEntityPlaceholderExpression = queryFieldExpression.Instance as QdmEntityPlaceholderExpression;
				if (qdmEntityPlaceholderExpression != null)
				{
					if (qdmEntityPlaceholderExpression.TargetEntity == null)
					{
						return qdmEntityPlaceholderExpression.Target.FieldInstance(queryFieldExpression.Column.EdmName).IsValid;
					}
					IConceptualEntity targetEntity = qdmEntityPlaceholderExpression.TargetEntity;
					return ((targetEntity != null) ? targetEntity.GetPropertyByEdmName(queryFieldExpression.Column.EdmName).AsColumn() : null) != null;
				}
			}
			return false;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0002733C File Offset: 0x0002553C
		public static IReadOnlyList<IEdmFieldInstance> GetReferencedModelFieldsForFilter(this QueryExpression expression)
		{
			QueryInExpression queryInExpression;
			bool flag;
			if (QueryFilterExpressionAnalyzer.TryExtractExpression<QueryInExpression>(expression, out queryInExpression, out flag))
			{
				IEnumerable<QueryExpression> expressions = queryInExpression.Expressions;
				Func<QueryExpression, IEdmFieldInstance> func;
				if ((func = QdmExpressionBuilder.<>O.<2>__GetReferencedModelField) == null)
				{
					func = (QdmExpressionBuilder.<>O.<2>__GetReferencedModelField = new Func<QueryExpression, IEdmFieldInstance>(QdmExpressionBuilder.GetReferencedModelField));
				}
				HashSet<IEdmFieldInstance> hashSet = expressions.Select(func).ToSet<IEdmFieldInstance>();
				HashSet<IEdmFieldInstance> hashSet2 = (from f in queryInExpression.Values.SelectMany(delegate(IReadOnlyList<QueryExpression> vlist)
					{
						Func<QueryExpression, IEdmFieldInstance> func2;
						if ((func2 = QdmExpressionBuilder.<>O.<2>__GetReferencedModelField) == null)
						{
							func2 = (QdmExpressionBuilder.<>O.<2>__GetReferencedModelField = new Func<QueryExpression, IEdmFieldInstance>(QdmExpressionBuilder.GetReferencedModelField));
						}
						return vlist.Select(func2);
					})
					where f.IsValid
					select f).ToSet<IEdmFieldInstance>();
				hashSet.UnionWith(hashSet2);
				return hashSet.ToList<IEdmFieldInstance>();
			}
			QueryInTableExpression queryInTableExpression;
			if (!QueryFilterExpressionAnalyzer.TryExtractExpression<QueryInTableExpression>(expression, out queryInTableExpression, out flag))
			{
				return expression.GetReferencedModelField().AsReadOnlyList<IEdmFieldInstance>();
			}
			QueryExpression leftExpression = queryInTableExpression.LeftExpression;
			QueryTupleExpression queryTupleExpression = leftExpression as QueryTupleExpression;
			if (queryTupleExpression != null)
			{
				return queryTupleExpression.NamedColumns.Select((KeyValuePair<string, QueryExpression> pair) => pair.Value.GetReferencedModelField()).Distinct<IEdmFieldInstance>().ToList<IEdmFieldInstance>();
			}
			return leftExpression.GetReferencedModelField().AsReadOnlyList<IEdmFieldInstance>();
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00027454 File Offset: 0x00025654
		public static IReadOnlyList<IConceptualColumn> GetReferencedModelColumnsForFilter(this QueryExpression expression)
		{
			QueryInExpression queryInExpression;
			bool flag;
			if (QueryFilterExpressionAnalyzer.TryExtractExpression<QueryInExpression>(expression, out queryInExpression, out flag))
			{
				IEnumerable<QueryExpression> expressions = queryInExpression.Expressions;
				Func<QueryExpression, IConceptualColumn> func;
				if ((func = QdmExpressionBuilder.<>O.<3>__GetReferencedModelColumn) == null)
				{
					func = (QdmExpressionBuilder.<>O.<3>__GetReferencedModelColumn = new Func<QueryExpression, IConceptualColumn>(QdmExpressionBuilder.GetReferencedModelColumn));
				}
				HashSet<IConceptualColumn> hashSet = expressions.Select(func).ToSet<IConceptualColumn>();
				HashSet<IConceptualColumn> hashSet2 = (from f in queryInExpression.Values.SelectMany(delegate(IReadOnlyList<QueryExpression> vlist)
					{
						Func<QueryExpression, IConceptualColumn> func2;
						if ((func2 = QdmExpressionBuilder.<>O.<3>__GetReferencedModelColumn) == null)
						{
							func2 = (QdmExpressionBuilder.<>O.<3>__GetReferencedModelColumn = new Func<QueryExpression, IConceptualColumn>(QdmExpressionBuilder.GetReferencedModelColumn));
						}
						return vlist.Select(func2);
					})
					where f != null
					select f).ToSet<IConceptualColumn>();
				hashSet.UnionWith(hashSet2);
				return hashSet.ToList<IConceptualColumn>();
			}
			QueryInTableExpression queryInTableExpression;
			if (QueryFilterExpressionAnalyzer.TryExtractExpression<QueryInTableExpression>(expression, out queryInTableExpression, out flag))
			{
				QueryExpression leftExpression = queryInTableExpression.LeftExpression;
				QueryTupleExpression queryTupleExpression = leftExpression as QueryTupleExpression;
				if (queryTupleExpression != null)
				{
					return queryTupleExpression.NamedColumns.Select((KeyValuePair<string, QueryExpression> pair) => pair.Value.GetReferencedModelColumn()).Distinct<IConceptualColumn>().ToList<IConceptualColumn>();
				}
				return leftExpression.GetReferencedModelColumn().AsReadOnlyList<IConceptualColumn>();
			}
			else
			{
				IConceptualColumn referencedModelColumn = expression.GetReferencedModelColumn();
				if (referencedModelColumn != null)
				{
					return referencedModelColumn.AsReadOnlyList<IConceptualColumn>();
				}
				return Microsoft.DataShaping.Util.EmptyReadOnlyList<IConceptualColumn>();
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002757C File Offset: 0x0002577C
		public static IEdmFieldInstance GetReferencedModelField(this QueryExpression expression)
		{
			QueryFieldExpression queryFieldExpression = expression as QueryFieldExpression;
			if (queryFieldExpression != null)
			{
				QdmEntityPlaceholderExpression qdmEntityPlaceholderExpression = queryFieldExpression.Instance as QdmEntityPlaceholderExpression;
				if (qdmEntityPlaceholderExpression != null)
				{
					return qdmEntityPlaceholderExpression.Target.FieldInstance(queryFieldExpression.Column.EdmName);
				}
			}
			return EdmFieldInstance.Empty;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000275C8 File Offset: 0x000257C8
		public static IConceptualColumn GetReferencedModelColumn(this QueryExpression expression)
		{
			QueryFieldExpression queryFieldExpression = expression as QueryFieldExpression;
			if (queryFieldExpression != null)
			{
				QdmEntityPlaceholderExpression qdmEntityPlaceholderExpression = queryFieldExpression.Instance as QdmEntityPlaceholderExpression;
				if (qdmEntityPlaceholderExpression != null)
				{
					return qdmEntityPlaceholderExpression.TargetEntity.GetPropertyByEdmName(queryFieldExpression.Column.EdmName).AsColumn();
				}
			}
			return null;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002760C File Offset: 0x0002580C
		public static ISet<EntitySet> FindEntitySetReferences(this QueryExpression expression, QdmExpressionBuilder.EntityRefSearchBehavior searchBehavior = QdmExpressionBuilder.EntityRefSearchBehavior.All)
		{
			QdmExpressionBuilder.ExpressionEntityReferenceFinder expressionEntityReferenceFinder = new QdmExpressionBuilder.ExpressionEntityReferenceFinder(searchBehavior);
			expression.Accept<QueryExpression>(expressionEntityReferenceFinder);
			return expressionEntityReferenceFinder.ReferencedEntitySets;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00027630 File Offset: 0x00025830
		public static ISet<IConceptualEntity> FindEntityReferences(this QueryExpression expression, QdmExpressionBuilder.EntityRefSearchBehavior searchBehavior = QdmExpressionBuilder.EntityRefSearchBehavior.All)
		{
			QdmExpressionBuilder.ExpressionEntityReferenceFinder expressionEntityReferenceFinder = new QdmExpressionBuilder.ExpressionEntityReferenceFinder(searchBehavior);
			expression.Accept<QueryExpression>(expressionEntityReferenceFinder);
			return expressionEntityReferenceFinder.ReferencedEntities;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00027652 File Offset: 0x00025852
		internal static QueryExpression RewriteEntityPlaceholders(this QueryExpression expression, QueryVariableReferenceExpression variableRef)
		{
			return QdmExpressionBuilder.RewriteEntityPlaceholders(expression, variableRef, new ConceptualResultType[] { variableRef.ConceptualResultType });
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002766A File Offset: 0x0002586A
		internal static QueryExpression RewriteEntityPlaceholders(QueryExpression expression, QueryVariableReferenceExpression variableRef, IReadOnlyList<ConceptualResultType> entityTypesToReplace)
		{
			return expression.Accept<QueryExpression>(new QdmExpressionBuilder.ExpressionEntityPlaceholderTransform(variableRef, entityTypesToReplace));
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00027679 File Offset: 0x00025879
		internal static IEnumerable<KeyValuePair<string, QueryExpression>> RewriteEntityPlaceholdersToScalarEntityReferences(IEnumerable<KeyValuePair<string, QueryExpression>> pairs)
		{
			if (pairs == null)
			{
				return null;
			}
			return pairs.Select((KeyValuePair<string, QueryExpression> p) => p.Value.RewriteEntityPlaceholdersToScalarEntityReferences(null, null).As(p.Key));
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000276A5 File Offset: 0x000258A5
		internal static IEnumerable<QueryExpression> RewriteEntityPlaceholdersToScalarEntityReferences(IEnumerable<QueryExpression> exprs)
		{
			if (exprs == null)
			{
				return null;
			}
			return exprs.Select((QueryExpression p) => p.RewriteEntityPlaceholdersToScalarEntityReferences(null, null));
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000276D1 File Offset: 0x000258D1
		internal static IEnumerable<QueryGroupAndJoinAdditionalColumn> RewriteEntityPlaceholdersToScalarEntityReferences(IEnumerable<QueryGroupAndJoinAdditionalColumn> columns)
		{
			if (columns == null)
			{
				return null;
			}
			return columns.Select((QueryGroupAndJoinAdditionalColumn c) => QdmExpressionBuilder.RewriteEntityPlaceholdersToScalarEntityReferences(c));
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00027700 File Offset: 0x00025900
		internal static QueryGroupAndJoinAdditionalColumn RewriteEntityPlaceholdersToScalarEntityReferences(QueryGroupAndJoinAdditionalColumn column)
		{
			QueryExpression queryExpression = column.Expression.RewriteEntityPlaceholdersToScalarEntityReferences(null, null);
			if (queryExpression == column.Expression)
			{
				return column;
			}
			return new QueryGroupAndJoinAdditionalColumn(column.Name, queryExpression, column.SuppressJoinPredicate);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00027738 File Offset: 0x00025938
		internal static IEnumerable<IGroupItem> RewriteEntityPlaceholdersToScalarEntityReferences(IEnumerable<IGroupItem> groupItems)
		{
			if (groupItems == null)
			{
				return null;
			}
			return groupItems.Select((IGroupItem g) => QdmExpressionBuilder.RewriteEntityPlaceholdersToScalarEntityReferences(g));
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00027764 File Offset: 0x00025964
		internal static IGroupItem RewriteEntityPlaceholdersToScalarEntityReferences(IGroupItem groupItem)
		{
			CompositeKeyGroupItem compositeKeyGroupItem = groupItem as CompositeKeyGroupItem;
			if (compositeKeyGroupItem != null)
			{
				return new CompositeKeyGroupItem(QdmExpressionBuilder.RewriteEntityPlaceholdersToScalarEntityReferences(compositeKeyGroupItem.Keys));
			}
			NamedRollupGroupItem namedRollupGroupItem = groupItem as NamedRollupGroupItem;
			if (namedRollupGroupItem != null)
			{
				CompositeKeyGroupItem compositeKeyGroupItem2 = (CompositeKeyGroupItem)QdmExpressionBuilder.RewriteEntityPlaceholdersToScalarEntityReferences(namedRollupGroupItem.GroupKeysItem);
				List<QueryExpression> list = ((namedRollupGroupItem.ContextTables != null) ? QdmExpressionBuilder.RewriteEntityPlaceholdersToScalarEntityReferences(namedRollupGroupItem.ContextTables).ToList<QueryExpression>() : null);
				return new NamedRollupGroupItem(compositeKeyGroupItem2, namedRollupGroupItem.SubtotalIndicatorColumnName, list);
			}
			RollupAddIsSubtotalGroupItem rollupAddIsSubtotalGroupItem = (RollupAddIsSubtotalGroupItem)groupItem;
			IReadOnlyList<NamedRollupGroupItem> readOnlyList = rollupAddIsSubtotalGroupItem.GroupItems.Select((NamedRollupGroupItem g) => QdmExpressionBuilder.RewriteEntityPlaceholdersToScalarEntityReferences(g)).Cast<NamedRollupGroupItem>().ToList<NamedRollupGroupItem>();
			List<QueryExpression> list2 = ((rollupAddIsSubtotalGroupItem.ContextTables != null) ? QdmExpressionBuilder.RewriteEntityPlaceholdersToScalarEntityReferences(rollupAddIsSubtotalGroupItem.ContextTables).ToList<QueryExpression>() : null);
			return new RollupAddIsSubtotalGroupItem(readOnlyList, list2);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0002782E File Offset: 0x00025A2E
		internal static QueryExpression RewriteEntityPlaceholdersToScalarEntityReferences(this QueryExpression expression, EntitySet targetEntitySet = null, IConceptualEntity targetEntity = null)
		{
			return expression.Accept<QueryExpression>(new QdmExpressionBuilder.ExpressionEntityPlaceholderToScalarEntityReferenceTransform(targetEntitySet, targetEntity));
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0002783D File Offset: 0x00025A3D
		internal static QueryExpression CalculateIfNeeded(this QueryExpression expression)
		{
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = (ConceptualPrimitiveResultType)expression.ConceptualResultType;
			if (expression is QueryMeasureExpression)
			{
				return expression;
			}
			return expression.Calculate(Array.Empty<QueryExpression>());
		}

		// Token: 0x0200031D RID: 797
		internal enum EntityRefSearchBehavior
		{
			// Token: 0x04001164 RID: 4452
			All,
			// Token: 0x04001165 RID: 4453
			PlaceholdersOnly,
			// Token: 0x04001166 RID: 4454
			AnchoredOnly
		}

		// Token: 0x0200031E RID: 798
		private sealed class ExpressionEntityReferenceFinder : DefaultExpressionVisitor
		{
			// Token: 0x06001DD9 RID: 7641 RVA: 0x00051B72 File Offset: 0x0004FD72
			internal ExpressionEntityReferenceFinder(QdmExpressionBuilder.EntityRefSearchBehavior behavior)
			{
				this._behavior = behavior;
			}

			// Token: 0x170007FA RID: 2042
			// (get) Token: 0x06001DDA RID: 7642 RVA: 0x00051B9C File Offset: 0x0004FD9C
			internal ISet<EntitySet> ReferencedEntitySets
			{
				get
				{
					return this._referencedEntitySets;
				}
			}

			// Token: 0x170007FB RID: 2043
			// (get) Token: 0x06001DDB RID: 7643 RVA: 0x00051BA4 File Offset: 0x0004FDA4
			internal ISet<IConceptualEntity> ReferencedEntities
			{
				get
				{
					return this._referencedEntities;
				}
			}

			// Token: 0x06001DDC RID: 7644 RVA: 0x00051BAC File Offset: 0x0004FDAC
			protected internal override QueryExpression Visit(QueryExtensionExpression expression)
			{
				QdmEntityPlaceholderExpression qdmEntityPlaceholderExpression = expression as QdmEntityPlaceholderExpression;
				if (qdmEntityPlaceholderExpression != null)
				{
					if (qdmEntityPlaceholderExpression.Target != null)
					{
						this._referencedEntitySets.Add(qdmEntityPlaceholderExpression.Target);
					}
					if (qdmEntityPlaceholderExpression.TargetEntity != null)
					{
						this._referencedEntities.Add(qdmEntityPlaceholderExpression.TargetEntity);
					}
				}
				return expression;
			}

			// Token: 0x06001DDD RID: 7645 RVA: 0x00051BF8 File Offset: 0x0004FDF8
			protected internal override QueryExpression Visit(QueryCalculateExpression expression)
			{
				if (this._behavior == QdmExpressionBuilder.EntityRefSearchBehavior.AnchoredOnly)
				{
					this.VisitExpression(expression.Argument);
					return expression;
				}
				return base.Visit(expression);
			}

			// Token: 0x06001DDE RID: 7646 RVA: 0x00051C19 File Offset: 0x0004FE19
			protected internal override QueryExpression Visit(QueryMeasureExpression expression)
			{
				if (this._behavior == QdmExpressionBuilder.EntityRefSearchBehavior.AnchoredOnly)
				{
					return expression;
				}
				return base.Visit(expression);
			}

			// Token: 0x06001DDF RID: 7647 RVA: 0x00051C2D File Offset: 0x0004FE2D
			protected override EntitySet VisitEntitySet(EntitySet entitySet)
			{
				if (this._behavior != QdmExpressionBuilder.EntityRefSearchBehavior.PlaceholdersOnly)
				{
					this._referencedEntitySets.Add(entitySet);
				}
				return base.VisitEntitySet(entitySet);
			}

			// Token: 0x06001DE0 RID: 7648 RVA: 0x00051C4C File Offset: 0x0004FE4C
			protected override IConceptualEntity VisitEntity(IConceptualEntity entity)
			{
				if (entity == null)
				{
					return null;
				}
				if (this._behavior != QdmExpressionBuilder.EntityRefSearchBehavior.PlaceholdersOnly)
				{
					this._referencedEntities.Add(entity);
				}
				return base.VisitEntity(entity);
			}

			// Token: 0x04001167 RID: 4455
			private readonly QdmExpressionBuilder.EntityRefSearchBehavior _behavior;

			// Token: 0x04001168 RID: 4456
			private readonly HashSet<EntitySet> _referencedEntitySets = new HashSet<EntitySet>();

			// Token: 0x04001169 RID: 4457
			private HashSet<IConceptualEntity> _referencedEntities = new HashSet<IConceptualEntity>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
		}

		// Token: 0x0200031F RID: 799
		private sealed class ExpressionEntityPlaceholderTransform : DefaultExpressionVisitor
		{
			// Token: 0x06001DE1 RID: 7649 RVA: 0x00051C70 File Offset: 0x0004FE70
			internal ExpressionEntityPlaceholderTransform(QueryVariableReferenceExpression variableRef, IReadOnlyList<ConceptualResultType> entityTypesToReplace)
			{
				this._variableRef = variableRef;
				this._entityTypesToReplace = entityTypesToReplace;
			}

			// Token: 0x06001DE2 RID: 7650 RVA: 0x00051C88 File Offset: 0x0004FE88
			protected internal override QueryExpression Visit(QueryExtensionExpression expression)
			{
				QdmEntityPlaceholderExpression qdmEntityPlaceholderExpression = expression as QdmEntityPlaceholderExpression;
				if (qdmEntityPlaceholderExpression != null)
				{
					if (qdmEntityPlaceholderExpression.TargetEntity != null)
					{
						QdmExpressionBuilder.ExpressionEntityPlaceholderTransform.<>c__DisplayClass3_0 CS$<>8__locals1 = new QdmExpressionBuilder.ExpressionEntityPlaceholderTransform.<>c__DisplayClass3_0();
						QdmExpressionBuilder.ExpressionEntityPlaceholderTransform.<>c__DisplayClass3_0 CS$<>8__locals2 = CS$<>8__locals1;
						IConceptualEntity targetEntity = qdmEntityPlaceholderExpression.TargetEntity;
						CS$<>8__locals2.expressionResultType = ((targetEntity != null) ? targetEntity.GetExtensionAwareResultType().RowType : null);
						if (this._entityTypesToReplace.Any((ConceptualResultType e) => e.GetRowType().IsSupersetOf(CS$<>8__locals1.expressionResultType)))
						{
							return this._variableRef;
						}
					}
					else if (this._entityTypesToReplace.Contains(qdmEntityPlaceholderExpression.Target.ElementType.ConceptualType))
					{
						return this._variableRef;
					}
				}
				if (expression is QdmTableColumnReferenceExpression)
				{
					return expression;
				}
				return base.Visit(expression);
			}

			// Token: 0x0400116A RID: 4458
			private readonly QueryVariableReferenceExpression _variableRef;

			// Token: 0x0400116B RID: 4459
			private readonly IReadOnlyList<ConceptualResultType> _entityTypesToReplace;
		}

		// Token: 0x02000320 RID: 800
		private sealed class ExpressionEntityPlaceholderToScalarEntityReferenceTransform : DefaultExpressionVisitor
		{
			// Token: 0x06001DE3 RID: 7651 RVA: 0x00051D1F File Offset: 0x0004FF1F
			public ExpressionEntityPlaceholderToScalarEntityReferenceTransform(EntitySet targetEntitySet, IConceptualEntity targetEntity)
			{
				this._targetEntitySet = targetEntitySet;
				this._targetEntity = targetEntity;
			}

			// Token: 0x06001DE4 RID: 7652 RVA: 0x00051D38 File Offset: 0x0004FF38
			protected internal override QueryExpression Visit(QueryExtensionExpression expression)
			{
				QdmEntityPlaceholderExpression qdmEntityPlaceholderExpression = expression as QdmEntityPlaceholderExpression;
				if (qdmEntityPlaceholderExpression != null)
				{
					if (this._targetEntity == null && this._targetEntitySet == null)
					{
						if (qdmEntityPlaceholderExpression.TargetEntity == null)
						{
							return qdmEntityPlaceholderExpression.Target.ScalarEntity(null);
						}
						return qdmEntityPlaceholderExpression.TargetEntity.ScalarEntity();
					}
					else if (qdmEntityPlaceholderExpression.TargetEntity != null)
					{
						if (ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(qdmEntityPlaceholderExpression.TargetEntity, this._targetEntity))
						{
							return qdmEntityPlaceholderExpression.TargetEntity.ScalarEntity();
						}
					}
					else if (qdmEntityPlaceholderExpression.Target.Equals(this._targetEntitySet))
					{
						return qdmEntityPlaceholderExpression.Target.ScalarEntity(null);
					}
				}
				return base.Visit(expression);
			}

			// Token: 0x0400116C RID: 4460
			private readonly EntitySet _targetEntitySet;

			// Token: 0x0400116D RID: 4461
			private readonly IConceptualEntity _targetEntity;
		}

		// Token: 0x02000321 RID: 801
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400116E RID: 4462
			public static Func<QueryExpression, QueryExpression, QueryComparisonExpression> <0>__GreaterThan;

			// Token: 0x0400116F RID: 4463
			public static Func<QueryExpression, QueryExpression, QueryComparisonExpression> <1>__LessThan;

			// Token: 0x04001170 RID: 4464
			public static Func<QueryExpression, IEdmFieldInstance> <2>__GetReferencedModelField;

			// Token: 0x04001171 RID: 4465
			public static Func<QueryExpression, IConceptualColumn> <3>__GetReferencedModelColumn;
		}
	}
}
