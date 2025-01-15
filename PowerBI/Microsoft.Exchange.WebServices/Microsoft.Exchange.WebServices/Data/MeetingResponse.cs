using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200019E RID: 414
	[ServiceObjectDefinition("MeetingResponse")]
	public class MeetingResponse : MeetingMessage
	{
		// Token: 0x060013CB RID: 5067 RVA: 0x000368FE File Offset: 0x000358FE
		internal MeetingResponse(ItemAttachment parentAttachment)
			: base(parentAttachment)
		{
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00036907 File Offset: 0x00035907
		internal MeetingResponse(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00036910 File Offset: 0x00035910
		public new static MeetingResponse Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<MeetingResponse>(id, propertySet);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0003691A File Offset: 0x0003591A
		public new static MeetingResponse Bind(ExchangeService service, ItemId id)
		{
			return MeetingResponse.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00036928 File Offset: 0x00035928
		internal override ServiceObjectSchema GetSchema()
		{
			return MeetingResponseSchema.Instance;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0003692F File Offset: 0x0003592F
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00036932 File Offset: 0x00035932
		public DateTime Start
		{
			get
			{
				return (DateTime)base.PropertyBag[MeetingResponseSchema.Start];
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00036949 File Offset: 0x00035949
		public DateTime End
		{
			get
			{
				return (DateTime)base.PropertyBag[MeetingResponseSchema.End];
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00036960 File Offset: 0x00035960
		public string Location
		{
			get
			{
				return (string)base.PropertyBag[MeetingResponseSchema.Location];
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00036977 File Offset: 0x00035977
		public Recurrence Recurrence
		{
			get
			{
				return (Recurrence)base.PropertyBag[AppointmentSchema.Recurrence];
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0003698E File Offset: 0x0003598E
		public DateTime ProposedStart
		{
			get
			{
				return (DateTime)base.PropertyBag[MeetingResponseSchema.ProposedStart];
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x000369A5 File Offset: 0x000359A5
		public DateTime ProposedEnd
		{
			get
			{
				return (DateTime)base.PropertyBag[MeetingResponseSchema.ProposedEnd];
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x000369BC File Offset: 0x000359BC
		public EnhancedLocation EnhancedLocation
		{
			get
			{
				return (EnhancedLocation)base.PropertyBag[MeetingResponseSchema.EnhancedLocation];
			}
		}
	}
}
