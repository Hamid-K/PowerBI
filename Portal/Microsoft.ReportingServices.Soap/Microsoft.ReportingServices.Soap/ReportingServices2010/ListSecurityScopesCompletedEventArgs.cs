using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200010E RID: 270
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSecurityScopesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x0000E6AA File Offset: 0x0000C8AA
		internal ListSecurityScopesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0000E6BD File Offset: 0x0000C8BD
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x0400026F RID: 623
		private object[] results;
	}
}
