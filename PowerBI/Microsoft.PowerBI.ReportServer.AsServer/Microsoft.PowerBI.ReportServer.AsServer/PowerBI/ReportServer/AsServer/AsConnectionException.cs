using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000017 RID: 23
	public sealed class AsConnectionException : AnalysisServicesException
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000414B File Offset: 0x0000234B
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00004153 File Offset: 0x00002353
		public AsConnectionExceptionErrorCode ErrorCode { get; private set; }

		// Token: 0x0600007D RID: 125 RVA: 0x0000415C File Offset: 0x0000235C
		public AsConnectionException(string message, AsConnectionExceptionErrorCode code, Exception innerException)
			: base(message, innerException)
		{
			this.ErrorCode = code;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000416D File Offset: 0x0000236D
		public AsConnectionException(string message, AsConnectionExceptionErrorCode errorCode)
			: base(message)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000417D File Offset: 0x0000237D
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ErrorCode", this.ErrorCode, typeof(AsConnectionExceptionErrorCode));
			base.GetObjectData(info, context);
		}
	}
}
