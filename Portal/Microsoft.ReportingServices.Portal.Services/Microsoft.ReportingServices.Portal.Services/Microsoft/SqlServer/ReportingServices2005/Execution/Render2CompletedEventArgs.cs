using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000B3 RID: 179
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class Render2CompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x0001BBD2 File Offset: 0x00019DD2
		internal Render2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001BBE5 File Offset: 0x00019DE5
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001BBFA File Offset: 0x00019DFA
		public string Extension
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001BC0F File Offset: 0x00019E0F
		public string MimeType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[2];
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001BC24 File Offset: 0x00019E24
		public string Encoding
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[3];
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001BC39 File Offset: 0x00019E39
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[4];
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001BC4E File Offset: 0x00019E4E
		public string[] StreamIds
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[5];
			}
		}

		// Token: 0x040001DD RID: 477
		private object[] results;
	}
}
