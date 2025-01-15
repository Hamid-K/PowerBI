using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Resources;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015F3 RID: 5619
	internal static class ResourceCredentialCollectionExtensions
	{
		// Token: 0x06008D61 RID: 36193 RVA: 0x001D9524 File Offset: 0x001D7724
		public static string GetHash(this ResourceCredentialCollection credentials)
		{
			if (credentials.Count == 0)
			{
				return string.Empty;
			}
			Stream stream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			foreach (IResourceCredential resourceCredential in credentials)
			{
				foreach (string text in ResourceCredentialCollectionExtensions.GetCacheParts(resourceCredential))
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
			using (HashAlgorithm hashAlgorithm = CryptoAlgorithmFactory.CreateSHA256Algorithm())
			{
				text2 = Convert.ToBase64String(hashAlgorithm.ComputeHash(stream));
			}
			return text2;
		}

		// Token: 0x06008D62 RID: 36194 RVA: 0x001D960C File Offset: 0x001D780C
		public static IResourceCredential RemoveAdornments(this ResourceCredentialCollection credentials)
		{
			foreach (IResourceCredential resourceCredential in credentials)
			{
				if (!ResourceCredentialCollectionExtensions.adornmentTypes.Contains(resourceCredential.GetType()))
				{
					return resourceCredential;
				}
			}
			return null;
		}

		// Token: 0x06008D63 RID: 36195 RVA: 0x001D9668 File Offset: 0x001D7868
		public static bool TryGetCredential<T>(this ResourceCredentialCollection credentials, out T result) where T : IResourceCredential
		{
			foreach (IResourceCredential resourceCredential in credentials)
			{
				if (resourceCredential is T)
				{
					result = (T)((object)resourceCredential);
					return true;
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x06008D64 RID: 36196 RVA: 0x001D96CC File Offset: 0x001D78CC
		public static bool IsOAuth(this ResourceCredentialCollection credentials)
		{
			return credentials.RemoveAdornments() is OAuthCredential;
		}

		// Token: 0x06008D65 RID: 36197 RVA: 0x001D96DC File Offset: 0x001D78DC
		public static bool HasRefreshableCredential(this ResourceCredentialCollection credentials)
		{
			IResourceCredential resourceCredential = credentials.RemoveAdornments();
			ResourceKindInfo resourceKindInfo;
			return ResourceKinds.Lookup(credentials.Resource.Kind, out resourceKindInfo) && resourceKindInfo.CanRefresh(resourceCredential);
		}

		// Token: 0x06008D66 RID: 36198 RVA: 0x001D9710 File Offset: 0x001D7910
		public static bool IsSameAs(this ResourceCredentialCollection credentials, ResourceCredentialCollection other)
		{
			if (credentials == other)
			{
				return true;
			}
			foreach (IResourceCredential resourceCredential in credentials)
			{
				Type type = resourceCredential.GetType();
				IResourceCredential resourceCredential2 = other.FirstOrDefault((IResourceCredential c) => c.GetType() == type);
				if (resourceCredential2 == null)
				{
					return false;
				}
				using (IEnumerator<string> enumerator2 = ResourceCredentialCollectionExtensions.GetCacheParts(resourceCredential).GetEnumerator())
				{
					using (IEnumerator<string> enumerator3 = ResourceCredentialCollectionExtensions.GetCacheParts(resourceCredential2).GetEnumerator())
					{
						for (;;)
						{
							bool flag = enumerator2.MoveNext();
							if (flag != enumerator3.MoveNext() || (flag && enumerator2.Current != enumerator3.Current))
							{
								break;
							}
							if (!flag)
							{
								goto Block_11;
							}
						}
						return false;
						Block_11:;
					}
				}
			}
			return true;
		}

		// Token: 0x06008D67 RID: 36199 RVA: 0x001D9814 File Offset: 0x001D7A14
		private static IEnumerable<string> GetCacheParts(IResourceCredential credential)
		{
			string text;
			if (credential is OAuthCredential && JwtParser.TryParseSubject(((OAuthCredential)credential).AccessToken, out text))
			{
				return new string[] { text };
			}
			return credential.GetCacheParts();
		}

		// Token: 0x04004CFB RID: 19707
		private static readonly HashSet<Type> adornmentTypes = new HashSet<Type>
		{
			typeof(ExchangeCredentialAdornment),
			typeof(EncryptedConnectionAdornment),
			typeof(ConnectionStringPropertiesAdornment),
			typeof(ApplicationCredentialPropertiesAdornment),
			typeof(ConnectionStringAdornment)
		};
	}
}
