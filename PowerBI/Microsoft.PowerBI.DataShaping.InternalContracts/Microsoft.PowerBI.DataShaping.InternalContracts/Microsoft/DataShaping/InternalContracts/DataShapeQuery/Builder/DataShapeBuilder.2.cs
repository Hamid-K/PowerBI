using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000E2 RID: 226
	internal class DataShapeBuilder<TParent> : BuilderBase<DataShape, TParent>, IWithDataShape<DataShapeBuilder<TParent>>, ICalculationContainer<DataShapeBuilder<TParent>>, IDataMemberContainer<DataShapeBuilder<TParent>>
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x0000D36C File Offset: 0x0000B56C
		internal DataShapeBuilder(TParent parent, DataShape activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0000D376 File Offset: 0x0000B576
		internal Identifier Id
		{
			get
			{
				return base.ActiveObject.Id;
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000D383 File Offset: 0x0000B583
		internal DataShapeBuilder<TParent> WithRequestedPrimaryLeafCount(int count)
		{
			base.ActiveObject.RequestedPrimaryLeafCount = count;
			return this;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000D398 File Offset: 0x0000B598
		public DataShapeBuilder<DataShapeBuilder<TParent>> WithDataShape(string identifier, string dataSourceId = null, bool filterEmptyGroups = true, Candidate<bool> contextOnly = null, bool independent = false, DataShapeUsage usage = DataShapeUsage.Query)
		{
			return base.AddDataShape<DataShapeBuilder<TParent>>(this, this.GetDataShapesList(), identifier, dataSourceId, filterEmptyGroups, contextOnly, independent, usage);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000D3BB File Offset: 0x0000B5BB
		public DataShapeBuilder<DataShapeBuilder<TParent>> WithDataShape(DataShape dataShape)
		{
			return base.AddDataShape<DataShapeBuilder<TParent>>(this, this.GetDataShapesList(), dataShape);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000D3CC File Offset: 0x0000B5CC
		public DataShapeBuilder<TParent> WithCalculation(string identifier, Expression value, bool suppressJoinPredicate = false, bool? respectInstanceFilters = null, string nativeReferenceName = null, bool isContextOnly = false)
		{
			base.ActiveObject.Calculations = base.AddCalculation(base.ActiveObject.Calculations, identifier, value, suppressJoinPredicate, respectInstanceFilters, nativeReferenceName, isContextOnly);
			return this;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000D400 File Offset: 0x0000B600
		public DataShapeBuilder<TParent> WithVisualCalculation(string identifier, string nativeReferenceName, string daxExpression)
		{
			return this.WithCalculation(identifier, daxExpression.VisualCalculation(), false, null, nativeReferenceName, false);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000D42D File Offset: 0x0000B62D
		public DataShapeBuilder<TParent> WithMessage(string code, string severity, string text)
		{
			base.ActiveObject.Messages = base.AddMessage(base.ActiveObject.Messages, code, severity, text);
			return this;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000D44F File Offset: 0x0000B64F
		public DataShapeBuilder<TParent> EnsureCalculations()
		{
			base.ActiveObject.Calculations = base.ActiveObject.Calculations ?? new List<Calculation>();
			return this;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000D471 File Offset: 0x0000B671
		public DataShapeBuilder<TParent> EnsureFilters()
		{
			this.GetFiltersList();
			return this;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000D47C File Offset: 0x0000B67C
		public DataRowBuilder<DataShapeBuilder<TParent>> WithDataRow()
		{
			DataShape activeObject = base.ActiveObject;
			List<DataRow> list = activeObject.DataRows;
			if (list == null)
			{
				list = new List<DataRow>();
				activeObject.DataRows = list;
			}
			DataRow dataRow = new DataRow();
			list.Add(dataRow);
			return new DataRowBuilder<DataShapeBuilder<TParent>>(this, dataRow);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0000D4BB File Offset: 0x0000B6BB
		public DataShapeBuilder<TParent> WithFilter(Filter filter)
		{
			this.GetFiltersList().Add(filter);
			return this;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000D4CA File Offset: 0x0000B6CA
		public DataShapeBuilder<TParent> WithModelParameter(string name, List<Expression> values, bool isListType)
		{
			this.GetModelParametersList().Add(new ModelParameter
			{
				Name = name,
				Values = values,
				IsListType = isListType
			});
			return this;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
		public FilterBuilder<DataShapeBuilder<TParent>> WithFilter(Expression target, FilterUsageKind usageKind = FilterUsageKind.Default)
		{
			List<Filter> filtersList = this.GetFiltersList();
			Filter filter = new Filter
			{
				Target = target,
				UsageKind = usageKind
			};
			filtersList.Add(filter);
			return new FilterBuilder<DataShapeBuilder<TParent>>(this, filter);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000D528 File Offset: 0x0000B728
		public DataShapeBuilder<TParent> WithFilters(IEnumerable<Filter> newFilters)
		{
			if (newFilters != null)
			{
				this.GetFiltersList().AddRange(newFilters);
			}
			return this;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000D53C File Offset: 0x0000B73C
		public LimitBuilder<DataShapeBuilder<TParent>> WithLimit(Identifier identifier, Expression target, Expression within)
		{
			this.EnsureLimitsInternal();
			Limit limit = new Limit
			{
				Id = identifier,
				Targets = new List<Expression> { target },
				Within = within
			};
			base.ActiveObject.Limits.Add(limit);
			return new LimitBuilder<DataShapeBuilder<TParent>>(this, limit);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000D590 File Offset: 0x0000B790
		public LimitBuilder<DataShapeBuilder<TParent>> WithLimit(Identifier identifier, List<Expression> targets, Expression within)
		{
			this.EnsureLimitsInternal();
			Limit limit = new Limit
			{
				Id = identifier,
				Targets = targets,
				Within = within
			};
			base.ActiveObject.Limits.Add(limit);
			return new LimitBuilder<DataShapeBuilder<TParent>>(this, limit);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		public LimitBuilder<DataShapeBuilder<TParent>> WithLimit(Identifier identifier, Expression within)
		{
			this.EnsureLimitsInternal();
			Limit limit = new Limit
			{
				Id = identifier,
				Within = within
			};
			base.ActiveObject.Limits.Add(limit);
			return new LimitBuilder<DataShapeBuilder<TParent>>(this, limit);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000D618 File Offset: 0x0000B818
		public DynamicLimitsBuilder<DataShapeBuilder<TParent>> WithDynamicLimits()
		{
			DynamicLimits dynamicLimits = new DynamicLimits();
			base.ActiveObject.DynamicLimits = dynamicLimits;
			return new DynamicLimitsBuilder<DataShapeBuilder<TParent>>(this, dynamicLimits);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0000D63E File Offset: 0x0000B83E
		public DataShapeBuilder<TParent> WithPrimaryStaticMember(string identifier, bool? subtotalStartPosition = null)
		{
			return this.WithDataMember(identifier, true, subtotalStartPosition).Parent();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000D653 File Offset: 0x0000B853
		public DataShapeBuilder<TParent> WithSecondaryStaticMember(string identifier, bool? subtotalStartPosition = null)
		{
			return this.WithDataMember(identifier, false, subtotalStartPosition).Parent();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000D668 File Offset: 0x0000B868
		public DataMemberBuilder<DataShapeBuilder<TParent>> WithStaticMember(string identifier, bool isPrimary, bool? subtotalStartPosition)
		{
			return this.WithDataMember(identifier, isPrimary, subtotalStartPosition);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0000D678 File Offset: 0x0000B878
		public DataMemberBuilder<DataShapeBuilder<TParent>> WithPrimaryMember(Identifier identifier, bool? subtotalStartPosition = null)
		{
			return this.WithDataMember(identifier, true, subtotalStartPosition);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0000D684 File Offset: 0x0000B884
		public DataMemberBuilder<DataShapeBuilder<TParent>> WithSecondaryMember(Identifier identifier)
		{
			return this.WithDataMember(identifier, false, null);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		private DataMemberBuilder<DataShapeBuilder<TParent>> WithDataMember(Identifier identifier, bool isPrimary, bool? subtotalStartPosition = null)
		{
			DataMember dataMember = new DataMember();
			dataMember.Id = identifier;
			bool? flag = subtotalStartPosition;
			dataMember.SubtotalStartPosition = ((flag != null) ? flag.GetValueOrDefault() : null);
			DataMember dataMember2 = dataMember;
			DataShape activeObject = base.ActiveObject;
			DataHierarchy dataHierarchy = (isPrimary ? activeObject.PrimaryHierarchy : activeObject.SecondaryHierarchy);
			if (dataHierarchy == null)
			{
				dataHierarchy = new DataHierarchy();
				if (isPrimary)
				{
					activeObject.PrimaryHierarchy = dataHierarchy;
				}
				else
				{
					activeObject.SecondaryHierarchy = dataHierarchy;
				}
			}
			List<DataMember> list = dataHierarchy.DataMembers;
			if (list == null)
			{
				list = new List<DataMember>();
				dataHierarchy.DataMembers = list;
			}
			list.Add(dataMember2);
			return new DataMemberBuilder<DataShapeBuilder<TParent>>(this, dataMember2);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000D73C File Offset: 0x0000B93C
		protected List<DataShape> GetDataShapesList()
		{
			return base.ActiveObject.DataShapes = base.ActiveObject.DataShapes ?? new List<DataShape>();
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000D76C File Offset: 0x0000B96C
		private List<Filter> GetFiltersList()
		{
			return base.ActiveObject.Filters = base.ActiveObject.Filters ?? new List<Filter>();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000D79C File Offset: 0x0000B99C
		private List<ModelParameter> GetModelParametersList()
		{
			return base.ActiveObject.ModelParameters = base.ActiveObject.ModelParameters ?? new List<ModelParameter>();
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		private List<Limit> EnsureLimitsInternal()
		{
			DataShape activeObject = base.ActiveObject;
			List<Limit> list = activeObject.Limits;
			if (list == null)
			{
				list = new List<Limit>();
				activeObject.Limits = list;
			}
			return list;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		private List<Expression> ConvertToReferenceExpressions(string[] targetDataShapeIDs)
		{
			List<Expression> list = new List<Expression>();
			foreach (string text in targetDataShapeIDs)
			{
				Expression expression = null;
				if (text != null)
				{
					expression = text.StructureReference();
				}
				list.Add(expression);
			}
			return list;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0000D83B File Offset: 0x0000BA3B
		public DataShapeBuilder<TParent> WithRestartToken(params Candidate<ScalarValue>[] values)
		{
			return this.WithRestartToken(new RestartToken(values));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0000D849 File Offset: 0x0000BA49
		public DataShapeBuilder<TParent> WithRestartTokens(List<RestartToken> restartTokens)
		{
			base.ActiveObject.RestartTokens = restartTokens;
			return this;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0000D858 File Offset: 0x0000BA58
		public DataShapeBuilder<TParent> WithRestartToken(RestartToken restartToken)
		{
			if (base.ActiveObject.RestartTokens == null)
			{
				base.ActiveObject.RestartTokens = new List<RestartToken>();
			}
			base.ActiveObject.RestartTokens.Add(restartToken);
			return this;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0000D889 File Offset: 0x0000BA89
		public DataShapeBuilder<TParent> WithRestartToken(params ScalarValue[] values)
		{
			return this.WithRestartToken(values.Select((ScalarValue v) => Candidate<ScalarValue>.Valid(v)).ToArray<Candidate<ScalarValue>>());
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0000D8BB File Offset: 0x0000BABB
		public DataShapeBuilder<TParent> IncludeRestartToken()
		{
			base.ActiveObject.IncludeRestartToken = true;
			return this;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0000D8CF File Offset: 0x0000BACF
		public DataShapeBuilder<TParent> WithRestartMatchingBehavior(RestartMatchingBehavior restartMatchingBehavior)
		{
			base.ActiveObject.RestartMatchingBehavior = new RestartMatchingBehavior?(restartMatchingBehavior);
			return this;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		public DataTransformBuilder<DataShapeBuilder<TParent>> WithDataTransform(Identifier id, string algorithm)
		{
			if (base.ActiveObject.Transforms == null)
			{
				base.ActiveObject.Transforms = new List<DataTransform>();
			}
			DataTransform dataTransform = new DataTransform();
			dataTransform.Id = id;
			dataTransform.Algorithm = algorithm;
			base.ActiveObject.Transforms.Add(dataTransform);
			return new DataTransformBuilder<DataShapeBuilder<TParent>>(this, dataTransform);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0000D940 File Offset: 0x0000BB40
		public ExtensionSchemaBuilder<DataShapeBuilder<TParent>> WithExtensionSchema(string name = null)
		{
			ExtensionSchema extensionSchema = new ExtensionSchema();
			base.ActiveObject.ExtensionSchema = extensionSchema;
			if (name != null)
			{
				extensionSchema.Name = name;
			}
			return new ExtensionSchemaBuilder<DataShapeBuilder<TParent>>(this, extensionSchema);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0000D970 File Offset: 0x0000BB70
		public DataShapeBuilder<TParent> WithExtensionSchema(ExtensionSchema extensionSchema)
		{
			base.ActiveObject.ExtensionSchema = extensionSchema;
			return this;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000D97F File Offset: 0x0000BB7F
		public DataShapeBuilder<TParent> WithDataSourceVariables(string dataSourcevariables)
		{
			base.ActiveObject.DataSourceVariables = dataSourcevariables;
			return this;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0000D990 File Offset: 0x0000BB90
		public DataShapeBuilder<TParent> WithQueryParameterDeclaration(string name, ConceptualResultType type)
		{
			if (base.ActiveObject.QueryParameters == null)
			{
				base.ActiveObject.QueryParameters = new List<QueryParameterDeclaration>();
			}
			QueryParameterDeclaration queryParameterDeclaration = new QueryParameterDeclaration
			{
				Name = name,
				Type = type
			};
			base.ActiveObject.QueryParameters.Add(queryParameterDeclaration);
			return this;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		public VisualAxisBuilder<DataShapeBuilder<TParent>> WithVisualAxisRows()
		{
			return this.WithVisualAxis("rows");
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000D9ED File Offset: 0x0000BBED
		public VisualAxisBuilder<DataShapeBuilder<TParent>> WithVisualAxisColumns()
		{
			return this.WithVisualAxis("columns");
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0000D9FA File Offset: 0x0000BBFA
		public VisualAxisBuilder<DataShapeBuilder<TParent>> WithVisualAxisRowPages()
		{
			return this.WithVisualAxis("rowpages");
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000DA08 File Offset: 0x0000BC08
		public VisualAxisBuilder<DataShapeBuilder<TParent>> WithVisualAxis(string name)
		{
			if (base.ActiveObject.VisualCalculationMetadata == null)
			{
				base.ActiveObject.VisualCalculationMetadata = new List<VisualAxis>();
			}
			VisualAxis visualAxis = new VisualAxis
			{
				Name = name
			};
			base.ActiveObject.VisualCalculationMetadata.Add(visualAxis);
			return new VisualAxisBuilder<DataShapeBuilder<TParent>>(this, visualAxis);
		}
	}
}
