using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Extensions;
using AngleSharp.Network;

namespace AngleSharp
{
	// Token: 0x02000019 RID: 25
	public sealed class Url : IEquatable<Url>
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00004400 File Offset: 0x00002600
		private Url(string scheme, string host, string port)
		{
			this._schemeData = string.Empty;
			this._path = string.Empty;
			this._scheme = scheme;
			this._host = host;
			this._port = port;
			this._relative = ProtocolNames.IsRelative(this._scheme);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000444F File Offset: 0x0000264F
		public Url(string address)
		{
			this._error = this.ParseUrl(address, null);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004465 File Offset: 0x00002665
		public Url(Url baseAddress, string relativeAddress)
		{
			this._error = this.ParseUrl(relativeAddress, baseAddress);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000447C File Offset: 0x0000267C
		public Url(Url address)
		{
			this._fragment = address._fragment;
			this._query = address._query;
			this._path = address._path;
			this._scheme = address._scheme;
			this._port = address._port;
			this._host = address._host;
			this._username = address._username;
			this._password = address._password;
			this._relative = address._relative;
			this._schemeData = address._schemeData;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004507 File Offset: 0x00002707
		public static Url Create(string address)
		{
			return new Url(address);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000450F File Offset: 0x0000270F
		public static Url Convert(Uri uri)
		{
			return new Url(uri.OriginalString);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000451C File Offset: 0x0000271C
		public string Origin
		{
			get
			{
				if (this._scheme.Is(ProtocolNames.Blob))
				{
					Url url = new Url(this._schemeData);
					if (!url.IsInvalid)
					{
						return url.Origin;
					}
				}
				else if (ProtocolNames.IsOriginable(this._scheme))
				{
					StringBuilder stringBuilder = Pool.NewStringBuilder();
					if (!string.IsNullOrEmpty(this._host))
					{
						if (!string.IsNullOrEmpty(this._scheme))
						{
							stringBuilder.Append(this._scheme).Append(':');
						}
						stringBuilder.Append('/').Append('/').Append(this._host);
						if (!string.IsNullOrEmpty(this._port))
						{
							stringBuilder.Append(':').Append(this._port);
						}
					}
					return stringBuilder.ToPool();
				}
				return null;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000045DE File Offset: 0x000027DE
		public bool IsInvalid
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000045E6 File Offset: 0x000027E6
		public bool IsRelative
		{
			get
			{
				return this._relative && string.IsNullOrEmpty(this._scheme);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000045FD File Offset: 0x000027FD
		public bool IsAbsolute
		{
			get
			{
				return !this.IsRelative;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004608 File Offset: 0x00002808
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00004610 File Offset: 0x00002810
		public string UserName
		{
			get
			{
				return this._username;
			}
			set
			{
				this._username = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004619 File Offset: 0x00002819
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004621 File Offset: 0x00002821
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000462A File Offset: 0x0000282A
		public string Data
		{
			get
			{
				return this._schemeData;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004632 File Offset: 0x00002832
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000463A File Offset: 0x0000283A
		public string Fragment
		{
			get
			{
				return this._fragment;
			}
			set
			{
				if (value == null)
				{
					this._fragment = null;
					return;
				}
				this.ParseFragment(value, 0);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004650 File Offset: 0x00002850
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004681 File Offset: 0x00002881
		public string Host
		{
			get
			{
				return this.HostName + (string.IsNullOrEmpty(this._port) ? string.Empty : (":" + this._port));
			}
			set
			{
				this.ParseHostName(value ?? string.Empty, 0, false, true);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004697 File Offset: 0x00002897
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000469F File Offset: 0x0000289F
		public string HostName
		{
			get
			{
				return this._host;
			}
			set
			{
				this.ParseHostName(value ?? string.Empty, 0, true, false);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000046B5 File Offset: 0x000028B5
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000046BD File Offset: 0x000028BD
		public string Href
		{
			get
			{
				return this.Serialize();
			}
			set
			{
				this._error = this.ParseUrl(value ?? string.Empty, null);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000046D6 File Offset: 0x000028D6
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000046DE File Offset: 0x000028DE
		public string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this.ParsePath(value ?? string.Empty, 0, true);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000046F3 File Offset: 0x000028F3
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000046FB File Offset: 0x000028FB
		public string Port
		{
			get
			{
				return this._port;
			}
			set
			{
				this.ParsePort(value ?? string.Empty, 0, true);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004710 File Offset: 0x00002910
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004718 File Offset: 0x00002918
		public string Scheme
		{
			get
			{
				return this._scheme;
			}
			set
			{
				this.ParseScheme(value ?? string.Empty, true);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000472C File Offset: 0x0000292C
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004734 File Offset: 0x00002934
		public string Query
		{
			get
			{
				return this._query;
			}
			set
			{
				this.ParseQuery(value ?? string.Empty, 0, true);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004749 File Offset: 0x00002949
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004754 File Offset: 0x00002954
		public override bool Equals(object obj)
		{
			Url url = obj as Url;
			return url != null && this.Equals(url);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004774 File Offset: 0x00002974
		public bool Equals(Url other)
		{
			return this._fragment.Is(other._fragment) && this._query.Is(other._query) && this._path.Is(other._path) && this._scheme.Isi(other._scheme) && this._port.Is(other._port) && this._host.Isi(other._host) && this._username.Is(other._username) && this._password.Is(other._password) && this._schemeData.Is(other._schemeData);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004832 File Offset: 0x00002A32
		public static implicit operator Uri(Url value)
		{
			return new Uri(value.Serialize(), value.IsRelative ? UriKind.Relative : UriKind.Absolute);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000046B5 File Offset: 0x000028B5
		public override string ToString()
		{
			return this.Serialize();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000484C File Offset: 0x00002A4C
		private string Serialize()
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			if (!string.IsNullOrEmpty(this._scheme))
			{
				stringBuilder.Append(this._scheme).Append(':');
			}
			if (this._relative)
			{
				if (!string.IsNullOrEmpty(this._host) || !string.IsNullOrEmpty(this._scheme))
				{
					stringBuilder.Append('/').Append('/');
					if (!string.IsNullOrEmpty(this._username) || this._password != null)
					{
						stringBuilder.Append(this._username);
						if (this._password != null)
						{
							stringBuilder.Append(':').Append(this._password);
						}
						stringBuilder.Append('@');
					}
					stringBuilder.Append(this._host);
					if (!string.IsNullOrEmpty(this._port))
					{
						stringBuilder.Append(':').Append(this._port);
					}
					stringBuilder.Append('/');
				}
				stringBuilder.Append(this._path);
			}
			else
			{
				stringBuilder.Append(this._schemeData);
			}
			if (this._query != null)
			{
				stringBuilder.Append('?').Append(this._query);
			}
			if (this._fragment != null)
			{
				stringBuilder.Append('#').Append(this._fragment);
			}
			return stringBuilder.ToPool();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004990 File Offset: 0x00002B90
		private bool ParseUrl(string input, Url baseUrl = null)
		{
			this.Reset(baseUrl ?? Url.DefaultBase);
			return !this.ParseScheme(input.Trim(), false);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000049B4 File Offset: 0x00002BB4
		private void Reset(Url baseUrl)
		{
			this._schemeData = string.Empty;
			this._scheme = baseUrl._scheme;
			this._host = baseUrl._host;
			this._path = baseUrl._path;
			this._port = baseUrl._port;
			this._relative = ProtocolNames.IsRelative(this._scheme);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004A10 File Offset: 0x00002C10
		private bool ParseScheme(string input, bool onlyScheme = false)
		{
			if (input.Length > 0 && input[0].IsLetter())
			{
				int i = 1;
				while (i < input.Length)
				{
					char c = input[i];
					if (c.IsAlphanumericAscii() || c == '+' || c == '-' || c == '.')
					{
						i++;
					}
					else
					{
						if (c != ':')
						{
							break;
						}
						string scheme = this._scheme;
						this._scheme = input.Substring(0, i).ToLowerInvariant();
						if (onlyScheme)
						{
							return true;
						}
						this._relative = ProtocolNames.IsRelative(this._scheme);
						if (this._scheme.Is(ProtocolNames.File))
						{
							this._host = string.Empty;
							this._port = string.Empty;
							return this.RelativeState(input, i + 1);
						}
						if (!this._relative)
						{
							this._host = string.Empty;
							this._port = string.Empty;
							this._path = string.Empty;
							return this.ParseSchemeData(input, i + 1);
						}
						if (!this._scheme.Is(scheme))
						{
							if (i < input.Length - 1 && input[++i] == '/' && ++i < input.Length && input[i] == '/')
							{
								i++;
							}
							return this.IgnoreSlashesState(input, i);
						}
						c = input[++i];
						if (c == '/' && i + 2 < input.Length && input[i + 1] == '/')
						{
							return this.IgnoreSlashesState(input, i + 2);
						}
						return this.RelativeState(input, i);
					}
				}
			}
			return !onlyScheme && this.RelativeState(input, 0);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004BB0 File Offset: 0x00002DB0
		private bool ParseSchemeData(string input, int index)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			while (index < input.Length)
			{
				char c = input[index];
				if (c == '?')
				{
					this._schemeData = stringBuilder.ToPool();
					return this.ParseQuery(input, index + 1, false);
				}
				if (c == '#')
				{
					this._schemeData = stringBuilder.ToPool();
					return this.ParseFragment(input, index + 1);
				}
				if (c == '%' && index + 2 < input.Length && input[index + 1].IsHex() && input[index + 2].IsHex())
				{
					stringBuilder.Append(input[index++]);
					stringBuilder.Append(input[index++]);
					stringBuilder.Append(input[index]);
				}
				else if (c.IsInRange(32, 126))
				{
					stringBuilder.Append(c);
				}
				else if (c != '\t' && c != '\n' && c != '\r')
				{
					index += Url.Utf8PercentEncode(stringBuilder, input, index);
				}
				index++;
			}
			this._schemeData = stringBuilder.ToPool();
			return true;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004CC0 File Offset: 0x00002EC0
		private bool RelativeState(string input, int index)
		{
			this._relative = true;
			if (index != input.Length)
			{
				char c = input[index];
				if (c <= '/')
				{
					if (c == '#')
					{
						return this.ParseFragment(input, index + 1);
					}
					if (c != '/')
					{
						goto IL_00DD;
					}
				}
				else
				{
					if (c == '?')
					{
						return this.ParseQuery(input, index + 1, false);
					}
					if (c != '\\')
					{
						goto IL_00DD;
					}
				}
				if (index == input.Length - 1)
				{
					return this.ParsePath(input, index, false);
				}
				if (!input[++index].IsOneOf('/', '\\'))
				{
					if (this._scheme.Is(ProtocolNames.File))
					{
						this._host = string.Empty;
						this._port = string.Empty;
					}
					return this.ParsePath(input, index - 1, false);
				}
				if (this._scheme.Is(ProtocolNames.File))
				{
					return this.ParseFileHost(input, index + 1);
				}
				return this.IgnoreSlashesState(input, index + 1);
				IL_00DD:
				if (input[index].IsLetter() && this._scheme.Is(ProtocolNames.File) && index + 1 < input.Length && input[index + 1].IsOneOf(':', '/') && (index + 2 == input.Length || input[index + 2].IsOneOf('/', '\\', '#', '?')))
				{
					this._host = string.Empty;
					this._path = string.Empty;
					this._port = string.Empty;
				}
				return this.ParsePath(input, index, false);
			}
			return true;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004E38 File Offset: 0x00003038
		private bool IgnoreSlashesState(string input, int index)
		{
			while (index < input.Length)
			{
				if (!input[index].IsOneOf('\\', '/'))
				{
					return this.ParseAuthority(input, index);
				}
				index++;
			}
			return false;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004E68 File Offset: 0x00003068
		private bool ParseAuthority(string input, int index)
		{
			int num = index;
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			string text = null;
			string text2 = null;
			while (index < input.Length)
			{
				char c = input[index];
				if (c == '@')
				{
					if (text == null)
					{
						text = stringBuilder.ToString();
					}
					else
					{
						text2 = stringBuilder.ToString();
					}
					this._username = text;
					this._password = text2;
					stringBuilder.Append("%40");
					num = index + 1;
				}
				else if (c == ':' && text == null)
				{
					text = stringBuilder.ToString();
					text2 = string.Empty;
					stringBuilder.Clear();
				}
				else if (c == '%' && index + 2 < input.Length && input[index + 1].IsHex() && input[index + 2].IsHex())
				{
					stringBuilder.Append(input[index++]).Append(input[index++]).Append(input[index]);
				}
				else if (!c.IsOneOf('\t', '\n', '\r'))
				{
					if (c.IsOneOf('/', '\\', '#', '?'))
					{
						break;
					}
					if (c != ':' && (c == '#' || c == '?' || c.IsNormalPathCharacter()))
					{
						stringBuilder.Append(c);
					}
					else
					{
						index += Url.Utf8PercentEncode(stringBuilder, input, index);
					}
				}
				index++;
			}
			stringBuilder.ToPool();
			return this.ParseHostName(input, num, false, false);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004FC4 File Offset: 0x000031C4
		private bool ParseFileHost(string input, int index)
		{
			int num = index;
			this._path = string.Empty;
			while (index < input.Length)
			{
				char c = input[index];
				if (c == '/' || c == '\\' || c == '#' || c == '?')
				{
					break;
				}
				index++;
			}
			int num2 = index - num;
			if (num2 == 2 && input[index - 2].IsLetter() && (input[index - 1] == '|' || input[index - 1] == ':'))
			{
				return this.ParsePath(input, index - 2, false);
			}
			if (num2 != 0)
			{
				this._host = Url.SanatizeHost(input, num, num2);
			}
			return this.ParsePath(input, index, false);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005064 File Offset: 0x00003264
		private bool ParseHostName(string input, int index, bool onlyHost = false, bool onlyPort = false)
		{
			bool flag = false;
			int num = index;
			while (index < input.Length)
			{
				char c = input[index];
				if (c <= '/')
				{
					if (c == '#' || c == '/')
					{
						goto IL_0073;
					}
				}
				else if (c != ':')
				{
					if (c == '?')
					{
						goto IL_0073;
					}
					switch (c)
					{
					case '[':
						flag = true;
						break;
					case '\\':
						goto IL_0073;
					case ']':
						flag = false;
						break;
					}
				}
				else if (!flag)
				{
					this._host = Url.SanatizeHost(input, num, index - num);
					return onlyHost || this.ParsePort(input, index + 1, onlyPort);
				}
				index++;
				continue;
				IL_0073:
				this._host = Url.SanatizeHost(input, num, index - num);
				bool flag2 = string.IsNullOrEmpty(this._host);
				if (!onlyHost)
				{
					this._port = string.Empty;
					return this.ParsePath(input, index, false) && !flag2;
				}
				return !flag2;
			}
			this._host = Url.SanatizeHost(input, num, index - num);
			if (!onlyHost)
			{
				this._path = string.Empty;
				this._query = null;
				this._fragment = null;
			}
			return true;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005164 File Offset: 0x00003364
		private bool ParsePort(string input, int index, bool onlyPort = false)
		{
			int num = index;
			while (index < input.Length)
			{
				char c = input[index];
				if (c == '?' || c == '/' || c == '\\' || c == '#')
				{
					break;
				}
				if (!c.IsDigit() && c != '\t' && c != '\n' && c != '\r')
				{
					return false;
				}
				index++;
			}
			this._port = Url.SanatizePort(input, num, index - num);
			if (PortNumbers.GetDefaultPort(this._scheme) == this._port)
			{
				this._port = string.Empty;
			}
			if (!onlyPort)
			{
				this._path = string.Empty;
				return this.ParsePath(input, index, false);
			}
			return true;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005208 File Offset: 0x00003408
		private bool ParsePath(string input, int index, bool onlyPath = false)
		{
			int num = index;
			if (index < input.Length && (input[index] == '/' || input[index] == '\\'))
			{
				index++;
			}
			List<string> list = new List<string>();
			if (!onlyPath && !string.IsNullOrEmpty(this._path) && index - num == 0)
			{
				string[] array = this._path.Split(new char[] { '/' });
				if (array.Length > 1)
				{
					list.AddRange(array);
					list.RemoveAt(array.Length - 1);
				}
			}
			int count = list.Count;
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			while (index <= input.Length)
			{
				char c = ((index == input.Length) ? char.MaxValue : input[index]);
				bool flag = !onlyPath && (c == '#' || c == '?');
				if (c == '\uffff' || c == '/' || c == '\\' || flag)
				{
					string text = stringBuilder.ToString();
					bool flag2 = false;
					stringBuilder.Clear();
					if (text.Isi(Url.currentDirectoryAlternative))
					{
						text = Url.currentDirectory;
					}
					else if (text.Isi(Url.upperDirectoryAlternatives[0]) || text.Isi(Url.upperDirectoryAlternatives[1]) || text.Isi(Url.upperDirectoryAlternatives[2]))
					{
						text = Url.upperDirectory;
					}
					if (text.Is(Url.upperDirectory))
					{
						if (list.Count > 0)
						{
							list.RemoveAt(list.Count - 1);
						}
						flag2 = true;
					}
					else if (!text.Is(Url.currentDirectory))
					{
						if (this._scheme.Is(ProtocolNames.File) && list.Count == count && text.Length == 2 && text[0].IsLetter() && text[1] == '|')
						{
							text = text.Replace('|', ':');
							list.Clear();
						}
						list.Add(text);
					}
					else
					{
						flag2 = true;
					}
					if (flag2 && c != '/' && c != '\\')
					{
						list.Add(string.Empty);
					}
					if (flag)
					{
						break;
					}
				}
				else if (c == '%' && index + 2 < input.Length && input[index + 1].IsHex() && input[index + 2].IsHex())
				{
					stringBuilder.Append(input[index++]);
					stringBuilder.Append(input[index++]);
					stringBuilder.Append(input[index]);
				}
				else if (c != '\t' && c != '\n' && c != '\r')
				{
					if (c.IsNormalPathCharacter())
					{
						stringBuilder.Append(c);
					}
					else
					{
						index += Url.Utf8PercentEncode(stringBuilder, input, index);
					}
				}
				index++;
			}
			stringBuilder.ToPool();
			this._path = string.Join("/", list);
			if (index >= input.Length)
			{
				return true;
			}
			if (input[index] == '?')
			{
				return this.ParseQuery(input, index + 1, false);
			}
			return this.ParseFragment(input, index + 1);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000054F8 File Offset: 0x000036F8
		private bool ParseQuery(string input, int index, bool onlyQuery = false)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			bool flag = false;
			while (index < input.Length)
			{
				char c = input[index];
				flag = !onlyQuery && input[index] == '#';
				if (flag)
				{
					break;
				}
				if (c.IsNormalQueryCharacter())
				{
					stringBuilder.Append(c);
				}
				else
				{
					index += Url.Utf8PercentEncode(stringBuilder, input, index);
				}
				index++;
			}
			this._query = stringBuilder.ToPool();
			return !flag || this.ParseFragment(input, index + 1);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005574 File Offset: 0x00003774
		private bool ParseFragment(string input, int index)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			while (index < input.Length)
			{
				char c = input[index];
				if (c != '\0')
				{
					switch (c)
					{
					case '\t':
					case '\n':
					case '\r':
						goto IL_0040;
					case '\v':
					case '\f':
						break;
					default:
						if (c == '\uffff')
						{
							goto IL_0040;
						}
						break;
					}
					stringBuilder.Append(c);
				}
				IL_0040:
				index++;
			}
			this._fragment = stringBuilder.ToPool();
			return true;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000055DC File Offset: 0x000037DC
		private static int Utf8PercentEncode(StringBuilder buffer, string source, int index)
		{
			int num = (char.IsSurrogatePair(source, index) ? 2 : 1);
			byte[] bytes = TextEncoding.Utf8.GetBytes(source.Substring(index, num));
			for (int i = 0; i < bytes.Length; i++)
			{
				buffer.Append('%').Append(bytes[i].ToString("X2"));
			}
			return num - 1;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000563C File Offset: 0x0000383C
		private static string SanatizeHost(string hostName, int start, int length)
		{
			if (length > 1 && hostName[start] == '[' && hostName[start + length - 1] == ']')
			{
				return hostName.Substring(start, length);
			}
			byte[] array = new byte[4 * length];
			int num = 0;
			int num2 = start + length;
			int i = start;
			while (i < num2)
			{
				char c = hostName[i];
				if (c <= '%')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
							case '\n':
							case '\r':
								break;
							case '\v':
							case '\f':
								goto IL_0163;
							default:
								goto IL_0163;
							}
						}
					}
					else if (c != ' ' && c != '#')
					{
						if (c != '%')
						{
							goto IL_0163;
						}
						if (i + 2 < num2 && hostName[i + 1].IsHex() && hostName[i + 2].IsHex())
						{
							int num3 = hostName[i + 1].FromHex() * 16 + hostName[i + 2].FromHex();
							array[num++] = (byte)num3;
							i += 2;
						}
						else
						{
							array[num++] = 37;
						}
					}
				}
				else if (c <= ':')
				{
					if (c != '.')
					{
						if (c != '/' && c != ':')
						{
							goto IL_0163;
						}
					}
					else
					{
						array[num++] = (byte)hostName[i];
					}
				}
				else if (c != '?' && c != '@')
				{
					switch (c)
					{
					case '[':
					case '\\':
					case ']':
						break;
					default:
						goto IL_0163;
					}
				}
				IL_021C:
				i++;
				continue;
				IL_0163:
				char c2 = '\0';
				if (Symbols.Punycode.TryGetValue(hostName[i], out c2))
				{
					array[num++] = (byte)c2;
					goto IL_021C;
				}
				if (hostName[i].IsAlphanumericAscii())
				{
					array[num++] = (byte)char.ToLowerInvariant(hostName[i]);
					goto IL_021C;
				}
				int num4 = ((i + 1 < num2 && char.IsSurrogatePair(hostName, i)) ? 2 : 1);
				if (num4 != 1 || hostName[i] == '-' || char.IsLetterOrDigit(hostName[i]))
				{
					byte[] bytes = TextEncoding.Utf8.GetBytes(hostName.Substring(i, num4));
					for (int j = 0; j < bytes.Length; j++)
					{
						array[num++] = bytes[j];
					}
					i += num4 - 1;
					goto IL_021C;
				}
				goto IL_021C;
			}
			return TextEncoding.Utf8.GetString(array, 0, num);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005880 File Offset: 0x00003A80
		private static string SanatizePort(string port, int start, int length)
		{
			char[] array = new char[length];
			int num = 0;
			int num2 = start + length;
			for (int i = start; i < num2; i++)
			{
				switch (port[i])
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				default:
					if (num == 1 && array[0] == '0')
					{
						array[0] = port[i];
					}
					else
					{
						array[num++] = port[i];
					}
					break;
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x0400003F RID: 63
		private static readonly string currentDirectory = ".";

		// Token: 0x04000040 RID: 64
		private static readonly string currentDirectoryAlternative = "%2e";

		// Token: 0x04000041 RID: 65
		private static readonly string upperDirectory = "..";

		// Token: 0x04000042 RID: 66
		private static readonly string[] upperDirectoryAlternatives = new string[] { "%2e%2e", ".%2e", "%2e." };

		// Token: 0x04000043 RID: 67
		private static readonly Url DefaultBase = new Url(string.Empty, string.Empty, string.Empty);

		// Token: 0x04000044 RID: 68
		private string _fragment;

		// Token: 0x04000045 RID: 69
		private string _query;

		// Token: 0x04000046 RID: 70
		private string _path;

		// Token: 0x04000047 RID: 71
		private string _scheme;

		// Token: 0x04000048 RID: 72
		private string _port;

		// Token: 0x04000049 RID: 73
		private string _host;

		// Token: 0x0400004A RID: 74
		private string _username;

		// Token: 0x0400004B RID: 75
		private string _password;

		// Token: 0x0400004C RID: 76
		private bool _relative;

		// Token: 0x0400004D RID: 77
		private string _schemeData;

		// Token: 0x0400004E RID: 78
		private bool _error;
	}
}
