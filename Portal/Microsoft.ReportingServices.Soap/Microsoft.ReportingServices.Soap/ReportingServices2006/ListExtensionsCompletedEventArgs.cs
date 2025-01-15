using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001D9 RID: 473
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListExtensionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FBD RID: 4029 RVA: 0x000181E2 File Offset: 0x000163E2
		internal ListExtensionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x000181F5 File Offset: 0x000163F5
		public Extension[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Extension[])this.results[0];
			}
		}

		// Token: 0x040004A5 RID: 1189
		private object[] results;
	}
}
