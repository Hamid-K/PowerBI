using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001F1 RID: 497
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "ReportServiceAuthenticationSoap", Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	public class ReportServiceAuthentication : SoapHttpClientProtocol
	{
		// Token: 0x0600100A RID: 4106 RVA: 0x000183D9 File Offset: 0x000165D9
		public ReportServiceAuthentication()
		{
			base.Url = "http://localhost/reportserver/reportserviceauthentication.asmx";
		}

		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x0600100B RID: 4107 RVA: 0x000183EC File Offset: 0x000165EC
		// (remove) Token: 0x0600100C RID: 4108 RVA: 0x00018424 File Offset: 0x00016624
		public event LogonUserCompletedEventHandler LogonUserCompleted;

		// Token: 0x140000DA RID: 218
		// (add) Token: 0x0600100D RID: 4109 RVA: 0x0001845C File Offset: 0x0001665C
		// (remove) Token: 0x0600100E RID: 4110 RVA: 0x00018494 File Offset: 0x00016694
		public event LogoffCompletedEventHandler LogoffCompleted;

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x0600100F RID: 4111 RVA: 0x000184CC File Offset: 0x000166CC
		// (remove) Token: 0x06001010 RID: 4112 RVA: 0x00018504 File Offset: 0x00016704
		public event GetAuthenticationModeCompletedEventHandler GetAuthenticationModeCompleted;

		// Token: 0x06001011 RID: 4113 RVA: 0x0001853C File Offset: 0x0001673C
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/LogonUser", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool LogonUser(string userName, string password, string authority, out string cookieName)
		{
			object[] array = base.Invoke("LogonUser", new object[] { userName, password, authority });
			cookieName = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0001857A File Offset: 0x0001677A
		public IAsyncResult BeginLogonUser(string userName, string password, string authority, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LogonUser", new object[] { userName, password, authority }, callback, asyncState);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000185A0 File Offset: 0x000167A0
		public bool EndLogonUser(IAsyncResult asyncResult, out string cookieName)
		{
			object[] array = base.EndInvoke(asyncResult);
			cookieName = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x000185C7 File Offset: 0x000167C7
		public void LogonUserAsync(string userName, string password, string authority)
		{
			this.LogonUserAsync(userName, password, authority, null);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x000185D4 File Offset: 0x000167D4
		public void LogonUserAsync(string userName, string password, string authority, object userState)
		{
			if (this.LogonUserOperationCompleted == null)
			{
				this.LogonUserOperationCompleted = new SendOrPostCallback(this.OnLogonUserOperationCompleted);
			}
			base.InvokeAsync("LogonUser", new object[] { userName, password, authority }, this.LogonUserOperationCompleted, userState);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00018620 File Offset: 0x00016820
		private void OnLogonUserOperationCompleted(object arg)
		{
			if (this.LogonUserCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LogonUserCompleted(this, new LogonUserCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00018665 File Offset: 0x00016865
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/Logoff", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void Logoff()
		{
			base.Invoke("Logoff", new object[0]);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00018679 File Offset: 0x00016879
		public IAsyncResult BeginLogoff(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Logoff", new object[0], callback, asyncState);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0001868E File Offset: 0x0001688E
		public void EndLogoff(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00018698 File Offset: 0x00016898
		public void LogoffAsync()
		{
			this.LogoffAsync(null);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x000186A1 File Offset: 0x000168A1
		public void LogoffAsync(object userState)
		{
			if (this.LogoffOperationCompleted == null)
			{
				this.LogoffOperationCompleted = new SendOrPostCallback(this.OnLogoffOperationCompleted);
			}
			base.InvokeAsync("Logoff", new object[0], this.LogoffOperationCompleted, userState);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x000186D8 File Offset: 0x000168D8
		private void OnLogoffOperationCompleted(object arg)
		{
			if (this.LogoffCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LogoffCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00018717 File Offset: 0x00016917
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices/GetAuthenticationMode", RequestNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public AuthenticationMode GetAuthenticationMode()
		{
			return (AuthenticationMode)base.Invoke("GetAuthenticationMode", new object[0])[0];
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00018731 File Offset: 0x00016931
		public IAsyncResult BeginGetAuthenticationMode(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAuthenticationMode", new object[0], callback, asyncState);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00018746 File Offset: 0x00016946
		public AuthenticationMode EndGetAuthenticationMode(IAsyncResult asyncResult)
		{
			return (AuthenticationMode)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00018756 File Offset: 0x00016956
		public void GetAuthenticationModeAsync()
		{
			this.GetAuthenticationModeAsync(null);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0001875F File Offset: 0x0001695F
		public void GetAuthenticationModeAsync(object userState)
		{
			if (this.GetAuthenticationModeOperationCompleted == null)
			{
				this.GetAuthenticationModeOperationCompleted = new SendOrPostCallback(this.OnGetAuthenticationModeOperationCompleted);
			}
			base.InvokeAsync("GetAuthenticationMode", new object[0], this.GetAuthenticationModeOperationCompleted, userState);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00018794 File Offset: 0x00016994
		private void OnGetAuthenticationModeOperationCompleted(object arg)
		{
			if (this.GetAuthenticationModeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAuthenticationModeCompleted(this, new GetAuthenticationModeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000187D9 File Offset: 0x000169D9
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x040004B0 RID: 1200
		private SendOrPostCallback LogonUserOperationCompleted;

		// Token: 0x040004B1 RID: 1201
		private SendOrPostCallback LogoffOperationCompleted;

		// Token: 0x040004B2 RID: 1202
		private SendOrPostCallback GetAuthenticationModeOperationCompleted;
	}
}
