using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x02000204 RID: 516
	public class EdmIntegerConstant : EdmValue, IEdmIntegerConstantExpression, IEdmExpression, IEdmIntegerValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x000227A3 File Offset: 0x000209A3
		public EdmIntegerConstant(long value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000227B4 File Offset: 0x000209B4
		public EdmIntegerConstant(IEdmPrimitiveTypeReference type, long value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x000227C4 File Offset: 0x000209C4
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x000227CC File Offset: 0x000209CC
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IntegerConstant;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x000227CF File Offset: 0x000209CF
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Integer;
			}
		}

		// Token: 0x0400058E RID: 1422
		private readonly long value;
	}
}
