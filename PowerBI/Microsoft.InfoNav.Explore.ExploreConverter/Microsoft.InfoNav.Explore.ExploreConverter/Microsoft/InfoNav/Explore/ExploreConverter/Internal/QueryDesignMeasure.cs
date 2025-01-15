using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000073 RID: 115
	internal sealed class QueryDesignMeasure
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000BA8E File Offset: 0x00009C8E
		internal QueryDesignMeasure(IRdmQueryExpression expression, string name)
		{
			this._expression = expression;
			this._name = name;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		public IRdmQueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000BAAC File Offset: 0x00009CAC
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04000188 RID: 392
		private readonly IRdmQueryExpression _expression;

		// Token: 0x04000189 RID: 393
		private readonly string _name;
	}
}
