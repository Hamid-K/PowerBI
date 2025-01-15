using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000123 RID: 291
	public class EdmTimeOfDayConstant : EdmValue, IEdmTimeOfDayConstantExpression, IEdmExpression, IEdmElement, IEdmTimeOfDayValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x06000792 RID: 1938 RVA: 0x00013FE7 File Offset: 0x000121E7
		public EdmTimeOfDayConstant(TimeOfDay value)
			: this(null, value)
		{
			this.value = value;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00013FF8 File Offset: 0x000121F8
		public EdmTimeOfDayConstant(IEdmTemporalTypeReference type, TimeOfDay value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00014008 File Offset: 0x00012208
		public TimeOfDay Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00014010 File Offset: 0x00012210
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.TimeOfDayConstant;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x000139D0 File Offset: 0x00011BD0
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.TimeOfDay;
			}
		}

		// Token: 0x04000434 RID: 1076
		private readonly TimeOfDay value;
	}
}
