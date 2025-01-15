using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x020001C5 RID: 453
	public class EdmDateTimeOffsetConstant : EdmValue, IEdmDateTimeOffsetConstantExpression, IEdmExpression, IEdmDateTimeOffsetValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x00019693 File Offset: 0x00017893
		public EdmDateTimeOffsetConstant(DateTimeOffset value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000196A4 File Offset: 0x000178A4
		public EdmDateTimeOffsetConstant(IEdmTemporalTypeReference type, DateTimeOffset value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x000196B4 File Offset: 0x000178B4
		public DateTimeOffset Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x000196BC File Offset: 0x000178BC
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateTimeOffsetConstant;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x000196BF File Offset: 0x000178BF
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.DateTimeOffset;
			}
		}

		// Token: 0x040004AA RID: 1194
		private readonly DateTimeOffset value;
	}
}
