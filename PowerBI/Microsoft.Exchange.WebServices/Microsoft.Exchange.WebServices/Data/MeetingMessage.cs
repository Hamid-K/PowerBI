using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200019B RID: 411
	[ServiceObjectDefinition("MeetingMessage")]
	[EditorBrowsable(1)]
	public class MeetingMessage : EmailMessage
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x000362BF File Offset: 0x000352BF
		internal MeetingMessage(ItemAttachment parentAttachment)
			: base(parentAttachment)
		{
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000362C8 File Offset: 0x000352C8
		internal MeetingMessage(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000362D1 File Offset: 0x000352D1
		public new static MeetingMessage Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<MeetingMessage>(id, propertySet);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000362DB File Offset: 0x000352DB
		public new static MeetingMessage Bind(ExchangeService service, ItemId id)
		{
			return MeetingMessage.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000362E9 File Offset: 0x000352E9
		internal override ServiceObjectSchema GetSchema()
		{
			return MeetingMessageSchema.Instance;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000362F0 File Offset: 0x000352F0
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x000362F3 File Offset: 0x000352F3
		public ItemId AssociatedAppointmentId
		{
			get
			{
				return (ItemId)base.PropertyBag[MeetingMessageSchema.AssociatedAppointmentId];
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0003630A File Offset: 0x0003530A
		public bool IsDelegated
		{
			get
			{
				return (bool)base.PropertyBag[MeetingMessageSchema.IsDelegated];
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00036321 File Offset: 0x00035321
		public bool IsOutOfDate
		{
			get
			{
				return (bool)base.PropertyBag[MeetingMessageSchema.IsOutOfDate];
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x00036338 File Offset: 0x00035338
		public bool HasBeenProcessed
		{
			get
			{
				return (bool)base.PropertyBag[MeetingMessageSchema.HasBeenProcessed];
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0003634F File Offset: 0x0003534F
		public bool? IsOrganizer
		{
			get
			{
				return (bool?)base.PropertyBag[MeetingMessageSchema.IsOrganizer];
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x00036366 File Offset: 0x00035366
		public MeetingResponseType ResponseType
		{
			get
			{
				return (MeetingResponseType)base.PropertyBag[MeetingMessageSchema.ResponseType];
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0003637D File Offset: 0x0003537D
		public string ICalUid
		{
			get
			{
				return (string)base.PropertyBag[MeetingMessageSchema.ICalUid];
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x00036394 File Offset: 0x00035394
		public DateTime? ICalRecurrenceId
		{
			get
			{
				return (DateTime?)base.PropertyBag[MeetingMessageSchema.ICalRecurrenceId];
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x000363AB File Offset: 0x000353AB
		public DateTime? ICalDateTimeStamp
		{
			get
			{
				return (DateTime?)base.PropertyBag[MeetingMessageSchema.ICalDateTimeStamp];
			}
		}
	}
}
