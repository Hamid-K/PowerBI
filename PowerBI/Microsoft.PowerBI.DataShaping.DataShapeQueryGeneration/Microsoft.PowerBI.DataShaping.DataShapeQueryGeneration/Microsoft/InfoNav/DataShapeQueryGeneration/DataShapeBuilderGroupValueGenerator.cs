using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200000B RID: 11
	internal sealed class DataShapeBuilderGroupValueGenerator<TContainer> : IQueryGroupValueVisitor<bool> where TContainer : class, ICalculationContainer<TContainer>
	{
		// Token: 0x0600006E RID: 110 RVA: 0x000040D4 File Offset: 0x000022D4
		private DataShapeBuilderGroupValueGenerator(DataShapeIdGenerator ids, DataShapeExpressionsAxisGroupingBuilder groupingBuilder, SelectBindingsBuilder selectBindingsBuilder, Dictionary<ProjectedDsqExpression, string> expressionToIdMapping, TContainer container, Func<IConceptualProperty, ConceptualPropertyReference> getConceptualPropertyReference, bool contextOnly, int? primaryDepth, int? secondaryDepth)
		{
			this._ids = ids;
			this._groupingBuilder = groupingBuilder;
			this._selectBindingsBuilder = selectBindingsBuilder;
			this._expressionToIdMapping = expressionToIdMapping;
			this._container = container;
			this._getConceptualPropertyReference = getConceptualPropertyReference;
			this._contextOnly = contextOnly;
			this._primaryDepth = primaryDepth;
			this._secondaryDepth = secondaryDepth;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000412C File Offset: 0x0000232C
		internal static void Generate(QueryGroupValue groupValue, DataShapeIdGenerator ids, DataShapeExpressionsAxisGroupingBuilder groupingBuilder, SelectBindingsBuilder selectBindingsBuilder, Dictionary<ProjectedDsqExpression, string> expressionToIdMapping, TContainer container, Func<IConceptualProperty, ConceptualPropertyReference> getConceptualPropertyReference, bool contextOnly, int? primaryDepth, int? secondaryDepth)
		{
			DataShapeBuilderGroupValueGenerator<TContainer> dataShapeBuilderGroupValueGenerator = new DataShapeBuilderGroupValueGenerator<TContainer>(ids, groupingBuilder, selectBindingsBuilder, expressionToIdMapping, container, getConceptualPropertyReference, contextOnly, primaryDepth, secondaryDepth);
			groupValue.Accept<bool>(dataShapeBuilderGroupValueGenerator);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004158 File Offset: 0x00002358
		public bool Visit(QueryGroupSingleValue value)
		{
			if (this._contextOnly && !value.BindingHints.IsProjected)
			{
				return true;
			}
			string text = ((!value.BindingHints.IsProjected) ? this._ids.CreateKeyId() : this._ids.CreateGroupId());
			if (!this._contextOnly)
			{
				if (value.BindingHints.IsProjected)
				{
					this.SetSelectBinding(value.ProjectedExpression, text);
				}
				if (value.BindingHints.IsIdentityKey)
				{
					this.SetIdentity(value.BindingHints.Field, value.BindingHints.SelectIndicesWithThisIdentity, value.ProjectedExpression, text);
				}
				if (value.BindingHints.IsOrderByKey)
				{
					this.SetOrderByKey(value.BindingHints.Field, value.ProjectedExpression, text);
				}
			}
			DataShapeBuilderUtils.AddCalculations<TContainer>(this._ids, this._selectBindingsBuilder, this._expressionToIdMapping, this._container, value.ProjectedExpression, text);
			return true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004240 File Offset: 0x00002440
		public bool Visit(QueryGroupIntervalValue value)
		{
			string text = this._ids.CreateGroupId();
			string text2 = this._ids.CreateGroupId();
			if (!this._contextOnly)
			{
				IConceptualColumn sourceField = value.SourceField;
				ConceptualPropertyReference conceptualPropertyReference = this._getConceptualPropertyReference(sourceField);
				int? semanticQuerySelectIndex = value.MinExpression.SemanticQuerySelectIndex;
				this._groupingBuilder.WithKey(conceptualPropertyReference, sourceField, new int?(semanticQuerySelectIndex.Value), null, true, null);
				this._selectBindingsBuilder.AddToBindingIdentityKeys(semanticQuerySelectIndex.Value.ArrayWrap<int>(), null, null, conceptualPropertyReference, sourceField, null);
				IntervalValue intervalValue = this._selectBindingsBuilder.SetIntervalForSelect(semanticQuerySelectIndex.Value, text, text2);
				this._selectBindingsBuilder.AddIntervalSelectBinding(semanticQuerySelectIndex.Value, intervalValue, sourceField.FormatString, SelectKind.Group, this._primaryDepth, this._secondaryDepth, null);
			}
			DataShapeBuilderUtils.AddCalculation<TContainer>(this._container, value.MinExpression, text, new bool?(value.MinExpression.SuppressJoinPredicate));
			DataShapeBuilderUtils.AddCalculation<TContainer>(this._container, value.MaxExpression, text2, new bool?(value.MaxExpression.SuppressJoinPredicate));
			return true;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004364 File Offset: 0x00002564
		private void SetSelectBinding(ProjectedDsqExpression projectedExpression, string id)
		{
			SelectKind selectKind = SelectKind.Group;
			int? semanticQuerySelectIndex = projectedExpression.SemanticQuerySelectIndex;
			if (semanticQuerySelectIndex != null)
			{
				int value = semanticQuerySelectIndex.Value;
				this._selectBindingsBuilder.SetCalcIdForSelect(value, id);
				this._selectBindingsBuilder.AddSelectBinding(value, id, projectedExpression.Value.FormatString, selectKind, this._primaryDepth, this._secondaryDepth, projectedExpression.Value.LineageProperty);
			}
			List<int> additionalSemanticQuerySelectIndices = projectedExpression.AdditionalSemanticQuerySelectIndices;
			if (additionalSemanticQuerySelectIndices != null)
			{
				foreach (int num in additionalSemanticQuerySelectIndices)
				{
					this._selectBindingsBuilder.SetCalcIdForSelect(num, id);
					this._selectBindingsBuilder.AddSelectBinding(num, id, projectedExpression.Value.FormatString, selectKind, this._primaryDepth, this._secondaryDepth, projectedExpression.Value.LineageProperty);
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004450 File Offset: 0x00002650
		private void SetIdentity(IConceptualColumn column, IReadOnlyList<int> selectIndicesWithThisIdentity, ProjectedDsqExpression projection, string id)
		{
			ConceptualPropertyReference conceptualPropertyReference = this._getConceptualPropertyReference(column);
			if (projection.SemanticQuerySelectIndex != null)
			{
				this._groupingBuilder.WithKey(conceptualPropertyReference, column, new int?(projection.SemanticQuerySelectIndex.Value), null, true, selectIndicesWithThisIdentity);
			}
			else
			{
				this._groupingBuilder.WithKey(conceptualPropertyReference, column, null, id, true, selectIndicesWithThisIdentity);
			}
			this._selectBindingsBuilder.AddToBindingIdentityKeys(selectIndicesWithThisIdentity, projection.SemanticQuerySelectIndex, projection.AdditionalSemanticQuerySelectIndices, conceptualPropertyReference, column, id);
			if (!selectIndicesWithThisIdentity.IsNullOrEmpty<int>())
			{
				foreach (int num in selectIndicesWithThisIdentity)
				{
					if (this._selectBindingsBuilder.GetSelectBinding(num) == null)
					{
						this._selectBindingsBuilder.AddSelectBinding(num, null, null, SelectKind.Group, this._primaryDepth, this._secondaryDepth, null);
					}
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004540 File Offset: 0x00002740
		private void SetOrderByKey(IConceptualColumn column, ProjectedDsqExpression projection, string id)
		{
			ConceptualPropertyReference conceptualPropertyReference = this._getConceptualPropertyReference(column);
			this._groupingBuilder.WithKey(conceptualPropertyReference, column, projection.SemanticQuerySelectIndex, id, false, null);
		}

		// Token: 0x04000042 RID: 66
		private readonly DataShapeIdGenerator _ids;

		// Token: 0x04000043 RID: 67
		private readonly DataShapeExpressionsAxisGroupingBuilder _groupingBuilder;

		// Token: 0x04000044 RID: 68
		private readonly SelectBindingsBuilder _selectBindingsBuilder;

		// Token: 0x04000045 RID: 69
		private readonly Dictionary<ProjectedDsqExpression, string> _expressionToIdMapping;

		// Token: 0x04000046 RID: 70
		private readonly TContainer _container;

		// Token: 0x04000047 RID: 71
		private readonly bool _contextOnly;

		// Token: 0x04000048 RID: 72
		private readonly Func<IConceptualProperty, ConceptualPropertyReference> _getConceptualPropertyReference;

		// Token: 0x04000049 RID: 73
		private readonly int? _primaryDepth;

		// Token: 0x0400004A RID: 74
		private readonly int? _secondaryDepth;
	}
}
