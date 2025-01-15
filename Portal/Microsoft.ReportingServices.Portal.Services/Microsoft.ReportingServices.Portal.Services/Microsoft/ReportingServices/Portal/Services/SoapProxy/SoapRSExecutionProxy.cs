using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.SoapProxy;
using Microsoft.SqlServer.ReportingServices2005.Execution;

namespace Microsoft.ReportingServices.Portal.Services.SoapProxy
{
	// Token: 0x0200002A RID: 42
	internal sealed class SoapRSExecutionProxy : ISoapRSExecutionProxy
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000D4AD File Offset: 0x0000B6AD
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000D4BA File Offset: 0x0000B6BA
		public int Timeout
		{
			get
			{
				return this.rsExecutionConnection.Timeout;
			}
			set
			{
				this.rsExecutionConnection.Timeout = value;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		internal SoapRSExecutionProxy(IPortalConfigurationManager portalConfigurationManager, IPrincipal userPrincipal)
		{
			this.portalConfigurationManager = portalConfigurationManager;
			this.rsExecutionConnection = new SoapRsExecutionConnection(portalConfigurationManager.Current.ReportServerUrl, portalConfigurationManager.Current.ReportServerHostName, userPrincipal.Identity);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000D500 File Offset: 0x0000B700
		public void LoadReport(IPrincipal userPrincipal, string report, string historyId)
		{
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<ExecutionInfo3>(this.rsExecutionConnection, userPrincipal, () => this.rsExecutionConnection.LoadReport3(report, historyId));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000D544 File Offset: 0x0000B744
		public RenderResults Render(IPrincipal userPrincipal, string format, string deviceInfo)
		{
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<RenderResults>(this.rsExecutionConnection, userPrincipal, () => this.RenderInternal(this.rsExecutionConnection, format, deviceInfo));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000D584 File Offset: 0x0000B784
		private RenderResults RenderInternal(SoapRsExecutionConnection rsExecutionConnection, string format, string deviceInfo)
		{
			string[] array = new string[0];
			Warning[] array2 = new Warning[0];
			string text;
			string text2;
			string text3;
			byte[] array3 = rsExecutionConnection.Render2(format, deviceInfo, PageCountMode.Actual, out text, out text2, out text3, out array2, out array);
			return new RenderResults
			{
				ByteContents = array3,
				Extension = text,
				MimeType = text2,
				Encoding = text3,
				StreamIds = array
			};
		}

		// Token: 0x04000091 RID: 145
		private readonly IPortalConfigurationManager portalConfigurationManager;

		// Token: 0x04000092 RID: 146
		private readonly SoapRsExecutionConnection rsExecutionConnection;
	}
}
