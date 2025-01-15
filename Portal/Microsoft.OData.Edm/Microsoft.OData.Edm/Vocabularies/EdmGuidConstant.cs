using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012B RID: 299
	public class EdmGuidConstant : EdmValue, IEdmGuidConstantExpression, IEdmExpression, IEdmElement, IEdmGuidValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x00012277 File Offset: 0x00010477
		public EdmGuidConstant(Guid value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00012288 File Offset: 0x00010488
		public EdmGuidConstant(IEdmPrimitiveTypeReference type, Guid value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00012298 File Offset: 0x00010498
		public Guid Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00003A59 File Offset: 0x00001C59
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.GuidConstant;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00002715 File Offset: 0x00000915
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Guid;
			}
		}

		// Token: 0x04000331 RID: 817
		private readonly Guid value;
	}
}
