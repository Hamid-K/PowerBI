using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000126 RID: 294
	public class EdmDateTimeOffsetConstant : EdmValue, IEdmDateTimeOffsetConstantExpression, IEdmExpression, IEdmElement, IEdmDateTimeOffsetValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x000121C1 File Offset: 0x000103C1
		public EdmDateTimeOffsetConstant(DateTimeOffset value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000121D2 File Offset: 0x000103D2
		public EdmDateTimeOffsetConstant(IEdmTemporalTypeReference type, DateTimeOffset value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x000121E2 File Offset: 0x000103E2
		public DateTimeOffset Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0000268B File Offset: 0x0000088B
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateTimeOffsetConstant;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x000039FB File Offset: 0x00001BFB
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.DateTimeOffset;
			}
		}

		// Token: 0x0400032C RID: 812
		private readonly DateTimeOffset value;
	}
}
