using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200016D RID: 365
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSystemPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E45 RID: 3653 RVA: 0x00017906 File Offset: 0x00015B06
		internal GetSystemPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00017919 File Offset: 0x00015B19
		public Property[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Property[])this.results[0];
			}
		}

		// Token: 0x0400047B RID: 1147
		private object[] results;
	}
}
