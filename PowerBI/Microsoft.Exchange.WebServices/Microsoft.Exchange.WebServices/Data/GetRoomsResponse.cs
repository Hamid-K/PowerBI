using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000172 RID: 370
	internal sealed class GetRoomsResponse : ServiceResponse
	{
		// Token: 0x060010D4 RID: 4308 RVA: 0x00031645 File Offset: 0x00030645
		internal GetRoomsResponse()
		{
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00031658 File Offset: 0x00030658
		public Collection<EmailAddress> Rooms
		{
			get
			{
				return this.rooms;
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00031660 File Offset: 0x00030660
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			this.Rooms.Clear();
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "Rooms");
			if (!reader.IsEmptyElement)
			{
				reader.Read();
				while (reader.IsStartElement(XmlNamespace.Types, "Room"))
				{
					reader.Read();
					EmailAddress emailAddress = new EmailAddress();
					emailAddress.LoadFromXml(reader, "Id");
					this.Rooms.Add(emailAddress);
					reader.ReadEndElement(XmlNamespace.Types, "Room");
					reader.Read();
				}
				reader.EnsureCurrentNodeIsEndElement(XmlNamespace.Messages, "Rooms");
			}
		}

		// Token: 0x040009CB RID: 2507
		private Collection<EmailAddress> rooms = new Collection<EmailAddress>();
	}
}
