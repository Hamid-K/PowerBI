using System;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x02000193 RID: 403
	public class EdmDateTimeOffsetConstant : EdmValue, IEdmDateTimeOffsetConstantExpression, IEdmExpression, IEdmDateTimeOffsetValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x00018434 File Offset: 0x00016634
		public EdmDateTimeOffsetConstant(DateTimeOffset value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00018445 File Offset: 0x00016645
		public EdmDateTimeOffsetConstant(IEdmTemporalTypeReference type, DateTimeOffset value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00018455 File Offset: 0x00016655
		public DateTimeOffset Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0001845D File Offset: 0x0001665D
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateTimeOffsetConstant;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00018460 File Offset: 0x00016660
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.DateTimeOffset;
			}
		}

		// Token: 0x04000459 RID: 1113
		private readonly DateTimeOffset value;
	}
}
