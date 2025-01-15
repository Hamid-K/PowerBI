using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002CF RID: 719
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDataDrivenSubscriptionPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600177F RID: 6015 RVA: 0x000230FF File Offset: 0x000212FF
		internal GetDataDrivenSubscriptionPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x00023112 File Offset: 0x00021312
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x00023127 File Offset: 0x00021327
		public ExtensionSettings ExtensionSettings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionSettings)this.results[1];
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0002313C File Offset: 0x0002133C
		public DataRetrievalPlan DataRetrievalPlan
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataRetrievalPlan)this.results[2];
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x00023151 File Offset: 0x00021351
		public string Description
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[3];
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00023166 File Offset: 0x00021366
		public ActiveState Active
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ActiveState)this.results[4];
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0002317B File Offset: 0x0002137B
		public string Status
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[5];
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x00023190 File Offset: 0x00021390
		public string EventType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[6];
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x000231A5 File Offset: 0x000213A5
		public string MatchData
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[7];
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x000231BA File Offset: 0x000213BA
		public ParameterValueOrFieldReference[] Parameters
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ParameterValueOrFieldReference[])this.results[8];
			}
		}

		// Token: 0x04000706 RID: 1798
		private object[] results;
	}
}
