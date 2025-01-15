using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200011F RID: 287
	internal sealed class GetRoomListsRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000E16 RID: 3606 RVA: 0x0002B9B5 File Offset: 0x0002A9B5
		internal GetRoomListsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0002B9BE File Offset: 0x0002A9BE
		internal override string GetXmlElementName()
		{
			return "GetRoomLists";
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0002B9C5 File Offset: 0x0002A9C5
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0002B9C7 File Offset: 0x0002A9C7
		internal override string GetResponseXmlElementName()
		{
			return "GetRoomListsResponse";
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0002B9D0 File Offset: 0x0002A9D0
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetRoomListsResponse getRoomListsResponse = new GetRoomListsResponse();
			getRoomListsResponse.LoadFromXml(reader, "GetRoomListsResponse");
			return getRoomListsResponse;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0002B9F0 File Offset: 0x0002A9F0
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0002B9F4 File Offset: 0x0002A9F4
		internal GetRoomListsResponse Execute()
		{
			GetRoomListsResponse getRoomListsResponse = (GetRoomListsResponse)base.InternalExecute();
			getRoomListsResponse.ThrowIfNecessary();
			return getRoomListsResponse;
		}
	}
}
