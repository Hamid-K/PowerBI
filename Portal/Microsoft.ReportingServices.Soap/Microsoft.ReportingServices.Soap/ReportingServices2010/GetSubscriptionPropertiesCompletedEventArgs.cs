using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000067 RID: 103
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSubscriptionPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x0000D9B6 File Offset: 0x0000BBB6
		internal GetSubscriptionPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0000D9C9 File Offset: 0x0000BBC9
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0000D9DE File Offset: 0x0000BBDE
		public ExtensionSettings ExtensionSettings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionSettings)this.results[1];
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0000D9F3 File Offset: 0x0000BBF3
		public string Description
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[2];
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0000DA08 File Offset: 0x0000BC08
		public ActiveState Active
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ActiveState)this.results[3];
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0000DA1D File Offset: 0x0000BC1D
		public string Status
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[4];
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0000DA32 File Offset: 0x0000BC32
		public string EventType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[5];
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000DA47 File Offset: 0x0000BC47
		public string MatchData
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[6];
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		public ParameterValue[] Parameters
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ParameterValue[])this.results[7];
			}
		}

		// Token: 0x0400022F RID: 559
		private object[] results;
	}
}
