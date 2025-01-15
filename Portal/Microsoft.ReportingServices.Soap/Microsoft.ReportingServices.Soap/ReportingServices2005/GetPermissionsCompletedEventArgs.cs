using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200024B RID: 587
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetPermissionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015BC RID: 5564 RVA: 0x000227F0 File Offset: 0x000209F0
		internal GetPermissionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00022803 File Offset: 0x00020A03
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x040006D6 RID: 1750
		private object[] results;
	}
}
