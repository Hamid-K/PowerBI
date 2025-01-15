using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Azure.Core
{
	// Token: 0x0200005B RID: 91
	[NullableContext(1)]
	[Nullable(0)]
	public class RequestUriBuilder
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00008B15 File Offset: 0x00006D15
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00008B1D File Offset: 0x00006D1D
		[Nullable(2)]
		public string Scheme
		{
			[NullableContext(2)]
			get
			{
				return this._scheme;
			}
			[NullableContext(2)]
			set
			{
				this.ResetUri();
				this._scheme = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00008B2C File Offset: 0x00006D2C
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00008B34 File Offset: 0x00006D34
		[Nullable(2)]
		public string Host
		{
			[NullableContext(2)]
			get
			{
				return this._host;
			}
			[NullableContext(2)]
			set
			{
				this.ResetUri();
				this._host = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00008B43 File Offset: 0x00006D43
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00008B4B File Offset: 0x00006D4B
		public int Port
		{
			get
			{
				return this._port;
			}
			set
			{
				this.ResetUri();
				this._port = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00008B5A File Offset: 0x00006D5A
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00008B90 File Offset: 0x00006D90
		public string Query
		{
			get
			{
				if (!this.HasQuery)
				{
					return string.Empty;
				}
				return this._pathAndQuery.ToString(this._queryIndex, this._pathAndQuery.Length - this._queryIndex);
			}
			set
			{
				this.ResetUri();
				if (this.HasQuery)
				{
					this._pathAndQuery.Remove(this._queryIndex, this._pathAndQuery.Length - this._queryIndex);
					this._queryIndex = -1;
				}
				if (!string.IsNullOrEmpty(value))
				{
					this._queryIndex = this._pathAndQuery.Length;
					if (value[0] != '?')
					{
						this._pathAndQuery.Append('?');
					}
					this._pathAndQuery.Append(value);
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00008C15 File Offset: 0x00006E15
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00008C40 File Offset: 0x00006E40
		public string Path
		{
			get
			{
				if (!this.HasQuery)
				{
					return this._pathAndQuery.ToString();
				}
				return this._pathAndQuery.ToString(0, this._queryIndex);
			}
			set
			{
				if (this.HasQuery)
				{
					this._pathAndQuery.Remove(0, this._queryIndex);
					this._pathAndQuery.Insert(0, value);
					this._queryIndex = value.Length;
					return;
				}
				this._pathAndQuery.Remove(0, this._pathAndQuery.Length);
				this._pathAndQuery.Append(value);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00008CA8 File Offset: 0x00006EA8
		protected bool HasPath
		{
			get
			{
				return this.PathLength > 0;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00008CB3 File Offset: 0x00006EB3
		protected bool HasQuery
		{
			get
			{
				return this._queryIndex != -1;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00008CC1 File Offset: 0x00006EC1
		private int PathLength
		{
			get
			{
				if (!this.HasQuery)
				{
					return this._pathAndQuery.Length;
				}
				return this._queryIndex;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00008CDD File Offset: 0x00006EDD
		private int QueryLength
		{
			get
			{
				if (!this.HasQuery)
				{
					return 0;
				}
				return this._pathAndQuery.Length - this._queryIndex;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00008CFB File Offset: 0x00006EFB
		public string PathAndQuery
		{
			get
			{
				return this._pathAndQuery.ToString();
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00008D08 File Offset: 0x00006F08
		public void Reset(Uri value)
		{
			this.Scheme = value.Scheme;
			this.Host = value.Host;
			this.Port = value.Port;
			this.Path = value.AbsolutePath;
			this.Query = value.Query;
			this._uri = value;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00008D58 File Offset: 0x00006F58
		public Uri ToUri()
		{
			if (this._uri == null)
			{
				this._uri = new Uri(this.ToString());
			}
			return this._uri;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00008D7F File Offset: 0x00006F7F
		public void AppendQuery(string name, string value)
		{
			this.AppendQuery(name, value, true);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00008D8A File Offset: 0x00006F8A
		public void AppendQuery(string name, string value, bool escapeValue)
		{
			if (escapeValue && !string.IsNullOrEmpty(value))
			{
				value = Uri.EscapeDataString(value);
			}
			this.AppendQuery(MemoryExtensions.AsSpan(name), MemoryExtensions.AsSpan(value), false);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00008DB4 File Offset: 0x00006FB4
		[NullableContext(0)]
		public void AppendQuery(ReadOnlySpan<char> name, ReadOnlySpan<char> value, bool escapeValue)
		{
			this.ResetUri();
			if (!this.HasQuery)
			{
				this._queryIndex = this._pathAndQuery.Length;
				this._pathAndQuery.Append('?');
			}
			else if (this.QueryLength != 1 || this._pathAndQuery[this._queryIndex] != '?')
			{
				this._pathAndQuery.Append('&');
			}
			this._pathAndQuery.Append(name.ToString());
			this._pathAndQuery.Append('=');
			if (escapeValue && !value.IsEmpty)
			{
				this._pathAndQuery.Append(Uri.EscapeDataString(value.ToString()));
				return;
			}
			this._pathAndQuery.Append(value.ToString());
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00008E87 File Offset: 0x00007087
		public void AppendPath(string value)
		{
			this.AppendPath(value, true);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008E91 File Offset: 0x00007091
		public void AppendPath(string value, bool escape)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			this.AppendPath(MemoryExtensions.AsSpan(value), escape);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00008EAC File Offset: 0x000070AC
		[NullableContext(0)]
		public unsafe void AppendPath(ReadOnlySpan<char> value, bool escape)
		{
			if (value.IsEmpty)
			{
				return;
			}
			this.ResetUri();
			int num = 0;
			if (this.PathLength == 1 && this._pathAndQuery[0] == '/' && *value[0] == 47)
			{
				num = 1;
			}
			string text = value.Slice(num).ToString();
			if (escape)
			{
				text = Uri.EscapeDataString(text);
			}
			if (this.HasQuery)
			{
				this._pathAndQuery.Insert(this._queryIndex, text);
				this._queryIndex += text.Length;
				return;
			}
			this._pathAndQuery.Append(text);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00008F50 File Offset: 0x00007150
		public override string ToString()
		{
			string scheme = this.Scheme;
			int num = ((scheme != null) ? scheme.Length : 0) + 3;
			string host = this.Host;
			StringBuilder stringBuilder = new StringBuilder(num + ((host != null) ? host.Length : 0) + this._pathAndQuery.Length + 10);
			stringBuilder.Append(this.Scheme);
			stringBuilder.Append("://");
			stringBuilder.Append(this.Host);
			if (!this.HasDefaultPortForScheme)
			{
				stringBuilder.Append(':');
				stringBuilder.Append(this.Port);
			}
			if (this._pathAndQuery.Length == 0 || this._pathAndQuery[0] != '/')
			{
				stringBuilder.Append('/');
			}
			stringBuilder.Append(this._pathAndQuery);
			return stringBuilder.ToString();
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00009018 File Offset: 0x00007218
		private bool HasDefaultPortForScheme
		{
			get
			{
				return (this.Port == 80 && string.Equals(this.Scheme, "http", StringComparison.InvariantCultureIgnoreCase)) || (this.Port == 443 && string.Equals(this.Scheme, "https", StringComparison.InvariantCultureIgnoreCase));
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009064 File Offset: 0x00007264
		private void ResetUri()
		{
			this._uri = null;
		}

		// Token: 0x0400013D RID: 317
		private const char QuerySeparator = '?';

		// Token: 0x0400013E RID: 318
		private const char PathSeparator = '/';

		// Token: 0x0400013F RID: 319
		private readonly StringBuilder _pathAndQuery = new StringBuilder();

		// Token: 0x04000140 RID: 320
		private int _queryIndex = -1;

		// Token: 0x04000141 RID: 321
		[Nullable(2)]
		private Uri _uri;

		// Token: 0x04000142 RID: 322
		private int _port;

		// Token: 0x04000143 RID: 323
		[Nullable(2)]
		private string _host;

		// Token: 0x04000144 RID: 324
		[Nullable(2)]
		private string _scheme;
	}
}
