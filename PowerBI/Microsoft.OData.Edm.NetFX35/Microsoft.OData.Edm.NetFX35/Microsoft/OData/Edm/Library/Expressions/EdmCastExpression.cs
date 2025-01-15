using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001D3 RID: 467
	public class EdmCastExpression : EdmElement, IEdmCastExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x00019A28 File Offset: 0x00017C28
		public EdmCastExpression(IEdmExpression operand, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(operand, "operand");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.operand = operand;
			this.type = type;
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00019A56 File Offset: 0x00017C56
		public IEdmExpression Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00019A5E File Offset: 0x00017C5E
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00019A66 File Offset: 0x00017C66
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Cast;
			}
		}

		// Token: 0x040004C1 RID: 1217
		private readonly IEdmExpression operand;

		// Token: 0x040004C2 RID: 1218
		private readonly IEdmTypeReference type;
	}
}
