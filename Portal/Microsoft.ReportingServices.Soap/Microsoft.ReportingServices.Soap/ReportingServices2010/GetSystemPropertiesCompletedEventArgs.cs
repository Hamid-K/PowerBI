using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000E7 RID: 231
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSystemPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x0000E3C1 File Offset: 0x0000C5C1
		internal GetSystemPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		public Property[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Property[])this.results[0];
			}
		}

		// Token: 0x0400025F RID: 607
		private object[] results;
	}
}
