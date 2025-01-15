using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000176 RID: 374
	[Serializable]
	internal sealed class VelocityAuthorizationException : VelocityPacketException
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x000277C1 File Offset: 0x000259C1
		public VelocityAuthorizationException()
			: this(ErrStatus.INTERNAL_ERROR)
		{
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000277CA File Offset: 0x000259CA
		internal VelocityAuthorizationException(ErrStatus status)
			: base("The authorization token was invalid.")
		{
			this._errStatus = status;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000277DE File Offset: 0x000259DE
		public VelocityAuthorizationException(string message)
			: this(ErrStatus.INTERNAL_ERROR, message)
		{
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000277E8 File Offset: 0x000259E8
		internal VelocityAuthorizationException(ErrStatus status, string message)
			: base(message)
		{
			this._errStatus = status;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000277F8 File Offset: 0x000259F8
		public VelocityAuthorizationException(string message, Exception innerException)
			: this(ErrStatus.INTERNAL_ERROR, message, innerException)
		{
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00027803 File Offset: 0x00025A03
		internal VelocityAuthorizationException(ErrStatus status, string message, Exception innerException)
			: base(message, innerException)
		{
			this._errStatus = status;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00027814 File Offset: 0x00025A14
		private VelocityAuthorizationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._errStatus = (ErrStatus)info.GetInt32("VelocityAuthorizationException.ErrorStatus");
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002782F File Offset: 0x00025A2F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("VelocityAuthorizationException.ErrorStatus", (int)this._errStatus);
			base.GetObjectData(info, context);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00027858 File Offset: 0x00025A58
		internal override ErrStatus GetErrorStatus()
		{
			return this._errStatus;
		}

		// Token: 0x04000874 RID: 2164
		private const string _errStatusPropertyName = "VelocityAuthorizationException.ErrorStatus";

		// Token: 0x04000875 RID: 2165
		private readonly ErrStatus _errStatus;
	}
}
