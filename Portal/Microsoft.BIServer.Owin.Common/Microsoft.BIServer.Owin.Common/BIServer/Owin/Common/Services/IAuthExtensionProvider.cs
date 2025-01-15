using System;
using Microsoft.BIServer.Configuration;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x02000010 RID: 16
	public interface IAuthExtensionProvider
	{
		// Token: 0x06000041 RID: 65
		IAuthenticationExtension2 GetAuthenticationExtension(Microsoft.BIServer.Configuration.AuthenticationType authType);

		// Token: 0x06000042 RID: 66
		bool IsAuthExtensionInBackCompatMode(Microsoft.BIServer.Configuration.AuthenticationType authType);
	}
}
