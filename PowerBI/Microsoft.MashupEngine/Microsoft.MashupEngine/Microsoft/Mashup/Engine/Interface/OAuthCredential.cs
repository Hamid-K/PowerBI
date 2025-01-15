using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200009A RID: 154
	public class OAuthCredential : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00003876 File Offset: 0x00001A76
		public OAuthCredential(string accessToken, string expires, string refreshToken, Dictionary<string, string> properties)
		{
			this.accessToken = accessToken;
			this.expires = expires;
			this.refreshToken = refreshToken;
			this.properties = properties ?? new Dictionary<string, string>();
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000038A4 File Offset: 0x00001AA4
		public string AccessToken
		{
			get
			{
				return this.accessToken;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000038AC File Offset: 0x00001AAC
		public string Expires
		{
			get
			{
				return this.expires;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000038B4 File Offset: 0x00001AB4
		public string RefreshToken
		{
			get
			{
				return this.refreshToken;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000038BC File Offset: 0x00001ABC
		public Dictionary<string, string> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000038C4 File Offset: 0x00001AC4
		public override int GetHashCode()
		{
			return this.accessToken.GetHashCode();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000038D1 File Offset: 0x00001AD1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as OAuthCredential);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000038D1 File Offset: 0x00001AD1
		public bool Equals(IResourceCredential other)
		{
			return this.Equals(other as OAuthCredential);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000038E0 File Offset: 0x00001AE0
		public bool Equals(OAuthCredential other)
		{
			if (other == null || other.AccessToken != this.AccessToken || other.Expires != this.Expires || other.RefreshToken != this.RefreshToken || other.Properties.Count != this.Properties.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, string> keyValuePair in this.Properties)
			{
				string text;
				if (!other.Properties.TryGetValue(keyValuePair.Key, out text) || keyValuePair.Value != text)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000039B0 File Offset: 0x00001BB0
		public IEnumerable<string> GetCacheParts()
		{
			if (string.IsNullOrEmpty(this.refreshToken))
			{
				yield return this.accessToken;
			}
			else
			{
				yield return this.refreshToken;
			}
			if (this.Properties.Count > 0)
			{
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(this.Properties);
				list.Sort((KeyValuePair<string, string> a, KeyValuePair<string, string> b) => string.CompareOrdinal(a.Key, b.Key));
				foreach (KeyValuePair<string, string> pair in list)
				{
					yield return pair.Key;
					yield return pair.Value;
					pair = default(KeyValuePair<string, string>);
				}
				List<KeyValuePair<string, string>>.Enumerator enumerator = default(List<KeyValuePair<string, string>>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x04000182 RID: 386
		private readonly string accessToken;

		// Token: 0x04000183 RID: 387
		private readonly string expires;

		// Token: 0x04000184 RID: 388
		private readonly string refreshToken;

		// Token: 0x04000185 RID: 389
		private readonly Dictionary<string, string> properties;
	}
}
