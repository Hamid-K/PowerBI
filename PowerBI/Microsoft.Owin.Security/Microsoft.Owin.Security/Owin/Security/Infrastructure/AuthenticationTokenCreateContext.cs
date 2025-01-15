using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x0200001E RID: 30
	public class AuthenticationTokenCreateContext : BaseContext
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00002DB7 File Offset: 0x00000FB7
		public AuthenticationTokenCreateContext(IOwinContext context, ISecureDataFormat<AuthenticationTicket> secureDataFormat, AuthenticationTicket ticket)
			: base(context)
		{
			if (secureDataFormat == null)
			{
				throw new ArgumentNullException("secureDataFormat");
			}
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			this._secureDataFormat = secureDataFormat;
			this.Ticket = ticket;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002DEA File Offset: 0x00000FEA
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002DF2 File Offset: 0x00000FF2
		public string Token { get; protected set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002DFB File Offset: 0x00000FFB
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002E03 File Offset: 0x00001003
		public AuthenticationTicket Ticket { get; protected set; }

		// Token: 0x0600008A RID: 138 RVA: 0x00002E0C File Offset: 0x0000100C
		public string SerializeTicket()
		{
			return this._secureDataFormat.Protect(this.Ticket);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002E1F File Offset: 0x0000101F
		public void SetToken(string tokenValue)
		{
			if (tokenValue == null)
			{
				throw new ArgumentNullException("tokenValue");
			}
			this.Token = tokenValue;
		}

		// Token: 0x0400003A RID: 58
		private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;
	}
}
