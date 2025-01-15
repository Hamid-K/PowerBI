using System;

namespace Microsoft.IdentityModel.JsonWebTokens
{
	// Token: 0x0200000C RID: 12
	internal static class LogMessages
	{
		// Token: 0x04000088 RID: 136
		internal const string IDX14000 = "IDX14000: Signature validation of this JWT is not supported for: Algorithm: '{0}', SecurityKey: '{1}'.";

		// Token: 0x04000089 RID: 137
		internal const string IDX14100 = "IDX14100: JWT is not well formed, there are no dots (.).\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'.";

		// Token: 0x0400008A RID: 138
		internal const string IDX14101 = "IDX14101: Unable to decode the payload '{0}' as Base64Url encoded string.";

		// Token: 0x0400008B RID: 139
		internal const string IDX14102 = "IDX14102: Unable to decode the header '{0}' as Base64Url encoded string.";

		// Token: 0x0400008C RID: 140
		internal const string IDX14103 = "IDX14103: Failed to create the token encryption provider.";

		// Token: 0x0400008D RID: 141
		internal const string IDX14107 = "IDX14107: Token string does not match the token formats: JWE (header.encryptedKey.iv.ciphertext.tag) or JWS (header.payload.signature)";

		// Token: 0x0400008E RID: 142
		internal const string IDX14112 = "IDX14112: Only a single 'Actor' is supported. Found second claim of type: '{0}'";

		// Token: 0x0400008F RID: 143
		internal const string IDX14113 = "IDX14113: A duplicate value for 'SecurityTokenDescriptor.{0}' exists in 'SecurityTokenDescriptor.Claims'. \nThe value of 'SecurityTokenDescriptor.{0}' is used.";

		// Token: 0x04000090 RID: 144
		internal const string IDX14114 = "IDX14114: Both '{0}.{1}' and '{0}.{2}' are null or empty.";

		// Token: 0x04000091 RID: 145
		internal const string IDX14116 = "IDX14116: '{0}' cannot contain the following claims: '{1}'. These values are added by default (if necessary) during security token creation.";

		// Token: 0x04000092 RID: 146
		internal const string IDX14120 = "IDX14120: JWT is not well formed, there is only one dot (.).\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'.";

		// Token: 0x04000093 RID: 147
		internal const string IDX14121 = "IDX14121: JWT is not a well formed JWE, there are there must be four dots (.).\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'.";

		// Token: 0x04000094 RID: 148
		internal const string IDX14122 = "IDX14122: JWT is not a well formed JWE, there are more than four dots (.) a JWE can have at most 4 dots.\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'.";

		// Token: 0x04000095 RID: 149
		internal const string IDX14200 = "IDX14200: Creating raw signature using the signature credentials.";

		// Token: 0x04000096 RID: 150
		internal const string IDX14201 = "IDX14201: Creating raw signature using the signature credentials. Caching SignatureProvider: '{0}'.";

		// Token: 0x04000097 RID: 151
		internal const string IDX14300 = "IDX14300: Could not parse '{0}' : '{1}' as a '{2}'.";

		// Token: 0x04000098 RID: 152
		internal const string IDX14301 = "IDX14301: Unable to parse the header into a JSON object. \nHeader: '{0}'.";

		// Token: 0x04000099 RID: 153
		internal const string IDX14302 = "IDX14302: Unable to parse the payload into a JSON object. \nPayload: '{0}'.";

		// Token: 0x0400009A RID: 154
		internal const string IDX14304 = "IDX14304: Claim with name '{0}' does not exist in the payload.";

		// Token: 0x0400009B RID: 155
		internal const string IDX14305 = "IDX14305: Unable to convert the '{0}' json property to the following type: '{1}'. Property type was: '{2}'. Value: '{3}'.";

		// Token: 0x0400009C RID: 156
		internal const string IDX14306 = "IDX14306: JWE Ciphertext cannot be an empty string.";

		// Token: 0x0400009D RID: 157
		internal const string IDX14307 = "IDX14307: JWE header is missing.";

		// Token: 0x0400009E RID: 158
		internal const string IDX14308 = "IDX14308: JWE initialization vector is missing.";

		// Token: 0x0400009F RID: 159
		internal const string IDX14309 = "IDX14309: Unable to decode the initialization vector as Base64Url encoded string.";

		// Token: 0x040000A0 RID: 160
		internal const string IDX14310 = "IDX14310: JWE authentication tag is missing.";

		// Token: 0x040000A1 RID: 161
		internal const string IDX14311 = "IDX14311: Unable to decode the authentication tag as a Base64Url encoded string.";

		// Token: 0x040000A2 RID: 162
		internal const string IDX14312 = "IDX14312: Unable to decode the cipher text as a Base64Url encoded string.";
	}
}
