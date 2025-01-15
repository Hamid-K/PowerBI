using System;
using System.Security.Principal;

namespace Microsoft.ReportingServices.Portal.Interfaces.SoapProxy
{
	// Token: 0x02000088 RID: 136
	public interface ISoapRSExecutionProxyFactory
	{
		// Token: 0x0600043C RID: 1084
		ISoapRSExecutionProxy CreateSoapRSExecutionProxy(IPrincipal userPrincipal);
	}
}
