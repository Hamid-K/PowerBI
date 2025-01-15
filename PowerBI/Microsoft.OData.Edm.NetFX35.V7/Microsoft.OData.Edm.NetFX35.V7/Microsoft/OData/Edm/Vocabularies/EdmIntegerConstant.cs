using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200011F RID: 287
	public class EdmIntegerConstant : EdmValue, IEdmIntegerConstantExpression, IEdmExpression, IEdmElement, IEdmIntegerValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x0600077C RID: 1916 RVA: 0x00013DC0 File Offset: 0x00011FC0
		public EdmIntegerConstant(long value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00013DD1 File Offset: 0x00011FD1
		public EdmIntegerConstant(IEdmPrimitiveTypeReference type, long value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00013DE1 File Offset: 0x00011FE1
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0000C558 File Offset: 0x0000A758
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IntegerConstant;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00013D4A File Offset: 0x00011F4A
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Integer;
			}
		}

		// Token: 0x0400042D RID: 1069
		private readonly long value;
	}
}
