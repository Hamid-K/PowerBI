using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001C8 RID: 456
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSubscriptionPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x00017F3D File Offset: 0x0001613D
		internal GetSubscriptionPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x00017F50 File Offset: 0x00016150
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00017F65 File Offset: 0x00016165
		public ExtensionSettings ExtensionSettings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionSettings)this.results[1];
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x00017F7A File Offset: 0x0001617A
		public string Description
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[2];
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00017F8F File Offset: 0x0001618F
		public ActiveState Active
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ActiveState)this.results[3];
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00017FA4 File Offset: 0x000161A4
		public string Status
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[4];
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00017FB9 File Offset: 0x000161B9
		public string EventType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[5];
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00017FCE File Offset: 0x000161CE
		public string MatchData
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[6];
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00017FE3 File Offset: 0x000161E3
		public ParameterValue[] Parameters
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ParameterValue[])this.results[7];
			}
		}

		// Token: 0x0400049D RID: 1181
		private object[] results;
	}
}
