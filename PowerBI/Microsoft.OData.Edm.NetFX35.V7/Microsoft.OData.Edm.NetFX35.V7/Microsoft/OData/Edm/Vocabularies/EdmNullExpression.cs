using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F8 RID: 248
	public class EdmNullExpression : EdmValue, IEdmNullExpression, IEdmExpression, IEdmElement, IEdmNullValue, IEdmValue
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x00013BA1 File Offset: 0x00011DA1
		private EdmNullExpression()
			: base(null)
		{
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00013BAA File Offset: 0x00011DAA
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Null;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00013BAA File Offset: 0x00011DAA
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Null;
			}
		}

		// Token: 0x0400041E RID: 1054
		public static EdmNullExpression Instance = new EdmNullExpression();
	}
}
