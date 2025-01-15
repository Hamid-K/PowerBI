using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000118 RID: 280
	public class EdmDateConstant : EdmValue, IEdmDateConstantExpression, IEdmExpression, IEdmElement, IEdmDateValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x00013CB7 File Offset: 0x00011EB7
		public EdmDateConstant(Date value)
			: this(null, value)
		{
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00013CC1 File Offset: 0x00011EC1
		public EdmDateConstant(IEdmPrimitiveTypeReference type, Date value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00013CD1 File Offset: 0x00011ED1
		public Date Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00013CD9 File Offset: 0x00011ED9
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateConstant;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00013AB8 File Offset: 0x00011CB8
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Date;
			}
		}

		// Token: 0x04000426 RID: 1062
		private readonly Date value;
	}
}
