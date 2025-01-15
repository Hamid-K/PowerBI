using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x020001D5 RID: 469
	public class EdmGuidConstant : EdmValue, IEdmGuidConstantExpression, IEdmExpression, IEdmGuidValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x00019A92 File Offset: 0x00017C92
		public EdmGuidConstant(Guid value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00019AA3 File Offset: 0x00017CA3
		public EdmGuidConstant(IEdmPrimitiveTypeReference type, Guid value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00019AB3 File Offset: 0x00017CB3
		public Guid Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00019ABB File Offset: 0x00017CBB
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.GuidConstant;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00019ABE File Offset: 0x00017CBE
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Guid;
			}
		}

		// Token: 0x040004C4 RID: 1220
		private readonly Guid value;
	}
}
