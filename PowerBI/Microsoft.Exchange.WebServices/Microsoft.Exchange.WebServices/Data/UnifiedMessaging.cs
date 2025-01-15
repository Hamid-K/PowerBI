using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200030B RID: 779
	public sealed class UnifiedMessaging
	{
		// Token: 0x06001BAE RID: 7086 RVA: 0x00049CC6 File Offset: 0x00048CC6
		internal UnifiedMessaging(ExchangeService service)
		{
			this.service = service;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00049CD8 File Offset: 0x00048CD8
		public PhoneCall PlayOnPhone(ItemId itemId, string dialString)
		{
			EwsUtilities.ValidateParam(itemId, "itemId");
			EwsUtilities.ValidateParam(dialString, "dialString");
			PlayOnPhoneResponse playOnPhoneResponse = new PlayOnPhoneRequest(this.service)
			{
				DialString = dialString,
				ItemId = itemId
			}.Execute();
			return new PhoneCall(this.service, playOnPhoneResponse.PhoneCallId);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00049D30 File Offset: 0x00048D30
		internal PhoneCall GetPhoneCallInformation(PhoneCallId id)
		{
			GetPhoneCallResponse getPhoneCallResponse = new GetPhoneCallRequest(this.service)
			{
				Id = id
			}.Execute();
			return getPhoneCallResponse.PhoneCall;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00049D60 File Offset: 0x00048D60
		internal void DisconnectPhoneCall(PhoneCallId id)
		{
			new DisconnectPhoneCallRequest(this.service)
			{
				Id = id
			}.Execute();
		}

		// Token: 0x04001467 RID: 5223
		private ExchangeService service;
	}
}
