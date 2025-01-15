using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Microsoft.Mashup.Engine1.Library.Uris.Internal
{
	// Token: 0x020002C9 RID: 713
	internal class InternalUriBuilder
	{
		// Token: 0x06001C36 RID: 7222 RVA: 0x00043570 File Offset: 0x00041770
		public InternalUriBuilder()
		{
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000435EC File Offset: 0x000417EC
		public InternalUriBuilder(string uri)
		{
			InternalUri internalUri = new InternalUri(uri, UriKind.RelativeOrAbsolute);
			if (internalUri.IsAbsoluteUri)
			{
				this.Init(internalUri);
				return;
			}
			uri = InternalUri.UriSchemeHttp + InternalUri.SchemeDelimiter + uri;
			this.Init(new InternalUri(uri));
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0004369C File Offset: 0x0004189C
		public InternalUriBuilder(InternalUri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.Init(uri);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0004372C File Offset: 0x0004192C
		private void Init(InternalUri uri)
		{
			this.m_fragment = uri.Fragment;
			this.m_query = uri.Query;
			this.m_host = uri.Host;
			this.m_path = uri.AbsolutePath;
			this.m_port = uri.Port;
			this.m_scheme = uri.Scheme;
			this.m_schemeDelimiter = (uri.HasAuthority ? InternalUri.SchemeDelimiter : ":");
			string userInfo = uri.UserInfo;
			if (!string.IsNullOrEmpty(userInfo))
			{
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					this.m_password = userInfo.Substring(num + 1);
					this.m_username = userInfo.Substring(0, num);
				}
				else
				{
					this.m_username = userInfo;
				}
			}
			this.SetFieldsFromUri(uri);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000437E4 File Offset: 0x000419E4
		public InternalUriBuilder(string schemeName, string hostName)
		{
			this.Scheme = schemeName;
			this.Host = hostName;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0004386B File Offset: 0x00041A6B
		public InternalUriBuilder(string scheme, string host, int portNumber)
			: this(scheme, host)
		{
			this.Port = portNumber;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0004387C File Offset: 0x00041A7C
		public InternalUriBuilder(string scheme, string host, int port, string pathValue)
			: this(scheme, host, port)
		{
			this.Path = pathValue;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00043890 File Offset: 0x00041A90
		public InternalUriBuilder(string scheme, string host, int port, string path, string extraValue)
			: this(scheme, host, port, path)
		{
			try
			{
				this.Extra = extraValue;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new ArgumentException("extraValue");
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (set) Token: 0x06001C3E RID: 7230 RVA: 0x000438E8 File Offset: 0x00041AE8
		private string Extra
		{
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length <= 0)
				{
					this.Fragment = string.Empty;
					this.Query = string.Empty;
					return;
				}
				if (value[0] == '#')
				{
					this.Fragment = value.Substring(1);
					return;
				}
				if (value[0] == '?')
				{
					int num = value.IndexOf('#');
					if (num == -1)
					{
						num = value.Length;
					}
					else
					{
						this.Fragment = value.Substring(num + 1);
					}
					this.Query = value.Substring(1, num - 1);
					return;
				}
				throw new ArgumentException("value");
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x00043983 File Offset: 0x00041B83
		// (set) Token: 0x06001C40 RID: 7232 RVA: 0x0004398B File Offset: 0x00041B8B
		public string Fragment
		{
			get
			{
				return this.m_fragment;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length > 0)
				{
					value = "#" + value;
				}
				this.m_fragment = value;
				this.m_changed = true;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x000439BB File Offset: 0x00041BBB
		// (set) Token: 0x06001C42 RID: 7234 RVA: 0x000439C4 File Offset: 0x00041BC4
		public string Host
		{
			get
			{
				return this.m_host;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.m_host = value;
				if (this.m_host.IndexOf(':') >= 0 && this.m_host[0] != '[')
				{
					this.m_host = "[" + this.m_host + "]";
				}
				this.m_changed = true;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x00043A24 File Offset: 0x00041C24
		// (set) Token: 0x06001C44 RID: 7236 RVA: 0x00043A2C File Offset: 0x00041C2C
		public string Password
		{
			get
			{
				return this.m_password;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.m_password = value;
				this.m_changed = true;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x00043A46 File Offset: 0x00041C46
		// (set) Token: 0x06001C46 RID: 7238 RVA: 0x00043A4E File Offset: 0x00041C4E
		public string Path
		{
			get
			{
				return this.m_path;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = "/";
				}
				this.m_path = InternalUri.InternalEscapeString(this.ConvertSlashes(value), this.m_uri.ShouldUseLegacyV2Quirks);
				this.m_changed = true;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00043A86 File Offset: 0x00041C86
		// (set) Token: 0x06001C48 RID: 7240 RVA: 0x00043A8E File Offset: 0x00041C8E
		public int Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				if (value < -1 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_port = value;
				this.m_changed = true;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00043AB5 File Offset: 0x00041CB5
		// (set) Token: 0x06001C4A RID: 7242 RVA: 0x00043ABD File Offset: 0x00041CBD
		public string Query
		{
			get
			{
				return this.m_query;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length > 0)
				{
					value = "?" + value;
				}
				this.m_query = value;
				this.m_changed = true;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00043AED File Offset: 0x00041CED
		// (set) Token: 0x06001C4C RID: 7244 RVA: 0x00043AF8 File Offset: 0x00041CF8
		public string Scheme
		{
			get
			{
				return this.m_scheme;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				int num = value.IndexOf(':');
				if (num != -1)
				{
					value = value.Substring(0, num);
				}
				if (value.Length != 0)
				{
					if (!InternalUri.CheckSchemeName(value))
					{
						throw new ArgumentException("value");
					}
					value = value.ToLower(CultureInfo.InvariantCulture);
				}
				this.m_scheme = value;
				this.m_changed = true;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00043B5C File Offset: 0x00041D5C
		public InternalUri Uri
		{
			get
			{
				if (this.m_changed)
				{
					this.m_uri = new InternalUri(this.ToString());
					this.SetFieldsFromUri(this.m_uri);
					this.m_changed = false;
				}
				return this.m_uri;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x00043B90 File Offset: 0x00041D90
		// (set) Token: 0x06001C4F RID: 7247 RVA: 0x00043B98 File Offset: 0x00041D98
		public string UserName
		{
			get
			{
				return this.m_username;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.m_username = value;
				this.m_changed = true;
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00043BB4 File Offset: 0x00041DB4
		private string ConvertSlashes(string path)
		{
			StringBuilder stringBuilder = new StringBuilder(path.Length);
			foreach (char c in path)
			{
				if (c == '\\')
				{
					c = '/';
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00043BFC File Offset: 0x00041DFC
		public override bool Equals(object rparam)
		{
			return rparam != null && this.Uri.Equals(rparam.ToString());
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00043C14 File Offset: 0x00041E14
		public override int GetHashCode()
		{
			return this.Uri.GetHashCode();
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00043C24 File Offset: 0x00041E24
		private void SetFieldsFromUri(InternalUri uri)
		{
			this.m_fragment = uri.Fragment;
			this.m_query = uri.Query;
			this.m_host = uri.Host;
			this.m_path = uri.AbsolutePath;
			this.m_port = uri.Port;
			this.m_scheme = uri.Scheme;
			this.m_schemeDelimiter = (uri.HasAuthority ? InternalUri.SchemeDelimiter : ":");
			string userInfo = uri.UserInfo;
			if (userInfo.Length > 0)
			{
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					this.m_password = userInfo.Substring(num + 1);
					this.m_username = userInfo.Substring(0, num);
					return;
				}
				this.m_username = userInfo;
			}
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00043CD8 File Offset: 0x00041ED8
		public override string ToString()
		{
			if (this.m_username.Length == 0 && this.m_password.Length > 0)
			{
				throw new UriFormatException(SR.GetString("net_uri_BadUserPassword"));
			}
			if (this.m_scheme.Length != 0)
			{
				InternalUriParser syntax = InternalUriParser.GetSyntax(this.m_scheme);
				if (syntax != null)
				{
					this.m_schemeDelimiter = ((syntax.InFact(UriSyntaxFlags.MustHaveAuthority) || (this.m_host.Length != 0 && syntax.NotAny(UriSyntaxFlags.MailToLikeUri) && syntax.InFact(UriSyntaxFlags.OptionalAuthority))) ? InternalUri.SchemeDelimiter : ":");
				}
				else
				{
					this.m_schemeDelimiter = ((this.m_host.Length != 0) ? InternalUri.SchemeDelimiter : ":");
				}
			}
			string text = ((this.m_scheme.Length != 0) ? (this.m_scheme + this.m_schemeDelimiter) : string.Empty);
			return string.Concat(new string[]
			{
				text,
				this.m_username,
				(this.m_password.Length > 0) ? (":" + this.m_password) : string.Empty,
				(this.m_username.Length > 0) ? "@" : string.Empty,
				this.m_host,
				(this.m_port != -1 && this.m_host.Length > 0) ? (":" + this.m_port.ToString()) : string.Empty,
				(this.m_host.Length > 0 && this.m_path.Length != 0 && this.m_path[0] != '/') ? "/" : string.Empty,
				this.m_path,
				this.m_query,
				this.m_fragment
			});
		}

		// Token: 0x0400092B RID: 2347
		private bool m_changed = true;

		// Token: 0x0400092C RID: 2348
		private string m_fragment = string.Empty;

		// Token: 0x0400092D RID: 2349
		private string m_host = "localhost";

		// Token: 0x0400092E RID: 2350
		private string m_password = string.Empty;

		// Token: 0x0400092F RID: 2351
		private string m_path = "/";

		// Token: 0x04000930 RID: 2352
		private int m_port = -1;

		// Token: 0x04000931 RID: 2353
		private string m_query = string.Empty;

		// Token: 0x04000932 RID: 2354
		private string m_scheme = "http";

		// Token: 0x04000933 RID: 2355
		private string m_schemeDelimiter = InternalUri.SchemeDelimiter;

		// Token: 0x04000934 RID: 2356
		private InternalUri m_uri;

		// Token: 0x04000935 RID: 2357
		private string m_username = string.Empty;
	}
}
