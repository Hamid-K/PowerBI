using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000130 RID: 304
	public class EdmTimeOfDayConstant : EdmValue, IEdmTimeOfDayConstantExpression, IEdmExpression, IEdmElement, IEdmTimeOfDayValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x060007D2 RID: 2002 RVA: 0x000124C7 File Offset: 0x000106C7
		public EdmTimeOfDayConstant(TimeOfDay value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000124D8 File Offset: 0x000106D8
		public EdmTimeOfDayConstant(IEdmTemporalTypeReference type, TimeOfDay value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x000124E8 File Offset: 0x000106E8
		public TimeOfDay Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x000124F0 File Offset: 0x000106F0
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.TimeOfDayConstant;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00011EB4 File Offset: 0x000100B4
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.TimeOfDay;
			}
		}

		// Token: 0x04000339 RID: 825
		private readonly TimeOfDay value;
	}
}
