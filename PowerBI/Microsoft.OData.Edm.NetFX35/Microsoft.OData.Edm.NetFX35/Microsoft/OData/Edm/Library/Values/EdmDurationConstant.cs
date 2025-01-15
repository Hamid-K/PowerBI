using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x02000116 RID: 278
	public class EdmDurationConstant : EdmValue, IEdmDurationConstantExpression, IEdmExpression, IEdmDurationValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x0000DD68 File Offset: 0x0000BF68
		public EdmDurationConstant(TimeSpan value)
			: this(null, value)
		{
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000DD72 File Offset: 0x0000BF72
		public EdmDurationConstant(IEdmTemporalTypeReference type, TimeSpan value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000DD82 File Offset: 0x0000BF82
		public TimeSpan Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0000DD8A File Offset: 0x0000BF8A
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DurationConstant;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000DD8E File Offset: 0x0000BF8E
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Duration;
			}
		}

		// Token: 0x0400021A RID: 538
		private readonly TimeSpan value;
	}
}
