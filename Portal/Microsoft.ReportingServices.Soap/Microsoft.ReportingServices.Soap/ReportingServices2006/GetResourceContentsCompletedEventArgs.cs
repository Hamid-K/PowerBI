using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000188 RID: 392
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetResourceContentsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E9C RID: 3740 RVA: 0x00017AD3 File Offset: 0x00015CD3
		internal GetResourceContentsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00017AE6 File Offset: 0x00015CE6
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00017AFB File Offset: 0x00015CFB
		public string MimeType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x04000486 RID: 1158
		private object[] results;
	}
}
