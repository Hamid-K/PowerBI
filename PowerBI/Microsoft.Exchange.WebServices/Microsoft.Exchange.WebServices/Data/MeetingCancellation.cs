using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200019C RID: 412
	[ServiceObjectDefinition("MeetingCancellation")]
	public class MeetingCancellation : MeetingMessage
	{
		// Token: 0x06001389 RID: 5001 RVA: 0x000363C2 File Offset: 0x000353C2
		internal MeetingCancellation(ItemAttachment parentAttachment)
			: base(parentAttachment)
		{
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000363CB File Offset: 0x000353CB
		internal MeetingCancellation(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000363D4 File Offset: 0x000353D4
		public new static MeetingCancellation Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<MeetingCancellation>(id, propertySet);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000363DE File Offset: 0x000353DE
		public new static MeetingCancellation Bind(ExchangeService service, ItemId id)
		{
			return MeetingCancellation.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000363EC File Offset: 0x000353EC
		internal override ServiceObjectSchema GetSchema()
		{
			return MeetingCancellationSchema.Instance;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000363F4 File Offset: 0x000353F4
		public CalendarActionResults RemoveMeetingFromCalendar()
		{
			return new CalendarActionResults(new RemoveFromCalendar(this).InternalCreate(null, default(MessageDisposition?)));
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0003641B File Offset: 0x0003541B
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0003641E File Offset: 0x0003541E
		public DateTime Start
		{
			get
			{
				return (DateTime)base.PropertyBag[MeetingCancellationSchema.Start];
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x00036435 File Offset: 0x00035435
		public DateTime End
		{
			get
			{
				return (DateTime)base.PropertyBag[MeetingCancellationSchema.End];
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x0003644C File Offset: 0x0003544C
		public string Location
		{
			get
			{
				return (string)base.PropertyBag[MeetingCancellationSchema.Location];
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00036463 File Offset: 0x00035463
		public Recurrence Recurrence
		{
			get
			{
				return (Recurrence)base.PropertyBag[AppointmentSchema.Recurrence];
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0003647A File Offset: 0x0003547A
		public EnhancedLocation EnhancedLocation
		{
			get
			{
				return (EnhancedLocation)base.PropertyBag[MeetingCancellationSchema.EnhancedLocation];
			}
		}
	}
}
