using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x0200000D RID: 13
	internal class AuthenticationExtensionBackCompatProxy : IAuthenticationExtension2, IExtension
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000277F File Offset: 0x0000097F
		public AuthenticationExtensionBackCompatProxy(IAuthenticationExtension objectToProxy)
		{
			this.m_proxiedObject = objectToProxy;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000278E File Offset: 0x0000098E
		public void SetConfiguration(string configuration)
		{
			this.m_proxiedObject.SetConfiguration(configuration);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000279C File Offset: 0x0000099C
		public string LocalizedName
		{
			get
			{
				return this.m_proxiedObject.LocalizedName;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027A9 File Offset: 0x000009A9
		public void GetUserInfo(out IIdentity userIdentity, out IntPtr userId)
		{
			this.m_proxiedObject.GetUserInfo(out userIdentity, out userId);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027B8 File Offset: 0x000009B8
		public void GetUserInfo(IRSRequestContext requestContext, out IIdentity userIdentity, out IntPtr userId)
		{
			this.m_proxiedObject.GetUserInfo(out userIdentity, out userId);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027C7 File Offset: 0x000009C7
		public bool IsValidPrincipalName(string principalName)
		{
			return this.m_proxiedObject.IsValidPrincipalName(principalName);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027D5 File Offset: 0x000009D5
		public bool LogonUser(string userName, string password, string authority)
		{
			return this.m_proxiedObject.LogonUser(userName, password, authority);
		}

		// Token: 0x04000037 RID: 55
		private readonly IAuthenticationExtension m_proxiedObject;
	}
}
