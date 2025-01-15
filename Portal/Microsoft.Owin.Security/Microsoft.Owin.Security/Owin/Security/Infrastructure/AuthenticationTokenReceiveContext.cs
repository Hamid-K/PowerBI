using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x02000020 RID: 32
	public class AuthenticationTokenReceiveContext : BaseContext
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00002F7B File Offset: 0x0000117B
		public AuthenticationTokenReceiveContext(IOwinContext context, ISecureDataFormat<AuthenticationTicket> secureDataFormat, string token)
			: base(context)
		{
			if (secureDataFormat == null)
			{
				throw new ArgumentNullException("secureDataFormat");
			}
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			this._secureDataFormat = secureDataFormat;
			this.Token = token;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002FAE File Offset: 0x000011AE
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002FB6 File Offset: 0x000011B6
		public string Token { get; protected set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002FBF File Offset: 0x000011BF
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002FC7 File Offset: 0x000011C7
		public AuthenticationTicket Ticket { get; protected set; }

		// Token: 0x0600009E RID: 158 RVA: 0x00002FD0 File Offset: 0x000011D0
		public void DeserializeTicket(string protectedData)
		{
			this.Ticket = this._secureDataFormat.Unprotect(protectedData);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002FE4 File Offset: 0x000011E4
		public void SetTicket(AuthenticationTicket ticket)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			this.Ticket = ticket;
		}

		// Token: 0x04000041 RID: 65
		private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;
	}
}
