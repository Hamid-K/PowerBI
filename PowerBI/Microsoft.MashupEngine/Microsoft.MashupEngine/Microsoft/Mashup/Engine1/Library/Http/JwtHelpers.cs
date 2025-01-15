using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Security;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A68 RID: 2664
	internal static class JwtHelpers
	{
		// Token: 0x06004A60 RID: 19040 RVA: 0x000F7E50 File Offset: 0x000F6050
		public static void TracePossibleJwtExpiration(this IHostTrace trace, WebException exception, ResourceCredentialCollection credentials)
		{
			MashupHttpWebResponse mashupHttpWebResponse = exception.Response as MashupHttpWebResponse;
			trace.TracePossibleJwtExpiration(mashupHttpWebResponse, credentials);
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x000F7E74 File Offset: 0x000F6074
		public static void TracePossibleJwtExpiration(this IHostTrace trace, MashupHttpWebResponse response, ResourceCredentialCollection credentials)
		{
			OAuthCredential oauthCredential = credentials.RemoveAdornments() as OAuthCredential;
			if (response != null)
			{
				trace.TracePossibleJwtExpirationWithAudience(oauthCredential);
			}
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x000F7E98 File Offset: 0x000F6098
		public static void TracePossibleJwtExpirationWithAudience(this IHostTrace trace, OAuthCredential credential)
		{
			DateTime dateTime;
			string text;
			string text2;
			if (credential != null && JwtParser.TryParseData(credential.AccessToken, out dateTime, out text, out text2))
			{
				trace.Add("JwtExpires", dateTime, false);
				trace.Add("JwtIdentifier", JwtHelpers.CreateHash(new string[] { text }), false);
				bool flag = !JwtHelpers.piiSafeAudience.Contains(text2.TrimEnd(new char[] { '/' }));
				trace.Add("JwtTokenAudience", text2, flag);
			}
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x000F7F14 File Offset: 0x000F6114
		private static string CreateHash(params string[] parts)
		{
			string text2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					foreach (string text in parts)
					{
						binaryWriter.Write(text);
					}
					binaryWriter.Flush();
					memoryStream.Position = 0L;
					text2 = JwtHelpers.CreateHash(memoryStream);
				}
			}
			return text2;
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x000F7F98 File Offset: 0x000F6198
		private static string CreateHash(Stream stream)
		{
			string text;
			using (HashAlgorithm hashAlgorithm = SHA256CryptoProvider.Create())
			{
				text = Convert.ToBase64String(hashAlgorithm.ComputeHash(stream));
			}
			return text;
		}

		// Token: 0x0400278C RID: 10124
		private static readonly HashSet<string> piiSafeAudience = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "https://database.windows.net", "https://analysis.windows.net/powerbi/api" };
	}
}
