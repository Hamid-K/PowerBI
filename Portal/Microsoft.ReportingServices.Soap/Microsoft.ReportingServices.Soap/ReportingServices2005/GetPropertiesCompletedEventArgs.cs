using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000278 RID: 632
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600164F RID: 5711 RVA: 0x00022AAD File Offset: 0x00020CAD
		internal GetPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00022AC0 File Offset: 0x00020CC0
		public Property[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Property[])this.results[0];
			}
		}

		// Token: 0x040006E7 RID: 1767
		private object[] results;
	}
}
