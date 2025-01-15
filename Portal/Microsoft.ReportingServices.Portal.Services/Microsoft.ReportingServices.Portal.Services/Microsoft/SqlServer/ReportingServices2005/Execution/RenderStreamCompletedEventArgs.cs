using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000B6 RID: 182
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RenderStreamCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x0001BC63 File Offset: 0x00019E63
		internal RenderStreamCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001BC76 File Offset: 0x00019E76
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001BC8B File Offset: 0x00019E8B
		public string Encoding
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		public string MimeType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[2];
			}
		}

		// Token: 0x040001DE RID: 478
		private object[] results;
	}
}
