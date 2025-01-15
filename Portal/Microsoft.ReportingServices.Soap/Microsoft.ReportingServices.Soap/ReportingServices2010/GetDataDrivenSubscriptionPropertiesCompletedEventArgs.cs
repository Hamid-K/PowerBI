using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200006A RID: 106
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDataDrivenSubscriptionPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x0000DA71 File Offset: 0x0000BC71
		internal GetDataDrivenSubscriptionPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0000DA84 File Offset: 0x0000BC84
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0000DA99 File Offset: 0x0000BC99
		public ExtensionSettings ExtensionSettings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionSettings)this.results[1];
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0000DAAE File Offset: 0x0000BCAE
		public DataRetrievalPlan DataRetrievalPlan
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataRetrievalPlan)this.results[2];
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0000DAC3 File Offset: 0x0000BCC3
		public string Description
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[3];
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0000DAD8 File Offset: 0x0000BCD8
		public ActiveState Active
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ActiveState)this.results[4];
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0000DAED File Offset: 0x0000BCED
		public string Status
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[5];
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0000DB02 File Offset: 0x0000BD02
		public string EventType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[6];
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0000DB17 File Offset: 0x0000BD17
		public string MatchData
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[7];
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		public ParameterValueOrFieldReference[] Parameters
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ParameterValueOrFieldReference[])this.results[8];
			}
		}

		// Token: 0x04000230 RID: 560
		private object[] results;
	}
}
