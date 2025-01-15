using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000128 RID: 296
	public class EdmDurationConstant : EdmValue, IEdmDurationConstantExpression, IEdmExpression, IEdmElement, IEdmDurationValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x0001220C File Offset: 0x0001040C
		public EdmDurationConstant(TimeSpan value)
			: this(null, value)
		{
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00012216 File Offset: 0x00010416
		public EdmDurationConstant(IEdmTemporalTypeReference type, TimeSpan value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00012226 File Offset: 0x00010426
		public TimeSpan Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00002623 File Offset: 0x00000823
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DurationConstant;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0000462D File Offset: 0x0000282D
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Duration;
			}
		}

		// Token: 0x0400032E RID: 814
		private readonly TimeSpan value;
	}
}
