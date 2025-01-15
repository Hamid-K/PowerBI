using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200011A RID: 282
	public class EdmDecimalConstant : EdmValue, IEdmDecimalConstantExpression, IEdmExpression, IEdmElement, IEdmDecimalValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x00013D06 File Offset: 0x00011F06
		public EdmDecimalConstant(decimal value)
			: this(null, value)
		{
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00013D10 File Offset: 0x00011F10
		public EdmDecimalConstant(IEdmDecimalTypeReference type, decimal value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00013D20 File Offset: 0x00011F20
		public decimal Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00008D57 File Offset: 0x00006F57
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DecimalConstant;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00009215 File Offset: 0x00007415
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Decimal;
			}
		}

		// Token: 0x04000428 RID: 1064
		private readonly decimal value;
	}
}
