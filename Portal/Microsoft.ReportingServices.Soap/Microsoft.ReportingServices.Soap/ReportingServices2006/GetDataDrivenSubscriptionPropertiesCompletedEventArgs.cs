using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001CA RID: 458
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDataDrivenSubscriptionPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x00017FF8 File Offset: 0x000161F8
		internal GetDataDrivenSubscriptionPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0001800B File Offset: 0x0001620B
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00018020 File Offset: 0x00016220
		public ExtensionSettings ExtensionSettings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionSettings)this.results[1];
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00018035 File Offset: 0x00016235
		public DataRetrievalPlan DataRetrievalPlan
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataRetrievalPlan)this.results[2];
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0001804A File Offset: 0x0001624A
		public string Description
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[3];
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0001805F File Offset: 0x0001625F
		public ActiveState Active
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ActiveState)this.results[4];
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00018074 File Offset: 0x00016274
		public string Status
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[5];
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00018089 File Offset: 0x00016289
		public string EventType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[6];
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0001809E File Offset: 0x0001629E
		public string MatchData
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[7];
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x000180B3 File Offset: 0x000162B3
		public ParameterValueOrFieldReference[] Parameters
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ParameterValueOrFieldReference[])this.results[8];
			}
		}

		// Token: 0x0400049E RID: 1182
		private object[] results;
	}
}
