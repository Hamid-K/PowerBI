using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Library.Uris.Internal
{
	// Token: 0x020002CC RID: 716
	internal abstract class InternalUriParser
	{
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x00043EA4 File Offset: 0x000420A4
		internal string SchemeName
		{
			get
			{
				return this.m_Scheme;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00043EAC File Offset: 0x000420AC
		internal int DefaultPort
		{
			get
			{
				return this.m_Port;
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00043EB4 File Offset: 0x000420B4
		protected InternalUriParser()
			: this(UriSyntaxFlags.MayHavePath, false)
		{
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		protected virtual InternalUriParser OnNewUri()
		{
			return this;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void OnRegister(string schemeName, int defaultPort)
		{
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00043EBF File Offset: 0x000420BF
		protected virtual void InitializeAndValidate(InternalUri uri, out UriFormatException parsingError)
		{
			parsingError = uri.ParseMinimal();
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00043ECC File Offset: 0x000420CC
		protected virtual string Resolve(InternalUri baseUri, InternalUri relativeUri, out UriFormatException parsingError)
		{
			if (baseUri.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_UserDrivenParsing", base.GetType().FullName));
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
			}
			string text = null;
			bool flag = false;
			InternalUri internalUri = InternalUri.ResolveHelper(baseUri, relativeUri, ref text, ref flag, out parsingError);
			if (parsingError != null)
			{
				return null;
			}
			if (internalUri != null)
			{
				return internalUri.OriginalString;
			}
			return text;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00043F3C File Offset: 0x0004213C
		protected virtual bool IsBaseOf(InternalUri baseUri, InternalUri relativeUri)
		{
			return baseUri.IsBaseOfHelper(relativeUri);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00043F48 File Offset: 0x00042148
		protected virtual string GetComponents(InternalUri uri, UriComponents components, UriFormat format)
		{
			if ((components & UriComponents.SerializationInfoString) != (UriComponents)0 && components != UriComponents.SerializationInfoString)
			{
				throw new ArgumentOutOfRangeException("components", components, SR.GetString("net_uri_NotJustSerialization"));
			}
			if ((format & (UriFormat)(-4)) != (UriFormat)0)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			if (uri.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_UserDrivenParsing", base.GetType().FullName));
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
			}
			return uri.GetComponentsHelper(components, format);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00043FD5 File Offset: 0x000421D5
		protected virtual bool IsWellFormedOriginalString(InternalUri uri)
		{
			return uri.InternalIsWellFormedOriginalString();
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00043FE0 File Offset: 0x000421E0
		public static void Register(InternalUriParser uriParser, string schemeName, int defaultPort)
		{
			if (uriParser == null)
			{
				throw new ArgumentNullException("uriParser");
			}
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (schemeName.Length == 1)
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			if (!Uri.CheckSchemeName(schemeName))
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			if ((defaultPort >= 65535 || defaultPort < 0) && defaultPort != -1)
			{
				throw new ArgumentOutOfRangeException("defaultPort");
			}
			schemeName = schemeName.ToLower(CultureInfo.InvariantCulture);
			InternalUriParser.FetchSyntax(uriParser, schemeName, defaultPort);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x00044060 File Offset: 0x00042260
		public static bool IsKnownScheme(string schemeName)
		{
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (!InternalUri.CheckSchemeName(schemeName))
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			InternalUriParser syntax = InternalUriParser.GetSyntax(schemeName.ToLower(CultureInfo.InvariantCulture));
			return syntax != null && syntax.NotAny(UriSyntaxFlags.V1_UnknownUri);
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x000440AF File Offset: 0x000422AF
		internal bool ShouldUseLegacyV2Quirks
		{
			get
			{
				return this.m_ShouldUseLegacyV2Quirks;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00002139 File Offset: 0x00000339
		internal static bool DontEnableStrictRFC3986ReservedCharacterSets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00002139 File Offset: 0x00000339
		internal static bool DontKeepUnicodeBidiFormattingCharacters
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000440B8 File Offset: 0x000422B8
		static InternalUriParser()
		{
			InternalUriParser.m_Table[InternalUriParser.HttpUri.SchemeName] = InternalUriParser.HttpUri;
			InternalUriParser.HttpsUri = new InternalUriParser.BuiltInUriParser("https", 443, InternalUriParser.HttpUri.m_Flags, false);
			InternalUriParser.m_Table[InternalUriParser.HttpsUri.SchemeName] = InternalUriParser.HttpsUri;
			InternalUriParser.WsUri = new InternalUriParser.BuiltInUriParser("ws", 80, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.WsUri.SchemeName] = InternalUriParser.WsUri;
			InternalUriParser.WssUri = new InternalUriParser.BuiltInUriParser("wss", 443, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.WssUri.SchemeName] = InternalUriParser.WssUri;
			InternalUriParser.FtpUri = new InternalUriParser.BuiltInUriParser("ftp", 21, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.FtpUri.SchemeName] = InternalUriParser.FtpUri;
			InternalUriParser.FileUri = new InternalUriParser.BuiltInUriParser("file", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn, true);
			InternalUriParser.m_Table[InternalUriParser.FileUri.SchemeName] = InternalUriParser.FileUri;
			InternalUriParser.GopherUri = new InternalUriParser.BuiltInUriParser("gopher", 70, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.GopherUri.SchemeName] = InternalUriParser.GopherUri;
			InternalUriParser.NntpUri = new InternalUriParser.BuiltInUriParser("nntp", 119, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.NntpUri.SchemeName] = InternalUriParser.NntpUri;
			InternalUriParser.NewsUri = new InternalUriParser.BuiltInUriParser("news", -1, UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.NewsUri.SchemeName] = InternalUriParser.NewsUri;
			InternalUriParser.MailToUri = new InternalUriParser.BuiltInUriParser("mailto", 25, UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.MailToLikeUri | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.MailToUri.SchemeName] = InternalUriParser.MailToUri;
			InternalUriParser.UuidUri = new InternalUriParser.BuiltInUriParser("uuid", -1, InternalUriParser.NewsUri.m_Flags, false);
			InternalUriParser.m_Table[InternalUriParser.UuidUri.SchemeName] = InternalUriParser.UuidUri;
			InternalUriParser.TelnetUri = new InternalUriParser.BuiltInUriParser("telnet", 23, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.TelnetUri.SchemeName] = InternalUriParser.TelnetUri;
			InternalUriParser.LdapUri = new InternalUriParser.BuiltInUriParser("ldap", 389, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.LdapUri.SchemeName] = InternalUriParser.LdapUri;
			InternalUriParser.NetTcpUri = new InternalUriParser.BuiltInUriParser("net.tcp", 808, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.NetTcpUri.SchemeName] = InternalUriParser.NetTcpUri;
			InternalUriParser.NetPipeUri = new InternalUriParser.BuiltInUriParser("net.pipe", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.NetPipeUri.SchemeName] = InternalUriParser.NetPipeUri;
			InternalUriParser.VsMacrosUri = new InternalUriParser.BuiltInUriParser("vsmacros", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
			InternalUriParser.m_Table[InternalUriParser.VsMacrosUri.SchemeName] = InternalUriParser.VsMacrosUri;
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x000443EE File Offset: 0x000425EE
		internal UriSyntaxFlags Flags
		{
			get
			{
				return this.m_Flags;
			}
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x000443F6 File Offset: 0x000425F6
		internal bool NotAny(UriSyntaxFlags flags)
		{
			return this.IsFullMatch(flags, UriSyntaxFlags.None);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00044400 File Offset: 0x00042600
		internal bool InFact(UriSyntaxFlags flags)
		{
			return !this.IsFullMatch(flags, UriSyntaxFlags.None);
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0004440D File Offset: 0x0004260D
		internal bool IsAllSet(UriSyntaxFlags flags)
		{
			return this.IsFullMatch(flags, flags);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00044418 File Offset: 0x00042618
		private bool IsFullMatch(UriSyntaxFlags flags, UriSyntaxFlags expected)
		{
			UriSyntaxFlags uriSyntaxFlags;
			if ((flags & UriSyntaxFlags.UnEscapeDotsAndSlashes) == UriSyntaxFlags.None || !this.m_UpdatableFlagsUsed)
			{
				uriSyntaxFlags = this.m_Flags;
			}
			else
			{
				uriSyntaxFlags = (this.m_Flags & ~UriSyntaxFlags.UnEscapeDotsAndSlashes) | this.m_UpdatableFlags;
			}
			return (uriSyntaxFlags & flags) == expected;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0004445D File Offset: 0x0004265D
		internal InternalUriParser(UriSyntaxFlags flags, bool shouldUseLegacyV2Quirks = false)
		{
			this.m_Flags = flags;
			this.m_Scheme = string.Empty;
			this.m_ShouldUseLegacyV2Quirks = shouldUseLegacyV2Quirks;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00044480 File Offset: 0x00042680
		private static void FetchSyntax(InternalUriParser syntax, string lwrCaseSchemeName, int defaultPort)
		{
			if (syntax.SchemeName.Length != 0)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_NeedFreshParser", syntax.SchemeName));
			}
			Dictionary<string, InternalUriParser> table = InternalUriParser.m_Table;
			lock (table)
			{
				syntax.m_Flags &= ~UriSyntaxFlags.V1_UnknownUri;
				InternalUriParser internalUriParser = null;
				InternalUriParser.m_Table.TryGetValue(lwrCaseSchemeName, out internalUriParser);
				if (internalUriParser != null)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_AlreadyRegistered", internalUriParser.SchemeName));
				}
				InternalUriParser.m_TempTable.TryGetValue(syntax.SchemeName, out internalUriParser);
				if (internalUriParser != null)
				{
					lwrCaseSchemeName = internalUriParser.m_Scheme;
					InternalUriParser.m_TempTable.Remove(lwrCaseSchemeName);
				}
				syntax.OnRegister(lwrCaseSchemeName, defaultPort);
				syntax.m_Scheme = lwrCaseSchemeName;
				syntax.CheckSetIsSimpleFlag();
				syntax.m_Port = defaultPort;
				InternalUriParser.m_Table[syntax.SchemeName] = syntax;
			}
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00044570 File Offset: 0x00042770
		internal static InternalUriParser FindOrFetchAsUnknownV1Syntax(string lwrCaseScheme)
		{
			InternalUriParser internalUriParser = null;
			InternalUriParser.m_Table.TryGetValue(lwrCaseScheme, out internalUriParser);
			if (internalUriParser != null)
			{
				return internalUriParser;
			}
			InternalUriParser.m_TempTable.TryGetValue(lwrCaseScheme, out internalUriParser);
			if (internalUriParser != null)
			{
				return internalUriParser;
			}
			Dictionary<string, InternalUriParser> table = InternalUriParser.m_Table;
			InternalUriParser internalUriParser2;
			lock (table)
			{
				if (InternalUriParser.m_TempTable.Count >= 512)
				{
					InternalUriParser.m_TempTable = new Dictionary<string, InternalUriParser>(25);
				}
				internalUriParser = new InternalUriParser.BuiltInUriParser(lwrCaseScheme, -1, UriSyntaxFlags.OptionalAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.V1_UnknownUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);
				InternalUriParser.m_TempTable[lwrCaseScheme] = internalUriParser;
				internalUriParser2 = internalUriParser;
			}
			return internalUriParser2;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0004460C File Offset: 0x0004280C
		internal static InternalUriParser GetSyntax(string lwrCaseScheme)
		{
			InternalUriParser internalUriParser = null;
			InternalUriParser.m_Table.TryGetValue(lwrCaseScheme, out internalUriParser);
			if (internalUriParser == null)
			{
				InternalUriParser.m_TempTable.TryGetValue(lwrCaseScheme, out internalUriParser);
			}
			return internalUriParser;
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0004463B File Offset: 0x0004283B
		internal bool IsSimple
		{
			get
			{
				return this.InFact(UriSyntaxFlags.SimpleUserSyntax);
			}
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0000336E File Offset: 0x0000156E
		internal void CheckSetIsSimpleFlag()
		{
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00044648 File Offset: 0x00042848
		internal void SetUpdatableFlags(UriSyntaxFlags flags)
		{
			this.m_UpdatableFlags = flags;
			this.m_UpdatableFlagsUsed = true;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0004465C File Offset: 0x0004285C
		internal InternalUriParser InternalOnNewUri()
		{
			InternalUriParser internalUriParser = this.OnNewUri();
			if (this != internalUriParser)
			{
				internalUriParser.m_Scheme = this.m_Scheme;
				internalUriParser.m_Port = this.m_Port;
				internalUriParser.m_Flags = this.m_Flags;
			}
			return internalUriParser;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x00044699 File Offset: 0x00042899
		internal void InternalValidate(InternalUri thisUri, out UriFormatException parsingError)
		{
			this.InitializeAndValidate(thisUri, out parsingError);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000446A3 File Offset: 0x000428A3
		internal string InternalResolve(InternalUri thisBaseUri, InternalUri uriLink, out UriFormatException parsingError)
		{
			return this.Resolve(thisBaseUri, uriLink, out parsingError);
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x000446AE File Offset: 0x000428AE
		internal bool InternalIsBaseOf(InternalUri thisBaseUri, InternalUri uriLink)
		{
			return this.IsBaseOf(thisBaseUri, uriLink);
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000446B8 File Offset: 0x000428B8
		internal string InternalGetComponents(InternalUri thisUri, UriComponents uriComponents, UriFormat uriFormat)
		{
			return this.GetComponents(thisUri, uriComponents, uriFormat);
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x000446C3 File Offset: 0x000428C3
		internal bool InternalIsWellFormedOriginalString(InternalUri thisUri)
		{
			return this.IsWellFormedOriginalString(thisUri);
		}

		// Token: 0x0400094D RID: 2381
		private const UriSyntaxFlags SchemeOnlyFlags = UriSyntaxFlags.MayHavePath;

		// Token: 0x0400094E RID: 2382
		private static readonly Dictionary<string, InternalUriParser> m_Table = new Dictionary<string, InternalUriParser>(25);

		// Token: 0x0400094F RID: 2383
		private static Dictionary<string, InternalUriParser> m_TempTable = new Dictionary<string, InternalUriParser>(25);

		// Token: 0x04000950 RID: 2384
		private UriSyntaxFlags m_Flags;

		// Token: 0x04000951 RID: 2385
		private bool m_ShouldUseLegacyV2Quirks;

		// Token: 0x04000952 RID: 2386
		private volatile UriSyntaxFlags m_UpdatableFlags;

		// Token: 0x04000953 RID: 2387
		private volatile bool m_UpdatableFlagsUsed;

		// Token: 0x04000954 RID: 2388
		private const UriSyntaxFlags c_UpdatableFlags = UriSyntaxFlags.UnEscapeDotsAndSlashes;

		// Token: 0x04000955 RID: 2389
		private int m_Port;

		// Token: 0x04000956 RID: 2390
		private string m_Scheme;

		// Token: 0x04000957 RID: 2391
		internal const int NoDefaultPort = -1;

		// Token: 0x04000958 RID: 2392
		private const int c_InitialTableSize = 25;

		// Token: 0x04000959 RID: 2393
		internal static InternalUriParser HttpUri = new InternalUriParser.BuiltInUriParser("http", 80, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing, false);

		// Token: 0x0400095A RID: 2394
		internal static InternalUriParser HttpsUri;

		// Token: 0x0400095B RID: 2395
		internal static InternalUriParser WsUri;

		// Token: 0x0400095C RID: 2396
		internal static InternalUriParser WssUri;

		// Token: 0x0400095D RID: 2397
		internal static InternalUriParser FtpUri;

		// Token: 0x0400095E RID: 2398
		internal static InternalUriParser FileUri;

		// Token: 0x0400095F RID: 2399
		internal static InternalUriParser GopherUri;

		// Token: 0x04000960 RID: 2400
		internal static InternalUriParser NntpUri;

		// Token: 0x04000961 RID: 2401
		internal static InternalUriParser NewsUri;

		// Token: 0x04000962 RID: 2402
		internal static InternalUriParser MailToUri;

		// Token: 0x04000963 RID: 2403
		internal static InternalUriParser UuidUri;

		// Token: 0x04000964 RID: 2404
		internal static InternalUriParser TelnetUri;

		// Token: 0x04000965 RID: 2405
		internal static InternalUriParser LdapUri;

		// Token: 0x04000966 RID: 2406
		internal static InternalUriParser NetTcpUri;

		// Token: 0x04000967 RID: 2407
		internal static InternalUriParser NetPipeUri;

		// Token: 0x04000968 RID: 2408
		internal static InternalUriParser VsMacrosUri;

		// Token: 0x04000969 RID: 2409
		private const int c_MaxCapacity = 512;

		// Token: 0x0400096A RID: 2410
		private const UriSyntaxFlags UnknownV1SyntaxFlags = UriSyntaxFlags.OptionalAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.V1_UnknownUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400096B RID: 2411
		private const UriSyntaxFlags HttpSyntaxFlagsBase = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400096C RID: 2412
		private const UriSyntaxFlags HttpSyntaxFlagsV2 = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400096D RID: 2413
		private const UriSyntaxFlags HttpSyntaxFlagsV3 = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400096E RID: 2414
		private const UriSyntaxFlags FtpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400096F RID: 2415
		private const UriSyntaxFlags FileSyntaxFlagsBase = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn;

		// Token: 0x04000970 RID: 2416
		private const UriSyntaxFlags FileSyntaxFlagsV2 = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn;

		// Token: 0x04000971 RID: 2417
		private const UriSyntaxFlags FileSyntaxFlagsV3 = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000972 RID: 2418
		private const UriSyntaxFlags VsmacrosSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000973 RID: 2419
		private const UriSyntaxFlags GopherSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000974 RID: 2420
		private const UriSyntaxFlags NewsSyntaxFlags = UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000975 RID: 2421
		private const UriSyntaxFlags NntpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000976 RID: 2422
		private const UriSyntaxFlags TelnetSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000977 RID: 2423
		private const UriSyntaxFlags LdapSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000978 RID: 2424
		private const UriSyntaxFlags MailtoSyntaxFlags = UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.MailToLikeUri | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000979 RID: 2425
		private const UriSyntaxFlags NetPipeSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400097A RID: 2426
		private const UriSyntaxFlags NetTcpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x020002CD RID: 717
		private class BuiltInUriParser : InternalUriParser
		{
			// Token: 0x06001C77 RID: 7287 RVA: 0x000446CC File Offset: 0x000428CC
			internal BuiltInUriParser(string lwrCaseScheme, int defaultPort, UriSyntaxFlags syntaxFlags, bool shouldUseLegacyV2Quirks = false)
				: base(syntaxFlags | UriSyntaxFlags.SimpleUserSyntax | UriSyntaxFlags.BuiltInSyntax, shouldUseLegacyV2Quirks)
			{
				this.m_Scheme = lwrCaseScheme;
				this.m_Port = defaultPort;
			}
		}
	}
}
