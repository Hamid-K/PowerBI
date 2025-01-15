using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000159 RID: 345
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListModelItemChildrenCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E03 RID: 3587 RVA: 0x0001779C File Offset: 0x0001599C
		internal ListModelItemChildrenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x000177AF File Offset: 0x000159AF
		public ModelItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ModelItem[])this.results[0];
			}
		}

		// Token: 0x04000473 RID: 1139
		private object[] results;
	}
}
