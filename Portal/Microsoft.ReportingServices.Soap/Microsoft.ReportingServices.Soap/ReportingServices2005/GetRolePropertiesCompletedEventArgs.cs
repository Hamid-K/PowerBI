using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002E7 RID: 743
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetRolePropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x00023361 File Offset: 0x00021561
		internal GetRolePropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00023374 File Offset: 0x00021574
		public Task[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Task[])this.results[0];
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00023389 File Offset: 0x00021589
		public string Description
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x04000710 RID: 1808
		private object[] results;
	}
}
