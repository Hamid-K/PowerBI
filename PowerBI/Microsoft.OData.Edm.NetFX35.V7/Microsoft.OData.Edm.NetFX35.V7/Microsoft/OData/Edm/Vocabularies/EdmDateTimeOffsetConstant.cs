using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000119 RID: 281
	public class EdmDateTimeOffsetConstant : EdmValue, IEdmDateTimeOffsetConstantExpression, IEdmExpression, IEdmElement, IEdmDateTimeOffsetValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x00013CDD File Offset: 0x00011EDD
		public EdmDateTimeOffsetConstant(DateTimeOffset value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00013CEE File Offset: 0x00011EEE
		public EdmDateTimeOffsetConstant(IEdmTemporalTypeReference type, DateTimeOffset value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00013CFE File Offset: 0x00011EFE
		public DateTimeOffset Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00009097 File Offset: 0x00007297
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateTimeOffsetConstant;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00008D57 File Offset: 0x00006F57
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.DateTimeOffset;
			}
		}

		// Token: 0x04000427 RID: 1063
		private readonly DateTimeOffset value;
	}
}
