using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000F8 RID: 248
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListJobTypesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x0000E4D9 File Offset: 0x0000C6D9
		internal ListJobTypesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x04000266 RID: 614
		private object[] results;
	}
}
