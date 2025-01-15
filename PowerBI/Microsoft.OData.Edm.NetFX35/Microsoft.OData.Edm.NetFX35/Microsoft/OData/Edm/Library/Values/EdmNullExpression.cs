using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x020001BA RID: 442
	public class EdmNullExpression : EdmValue, IEdmNullExpression, IEdmExpression, IEdmNullValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000954 RID: 2388 RVA: 0x000193CC File Offset: 0x000175CC
		private EdmNullExpression()
			: base(null)
		{
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x000193D5 File Offset: 0x000175D5
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Null;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x000193D9 File Offset: 0x000175D9
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Null;
			}
		}

		// Token: 0x04000497 RID: 1175
		public static EdmNullExpression Instance = new EdmNullExpression();
	}
}
