using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002CD RID: 717
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSubscriptionPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x00023044 File Offset: 0x00021244
		internal GetSubscriptionPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00023057 File Offset: 0x00021257
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x0002306C File Offset: 0x0002126C
		public ExtensionSettings ExtensionSettings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionSettings)this.results[1];
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x00023081 File Offset: 0x00021281
		public string Description
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[2];
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x00023096 File Offset: 0x00021296
		public ActiveState Active
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ActiveState)this.results[3];
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x000230AB File Offset: 0x000212AB
		public string Status
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[4];
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x000230C0 File Offset: 0x000212C0
		public string EventType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[5];
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x000230D5 File Offset: 0x000212D5
		public string MatchData
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[6];
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x000230EA File Offset: 0x000212EA
		public ParameterValue[] Parameters
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ParameterValue[])this.results[7];
			}
		}

		// Token: 0x04000705 RID: 1797
		private object[] results;
	}
}
