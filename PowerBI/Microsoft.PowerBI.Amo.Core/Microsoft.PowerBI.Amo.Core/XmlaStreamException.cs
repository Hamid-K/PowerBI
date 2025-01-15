using System;
using System.IO;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200002E RID: 46
	internal sealed class XmlaStreamException : IOException
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000059AC File Offset: 0x00003BAC
		public XmlaStreamException(string message)
			: base(message)
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000059B5 File Offset: 0x00003BB5
		public XmlaStreamException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000059BF File Offset: 0x00003BBF
		public XmlaStreamException(Exception innerException)
			: base(string.Empty, innerException)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000059CD File Offset: 0x00003BCD
		internal XmlaStreamException(string message, ConnectionExceptionCause cause)
			: this(message)
		{
			this.cause = cause;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000059DD File Offset: 0x00003BDD
		internal XmlaStreamException(Exception innerException, ConnectionExceptionCause cause)
			: this(innerException)
		{
			this.cause = cause;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000059ED File Offset: 0x00003BED
		public ConnectionExceptionCause ExceptionCause
		{
			get
			{
				return this.cause;
			}
		}

		// Token: 0x040000E6 RID: 230
		private ConnectionExceptionCause cause;
	}
}
