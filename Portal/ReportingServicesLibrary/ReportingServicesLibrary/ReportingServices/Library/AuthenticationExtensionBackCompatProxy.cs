using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000019 RID: 25
	internal class AuthenticationExtensionBackCompatProxy : IAuthenticationExtension2, IExtension
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00004EA7 File Offset: 0x000030A7
		public AuthenticationExtensionBackCompatProxy(IAuthenticationExtension objectToProxy)
		{
			this.m_proxiedObject = objectToProxy;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004EB6 File Offset: 0x000030B6
		public void SetConfiguration(string configuration)
		{
			this.m_proxiedObject.SetConfiguration(configuration);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004EC4 File Offset: 0x000030C4
		public string LocalizedName
		{
			get
			{
				return this.m_proxiedObject.LocalizedName;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004ED1 File Offset: 0x000030D1
		public void GetUserInfo(out IIdentity userIdentity, out IntPtr userId)
		{
			this.m_proxiedObject.GetUserInfo(out userIdentity, out userId);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004EE0 File Offset: 0x000030E0
		public void GetUserInfo(IRSRequestContext requestContext, out IIdentity userIdentity, out IntPtr userId)
		{
			this.m_proxiedObject.GetUserInfo(out userIdentity, out userId);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004EEF File Offset: 0x000030EF
		public bool IsValidPrincipalName(string principalName)
		{
			return this.m_proxiedObject.IsValidPrincipalName(principalName);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004EFD File Offset: 0x000030FD
		public bool LogonUser(string userName, string password, string authority)
		{
			return this.m_proxiedObject.LogonUser(userName, password, authority);
		}

		// Token: 0x040000A5 RID: 165
		private readonly IAuthenticationExtension m_proxiedObject;
	}
}
