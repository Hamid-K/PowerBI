using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Security;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001976 RID: 6518
	internal static class PersistentCacheExtensions
	{
		// Token: 0x0600A572 RID: 42354 RVA: 0x0022390C File Offset: 0x00221B0C
		public static string GetCacheKey(this StructuredCacheKey key, bool userSpecific = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < key.Parts.Length; i++)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('/');
				}
				PersistentCacheExtensions.AppendCacheKeyPart(stringBuilder, key.Parts[i]);
			}
			if (userSpecific && key.Credentials != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('/');
				}
				PersistentCacheExtensions.AppendCacheKeyPart(stringBuilder, PersistentCacheExtensions.GetHash(key.Credentials));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600A573 RID: 42355 RVA: 0x00223988 File Offset: 0x00221B88
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
				foreach (string text in PersistentCacheExtensions.GetCacheParts(resourceCredential))
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

		// Token: 0x0600A574 RID: 42356 RVA: 0x00223A70 File Offset: 0x00221C70
		private static IEnumerable<string> GetCacheParts(IResourceCredential credential)
		{
			string text;
			if (credential is OAuthCredential && JwtParser.TryParseSubject(((OAuthCredential)credential).AccessToken, out text))
			{
				return new string[] { text };
			}
			return credential.GetCacheParts();
		}

		// Token: 0x0600A575 RID: 42357 RVA: 0x00223AAC File Offset: 0x00221CAC
		private static void AppendCacheKeyPart(StringBuilder builder, string part)
		{
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

		// Token: 0x04005618 RID: 22040
		private const char CacheKeySeparator = '/';

		// Token: 0x04005619 RID: 22041
		private const char CacheKeyEscape = '\\';
	}
}
