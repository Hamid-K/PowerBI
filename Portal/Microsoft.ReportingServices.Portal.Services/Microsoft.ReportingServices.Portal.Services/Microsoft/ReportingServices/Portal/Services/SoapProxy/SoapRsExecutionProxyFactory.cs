using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.SoapProxy;

namespace Microsoft.ReportingServices.Portal.Services.SoapProxy
{
	// Token: 0x0200002C RID: 44
	internal sealed class SoapRsExecutionProxyFactory : ISoapRSExecutionProxyFactory
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000E230 File Offset: 0x0000C430
		internal SoapRsExecutionProxyFactory(IPortalConfigurationManager portalConfigurationManager)
		{
			this.portalConfigurationManager = portalConfigurationManager;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000E23F File Offset: 0x0000C43F
		public ISoapRSExecutionProxy CreateSoapRSExecutionProxy(IPrincipal userPrincipal)
		{
			return new SoapRSExecutionProxy(this.portalConfigurationManager, userPrincipal);
		}

		// Token: 0x04000094 RID: 148
		private readonly IPortalConfigurationManager portalConfigurationManager;
	}
}
