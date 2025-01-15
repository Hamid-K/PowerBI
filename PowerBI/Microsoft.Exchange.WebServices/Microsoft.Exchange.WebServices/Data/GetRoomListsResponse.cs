using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000171 RID: 369
	internal sealed class GetRoomListsResponse : ServiceResponse
	{
		// Token: 0x060010D1 RID: 4305 RVA: 0x000315B1 File Offset: 0x000305B1
		internal GetRoomListsResponse()
		{
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x000315C4 File Offset: 0x000305C4
		public EmailAddressCollection RoomLists
		{
			get
			{
				return this.roomLists;
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000315CC File Offset: 0x000305CC
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			this.RoomLists.Clear();
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "RoomLists");
			if (!reader.IsEmptyElement)
			{
				reader.Read();
				while (reader.IsStartElement(XmlNamespace.Types, "Address"))
				{
					EmailAddress emailAddress = new EmailAddress();
					emailAddress.LoadFromXml(reader, "Address");
					this.RoomLists.Add(emailAddress);
					reader.Read();
				}
				reader.EnsureCurrentNodeIsEndElement(XmlNamespace.Messages, "RoomLists");
			}
		}

		// Token: 0x040009CA RID: 2506
		private EmailAddressCollection roomLists = new EmailAddressCollection();
	}
}
