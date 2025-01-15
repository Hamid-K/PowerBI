using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200011E RID: 286
	public class EdmGuidConstant : EdmValue, IEdmGuidConstantExpression, IEdmExpression, IEdmElement, IEdmGuidValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x00013D97 File Offset: 0x00011F97
		public EdmGuidConstant(Guid value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00013DA8 File Offset: 0x00011FA8
		public EdmGuidConstant(IEdmPrimitiveTypeReference type, Guid value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00013DB8 File Offset: 0x00011FB8
		public Guid Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x000092ED File Offset: 0x000074ED
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.GuidConstant;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0000C876 File Offset: 0x0000AA76
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Guid;
			}
		}

		// Token: 0x0400042C RID: 1068
		private readonly Guid value;
	}
}
