using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000062 RID: 98
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemReferencesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x0000D966 File Offset: 0x0000BB66
		internal GetItemReferencesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000D979 File Offset: 0x0000BB79
		public ItemReferenceData[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ItemReferenceData[])this.results[0];
			}
		}

		// Token: 0x0400022D RID: 557
		private object[] results;
	}
}
