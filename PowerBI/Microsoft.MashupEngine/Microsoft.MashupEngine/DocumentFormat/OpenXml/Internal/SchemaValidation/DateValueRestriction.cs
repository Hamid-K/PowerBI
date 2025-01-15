using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200313F RID: 12607
	[Serializable]
	internal class DateValueRestriction : SimpleValueRestriction<DateTime, DateTimeValue>
	{
		// Token: 0x1700998E RID: 39310
		// (get) Token: 0x0601B55C RID: 111964 RVA: 0x00376571 File Offset: 0x00374771
		protected override DateTime MinValue
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x1700998F RID: 39311
		// (get) Token: 0x0601B55D RID: 111965 RVA: 0x00247E10 File Offset: 0x00246010
		protected override DateTime MaxValue
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17009990 RID: 39312
		// (get) Token: 0x0601B55F RID: 111967 RVA: 0x000E78B2 File Offset: 0x000E5AB2
		// (set) Token: 0x0601B560 RID: 111968 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Date;
			}
			set
			{
			}
		}
	}
}
