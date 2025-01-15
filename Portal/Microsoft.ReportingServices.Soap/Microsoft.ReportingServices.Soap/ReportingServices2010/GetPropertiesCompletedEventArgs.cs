using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200005F RID: 95
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x0000D93E File Offset: 0x0000BB3E
		internal GetPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000D951 File Offset: 0x0000BB51
		public Property[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Property[])this.results[0];
			}
		}

		// Token: 0x0400022C RID: 556
		private object[] results;
	}
}
