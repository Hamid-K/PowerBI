using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001B1 RID: 433
	[DataContract]
	[Serializable]
	public class ScrubbedUri : IContainsPrivateInformation
	{
		// Token: 0x06000B25 RID: 2853 RVA: 0x00026DBC File Offset: 0x00024FBC
		public ScrubbedUri(string uri)
		{
			if (!Uri.TryCreate(uri, UriKind.Absolute, out this.m_uri))
			{
				this.m_uri = null;
				this.m_scrubbedRawUri = new ScrubbedString(uri);
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00026E0B File Offset: 0x0002500B
		public ScrubbedUri(string uri, bool scrub)
			: this(uri)
		{
			this.m_markAsPrivate = scrub;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00026E1C File Offset: 0x0002501C
		public ScrubbedUri(Uri uri)
		{
			if (uri == null)
			{
				return;
			}
			if (uri.IsAbsoluteUri)
			{
				this.m_uri = uri;
				return;
			}
			this.m_scrubbedRawUri = new ScrubbedString(uri.ToString());
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00026E74 File Offset: 0x00025074
		public ScrubbedUri(Uri uri, bool scrub)
			: this(uri)
		{
			this.m_markAsPrivate = scrub;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00026E84 File Offset: 0x00025084
		public override string ToString()
		{
			if (this.m_uri != null)
			{
				if (!this.m_markAsPrivate)
				{
					return this.m_uri.ToString();
				}
				return this.m_uri.ToString().MarkAsPrivate();
			}
			else
			{
				if (this.m_scrubbedRawUri == null)
				{
					return string.Empty;
				}
				if (!this.m_markAsPrivate)
				{
					return this.m_scrubbedRawUri.ToString();
				}
				return this.m_scrubbedRawUri.ToString().MarkAsPrivate();
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00026EF8 File Offset: 0x000250F8
		public string ToPrivateString()
		{
			if (this.m_uri != null)
			{
				if (this.m_scrubbedResult.Equals(string.Empty))
				{
					this.m_scrubbedResult = new UriBuilder(this.m_uri.Scheme, this.m_uri.Host, this.m_uri.Port)
					{
						Path = this.ScrubPath(),
						Query = this.ScrubQuery(),
						Fragment = this.ScrubFragment()
					}.Uri.ToString();
				}
				return this.m_scrubbedResult;
			}
			if (this.m_scrubbedRawUri != null)
			{
				return this.m_scrubbedRawUri.ToPrivateString();
			}
			return string.Empty;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00026FA8 File Offset: 0x000251A8
		public string ToInternalString()
		{
			if (this.m_uri != null)
			{
				if (this.m_taggedResult.Equals(string.Empty))
				{
					this.m_taggedResult = new UriBuilder(this.m_uri.Scheme, this.m_uri.Host, this.m_uri.Port)
					{
						Path = this.TagPath(),
						Query = this.TagQuery(),
						Fragment = this.TagFragment()
					}.Uri.ToString();
				}
				return this.m_taggedResult;
			}
			if (this.m_scrubbedRawUri != null)
			{
				return this.m_scrubbedRawUri.ToInternalString();
			}
			return string.Empty;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00027057 File Offset: 0x00025257
		public string ToOriginalString()
		{
			if (this.m_uri != null)
			{
				return this.m_uri.ToString();
			}
			if (this.m_scrubbedRawUri == null)
			{
				return string.Empty;
			}
			return this.m_scrubbedRawUri.ToOriginalString();
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002708C File Offset: 0x0002528C
		protected virtual string ScrubPath()
		{
			return this.m_uri.AbsolutePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).StringJoin("/", (string path) => path.MarkAsPrivate());
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000270E0 File Offset: 0x000252E0
		protected virtual string ScrubQuery()
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(this.m_uri.Query);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in nameValueCollection.AllKeys)
			{
				stringBuilder.Append(text);
				stringBuilder.Append("=");
				stringBuilder.Append(nameValueCollection[text].MarkAsPrivate());
				stringBuilder.Append("&");
			}
			if (stringBuilder.Length != 0)
			{
				return stringBuilder.ToString(0, stringBuilder.Length - 1);
			}
			return string.Empty;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00027170 File Offset: 0x00025370
		protected virtual string ScrubFragment()
		{
			return this.m_uri.Fragment;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00027180 File Offset: 0x00025380
		protected virtual string TagPath()
		{
			return this.m_uri.AbsolutePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).StringJoin("/", (string path) => path.MarkAsInternal());
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000271D4 File Offset: 0x000253D4
		protected virtual string TagQuery()
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(this.m_uri.Query);
			foreach (string text in nameValueCollection.AllKeys)
			{
				nameValueCollection[text] = nameValueCollection[text].MarkAsInternal();
			}
			return nameValueCollection.ToString();
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00027170 File Offset: 0x00025370
		protected virtual string TagFragment()
		{
			return this.m_uri.Fragment;
		}

		// Token: 0x04000463 RID: 1123
		[DataMember]
		protected readonly Uri m_uri;

		// Token: 0x04000464 RID: 1124
		[DataMember]
		private ScrubbedString m_scrubbedRawUri;

		// Token: 0x04000465 RID: 1125
		[DataMember]
		private volatile string m_scrubbedResult = string.Empty;

		// Token: 0x04000466 RID: 1126
		[DataMember]
		private volatile string m_taggedResult = string.Empty;

		// Token: 0x04000467 RID: 1127
		[DataMember]
		private readonly bool m_markAsPrivate;
	}
}
