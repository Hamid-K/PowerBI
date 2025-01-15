using System;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x020001D8 RID: 472
	public class EdmIntegerConstant : EdmValue, IEdmIntegerConstantExpression, IEdmExpression, IEdmIntegerValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x00020CFB File Offset: 0x0001EEFB
		public EdmIntegerConstant(long value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00020D0C File Offset: 0x0001EF0C
		public EdmIntegerConstant(IEdmPrimitiveTypeReference type, long value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00020D1C File Offset: 0x0001EF1C
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00020D24 File Offset: 0x0001EF24
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IntegerConstant;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00020D27 File Offset: 0x0001EF27
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Integer;
			}
		}

		// Token: 0x04000547 RID: 1351
		private readonly long value;
	}
}
