using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000087 RID: 135
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListDatabaseCredentialRetrievalOptionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x0000DCFB File Offset: 0x0000BEFB
		internal ListDatabaseCredentialRetrievalOptionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0000DD0E File Offset: 0x0000BF0E
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x0400023B RID: 571
		private object[] results;
	}
}
