using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200011D RID: 285
	public class EdmFloatingConstant : EdmValue, IEdmFloatingConstantExpression, IEdmExpression, IEdmElement, IEdmFloatingValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x00013D75 File Offset: 0x00011F75
		public EdmFloatingConstant(double value)
			: this(null, value)
		{
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00013D7F File Offset: 0x00011F7F
		public EdmFloatingConstant(IEdmPrimitiveTypeReference type, double value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00013D8F File Offset: 0x00011F8F
		public double Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00009215 File Offset: 0x00007415
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FloatingConstant;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0000C558 File Offset: 0x0000A758
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Floating;
			}
		}

		// Token: 0x0400042B RID: 1067
		private readonly double value;
	}
}
