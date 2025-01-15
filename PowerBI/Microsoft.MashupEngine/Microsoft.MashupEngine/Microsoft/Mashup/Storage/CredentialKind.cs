using System;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200206C RID: 8300
	public enum CredentialKind : byte
	{
		// Token: 0x04006720 RID: 26400
		SqlCredentialKind,
		// Token: 0x04006721 RID: 26401
		BasicCredentialKind,
		// Token: 0x04006722 RID: 26402
		FeedKeyCredentialKind,
		// Token: 0x04006723 RID: 26403
		FtpLoginCredentialKind,
		// Token: 0x04006724 RID: 26404
		OAuthCredentialKind = 6,
		// Token: 0x04006725 RID: 26405
		WebApiKeyCredentialKind,
		// Token: 0x04006726 RID: 26406
		WindowsCredentialKind,
		// Token: 0x04006727 RID: 26407
		NoneCredentialKind,
		// Token: 0x04006728 RID: 26408
		UnknownCredentialKind = 255
	}
}
