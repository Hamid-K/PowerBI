using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000B1 RID: 177
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RenderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x0001BB41 File Offset: 0x00019D41
		internal RenderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001BB54 File Offset: 0x00019D54
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001BB69 File Offset: 0x00019D69
		public string Extension
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001BB7E File Offset: 0x00019D7E
		public string MimeType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[2];
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001BB93 File Offset: 0x00019D93
		public string Encoding
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[3];
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[4];
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001BBBD File Offset: 0x00019DBD
		public string[] StreamIds
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[5];
			}
		}

		// Token: 0x040001DC RID: 476
		private object[] results;
	}
}
