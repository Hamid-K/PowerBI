using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200313E RID: 12606
	[Serializable]
	internal class DateTimeValueRestriction : SimpleValueRestriction<DateTime, DateTimeValue>
	{
		// Token: 0x1700998B RID: 39307
		// (get) Token: 0x0601B557 RID: 111959 RVA: 0x00376571 File Offset: 0x00374771
		protected override DateTime MinValue
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x1700998C RID: 39308
		// (get) Token: 0x0601B558 RID: 111960 RVA: 0x00247E10 File Offset: 0x00246010
		protected override DateTime MaxValue
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x1700998D RID: 39309
		// (get) Token: 0x0601B55A RID: 111962 RVA: 0x000E78AA File Offset: 0x000E5AAA
		// (set) Token: 0x0601B55B RID: 111963 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.DateTime;
			}
			set
			{
			}
		}
	}
}
