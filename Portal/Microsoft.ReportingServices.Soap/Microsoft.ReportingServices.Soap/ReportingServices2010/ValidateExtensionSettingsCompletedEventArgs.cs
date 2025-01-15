using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000075 RID: 117
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ValidateExtensionSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x0000DBB9 File Offset: 0x0000BDB9
		internal ValidateExtensionSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		public ExtensionParameter[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExtensionParameter[])this.results[0];
			}
		}

		// Token: 0x04000234 RID: 564
		private object[] results;
	}
}
