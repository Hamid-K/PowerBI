using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public sealed class ConnectionException : AmoException
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00005893 File Offset: 0x00003A93
		public ConnectionException()
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000589B File Offset: 0x00003A9B
		public ConnectionException(string message)
			: base(message)
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000058A4 File Offset: 0x00003AA4
		public ConnectionException(string message, Exception innerException)
			: base(message, (innerException is XmlaStreamException && string.IsNullOrEmpty(innerException.Message)) ? innerException.InnerException : innerException)
		{
			if (innerException is XmlaStreamException)
			{
				this.cause = ((XmlaStreamException)innerException).ExceptionCause;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000058E4 File Offset: 0x00003AE4
		private ConnectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.cause = (ConnectionExceptionCause)info.GetValue("ExceptionCauseProperty", typeof(ConnectionExceptionCause));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000590E File Offset: 0x00003B0E
		internal ConnectionException(string message, ConnectionExceptionCause cause)
			: this(message)
		{
			this.cause = cause;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000591E File Offset: 0x00003B1E
		internal ConnectionException(string message, Exception innerException, ConnectionExceptionCause cause)
			: this(message, innerException)
		{
			this.cause = cause;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000592F File Offset: 0x00003B2F
		public ConnectionExceptionCause ExceptionCause
		{
			get
			{
				return this.cause;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005937 File Offset: 0x00003B37
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("ExceptionCauseProperty", this.cause, typeof(ConnectionExceptionCause));
		}

		// Token: 0x040000E4 RID: 228
		private const string ExceptionCauseSerializationId = "ExceptionCauseProperty";

		// Token: 0x040000E5 RID: 229
		private ConnectionExceptionCause cause;
	}
}
