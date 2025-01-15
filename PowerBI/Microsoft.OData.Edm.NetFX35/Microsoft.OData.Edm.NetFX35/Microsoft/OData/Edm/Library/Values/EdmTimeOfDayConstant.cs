using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x02000215 RID: 533
	public class EdmTimeOfDayConstant : EdmValue, IEdmTimeOfDayConstantExpression, IEdmExpression, IEdmTimeOfDayValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x00022F52 File Offset: 0x00021152
		public EdmTimeOfDayConstant(TimeOfDay value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00022F63 File Offset: 0x00021163
		public EdmTimeOfDayConstant(IEdmTemporalTypeReference type, TimeOfDay value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00022F73 File Offset: 0x00021173
		public TimeOfDay Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00022F7B File Offset: 0x0002117B
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.TimeOfDayConstant;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00022F7F File Offset: 0x0002117F
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.TimeOfDay;
			}
		}

		// Token: 0x040005C4 RID: 1476
		private readonly TimeOfDay value;
	}
}
