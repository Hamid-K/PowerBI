using System;
using System.Net;
using System.Security.Principal;
using Microsoft.SqlServer.ReportingServices2005.Execution;

namespace Microsoft.ReportingServices.Portal.Services.SoapProxy
{
	// Token: 0x02000028 RID: 40
	internal sealed class SoapRsExecutionConnection : RSExecutionConnection
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000D0CD File Offset: 0x0000B2CD
		public SoapRsExecutionConnection(string reportServerUrl, string reportServerHostName, IIdentity userContext)
			: base(reportServerUrl, EndpointVersion.Sql16)
		{
			this.userContext = userContext;
			this.reportServerHostName = reportServerHostName;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D0E5 File Offset: 0x0000B2E5
		protected override WebRequest GetWebRequest(Uri uri)
		{
			HttpWebRequest httpWebRequest = base.GetWebRequest(uri) as HttpWebRequest;
			SoapAuthenticationHelper.PrepareWebRequest(httpWebRequest, this.userContext, this.reportServerHostName);
			return httpWebRequest;
		}

		// Token: 0x0400008F RID: 143
		private readonly IIdentity userContext;

		// Token: 0x04000090 RID: 144
		private readonly string reportServerHostName;
	}
}
