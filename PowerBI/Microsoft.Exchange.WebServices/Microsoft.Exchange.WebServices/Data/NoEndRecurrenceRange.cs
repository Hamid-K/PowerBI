using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000BC RID: 188
	internal sealed class NoEndRecurrenceRange : RecurrenceRange
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x0001BC1F File Offset: 0x0001AC1F
		public NoEndRecurrenceRange()
		{
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001BC27 File Offset: 0x0001AC27
		public NoEndRecurrenceRange(DateTime startDate)
			: base(startDate)
		{
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0001BC30 File Offset: 0x0001AC30
		internal override string XmlElementName
		{
			get
			{
				return "NoEndRecurrence";
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001BC37 File Offset: 0x0001AC37
		internal override void SetupRecurrence(Recurrence recurrence)
		{
			base.SetupRecurrence(recurrence);
			recurrence.NeverEnds();
		}
	}
}
