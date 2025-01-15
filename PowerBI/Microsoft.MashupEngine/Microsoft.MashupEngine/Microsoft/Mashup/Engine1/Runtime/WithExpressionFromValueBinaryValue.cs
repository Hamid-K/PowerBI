using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001286 RID: 4742
	internal sealed class WithExpressionFromValueBinaryValue : DelegatingBinaryValue
	{
		// Token: 0x06007CA7 RID: 31911 RVA: 0x001AC129 File Offset: 0x001AA329
		public WithExpressionFromValueBinaryValue(BinaryValue binary, Value expressionValue)
			: base(binary)
		{
			this.expressionValue = expressionValue;
		}

		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x06007CA8 RID: 31912 RVA: 0x001AC139 File Offset: 0x001AA339
		public sealed override IExpression Expression
		{
			get
			{
				return this.expressionValue.Expression;
			}
		}

		// Token: 0x040044CD RID: 17613
		private readonly Value expressionValue;
	}
}
