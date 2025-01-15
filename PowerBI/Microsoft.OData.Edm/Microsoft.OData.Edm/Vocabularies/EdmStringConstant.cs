using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012E RID: 302
	public class EdmStringConstant : EdmValue, IEdmStringConstantExpression, IEdmExpression, IEdmElement, IEdmStringValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x0001234A File Offset: 0x0001054A
		public EdmStringConstant(string value)
			: this(null, value)
		{
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00012354 File Offset: 0x00010554
		public EdmStringConstant(IEdmStringTypeReference type, string value)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<string>(value, "value");
			this.value = value;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00012370 File Offset: 0x00010570
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00002715 File Offset: 0x00000915
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.StringConstant;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001212F File Offset: 0x0001032F
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.String;
			}
		}

		// Token: 0x04000335 RID: 821
		private readonly string value;
	}
}
