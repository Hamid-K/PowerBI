using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000066 RID: 102
	internal sealed class Field
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000B882 File Offset: 0x00009A82
		internal Field(string name, IRdmQueryExpression expression, bool? outerJoin)
		{
			this._name = name;
			this._expression = expression;
			this._outerJoin = outerJoin;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000B89F File Offset: 0x00009A9F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000B8A7 File Offset: 0x00009AA7
		public IRdmQueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000B8AF File Offset: 0x00009AAF
		public bool? OuterJoin
		{
			get
			{
				return this._outerJoin;
			}
		}

		// Token: 0x04000171 RID: 369
		private readonly string _name;

		// Token: 0x04000172 RID: 370
		private readonly IRdmQueryExpression _expression;

		// Token: 0x04000173 RID: 371
		private readonly bool? _outerJoin;
	}
}
