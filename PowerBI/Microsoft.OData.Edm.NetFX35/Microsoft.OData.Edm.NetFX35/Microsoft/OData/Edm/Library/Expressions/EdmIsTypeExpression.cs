using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001CD RID: 461
	public class EdmIsTypeExpression : EdmElement, IEdmIsTypeExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0001984C File Offset: 0x00017A4C
		public EdmIsTypeExpression(IEdmExpression operand, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(operand, "operand");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.operand = operand;
			this.type = type;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001987A File Offset: 0x00017A7A
		public IEdmExpression Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00019882 File Offset: 0x00017A82
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0001988A File Offset: 0x00017A8A
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IsType;
			}
		}

		// Token: 0x040004B5 RID: 1205
		private readonly IEdmExpression operand;

		// Token: 0x040004B6 RID: 1206
		private readonly IEdmTypeReference type;
	}
}
