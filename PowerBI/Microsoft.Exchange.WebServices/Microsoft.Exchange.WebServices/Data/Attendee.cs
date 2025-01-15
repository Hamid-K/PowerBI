using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200003B RID: 59
	public sealed class Attendee : EmailAddress
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000B256 File Offset: 0x0000A256
		public Attendee()
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B25E File Offset: 0x0000A25E
		public Attendee(string smtpAddress)
			: base(smtpAddress)
		{
			EwsUtilities.ValidateParam(smtpAddress, "smtpAddress");
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B272 File Offset: 0x0000A272
		public Attendee(string name, string smtpAddress)
			: base(name, smtpAddress)
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B27C File Offset: 0x0000A27C
		public Attendee(string name, string smtpAddress, string routingType)
			: base(name, smtpAddress, routingType)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B287 File Offset: 0x0000A287
		public Attendee(EmailAddress mailbox)
			: base(mailbox)
		{
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B290 File Offset: 0x0000A290
		public MeetingResponseType? ResponseType
		{
			get
			{
				return this.responseType;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000B298 File Offset: 0x0000A298
		public DateTime? LastResponseTime
		{
			get
			{
				return this.lastResponseTime;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B2A0 File Offset: 0x0000A2A0
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Mailbox")
				{
					this.LoadFromXml(reader, reader.LocalName);
					return true;
				}
				if (localName == "ResponseType")
				{
					this.responseType = new MeetingResponseType?(reader.ReadElementValue<MeetingResponseType>());
					return true;
				}
				if (localName == "LastResponseTime")
				{
					this.lastResponseTime = reader.ReadElementValueAsDateTime();
					return true;
				}
			}
			return base.TryReadElementFromXml(reader);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B317 File Offset: 0x0000A317
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(base.Namespace, "Mailbox");
			base.WriteElementsToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x04000133 RID: 307
		private MeetingResponseType? responseType;

		// Token: 0x04000134 RID: 308
		private DateTime? lastResponseTime;
	}
}
