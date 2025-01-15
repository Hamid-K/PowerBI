using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012A RID: 298
	public class EdmFloatingConstant : EdmValue, IEdmFloatingConstantExpression, IEdmExpression, IEdmElement, IEdmFloatingValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x00012255 File Offset: 0x00010455
		public EdmFloatingConstant(double value)
			: this(null, value)
		{
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001225F File Offset: 0x0001045F
		public EdmFloatingConstant(IEdmPrimitiveTypeReference type, double value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0001226F File Offset: 0x0001046F
		public double Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0000480B File Offset: 0x00002A0B
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FloatingConstant;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00003AFB File Offset: 0x00001CFB
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Floating;
			}
		}

		// Token: 0x04000330 RID: 816
		private readonly double value;
	}
}
