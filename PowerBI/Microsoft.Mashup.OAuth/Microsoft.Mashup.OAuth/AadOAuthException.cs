using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class AadOAuthException : OAuthException
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002103 File Offset: 0x00000303
		public AadOAuthException(AadOAuthError error, Exception innerException)
			: base(error.ErrorDescription + ". " + error.ErrorUri, innerException)
		{
			this.aadOAuthError = error;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002129 File Offset: 0x00000329
		protected AadOAuthException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.aadOAuthError = (AadOAuthError)info.GetValue("aadOAuthError", typeof(AadOAuthError));
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002153 File Offset: 0x00000353
		public AadOAuthError AadOAuthError
		{
			get
			{
				return this.aadOAuthError;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000215B File Offset: 0x0000035B
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("aadOAuthError", this.aadOAuthError);
			base.GetObjectData(info, context);
		}

		// Token: 0x04000008 RID: 8
		private const string aadAuthErrorName = "aadOAuthError";

		// Token: 0x04000009 RID: 9
		private readonly AadOAuthError aadOAuthError;
	}
}
