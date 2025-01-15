using System;

namespace System.IdentityModel.Tokens.Jwt
{
	// Token: 0x0200000D RID: 13
	internal static class LogMessages
	{
		// Token: 0x04000057 RID: 87
		internal const string IDX12401 = "IDX12401: Expires: '{0}' must be after NotBefore: '{1}'.";

		// Token: 0x04000058 RID: 88
		internal const string IDX12700 = "IDX12700: Error found while parsing date time. The '{0}' claim has value '{1}' which is could not be parsed to an integer.";

		// Token: 0x04000059 RID: 89
		internal const string IDX12701 = "IDX12701: Error found while parsing date time. The '{0}' claim has value '{1}' does not lie in the valid range.";

		// Token: 0x0400005A RID: 90
		internal const string IDX12706 = "IDX12706: '{0}' can only write SecurityTokens of type: '{1}', 'token' type is: '{2}'.";

		// Token: 0x0400005B RID: 91
		internal const string IDX12709 = "IDX12709: CanReadToken() returned false. JWT is not well formed.\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'.";

		// Token: 0x0400005C RID: 92
		internal const string IDX12710 = "IDX12710: Only a single 'Actor' is supported. Found second claim of type: '{0}', value: '{1}'";

		// Token: 0x0400005D RID: 93
		internal const string IDX12711 = "IDX12711: actor.BootstrapContext is not a string AND actor.BootstrapContext is not a JWT";

		// Token: 0x0400005E RID: 94
		internal const string IDX12712 = "IDX12712: actor.BootstrapContext is null. Creating the token using actor.Claims.";

		// Token: 0x0400005F RID: 95
		internal const string IDX12713 = "IDX12713: Creating actor value using actor.BootstrapContext(as string)";

		// Token: 0x04000060 RID: 96
		internal const string IDX12714 = "IDX12714: Creating actor value using actor.BootstrapContext.rawData";

		// Token: 0x04000061 RID: 97
		internal const string IDX12715 = "IDX12715: Creating actor value by writing the JwtSecurityToken created from actor.BootstrapContext";

		// Token: 0x04000062 RID: 98
		internal const string IDX12720 = "IDX12720: Token string does not match the token formats: JWE (header.encryptedKey.iv.ciphertext.tag) or JWS (header.payload.signature)";

		// Token: 0x04000063 RID: 99
		internal const string IDX12721 = "IDX12721: Creating JwtSecurityToken: Issuer: '{0}', Audience: '{1}'";

		// Token: 0x04000064 RID: 100
		internal const string IDX12722 = "IDX12722: Creating security token from the header: '{0}', payload: '{1}'.";

		// Token: 0x04000065 RID: 101
		internal const string IDX12723 = "IDX12723: Unable to decode the payload '{0}' as Base64Url encoded string.";

		// Token: 0x04000066 RID: 102
		internal const string IDX12729 = "IDX12729: Unable to decode the header '{0}' as Base64Url encoded string.";

		// Token: 0x04000067 RID: 103
		internal const string IDX12730 = "IDX12730: Failed to create the token encryption provider.";

		// Token: 0x04000068 RID: 104
		internal const string IDX12735 = "IDX12735: If JwtSecurityToken.InnerToken != null, then JwtSecurityToken.Header.EncryptingCredentials must be set.";

		// Token: 0x04000069 RID: 105
		internal const string IDX12736 = "IDX12736: JwtSecurityToken.SigningCredentials is not supported when JwtSecurityToken.InnerToken is set.";

		// Token: 0x0400006A RID: 106
		internal const string IDX12737 = "IDX12737: EncryptingCredentials set on JwtSecurityToken.InnerToken is not supported.";

		// Token: 0x0400006B RID: 107
		internal const string IDX12738 = "IDX12738: Header.Cty != null, assuming JWS. Cty: '{0}'.";

		// Token: 0x0400006C RID: 108
		internal const string IDX12739 = "IDX12739: JWT has three segments but is not in proper JWS format.";

		// Token: 0x0400006D RID: 109
		internal const string IDX12740 = "IDX12740: JWT has five segments but is not in proper JWE format.";

		// Token: 0x0400006E RID: 110
		internal const string IDX12741 = "IDX12741: JWT must have three segments (JWS) or five segments (JWE).";

		// Token: 0x0400006F RID: 111
		internal const string IDX12742 = "IDX12742: ''{0}' cannot contain the following claims: '{1}'. These values are added by default (if necessary) during security token creation.";
	}
}
