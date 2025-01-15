using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000178 RID: 376
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E69 RID: 3689 RVA: 0x000179A6 File Offset: 0x00015BA6
		internal GetPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x000179B9 File Offset: 0x00015BB9
		public Property[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Property[])this.results[0];
			}
		}

		// Token: 0x0400047F RID: 1151
		private object[] results;
	}
}
