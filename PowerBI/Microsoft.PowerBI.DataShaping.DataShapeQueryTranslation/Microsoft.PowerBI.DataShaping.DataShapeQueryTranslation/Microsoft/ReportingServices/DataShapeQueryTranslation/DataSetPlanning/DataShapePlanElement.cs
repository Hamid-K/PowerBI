using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000E3 RID: 227
	[DebuggerDisplay("[DataShape] Id={Scope.Id} [Projected={IsProjected}]")]
	internal sealed class DataShapePlanElement : ScopePlanElement
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x0002386E File Offset: 0x00021A6E
		internal DataShapePlanElement(DataShape dataShape, IList<NestedPlanElement> nestedElements, bool isProjected, FilterCondition filterCondition = null, IReadOnlyList<DataSetPlan> subQueryJoinPredicates = null, IReadOnlyList<DataSetPlan> applyFilters = null, FilterCondition valueFilter = null, IReadOnlyList<AnyValueFilterCondition> anyValueFilters = null, DefaultValueFilterCondition defaultValueFilter = null)
			: base(nestedElements, isProjected, filterCondition, null)
		{
			this.DataShape = dataShape;
			this.SubQueryJoinPredicates = subQueryJoinPredicates;
			this.ApplyFilters = applyFilters;
			this.ValueFilter = valueFilter;
			this.AnyValueFilters = anyValueFilters;
			this.DefaultValueFilter = defaultValueFilter;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x000238AA File Offset: 0x00021AAA
		public DataShape DataShape { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x000238B2 File Offset: 0x00021AB2
		public IReadOnlyList<DataSetPlan> SubQueryJoinPredicates { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x000238BA File Offset: 0x00021ABA
		internal IReadOnlyList<DataSetPlan> ApplyFilters { get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x000238C2 File Offset: 0x00021AC2
		public FilterCondition ValueFilter { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x000238CA File Offset: 0x00021ACA
		public IReadOnlyList<AnyValueFilterCondition> AnyValueFilters { get; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000238D2 File Offset: 0x00021AD2
		public DefaultValueFilterCondition DefaultValueFilter { get; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x000238DA File Offset: 0x00021ADA
		public override IScope Scope
		{
			get
			{
				return this.DataShape;
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000238E4 File Offset: 0x00021AE4
		public override ScopePlanElement OmitProjection()
		{
			if (!base.IsProjected)
			{
				return this;
			}
			return new DataShapePlanElement(this.DataShape, base.GetNestedElementsForOmitProjection(), false, base.FilterCondition, this.SubQueryJoinPredicates, null, this.ValueFilter, this.AnyValueFilters, this.DefaultValueFilter);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002392C File Offset: 0x00021B2C
		public override ScopePlanElement OmitNestedElements()
		{
			return new DataShapePlanElement(this.DataShape, null, base.IsProjected, base.FilterCondition, this.SubQueryJoinPredicates, this.ApplyFilters, this.ValueFilter, this.AnyValueFilters, this.DefaultValueFilter);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00023970 File Offset: 0x00021B70
		public override ScopePlanElement AddNestedPlanElement(NestedPlanElement expression)
		{
			return new DataShapePlanElement(this.DataShape, base.AddToNestedElementCollection(expression), base.IsProjected, base.FilterCondition, this.SubQueryJoinPredicates, this.ApplyFilters, this.ValueFilter, this.AnyValueFilters, this.DefaultValueFilter);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000239B9 File Offset: 0x00021BB9
		public override void Accept(DataSetPlanElementVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x000239C4 File Offset: 0x00021BC4
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DataShapePlanElement");
			builder.WriteAttribute<DataShape>("DataShape", this.DataShape, false, false);
			base.WriteToBase(builder);
			builder.WriteProperty<IReadOnlyList<DataSetPlan>>("SubQueryJoinPredicates", this.SubQueryJoinPredicates, false);
			builder.WriteProperty<IReadOnlyList<DataSetPlan>>("ApplyFilters", this.ApplyFilters, false);
			builder.WriteProperty<FilterCondition>("ValueFilter", this.ValueFilter, false);
			builder.WriteProperty<IReadOnlyList<AnyValueFilterCondition>>("AnyValueFilters", this.AnyValueFilters, false);
			builder.WriteProperty<DefaultValueFilterCondition>("DefaultValueFilter", this.DefaultValueFilter, false);
			builder.EndObject();
		}
	}
}
