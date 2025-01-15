using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001BF RID: 447
	[Schema]
	public class MeetingCancellationSchema : MeetingMessageSchema
	{
		// Token: 0x060014F8 RID: 5368 RVA: 0x0003AAA4 File Offset: 0x00039AA4
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(MeetingCancellationSchema.Start);
			base.RegisterProperty(MeetingCancellationSchema.End);
			base.RegisterProperty(MeetingCancellationSchema.Location);
			base.RegisterProperty(MeetingCancellationSchema.Recurrence);
			base.RegisterProperty(MeetingCancellationSchema.AppointmentType);
			base.RegisterProperty(MeetingCancellationSchema.EnhancedLocation);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0003AAF9 File Offset: 0x00039AF9
		internal MeetingCancellationSchema()
		{
		}

		// Token: 0x04000C49 RID: 3145
		public static readonly PropertyDefinition Start = AppointmentSchema.Start;

		// Token: 0x04000C4A RID: 3146
		public static readonly PropertyDefinition End = AppointmentSchema.End;

		// Token: 0x04000C4B RID: 3147
		public static readonly PropertyDefinition Location = AppointmentSchema.Location;

		// Token: 0x04000C4C RID: 3148
		public static readonly PropertyDefinition AppointmentType = AppointmentSchema.AppointmentType;

		// Token: 0x04000C4D RID: 3149
		public static readonly PropertyDefinition Recurrence = AppointmentSchema.Recurrence;

		// Token: 0x04000C4E RID: 3150
		public static readonly PropertyDefinition EnhancedLocation = AppointmentSchema.EnhancedLocation;

		// Token: 0x04000C4F RID: 3151
		internal new static readonly MeetingCancellationSchema Instance = new MeetingCancellationSchema();
	}
}
