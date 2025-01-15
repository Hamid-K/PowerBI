using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200011B RID: 283
	public class EdmDurationConstant : EdmValue, IEdmDurationConstantExpression, IEdmExpression, IEdmElement, IEdmDurationValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x00013D28 File Offset: 0x00011F28
		public EdmDurationConstant(TimeSpan value)
			: this(null, value)
		{
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00013D32 File Offset: 0x00011F32
		public EdmDurationConstant(IEdmTemporalTypeReference type, TimeSpan value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00013D42 File Offset: 0x00011F42
		public TimeSpan Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00013D4A File Offset: 0x00011F4A
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DurationConstant;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0000BFE1 File Offset: 0x0000A1E1
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Duration;
			}
		}

		// Token: 0x04000429 RID: 1065
		private readonly TimeSpan value;
	}
}
