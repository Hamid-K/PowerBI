using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004E RID: 78
	internal sealed class XmlaStreamException : IOException
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001E8DA File Offset: 0x0001CADA
		public XmlaStreamException(string message)
			: base(message)
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001E8E3 File Offset: 0x0001CAE3
		public XmlaStreamException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001E8ED File Offset: 0x0001CAED
		public XmlaStreamException(Exception innerException)
			: base(string.Empty, innerException)
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001E8FB File Offset: 0x0001CAFB
		internal XmlaStreamException(string message, ConnectionExceptionCause cause)
			: this(message)
		{
			this.cause = cause;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001E90B File Offset: 0x0001CB0B
		internal XmlaStreamException(Exception innerException, ConnectionExceptionCause cause)
			: this(innerException)
		{
			this.cause = cause;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001E91B File Offset: 0x0001CB1B
		public ConnectionExceptionCause ExceptionCause
		{
			get
			{
				return this.cause;
			}
		}

		// Token: 0x040003D9 RID: 985
		private ConnectionExceptionCause cause;
	}
}
