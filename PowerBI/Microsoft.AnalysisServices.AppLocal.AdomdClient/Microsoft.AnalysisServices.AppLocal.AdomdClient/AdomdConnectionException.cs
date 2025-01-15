using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000058 RID: 88
	[Serializable]
	public sealed class AdomdConnectionException : AdomdException
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00021723 File Offset: 0x0001F923
		public ConnectionExceptionCause ExceptionCause
		{
			get
			{
				return this.exceptionCause;
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0002172B File Offset: 0x0001F92B
		internal AdomdConnectionException()
		{
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00021733 File Offset: 0x0001F933
		private AdomdConnectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.exceptionCause = (ConnectionExceptionCause)info.GetValue("ExceptionCauseProperty", typeof(ConnectionExceptionCause));
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0002175D File Offset: 0x0001F95D
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ExceptionCauseProperty", this.exceptionCause, typeof(ConnectionExceptionCause));
			base.GetObjectData(info, context);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00021795 File Offset: 0x0001F995
		internal AdomdConnectionException(string message, ConnectionExceptionCause? exceptionCause = null)
			: base(message)
		{
			if (exceptionCause != null)
			{
				this.exceptionCause = exceptionCause.Value;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000217B4 File Offset: 0x0001F9B4
		internal AdomdConnectionException(string message, Exception innerException)
			: base(message, (innerException is XmlaStreamException && string.IsNullOrEmpty(innerException.Message)) ? innerException.InnerException : innerException)
		{
			if (innerException is XmlaStreamException)
			{
				this.exceptionCause = ((XmlaStreamException)innerException).ExceptionCause;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000217F4 File Offset: 0x0001F9F4
		internal AdomdConnectionException(string message, Exception innerException, ConnectionExceptionCause exceptionCause)
			: this(message, innerException)
		{
			this.exceptionCause = exceptionCause;
		}

		// Token: 0x04000434 RID: 1076
		private const string exceptionCauseSerializeName = "ExceptionCauseProperty";

		// Token: 0x04000435 RID: 1077
		private ConnectionExceptionCause exceptionCause;
	}
}
