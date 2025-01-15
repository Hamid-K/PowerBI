using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000EA RID: 234
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetUserSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x0000E3E9 File Offset: 0x0000C5E9
		internal GetUserSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		public Property[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Property[])this.results[0];
			}
		}

		// Token: 0x04000260 RID: 608
		private object[] results;
	}
}
