using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Expressions
{
	// Token: 0x020001A0 RID: 416
	public class EdmAssertTypeExpression : EdmElement, IEdmAssertTypeExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x000187A8 File Offset: 0x000169A8
		public EdmAssertTypeExpression(IEdmExpression operand, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(operand, "operand");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.operand = operand;
			this.type = type;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x000187D6 File Offset: 0x000169D6
		public IEdmExpression Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x000187DE File Offset: 0x000169DE
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000187E6 File Offset: 0x000169E6
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.AssertType;
			}
		}

		// Token: 0x04000470 RID: 1136
		private readonly IEdmExpression operand;

		// Token: 0x04000471 RID: 1137
		private readonly IEdmTypeReference type;
	}
}
