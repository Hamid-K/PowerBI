using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000123 RID: 291
	public class EdmBooleanConstant : EdmValue, IEdmBooleanConstantExpression, IEdmExpression, IEdmElement, IEdmBooleanValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x06000792 RID: 1938 RVA: 0x00012161 File Offset: 0x00010361
		public EdmBooleanConstant(bool value)
			: this(null, value)
		{
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001216B File Offset: 0x0001036B
		public EdmBooleanConstant(IEdmPrimitiveTypeReference type, bool value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001217B File Offset: 0x0001037B
		public bool Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00002732 File Offset: 0x00000932
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BooleanConstant;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00002732 File Offset: 0x00000932
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Boolean;
			}
		}

		// Token: 0x04000329 RID: 809
		private readonly bool value;
	}
}
