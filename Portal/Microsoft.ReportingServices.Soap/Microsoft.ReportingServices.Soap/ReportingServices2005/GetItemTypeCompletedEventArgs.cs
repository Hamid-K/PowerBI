using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200027B RID: 635
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemTypeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001659 RID: 5721 RVA: 0x00022AD5 File Offset: 0x00020CD5
		internal GetItemTypeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00022AE8 File Offset: 0x00020CE8
		public ItemTypeEnum Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ItemTypeEnum)this.results[0];
			}
		}

		// Token: 0x040006E8 RID: 1768
		private object[] results;
	}
}
