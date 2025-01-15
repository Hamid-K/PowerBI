using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Security;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C2B RID: 7211
	public static class StructuredCacheKeyExtensions
	{
		// Token: 0x0600B40A RID: 46090 RVA: 0x00248B88 File Offset: 0x00246D88
		public static string GetCacheKey(this StructuredCacheKey key, bool userSpecific)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < key.Parts.Length; i++)
			{
				StructuredCacheKeyExtensions.AppendCacheKeyPart(stringBuilder, key.Parts[i]);
			}
			if (userSpecific && key.Credentials != null)
			{
				StructuredCacheKeyExtensions.AppendCacheKeyPart(stringBuilder, StructuredCacheKeyExtensions.GetHash(key.Credentials));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600B40B RID: 46091 RVA: 0x00248BE0 File Offset: 0x00246DE0
		public static void AppendCacheKeyPart(StringBuilder builder, string part)
		{
			if (builder.Length > 0)
			{
				builder.Append('/');
			}
			if (part == null)
			{
				builder.Append('/');
				return;
			}
			if (part.IndexOf('/') == -1 && part.IndexOf('\\') == -1)
			{
				builder.Append(part);
				return;
			}
			foreach (char c in part)
			{
				if (c == '/' || c == '\\')
				{
					builder.Append('\\');
				}
				builder.Append(c);
			}
		}

		// Token: 0x0600B40C RID: 46092 RVA: 0x00248C64 File Offset: 0x00246E64
		public static string Escape(string value)
		{
			if (value == null)
			{
				return "/";
			}
			if (value.IndexOf('/') == -1 && value.IndexOf('\\') == -1)
			{
				return value;
			}
			return value.Replace("\\", "\\\\").Replace("/", "\\/");
		}

		// Token: 0x0600B40D RID: 46093 RVA: 0x00248CB4 File Offset: 0x00246EB4
		private static string GetHash(ResourceCredentialCollection credentials)
		{
			if (credentials.Count == 0)
			{
				return string.Empty;
			}
			Stream stream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			foreach (IResourceCredential resourceCredential in credentials)
			{
				foreach (string text in StructuredCacheKeyExtensions.GetCacheParts(resourceCredential))
				{
					if (text == null)
					{
						binaryWriter.Write(false);
					}
					else
					{
						binaryWriter.Write(true);
						binaryWriter.Write(text);
					}
				}
			}
			stream.Position = 0L;
			string text2;
			using (HashAlgorithm hashAlgorithm = SHA256CryptoProvider.Create())
			{
				text2 = Convert.ToBase64String(hashAlgorithm.ComputeHash(stream));
			}
			return text2;
		}

		// Token: 0x0600B40E RID: 46094 RVA: 0x00248D9C File Offset: 0x00246F9C
		private static IEnumerable<string> GetCacheParts(IResourceCredential credential)
		{
			string text;
			if (credential is OAuthCredential && JwtParser.TryParseSubject(((OAuthCredential)credential).AccessToken, out text))
			{
				return new string[] { text };
			}
			return credential.GetCacheParts();
		}

		// Token: 0x04005BA8 RID: 23464
		public const string Separator = "/";

		// Token: 0x04005BA9 RID: 23465
		private const char CacheKeySeparator = '/';

		// Token: 0x04005BAA RID: 23466
		private const char CacheKeyEscape = '\\';
	}
}
