using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002E3 RID: 739
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListRolesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060017C7 RID: 6087 RVA: 0x00023339 File Offset: 0x00021539
		internal ListRolesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0002334C File Offset: 0x0002154C
		public Role[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Role[])this.results[0];
			}
		}

		// Token: 0x0400070F RID: 1807
		private object[] results;
	}
}
