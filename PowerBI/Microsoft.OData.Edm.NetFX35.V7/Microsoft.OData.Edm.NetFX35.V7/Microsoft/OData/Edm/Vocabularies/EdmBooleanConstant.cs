using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000116 RID: 278
	public class EdmBooleanConstant : EdmValue, IEdmBooleanConstantExpression, IEdmExpression, IEdmElement, IEdmBooleanValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x00013C7D File Offset: 0x00011E7D
		public EdmBooleanConstant(bool value)
			: this(null, value)
		{
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00013C87 File Offset: 0x00011E87
		public EdmBooleanConstant(IEdmPrimitiveTypeReference type, bool value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00013C97 File Offset: 0x00011E97
		public bool Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00008F68 File Offset: 0x00007168
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BooleanConstant;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00008F68 File Offset: 0x00007168
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Boolean;
			}
		}

		// Token: 0x04000424 RID: 1060
		private readonly bool value;
	}
}
