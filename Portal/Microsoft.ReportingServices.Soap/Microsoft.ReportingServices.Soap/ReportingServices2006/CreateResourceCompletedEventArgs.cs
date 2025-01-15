using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000185 RID: 389
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateResourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x00017AAB File Offset: 0x00015CAB
		internal CreateResourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00017ABE File Offset: 0x00015CBE
		public CatalogItem Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem)this.results[0];
			}
		}

		// Token: 0x04000485 RID: 1157
		private object[] results;
	}
}
