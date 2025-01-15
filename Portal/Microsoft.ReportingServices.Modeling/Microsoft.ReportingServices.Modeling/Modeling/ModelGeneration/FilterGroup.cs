using System;
using System.ComponentModel;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000E5 RID: 229
	public sealed class FilterGroup : Filter
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00026E9C File Offset: 0x0002509C
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x00026EA4 File Offset: 0x000250A4
		public FilterGroupOperator Operator
		{
			get
			{
				return this.m_operator;
			}
			set
			{
				if (!EnumUtil.IsDefined<FilterGroupOperator>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				this.m_operator = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00026EBB File Offset: 0x000250BB
		public FilterCollection Filters
		{
			get
			{
				return this.m_filters;
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00026EC4 File Offset: 0x000250C4
		public override bool IsMatch(DsvItem dsvItem)
		{
			if (this.m_filters.Count == 0)
			{
				throw new InvalidOperationException();
			}
			bool flag = this.m_filters[0].IsMatch(dsvItem);
			for (int i = 1; i < this.m_filters.Count; i++)
			{
				bool flag2 = this.m_filters[i].IsMatch(dsvItem);
				switch (this.m_operator)
				{
				case FilterGroupOperator.And:
				case FilterGroupOperator.NotAnd:
					flag = flag && flag2;
					break;
				case FilterGroupOperator.Or:
				case FilterGroupOperator.NotOr:
					flag = flag || flag2;
					break;
				}
			}
			if (this.m_operator == FilterGroupOperator.NotAnd || this.m_operator == FilterGroupOperator.NotOr)
			{
				return !flag;
			}
			return flag;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00026F61 File Offset: 0x00025161
		protected override bool ShouldCombineWith(Filter other)
		{
			return false;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00026F64 File Offset: 0x00025164
		internal override bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "operator")
			{
				this.m_operator = xr.ReadValueAsEnum<FilterGroupOperator>();
				return true;
			}
			return base.LoadXmlAttribute(xr, objectFactory);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00026F96 File Offset: 0x00025196
		internal override bool LoadXmlElement(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "filter")
			{
				this.m_filters.Add(objectFactory.CreateFilter(xr));
				return true;
			}
			return base.LoadXmlElement(xr, objectFactory);
		}

		// Token: 0x040004E4 RID: 1252
		private const string OperatorAttr = "operator";

		// Token: 0x040004E5 RID: 1253
		private FilterGroupOperator m_operator;

		// Token: 0x040004E6 RID: 1254
		private readonly FilterCollection m_filters = new FilterCollection();
	}
}
