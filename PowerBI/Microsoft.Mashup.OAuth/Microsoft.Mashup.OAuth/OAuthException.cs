using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class OAuthException : Exception
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004949 File Offset: 0x00002B49
		public OAuthException(string message)
			: this(string.Empty, message, string.Empty, null)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000495D File Offset: 0x00002B5D
		public OAuthException(string message, string sts)
			: this(string.Empty, message, sts, null)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000496D File Offset: 0x00002B6D
		public OAuthException(string message, Exception innerException)
			: this(string.Empty, message, string.Empty, innerException)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004981 File Offset: 0x00002B81
		public OAuthException(string errorType, string message, Exception innerException)
			: this(errorType, message, string.Empty, innerException)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004991 File Offset: 0x00002B91
		public OAuthException(string errorType, string message, string sts, Exception innerException)
			: base(message, innerException)
		{
			this.errorType = errorType;
			this.message = message;
			this.sts = sts;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000049B1 File Offset: 0x00002BB1
		protected OAuthException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.errorType = info.GetString("errorType");
			this.message = info.GetString("message");
			this.sts = info.GetString("SecureTokenService");
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000049EE File Offset: 0x00002BEE
		public string SecureTokenService
		{
			get
			{
				return this.sts;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000049F6 File Offset: 0x00002BF6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("errorType", this.errorType);
			info.AddValue("message", this.message);
			info.AddValue("SecureTokenService", this.sts);
			base.GetObjectData(info, context);
		}

		// Token: 0x040000B0 RID: 176
		private const string errorTypeName = "errorType";

		// Token: 0x040000B1 RID: 177
		private const string messageName = "message";

		// Token: 0x040000B2 RID: 178
		private const string stsName = "SecureTokenService";

		// Token: 0x040000B3 RID: 179
		private readonly string errorType;

		// Token: 0x040000B4 RID: 180
		private readonly string message;

		// Token: 0x040000B5 RID: 181
		private readonly string sts;
	}
}
