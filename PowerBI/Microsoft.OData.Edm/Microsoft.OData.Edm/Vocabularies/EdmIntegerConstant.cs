using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012C RID: 300
	public class EdmIntegerConstant : EdmValue, IEdmIntegerConstantExpression, IEdmExpression, IEdmElement, IEdmIntegerValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x000122A0 File Offset: 0x000104A0
		public EdmIntegerConstant(long value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000122B1 File Offset: 0x000104B1
		public EdmIntegerConstant(IEdmPrimitiveTypeReference type, long value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x000122C1 File Offset: 0x000104C1
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00003AFB File Offset: 0x00001CFB
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IntegerConstant;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00002623 File Offset: 0x00000823
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Integer;
			}
		}

		// Token: 0x04000332 RID: 818
		private readonly long value;
	}
}
