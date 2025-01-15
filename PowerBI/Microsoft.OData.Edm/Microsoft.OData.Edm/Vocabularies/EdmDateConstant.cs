using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000125 RID: 293
	public class EdmDateConstant : EdmValue, IEdmDateConstantExpression, IEdmExpression, IEdmElement, IEdmDateValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x0600079A RID: 1946 RVA: 0x0001219B File Offset: 0x0001039B
		public EdmDateConstant(Date value)
			: this(null, value)
		{
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000121A5 File Offset: 0x000103A5
		public EdmDateConstant(IEdmPrimitiveTypeReference type, Date value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x000121B5 File Offset: 0x000103B5
		public Date Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x000121BD File Offset: 0x000103BD
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateConstant;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00011F9C File Offset: 0x0001019C
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Date;
			}
		}

		// Token: 0x0400032B RID: 811
		private readonly Date value;
	}
}
