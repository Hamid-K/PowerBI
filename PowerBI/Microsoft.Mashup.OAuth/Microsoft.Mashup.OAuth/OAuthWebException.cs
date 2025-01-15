using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class OAuthWebException : OAuthException
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000532E File Offset: 0x0000352E
		public OAuthWebException(OAuthError oauthError, byte[] responseBody, Exception innerException)
			: base(oauthError.Error, oauthError.ErrorDescription + " " + oauthError.ErrorUri, innerException)
		{
			this.oauthError = oauthError;
			this.responseBody = responseBody;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005364 File Offset: 0x00003564
		public OAuthWebException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.oauthError = (OAuthError)info.GetValue("oauthError", typeof(OAuthError));
			this.responseBody = (byte[])info.GetValue("responseBody", typeof(byte[]));
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000053B9 File Offset: 0x000035B9
		public OAuthError OAuthError
		{
			get
			{
				return this.oauthError;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000053C1 File Offset: 0x000035C1
		public byte[] ResponseBody
		{
			get
			{
				return this.responseBody;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000053C9 File Offset: 0x000035C9
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("oauthError", this.oauthError);
			info.AddValue("responseBody", this.responseBody);
			base.GetObjectData(info, context);
		}

		// Token: 0x040000D1 RID: 209
		private const string oauthErrorName = "oauthError";

		// Token: 0x040000D2 RID: 210
		private const string responseBodyName = "responseBody";

		// Token: 0x040000D3 RID: 211
		private readonly OAuthError oauthError;

		// Token: 0x040000D4 RID: 212
		private readonly byte[] responseBody;
	}
}
