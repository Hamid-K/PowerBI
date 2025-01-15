using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000092 RID: 146
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetRolePropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x0000DDC5 File Offset: 0x0000BFC5
		internal GetRolePropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0000DDED File Offset: 0x0000BFED
		public string Description
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x0400023F RID: 575
		private object[] results;
	}
}
