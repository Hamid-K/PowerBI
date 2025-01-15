using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000058 RID: 88
	[Serializable]
	public sealed class AdomdConnectionException : AdomdException
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x000213F3 File Offset: 0x0001F5F3
		public ConnectionExceptionCause ExceptionCause
		{
			get
			{
				return this.exceptionCause;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000213FB File Offset: 0x0001F5FB
		internal AdomdConnectionException()
		{
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00021403 File Offset: 0x0001F603
		private AdomdConnectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.exceptionCause = (ConnectionExceptionCause)info.GetValue("ExceptionCauseProperty", typeof(ConnectionExceptionCause));
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0002142D File Offset: 0x0001F62D
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

		// Token: 0x060005B8 RID: 1464 RVA: 0x00021465 File Offset: 0x0001F665
		internal AdomdConnectionException(string message, ConnectionExceptionCause? exceptionCause = null)
			: base(message)
		{
			if (exceptionCause != null)
			{
				this.exceptionCause = exceptionCause.Value;
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00021484 File Offset: 0x0001F684
		internal AdomdConnectionException(string message, Exception innerException)
			: base(message, (innerException is XmlaStreamException && string.IsNullOrEmpty(innerException.Message)) ? innerException.InnerException : innerException)
		{
			if (innerException is XmlaStreamException)
			{
				this.exceptionCause = ((XmlaStreamException)innerException).ExceptionCause;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000214C4 File Offset: 0x0001F6C4
		internal AdomdConnectionException(string message, Exception innerException, ConnectionExceptionCause exceptionCause)
			: this(message, innerException)
		{
			this.exceptionCause = exceptionCause;
		}

		// Token: 0x04000427 RID: 1063
		private const string exceptionCauseSerializeName = "ExceptionCauseProperty";

		// Token: 0x04000428 RID: 1064
		private ConnectionExceptionCause exceptionCause;
	}
}
