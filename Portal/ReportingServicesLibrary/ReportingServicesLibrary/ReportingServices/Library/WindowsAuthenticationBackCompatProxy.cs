using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001A RID: 26
	internal sealed class WindowsAuthenticationBackCompatProxy : AuthenticationExtensionBackCompatProxy, IWindowsAuthenticationExtension2, IAuthenticationExtension2, IExtension
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00004F0D File Offset: 0x0000310D
		public WindowsAuthenticationBackCompatProxy(IWindowsAuthenticationExtension objectToProxy)
			: base(objectToProxy)
		{
			this.m_proxiedObject = objectToProxy;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004F1D File Offset: 0x0000311D
		public byte[] PrincipalNameToSid(string userName)
		{
			return this.m_proxiedObject.PrincipalNameToSid(userName);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004F2B File Offset: 0x0000312B
		public string SidToPrincipalName(byte[] sid)
		{
			return this.m_proxiedObject.SidToPrincipalName(sid);
		}

		// Token: 0x040000A6 RID: 166
		private readonly IWindowsAuthenticationExtension m_proxiedObject;
	}
}
