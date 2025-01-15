using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x020001C4 RID: 452
	public class EdmBooleanConstant : EdmValue, IEdmBooleanConstantExpression, IEdmExpression, IEdmBooleanValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x0001966B File Offset: 0x0001786B
		public EdmBooleanConstant(bool value)
			: this(null, value)
		{
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00019675 File Offset: 0x00017875
		public EdmBooleanConstant(IEdmPrimitiveTypeReference type, bool value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00019685 File Offset: 0x00017885
		public bool Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0001968D File Offset: 0x0001788D
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BooleanConstant;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00019690 File Offset: 0x00017890
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Boolean;
			}
		}

		// Token: 0x040004A9 RID: 1193
		private readonly bool value;
	}
}
