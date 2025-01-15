using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F1 RID: 241
	public class EdmNullExpression : EdmValue, IEdmNullExpression, IEdmExpression, IEdmElement, IEdmNullValue, IEdmValue
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x00012085 File Offset: 0x00010285
		private EdmNullExpression()
			: base(null)
		{
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001208E File Offset: 0x0001028E
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Null;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001208E File Offset: 0x0001028E
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Null;
			}
		}

		// Token: 0x04000312 RID: 786
		public static EdmNullExpression Instance = new EdmNullExpression();
	}
}
