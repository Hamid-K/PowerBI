using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004E RID: 78
	internal sealed class XmlaStreamException : IOException
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0001E5AA File Offset: 0x0001C7AA
		public XmlaStreamException(string message)
			: base(message)
		{
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001E5B3 File Offset: 0x0001C7B3
		public XmlaStreamException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001E5BD File Offset: 0x0001C7BD
		public XmlaStreamException(Exception innerException)
			: base(string.Empty, innerException)
		{
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001E5CB File Offset: 0x0001C7CB
		internal XmlaStreamException(string message, ConnectionExceptionCause cause)
			: this(message)
		{
			this.cause = cause;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001E5DB File Offset: 0x0001C7DB
		internal XmlaStreamException(Exception innerException, ConnectionExceptionCause cause)
			: this(innerException)
		{
			this.cause = cause;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0001E5EB File Offset: 0x0001C7EB
		public ConnectionExceptionCause ExceptionCause
		{
			get
			{
				return this.cause;
			}
		}

		// Token: 0x040003CC RID: 972
		private ConnectionExceptionCause cause;
	}
}
