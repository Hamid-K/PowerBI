using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000267 RID: 615
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class RegenerateModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x000229BD File Offset: 0x00020BBD
		internal RegenerateModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x000229D0 File Offset: 0x00020BD0
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x040006E1 RID: 1761
		private object[] results;
	}
}
