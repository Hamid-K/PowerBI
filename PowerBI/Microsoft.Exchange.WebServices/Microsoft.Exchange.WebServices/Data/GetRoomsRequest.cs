using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000120 RID: 288
	internal sealed class GetRoomsRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000E1D RID: 3613 RVA: 0x0002BA14 File Offset: 0x0002AA14
		internal GetRoomsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0002BA1D File Offset: 0x0002AA1D
		internal override string GetXmlElementName()
		{
			return "GetRooms";
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0002BA24 File Offset: 0x0002AA24
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.RoomList.WriteToXml(writer, XmlNamespace.Messages, "RoomList");
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0002BA38 File Offset: 0x0002AA38
		internal override string GetResponseXmlElementName()
		{
			return "GetRoomsResponse";
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0002BA40 File Offset: 0x0002AA40
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetRoomsResponse getRoomsResponse = new GetRoomsResponse();
			getRoomsResponse.LoadFromXml(reader, "GetRoomsResponse");
			return getRoomsResponse;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0002BA60 File Offset: 0x0002AA60
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0002BA64 File Offset: 0x0002AA64
		internal GetRoomsResponse Execute()
		{
			GetRoomsResponse getRoomsResponse = (GetRoomsResponse)base.InternalExecute();
			getRoomsResponse.ThrowIfNecessary();
			return getRoomsResponse;
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0002BA84 File Offset: 0x0002AA84
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x0002BA8C File Offset: 0x0002AA8C
		internal EmailAddress RoomList
		{
			get
			{
				return this.roomList;
			}
			set
			{
				this.roomList = value;
			}
		}

		// Token: 0x04000917 RID: 2327
		private EmailAddress roomList;
	}
}
