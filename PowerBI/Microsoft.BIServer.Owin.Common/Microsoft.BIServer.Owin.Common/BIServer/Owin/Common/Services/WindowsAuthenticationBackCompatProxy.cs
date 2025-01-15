using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x0200000E RID: 14
	internal sealed class WindowsAuthenticationBackCompatProxy : AuthenticationExtensionBackCompatProxy, IWindowsAuthenticationExtension2, IAuthenticationExtension2, IExtension
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000027E5 File Offset: 0x000009E5
		public WindowsAuthenticationBackCompatProxy(IWindowsAuthenticationExtension objectToProxy)
			: base(objectToProxy)
		{
			this.m_proxiedObject = objectToProxy;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027F5 File Offset: 0x000009F5
		public byte[] PrincipalNameToSid(string userName)
		{
			return this.m_proxiedObject.PrincipalNameToSid(userName);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002803 File Offset: 0x00000A03
		public string SidToPrincipalName(byte[] sid)
		{
			return this.m_proxiedObject.SidToPrincipalName(sid);
		}

		// Token: 0x04000038 RID: 56
		private readonly IWindowsAuthenticationExtension m_proxiedObject;
	}
}
