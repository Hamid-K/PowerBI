using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000291 RID: 657
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetRenderResourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060016A2 RID: 5794 RVA: 0x00022C2A File Offset: 0x00020E2A
		internal GetRenderResourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00022C3D File Offset: 0x00020E3D
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00022C52 File Offset: 0x00020E52
		public string MimeType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x040006F0 RID: 1776
		private object[] results;
	}
}
