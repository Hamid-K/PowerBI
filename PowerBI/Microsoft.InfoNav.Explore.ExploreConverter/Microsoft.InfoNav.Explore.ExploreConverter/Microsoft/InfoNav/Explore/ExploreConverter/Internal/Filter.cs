using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000067 RID: 103
	internal sealed class Filter
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		internal Filter(CompoundFilterCondition<IRdmQueryExpression> filterCondition, string type, IRdmQueryExpression operand, FilterMode mode, FilterStateType filterType)
		{
			this._filterCondition = filterCondition;
			this._type = type;
			this._operand = operand;
			this._mode = mode;
			this._filterType = filterType;
			Contract.Check(!this.IsMeasureFilter || !this.IsDrilldownFilter, "Filter can either be drilldown or measure but not both");
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000B90E File Offset: 0x00009B0E
		public string Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000B916 File Offset: 0x00009B16
		public FilterMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000B91E File Offset: 0x00009B1E
		public IRdmQueryExpression Operand
		{
			get
			{
				return this._operand;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000B926 File Offset: 0x00009B26
		public CompoundFilterCondition<IRdmQueryExpression> FilterCondition
		{
			get
			{
				return this._filterCondition;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000B92E File Offset: 0x00009B2E
		public bool IsDrilldownFilter
		{
			get
			{
				return (this._filterType & FilterStateType.DrillDownFilter) == FilterStateType.DrillDownFilter;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000B93B File Offset: 0x00009B3B
		public bool IsMeasureFilter
		{
			get
			{
				return (this._filterType & FilterStateType.MeasureFilter) == FilterStateType.MeasureFilter;
			}
		}

		// Token: 0x04000174 RID: 372
		private readonly CompoundFilterCondition<IRdmQueryExpression> _filterCondition;

		// Token: 0x04000175 RID: 373
		private readonly string _type;

		// Token: 0x04000176 RID: 374
		private readonly IRdmQueryExpression _operand;

		// Token: 0x04000177 RID: 375
		private readonly FilterMode _mode;

		// Token: 0x04000178 RID: 376
		private readonly FilterStateType _filterType;
	}
}
