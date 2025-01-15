using System;
using System.Net;
using System.Security.Principal;
using Microsoft.SqlServer.ReportingServices2010;

namespace Microsoft.ReportingServices.Portal.Services.SoapProxy
{
	// Token: 0x0200002D RID: 45
	internal sealed class SoapRsManagementConnection : RSConnection2010
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000E24D File Offset: 0x0000C44D
		public SoapRsManagementConnection(string reportServerUrl, string reportServerHostName, IIdentity userContext)
			: base(reportServerUrl)
		{
			this.userContext = userContext;
			this.reportServerHostName = reportServerHostName;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000E264 File Offset: 0x0000C464
		protected override WebRequest GetWebRequest(Uri uri)
		{
			HttpWebRequest httpWebRequest = base.GetWebRequest(uri) as HttpWebRequest;
			SoapAuthenticationHelper.PrepareWebRequest(httpWebRequest, this.userContext, this.reportServerHostName);
			return httpWebRequest;
		}

		// Token: 0x04000095 RID: 149
		private readonly IIdentity userContext;

		// Token: 0x04000096 RID: 150
		private readonly string reportServerHostName;
	}
}
