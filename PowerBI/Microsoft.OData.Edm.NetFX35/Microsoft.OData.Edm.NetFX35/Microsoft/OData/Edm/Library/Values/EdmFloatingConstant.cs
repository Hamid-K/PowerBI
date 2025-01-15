using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x020001D4 RID: 468
	public class EdmFloatingConstant : EdmValue, IEdmFloatingConstantExpression, IEdmExpression, IEdmFloatingValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x00019A6A File Offset: 0x00017C6A
		public EdmFloatingConstant(double value)
			: this(null, value)
		{
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00019A74 File Offset: 0x00017C74
		public EdmFloatingConstant(IEdmPrimitiveTypeReference type, double value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00019A84 File Offset: 0x00017C84
		public double Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00019A8C File Offset: 0x00017C8C
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FloatingConstant;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00019A8F File Offset: 0x00017C8F
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Floating;
			}
		}

		// Token: 0x040004C3 RID: 1219
		private readonly double value;
	}
}
