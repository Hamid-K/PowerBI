using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000FA RID: 250
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListJobActionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000899 RID: 2201 RVA: 0x0000E501 File Offset: 0x0000C701
		internal ListJobActionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0000E514 File Offset: 0x0000C714
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x04000267 RID: 615
		private object[] results;
	}
}
