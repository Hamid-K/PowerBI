using System;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.Owin.Security.DataHandler
{
	// Token: 0x0200002C RID: 44
	public class TicketDataFormat : SecureDataFormat<AuthenticationTicket>
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000035F0 File Offset: 0x000017F0
		public TicketDataFormat(IDataProtector protector)
			: base(DataSerializers.Ticket, protector, TextEncodings.Base64Url)
		{
		}
	}
}
