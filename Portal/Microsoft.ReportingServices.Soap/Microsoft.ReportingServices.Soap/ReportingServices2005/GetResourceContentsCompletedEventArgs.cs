using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000286 RID: 646
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetResourceContentsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600167D RID: 5757 RVA: 0x00022B75 File Offset: 0x00020D75
		internal GetResourceContentsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00022B88 File Offset: 0x00020D88
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00022B9D File Offset: 0x00020D9D
		public string MimeType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x040006EC RID: 1772
		private object[] results;
	}
}
