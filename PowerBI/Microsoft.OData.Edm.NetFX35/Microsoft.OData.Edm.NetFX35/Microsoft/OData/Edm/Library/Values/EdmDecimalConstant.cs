using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x020001C6 RID: 454
	public class EdmDecimalConstant : EdmValue, IEdmDecimalConstantExpression, IEdmExpression, IEdmDecimalValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x000196C2 File Offset: 0x000178C2
		public EdmDecimalConstant(decimal value)
			: this(null, value)
		{
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000196CC File Offset: 0x000178CC
		public EdmDecimalConstant(IEdmDecimalTypeReference type, decimal value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x000196DC File Offset: 0x000178DC
		public decimal Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x000196E4 File Offset: 0x000178E4
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DecimalConstant;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x000196E7 File Offset: 0x000178E7
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Decimal;
			}
		}

		// Token: 0x040004AB RID: 1195
		private readonly decimal value;
	}
}
