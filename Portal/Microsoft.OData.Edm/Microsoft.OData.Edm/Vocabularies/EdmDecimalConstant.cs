using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000127 RID: 295
	public class EdmDecimalConstant : EdmValue, IEdmDecimalConstantExpression, IEdmExpression, IEdmElement, IEdmDecimalValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x060007A4 RID: 1956 RVA: 0x000121EA File Offset: 0x000103EA
		public EdmDecimalConstant(decimal value)
			: this(null, value)
		{
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000121F4 File Offset: 0x000103F4
		public EdmDecimalConstant(IEdmDecimalTypeReference type, decimal value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00012204 File Offset: 0x00010404
		public decimal Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000039FB File Offset: 0x00001BFB
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DecimalConstant;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0000480B File Offset: 0x00002A0B
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Decimal;
			}
		}

		// Token: 0x0400032D RID: 813
		private readonly decimal value;
	}
}
