using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000073 RID: 115
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetExtensionSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0000DB91 File Offset: 0x0000BD91
		internal GetExtensionSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
		public ExtensionParameter[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionParameter[])this.results[0];
			}
		}

		// Token: 0x04000233 RID: 563
		private object[] results;
	}
}
