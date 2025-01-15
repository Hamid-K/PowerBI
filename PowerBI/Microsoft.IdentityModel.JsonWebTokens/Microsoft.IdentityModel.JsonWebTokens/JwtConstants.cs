using System;

namespace Microsoft.IdentityModel.JsonWebTokens
{
	// Token: 0x02000007 RID: 7
	public static class JwtConstants
	{
		// Token: 0x0400003F RID: 63
		public const string HeaderType = "JWT";

		// Token: 0x04000040 RID: 64
		public const string HeaderTypeAlt = "http://openid.net/specs/jwt/1.0";

		// Token: 0x04000041 RID: 65
		public const string TokenType = "JWT";

		// Token: 0x04000042 RID: 66
		public const string TokenTypeAlt = "urn:ietf:params:oauth:token-type:jwt";

		// Token: 0x04000043 RID: 67
		public const string JsonCompactSerializationRegex = "^[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]*$";

		// Token: 0x04000044 RID: 68
		public const string JweCompactSerializationRegex = "^[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]*\\.[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]+$";

		// Token: 0x04000045 RID: 69
		public const int JweSegmentCount = 5;

		// Token: 0x04000046 RID: 70
		public const int JwsSegmentCount = 3;

		// Token: 0x04000047 RID: 71
		public const int MaxJwtSegmentCount = 5;

		// Token: 0x04000048 RID: 72
		public const string DirectKeyUseAlg = "dir";
	}
}
