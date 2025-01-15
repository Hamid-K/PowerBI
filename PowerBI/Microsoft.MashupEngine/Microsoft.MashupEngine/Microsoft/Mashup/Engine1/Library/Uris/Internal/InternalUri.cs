using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace Microsoft.Mashup.Engine1.Library.Uris.Internal
{
	// Token: 0x020002BB RID: 699
	[TypeConverter(typeof(UriTypeConverter))]
	[Serializable]
	internal class InternalUri : ISerializable
	{
		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00039DCA File Offset: 0x00037FCA
		private bool IsImplicitFile
		{
			get
			{
				return (this.m_Flags & InternalUri.Flags.ImplicitFile) > InternalUri.Flags.Zero;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00039DDD File Offset: 0x00037FDD
		private bool IsUncOrDosPath
		{
			get
			{
				return (this.m_Flags & (InternalUri.Flags.DosPath | InternalUri.Flags.UncPath)) > InternalUri.Flags.Zero;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x00039DF0 File Offset: 0x00037FF0
		private bool IsDosPath
		{
			get
			{
				return (this.m_Flags & InternalUri.Flags.DosPath) > InternalUri.Flags.Zero;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x00039E03 File Offset: 0x00038003
		private bool IsUncPath
		{
			get
			{
				return (this.m_Flags & InternalUri.Flags.UncPath) > InternalUri.Flags.Zero;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x00039E16 File Offset: 0x00038016
		private InternalUri.Flags HostType
		{
			get
			{
				return this.m_Flags & InternalUri.Flags.HostTypeMask;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x00039E25 File Offset: 0x00038025
		private InternalUriParser Syntax
		{
			get
			{
				return this.m_Syntax;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x00039E2D File Offset: 0x0003802D
		private bool IsNotAbsoluteUri
		{
			get
			{
				return this.m_Syntax == null;
			}
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00039E38 File Offset: 0x00038038
		internal static bool IriParsingStatic(InternalUriParser syntax)
		{
			return (syntax != null && syntax.InFact(UriSyntaxFlags.AllowIriParsing)) || syntax == null;
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00039E50 File Offset: 0x00038050
		private bool AllowIdn
		{
			get
			{
				return this.m_Syntax != null && (this.m_Syntax.Flags & UriSyntaxFlags.AllowIdn) != UriSyntaxFlags.None && (InternalUri.s_IdnScope == UriIdnScope.All || (InternalUri.s_IdnScope == UriIdnScope.AllExceptIntranet && this.NotAny(InternalUri.Flags.IntranetUri)));
			}
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x00039EA1 File Offset: 0x000380A1
		private bool AllowIdnStatic(InternalUriParser syntax, InternalUri.Flags flags)
		{
			return syntax != null && (syntax.Flags & UriSyntaxFlags.AllowIdn) != UriSyntaxFlags.None && (InternalUri.s_IdnScope == UriIdnScope.All || (InternalUri.s_IdnScope == UriIdnScope.AllExceptIntranet && InternalUri.StaticNotAny(flags, InternalUri.Flags.IntranetUri)));
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x00039EDD File Offset: 0x000380DD
		internal bool ShouldUseLegacyV2Quirks
		{
			get
			{
				return this.Syntax.ShouldUseLegacyV2Quirks;
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00039EEC File Offset: 0x000380EC
		private bool IsIntranet(string schemeHost)
		{
			bool flag = false;
			int num = -1;
			int num2 = -2147467259;
			if (this.m_Syntax.SchemeName.Length > 32)
			{
				return false;
			}
			if (InternalUri.s_ManagerRef == null)
			{
				object obj = InternalUri.s_IntranetLock;
				lock (obj)
				{
					if (InternalUri.s_ManagerRef == null)
					{
						InternalUri.s_ManagerRef = new InternetSecurityManager();
					}
				}
			}
			try
			{
				InternalUri.s_ManagerRef.MapUrlToZone(schemeHost.TrimStart(InternalUri._WSchars), out num, 0);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == num2)
				{
					flag = true;
				}
			}
			catch (InvalidComObjectException)
			{
				flag = true;
			}
			if (num == 1)
			{
				return true;
			}
			if (num == 2 || num == 4 || flag)
			{
				for (int i = 0; i < schemeHost.Length; i++)
				{
					if (schemeHost[i] == '.')
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x00039FE8 File Offset: 0x000381E8
		internal bool UserDrivenParsing
		{
			get
			{
				return (this.m_Flags & InternalUri.Flags.UserDrivenParsing) > InternalUri.Flags.Zero;
			}
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00039FFB File Offset: 0x000381FB
		private void SetUserDrivenParsing()
		{
			this.m_Flags = InternalUri.Flags.UserDrivenParsing | (this.m_Flags & InternalUri.Flags.UserEscaped);
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0003A018 File Offset: 0x00038218
		private ushort SecuredPathIndex
		{
			get
			{
				if (this.IsDosPath)
				{
					char c = this.m_String[(int)this.m_Info.Offset.Path];
					return (c == '/' || c == '\\') ? 3 : 2;
				}
				return 0;
			}
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0003A05A File Offset: 0x0003825A
		private bool NotAny(InternalUri.Flags flags)
		{
			return (this.m_Flags & flags) == InternalUri.Flags.Zero;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0003A068 File Offset: 0x00038268
		private bool InFact(InternalUri.Flags flags)
		{
			return (this.m_Flags & flags) > InternalUri.Flags.Zero;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0003A076 File Offset: 0x00038276
		private static bool StaticNotAny(InternalUri.Flags allFlags, InternalUri.Flags checkFlags)
		{
			return (allFlags & checkFlags) == InternalUri.Flags.Zero;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0003A07F File Offset: 0x0003827F
		private static bool StaticInFact(InternalUri.Flags allFlags, InternalUri.Flags checkFlags)
		{
			return (allFlags & checkFlags) > InternalUri.Flags.Zero;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0003A088 File Offset: 0x00038288
		private InternalUri.UriInfo EnsureUriInfo()
		{
			InternalUri.Flags flags = this.m_Flags;
			if ((this.m_Flags & InternalUri.Flags.MinimalUriInfoSet) == InternalUri.Flags.Zero)
			{
				this.CreateUriInfo(flags);
			}
			return this.m_Info;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0003A0B8 File Offset: 0x000382B8
		private void EnsureParseRemaining()
		{
			if ((this.m_Flags & (InternalUri.Flags)(-2147483648)) == InternalUri.Flags.Zero)
			{
				this.ParseRemaining();
			}
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0003A0CF File Offset: 0x000382CF
		private void EnsureHostString(bool allowDnsOptimization)
		{
			this.EnsureUriInfo();
			if (this.m_Info.Host == null)
			{
				if (allowDnsOptimization && this.InFact(InternalUri.Flags.CanonicalDnsHost))
				{
					return;
				}
				this.CreateHostString();
			}
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0003A0FD File Offset: 0x000382FD
		public InternalUri(string uriString)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			this.CreateThis(uriString, false, UriKind.Absolute);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0003A11C File Offset: 0x0003831C
		[Obsolete("The constructor has been deprecated. Please use new Uri(string). The dontEscape parameter is deprecated and is always false. https://go.microsoft.com/fwlink/?linkid=14202")]
		public InternalUri(string uriString, bool dontEscape)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			this.CreateThis(uriString, dontEscape, UriKind.Absolute);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0003A13B File Offset: 0x0003833B
		[Obsolete("The constructor has been deprecated. Please new Uri(Uri, string). The dontEscape parameter is deprecated and is always false. https://go.microsoft.com/fwlink/?linkid=14202")]
		public InternalUri(InternalUri baseUri, string relativeUri, bool dontEscape)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new ArgumentOutOfRangeException("baseUri");
			}
			this.CreateUri(baseUri, relativeUri, dontEscape);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0003A16D File Offset: 0x0003836D
		public InternalUri(string uriString, UriKind uriKind)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			this.CreateThis(uriString, false, uriKind);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0003A18C File Offset: 0x0003838C
		public InternalUri(InternalUri baseUri, string relativeUri)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new ArgumentOutOfRangeException("baseUri");
			}
			this.CreateUri(baseUri, relativeUri, false);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0003A1C0 File Offset: 0x000383C0
		private void CreateUri(InternalUri baseUri, string relativeUri, bool dontEscape)
		{
			this.CreateThis(relativeUri, dontEscape, UriKind.RelativeOrAbsolute);
			if (baseUri.Syntax.IsSimple)
			{
				UriFormatException ex;
				InternalUri internalUri = InternalUri.ResolveHelper(baseUri, this, ref relativeUri, ref dontEscape, out ex);
				if (ex != null)
				{
					throw ex;
				}
				if (internalUri != null)
				{
					if (internalUri != this)
					{
						this.CreateThisFromUri(internalUri);
					}
					return;
				}
			}
			else
			{
				dontEscape = false;
				UriFormatException ex;
				relativeUri = baseUri.Syntax.InternalResolve(baseUri, this, out ex);
				if (ex != null)
				{
					throw ex;
				}
			}
			this.m_Flags = InternalUri.Flags.Zero;
			this.m_Info = null;
			this.m_Syntax = null;
			this.CreateThis(relativeUri, dontEscape, UriKind.Absolute);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0003A244 File Offset: 0x00038444
		public InternalUri(InternalUri baseUri, InternalUri relativeUri)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new ArgumentOutOfRangeException("baseUri");
			}
			this.CreateThisFromUri(relativeUri);
			string text = null;
			bool flag;
			if (baseUri.Syntax.IsSimple)
			{
				flag = this.InFact(InternalUri.Flags.UserEscaped);
				UriFormatException ex;
				relativeUri = InternalUri.ResolveHelper(baseUri, this, ref text, ref flag, out ex);
				if (ex != null)
				{
					throw ex;
				}
				if (relativeUri != null)
				{
					if (relativeUri != this)
					{
						this.CreateThisFromUri(relativeUri);
					}
					return;
				}
			}
			else
			{
				flag = false;
				UriFormatException ex;
				text = baseUri.Syntax.InternalResolve(baseUri, this, out ex);
				if (ex != null)
				{
					throw ex;
				}
			}
			this.m_Flags = InternalUri.Flags.Zero;
			this.m_Info = null;
			this.m_Syntax = null;
			this.CreateThis(text, flag, UriKind.Absolute);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0003A2FC File Offset: 0x000384FC
		private unsafe static ParsingError GetCombinedString(InternalUri baseUri, string relativeStr, bool dontEscape, ref string result)
		{
			int num = 0;
			while (num < relativeStr.Length && relativeStr[num] != '/' && relativeStr[num] != '\\' && relativeStr[num] != '?' && relativeStr[num] != '#')
			{
				if (relativeStr[num] == ':')
				{
					if (num >= 2)
					{
						string text = relativeStr.Substring(0, num);
						fixed (string text2 = text)
						{
							char* ptr = text2;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							InternalUriParser internalUriParser = null;
							if (InternalUri.CheckSchemeSyntax(ptr, (ushort)text.Length, ref internalUriParser) == ParsingError.None)
							{
								if (baseUri.Syntax != internalUriParser)
								{
									result = relativeStr;
									return ParsingError.None;
								}
								if (num + 1 < relativeStr.Length)
								{
									relativeStr = relativeStr.Substring(num + 1);
								}
								else
								{
									relativeStr = string.Empty;
								}
							}
						}
						break;
					}
					break;
				}
				else
				{
					num++;
				}
			}
			if (relativeStr.Length == 0)
			{
				result = baseUri.OriginalString;
				return ParsingError.None;
			}
			result = InternalUri.CombineUri(baseUri, relativeStr, dontEscape ? UriFormat.UriEscaped : UriFormat.SafeUnescaped);
			return ParsingError.None;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0003A3EC File Offset: 0x000385EC
		private static UriFormatException GetException(ParsingError err)
		{
			switch (err)
			{
			case ParsingError.None:
				return null;
			case ParsingError.BadFormat:
				return new UriFormatException(SR.GetString("net_uri_BadFormat"));
			case ParsingError.BadScheme:
				return new UriFormatException(SR.GetString("net_uri_BadScheme"));
			case ParsingError.BadAuthority:
				return new UriFormatException(SR.GetString("net_uri_BadAuthority"));
			case ParsingError.EmptyUriString:
				return new UriFormatException(SR.GetString("net_uri_EmptyUri"));
			case ParsingError.SchemeLimit:
				return new UriFormatException(SR.GetString("net_uri_SchemeLimit"));
			case ParsingError.SizeLimit:
				return new UriFormatException(SR.GetString("net_uri_SizeLimit"));
			case ParsingError.MustRootedPath:
				return new UriFormatException(SR.GetString("net_uri_MustRootedPath"));
			case ParsingError.BadHostName:
				return new UriFormatException(SR.GetString("net_uri_BadHostName"));
			case ParsingError.NonEmptyHost:
				return new UriFormatException(SR.GetString("net_uri_BadFormat"));
			case ParsingError.BadPort:
				return new UriFormatException(SR.GetString("net_uri_BadPort"));
			case ParsingError.BadAuthorityTerminator:
				return new UriFormatException(SR.GetString("net_uri_BadAuthorityTerminator"));
			case ParsingError.CannotCreateRelative:
				return new UriFormatException(SR.GetString("net_uri_CannotCreateRelative"));
			default:
				return new UriFormatException(SR.GetString("net_uri_BadFormat"));
			}
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0003A50C File Offset: 0x0003870C
		protected InternalUri(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			string text = serializationInfo.GetString("AbsoluteUri");
			if (text.Length != 0)
			{
				this.CreateThis(text, false, UriKind.Absolute);
				return;
			}
			text = serializationInfo.GetString("RelativeUri");
			if (text == null)
			{
				throw new ArgumentNullException("uriString");
			}
			this.CreateThis(text, false, UriKind.Relative);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0003A560 File Offset: 0x00038760
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0003A56C File Offset: 0x0003876C
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (this.IsAbsoluteUri)
			{
				serializationInfo.AddValue("AbsoluteUri", this.GetParts(UriComponents.SerializationInfoString, UriFormat.UriEscaped));
				return;
			}
			serializationInfo.AddValue("AbsoluteUri", string.Empty);
			serializationInfo.AddValue("RelativeUri", this.GetParts(UriComponents.SerializationInfoString, UriFormat.UriEscaped));
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0003A5C0 File Offset: 0x000387C0
		public string AbsolutePath
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				string text = this.PrivateAbsolutePath;
				if (this.IsDosPath && text[0] == '/')
				{
					text = text.Substring(1);
				}
				return text;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0003A608 File Offset: 0x00038808
		private string PrivateAbsolutePath
		{
			get
			{
				InternalUri.UriInfo uriInfo = this.EnsureUriInfo();
				if (uriInfo.MoreInfo == null)
				{
					uriInfo.MoreInfo = new InternalUri.MoreInfo();
				}
				string text = uriInfo.MoreInfo.Path;
				if (text == null)
				{
					text = this.GetParts(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.UriEscaped);
					uriInfo.MoreInfo.Path = text;
				}
				return text;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x0003A658 File Offset: 0x00038858
		public string AbsoluteUri
		{
			get
			{
				if (this.m_Syntax == null)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				InternalUri.UriInfo uriInfo = this.EnsureUriInfo();
				if (uriInfo.MoreInfo == null)
				{
					uriInfo.MoreInfo = new InternalUri.MoreInfo();
				}
				string text = uriInfo.MoreInfo.AbsoluteUri;
				if (text == null)
				{
					text = this.GetParts(UriComponents.AbsoluteUri, UriFormat.UriEscaped);
					uriInfo.MoreInfo.AbsoluteUri = text;
				}
				return text;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x0003A6BD File Offset: 0x000388BD
		public string LocalPath
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				return this.GetLocalPath();
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x0003A6DD File Offset: 0x000388DD
		public string Authority
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				return this.GetParts(UriComponents.Host | UriComponents.Port, UriFormat.UriEscaped);
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0003A700 File Offset: 0x00038900
		public UriHostNameType HostNameType
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				if (this.m_Syntax.IsSimple)
				{
					this.EnsureUriInfo();
				}
				else
				{
					this.EnsureHostString(false);
				}
				InternalUri.Flags hostType = this.HostType;
				if (hostType <= InternalUri.Flags.DnsHostType)
				{
					if (hostType == InternalUri.Flags.IPv6HostType)
					{
						return UriHostNameType.IPv6;
					}
					if (hostType == InternalUri.Flags.IPv4HostType)
					{
						return UriHostNameType.IPv4;
					}
					if (hostType == InternalUri.Flags.DnsHostType)
					{
						return UriHostNameType.Dns;
					}
				}
				else
				{
					if (hostType == InternalUri.Flags.UncHostType)
					{
						return UriHostNameType.Basic;
					}
					if (hostType == InternalUri.Flags.BasicHostType)
					{
						return UriHostNameType.Basic;
					}
					if (hostType == InternalUri.Flags.HostTypeMask)
					{
						return UriHostNameType.Unknown;
					}
				}
				return UriHostNameType.Unknown;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x0003A79C File Offset: 0x0003899C
		public bool IsDefaultPort
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				if (this.m_Syntax.IsSimple)
				{
					this.EnsureUriInfo();
				}
				else
				{
					this.EnsureHostString(false);
				}
				return this.NotAny(InternalUri.Flags.NotDefaultPort);
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x0003A7EA File Offset: 0x000389EA
		public bool IsFile
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				return this.m_Syntax.SchemeName == InternalUri.UriSchemeFile;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0003A816 File Offset: 0x00038A16
		public bool IsLoopback
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				this.EnsureHostString(false);
				return this.InFact(InternalUri.Flags.LoopbackHost);
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x0003A844 File Offset: 0x00038A44
		public string PathAndQuery
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				string text = this.GetParts(UriComponents.PathAndQuery, UriFormat.UriEscaped);
				if (this.IsDosPath && text[0] == '/')
				{
					text = text.Substring(1);
				}
				return text;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x0003A890 File Offset: 0x00038A90
		public string[] Segments
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				string[] array = null;
				if (array == null)
				{
					string privateAbsolutePath = this.PrivateAbsolutePath;
					if (privateAbsolutePath.Length == 0)
					{
						array = new string[0];
					}
					else
					{
						ArrayList arrayList = new ArrayList();
						int num;
						for (int i = 0; i < privateAbsolutePath.Length; i = num + 1)
						{
							num = privateAbsolutePath.IndexOf('/', i);
							if (num == -1)
							{
								num = privateAbsolutePath.Length - 1;
							}
							arrayList.Add(privateAbsolutePath.Substring(i, num - i + 1));
						}
						array = (string[])arrayList.ToArray(typeof(string));
					}
				}
				return array;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0003A92F File Offset: 0x00038B2F
		public bool IsUnc
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				return this.IsUncPath;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0003A94F File Offset: 0x00038B4F
		public string Host
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				return this.GetParts(UriComponents.Host, UriFormat.UriEscaped);
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0003A971 File Offset: 0x00038B71
		private static bool StaticIsFile(InternalUriParser syntax)
		{
			return syntax.InFact(UriSyntaxFlags.FileLikeUri);
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0003A980 File Offset: 0x00038B80
		private static object InitializeLock
		{
			get
			{
				if (InternalUri.s_initLock == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref InternalUri.s_initLock, obj, null);
				}
				return InternalUri.s_initLock;
			}
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0003A9AC File Offset: 0x00038BAC
		private static void InitializeUriConfig()
		{
			if (!InternalUri.s_ConfigInitialized)
			{
				object initializeLock = InternalUri.InitializeLock;
				lock (initializeLock)
				{
					if (!InternalUri.s_ConfigInitialized && !InternalUri.s_ConfigInitializing)
					{
						InternalUri.s_ConfigInitializing = true;
						UriSectionInternal section = UriSectionInternal.GetSection();
						if (section != null)
						{
							InternalUri.s_IdnScope = section.IdnScope;
							InternalUri.SetEscapedDotSlashSettings(section, "http");
							InternalUri.SetEscapedDotSlashSettings(section, "https");
							InternalUri.SetEscapedDotSlashSettings(section, InternalUri.UriSchemeWs);
							InternalUri.SetEscapedDotSlashSettings(section, InternalUri.UriSchemeWss);
						}
						InternalUri.s_ConfigInitialized = true;
						InternalUri.s_ConfigInitializing = false;
					}
				}
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0003AA60 File Offset: 0x00038C60
		private static void SetEscapedDotSlashSettings(UriSectionInternal uriSection, string scheme)
		{
			SchemeSettingInternal schemeSetting = uriSection.GetSchemeSetting(scheme);
			if (schemeSetting != null && schemeSetting.Options == GenericUriParserOptions.DontUnescapePathDotsAndSlashes)
			{
				InternalUriParser.GetSyntax(scheme).SetUpdatableFlags(UriSyntaxFlags.None);
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0003AA94 File Offset: 0x00038C94
		private string GetLocalPath()
		{
			this.EnsureParseRemaining();
			if (!this.IsUncOrDosPath)
			{
				return this.GetUnescapedParts(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.Unescaped);
			}
			this.EnsureHostString(false);
			int num;
			if (this.NotAny(InternalUri.Flags.HostNotCanonical | InternalUri.Flags.PathNotCanonical | InternalUri.Flags.ShouldBeCompressed))
			{
				num = (int)(this.IsUncPath ? (this.m_Info.Offset.Host - 2) : this.m_Info.Offset.Path);
				string text = ((this.IsImplicitFile && this.m_Info.Offset.Host == (this.IsDosPath ? 0 : 2) && this.m_Info.Offset.Query == this.m_Info.Offset.End) ? this.m_String : ((this.IsDosPath && (this.m_String[num] == '/' || this.m_String[num] == '\\')) ? this.m_String.Substring(num + 1, (int)this.m_Info.Offset.Query - num - 1) : this.m_String.Substring(num, (int)this.m_Info.Offset.Query - num)));
				if (this.IsDosPath && text[1] == '|')
				{
					text = text.Remove(1, 1);
					text = text.Insert(1, ":");
				}
				for (int i = 0; i < text.Length; i++)
				{
					if (text[i] == '/')
					{
						text = text.Replace('/', '\\');
						break;
					}
				}
				return text;
			}
			int num2 = 0;
			num = (int)this.m_Info.Offset.Path;
			string host = this.m_Info.Host;
			char[] array = new char[host.Length + 3 + (int)this.m_Info.Offset.Fragment - (int)this.m_Info.Offset.Path];
			if (this.IsUncPath)
			{
				array[0] = '\\';
				array[1] = '\\';
				num2 = 2;
				InternalUriHelper.UnescapeString(host, 0, host.Length, array, ref num2, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, false);
			}
			else if (this.m_String[num] == '/' || this.m_String[num] == '\\')
			{
				num++;
			}
			ushort num3 = (ushort)num2;
			UnescapeMode unescapeMode = ((this.InFact(InternalUri.Flags.PathNotCanonical) && !this.IsImplicitFile) ? (UnescapeMode.Unescape | UnescapeMode.UnescapeAll) : UnescapeMode.CopyOnly);
			InternalUriHelper.UnescapeString(this.m_String, num, (int)this.m_Info.Offset.Query, array, ref num2, char.MaxValue, char.MaxValue, char.MaxValue, unescapeMode, this.m_Syntax, true);
			if (array[1] == '|')
			{
				array[1] = ':';
			}
			if (this.InFact(InternalUri.Flags.ShouldBeCompressed))
			{
				array = InternalUri.Compress(array, this.IsDosPath ? (num3 + 2) : num3, ref num2, this.m_Syntax);
			}
			for (ushort num4 = 0; num4 < (ushort)num2; num4 += 1)
			{
				if (array[(int)num4] == '/')
				{
					array[(int)num4] = '\\';
				}
			}
			return new string(array, 0, num2);
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x0003AD8C File Offset: 0x00038F8C
		public int Port
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				if (this.m_Syntax.IsSimple)
				{
					this.EnsureUriInfo();
				}
				else
				{
					this.EnsureHostString(false);
				}
				if (this.InFact(InternalUri.Flags.NotDefaultPort))
				{
					return (int)this.m_Info.Offset.PortValue;
				}
				return this.m_Syntax.DefaultPort;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0003ADF8 File Offset: 0x00038FF8
		public string Query
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				InternalUri.UriInfo uriInfo = this.EnsureUriInfo();
				if (uriInfo.MoreInfo == null)
				{
					uriInfo.MoreInfo = new InternalUri.MoreInfo();
				}
				string text = uriInfo.MoreInfo.Query;
				if (text == null)
				{
					text = this.GetParts(UriComponents.Query | UriComponents.KeepDelimiter, UriFormat.UriEscaped);
					uriInfo.MoreInfo.Query = text;
				}
				return text;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0003AE60 File Offset: 0x00039060
		public string Fragment
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				InternalUri.UriInfo uriInfo = this.EnsureUriInfo();
				if (uriInfo.MoreInfo == null)
				{
					uriInfo.MoreInfo = new InternalUri.MoreInfo();
				}
				string text = uriInfo.MoreInfo.Fragment;
				if (text == null)
				{
					text = this.GetParts(UriComponents.Fragment | UriComponents.KeepDelimiter, UriFormat.UriEscaped);
					uriInfo.MoreInfo.Fragment = text;
				}
				return text;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x0003AEC8 File Offset: 0x000390C8
		public string Scheme
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				return this.m_Syntax.SchemeName;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0003AEF0 File Offset: 0x000390F0
		private bool OriginalStringSwitched
		{
			get
			{
				return (this.m_iriParsing && this.InFact(InternalUri.Flags.HasUnicode)) || (this.AllowIdn && (this.InFact(InternalUri.Flags.IdnHost) || this.InFact(InternalUri.Flags.UnicodeHost)));
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x0003AF44 File Offset: 0x00039144
		public string OriginalString
		{
			get
			{
				if (!this.OriginalStringSwitched)
				{
					return this.m_String;
				}
				return this.m_originalUnicodeString;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0003AF5C File Offset: 0x0003915C
		public string DnsSafeHost
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				if (this.AllowIdn && ((this.m_Flags & InternalUri.Flags.IdnHost) != InternalUri.Flags.Zero || (this.m_Flags & InternalUri.Flags.UnicodeHost) != InternalUri.Flags.Zero))
				{
					this.EnsureUriInfo();
					return this.m_Info.DnsSafeHost;
				}
				this.EnsureHostString(false);
				if (!string.IsNullOrEmpty(this.m_Info.DnsSafeHost))
				{
					return this.m_Info.DnsSafeHost;
				}
				if (this.m_Info.Host.Length == 0)
				{
					return string.Empty;
				}
				string text = this.m_Info.Host;
				if (this.HostType == InternalUri.Flags.IPv6HostType)
				{
					text = text.Substring(1, text.Length - 2);
					if (this.m_Info.ScopeId != null)
					{
						text += this.m_Info.ScopeId;
					}
				}
				else if (this.HostType == InternalUri.Flags.BasicHostType && this.InFact(InternalUri.Flags.HostNotCanonical | InternalUri.Flags.E_HostNotCanonical))
				{
					char[] array = new char[text.Length];
					int num = 0;
					InternalUriHelper.UnescapeString(text, 0, text.Length, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, this.m_Syntax, false);
					text = new string(array, 0, num);
				}
				this.m_Info.DnsSafeHost = text;
				return text;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0003B0B4 File Offset: 0x000392B4
		public string IdnHost
		{
			get
			{
				string text = this.DnsSafeHost;
				if (this.HostType == InternalUri.Flags.DnsHostType)
				{
					text = DomainNameHelper.IdnEquivalent(text);
				}
				return text;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0003B0DE File Offset: 0x000392DE
		public bool IsAbsoluteUri
		{
			get
			{
				return this.m_Syntax != null;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0003B0E9 File Offset: 0x000392E9
		public bool UserEscaped
		{
			get
			{
				return this.InFact(InternalUri.Flags.UserEscaped);
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0003B0F7 File Offset: 0x000392F7
		public string UserInfo
		{
			get
			{
				if (this.IsNotAbsoluteUri)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
				}
				return this.GetParts(UriComponents.UserInfo, UriFormat.UriEscaped);
			}
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0003B11C File Offset: 0x0003931C
		public unsafe static UriHostNameType CheckHostName(string name)
		{
			if (name == null || name.Length == 0 || name.Length > 32767)
			{
				return UriHostNameType.Unknown;
			}
			int num = name.Length;
			fixed (string text = name)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				if (name[0] == '[' && name[name.Length - 1] == ']' && IPv6AddressHelper.IsValid(ptr, 1, ref num) && num == name.Length)
				{
					return UriHostNameType.IPv6;
				}
				num = name.Length;
				if (IPv4AddressHelper.IsValid(ptr, 0, ref num, false, false, false) && num == name.Length)
				{
					return UriHostNameType.IPv4;
				}
				num = name.Length;
				bool flag = false;
				if (DomainNameHelper.IsValid(ptr, 0, ref num, ref flag, false) && num == name.Length)
				{
					return UriHostNameType.Dns;
				}
				num = name.Length;
				flag = false;
				if (DomainNameHelper.IsValidByIri(ptr, 0, ref num, ref flag, false) && num == name.Length)
				{
					return UriHostNameType.Dns;
				}
			}
			num = name.Length + 2;
			name = "[" + name + "]";
			fixed (string text = name)
			{
				char* ptr2 = text;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				if (IPv6AddressHelper.IsValid(ptr2, 1, ref num) && num == name.Length)
				{
					return UriHostNameType.IPv6;
				}
			}
			return UriHostNameType.Unknown;
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0003B23C File Offset: 0x0003943C
		public string GetLeftPart(UriPartial part)
		{
			if (this.IsNotAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
			}
			this.EnsureUriInfo();
			switch (part)
			{
			case UriPartial.Scheme:
				return this.GetParts(UriComponents.Scheme | UriComponents.KeepDelimiter, UriFormat.UriEscaped);
			case UriPartial.Authority:
				if (this.NotAny(InternalUri.Flags.AuthorityFound) || this.IsDosPath)
				{
					return string.Empty;
				}
				return this.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port, UriFormat.UriEscaped);
			case UriPartial.Path:
				return this.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
			case UriPartial.Query:
				return this.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query, UriFormat.UriEscaped);
			default:
				throw new ArgumentException("part");
			}
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0003B2D4 File Offset: 0x000394D4
		public static string HexEscape(char character)
		{
			if (character > 'ÿ')
			{
				throw new ArgumentOutOfRangeException("character");
			}
			char[] array = new char[3];
			int num = 0;
			InternalUriHelper.EscapeAsciiChar(character, array, ref num);
			return new string(array);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0003B30C File Offset: 0x0003950C
		public static char HexUnescape(string pattern, ref int index)
		{
			if (index < 0 || index >= pattern.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (pattern[index] == '%' && pattern.Length - index >= 3)
			{
				char c = InternalUriHelper.EscapedAscii(pattern[index + 1], pattern[index + 2]);
				if (c != '\uffff')
				{
					index += 3;
					return c;
				}
			}
			int num = index;
			index = num + 1;
			return pattern[num];
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0003B384 File Offset: 0x00039584
		public static bool IsHexEncoding(string pattern, int index)
		{
			return pattern.Length - index >= 3 && (pattern[index] == '%' && InternalUriHelper.EscapedAscii(pattern[index + 1], pattern[index + 2]) != char.MaxValue);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0003B3BF File Offset: 0x000395BF
		internal static bool IsGenDelim(char ch)
		{
			return ch == ':' || ch == '/' || ch == '?' || ch == '#' || ch == '[' || ch == ']' || ch == '@';
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0003B3E8 File Offset: 0x000395E8
		public static bool CheckSchemeName(string schemeName)
		{
			if (schemeName == null || schemeName.Length == 0 || !InternalUri.IsAsciiLetter(schemeName[0]))
			{
				return false;
			}
			for (int i = schemeName.Length - 1; i > 0; i--)
			{
				if (!InternalUri.IsAsciiLetterOrDigit(schemeName[i]) && schemeName[i] != '+' && schemeName[i] != '-' && schemeName[i] != '.')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0003B455 File Offset: 0x00039655
		public static bool IsHexDigit(char character)
		{
			return (character >= '0' && character <= '9') || (character >= 'A' && character <= 'F') || (character >= 'a' && character <= 'f');
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0003B47C File Offset: 0x0003967C
		public static int FromHex(char digit)
		{
			if ((digit < '0' || digit > '9') && (digit < 'A' || digit > 'F') && (digit < 'a' || digit > 'f'))
			{
				throw new ArgumentException("digit");
			}
			if (digit > '9')
			{
				return (int)(((digit <= 'F') ? (digit - 'A') : (digit - 'a')) + '\n');
			}
			return (int)(digit - '0');
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0003B4D0 File Offset: 0x000396D0
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override int GetHashCode()
		{
			if (this.IsNotAbsoluteUri)
			{
				return InternalUri.CalculateCaseInsensitiveHashCode(this.OriginalString);
			}
			InternalUri.UriInfo uriInfo = this.EnsureUriInfo();
			if (uriInfo.MoreInfo == null)
			{
				uriInfo.MoreInfo = new InternalUri.MoreInfo();
			}
			int num = uriInfo.MoreInfo.Hash;
			if (num == 0)
			{
				string text = uriInfo.MoreInfo.RemoteUrl;
				if (text == null)
				{
					text = this.GetParts(UriComponents.HttpRequestUrl, UriFormat.SafeUnescaped);
				}
				num = InternalUri.CalculateCaseInsensitiveHashCode(text);
				if (num == 0)
				{
					num = 16777216;
				}
				uriInfo.MoreInfo.Hash = num;
			}
			return num;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0003B550 File Offset: 0x00039750
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override string ToString()
		{
			if (this.m_Syntax != null)
			{
				this.EnsureUriInfo();
				if (this.m_Info.String == null)
				{
					if (this.Syntax.IsSimple)
					{
						this.m_Info.String = this.GetComponentsHelper(UriComponents.AbsoluteUri, (UriFormat)32767);
					}
					else
					{
						this.m_Info.String = this.GetParts(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped);
					}
				}
				return this.m_Info.String;
			}
			if (!this.m_iriParsing || !this.InFact(InternalUri.Flags.HasUnicode))
			{
				return this.OriginalString;
			}
			return this.m_String;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0003B5E6 File Offset: 0x000397E6
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool operator ==(InternalUri uri1, InternalUri uri2)
		{
			return uri1 == uri2 || (uri1 != null && uri2 != null && uri2.Equals(uri1));
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0003B5FD File Offset: 0x000397FD
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool operator !=(InternalUri uri1, InternalUri uri2)
		{
			return uri1 != uri2 && (uri1 == null || uri2 == null || !uri2.Equals(uri1));
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x0003B618 File Offset: 0x00039818
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public unsafe override bool Equals(object comparand)
		{
			if (comparand == null)
			{
				return false;
			}
			if (this == comparand)
			{
				return true;
			}
			InternalUri internalUri = comparand as InternalUri;
			if (internalUri == null)
			{
				string text = comparand as string;
				if (text == null)
				{
					return false;
				}
				if (!InternalUri.TryCreate(text, UriKind.RelativeOrAbsolute, out internalUri))
				{
					return false;
				}
			}
			if (this.m_String == internalUri.m_String)
			{
				return true;
			}
			if (this.IsAbsoluteUri != internalUri.IsAbsoluteUri)
			{
				return false;
			}
			if (this.IsNotAbsoluteUri)
			{
				return this.OriginalString.Equals(internalUri.OriginalString);
			}
			if (this.NotAny((InternalUri.Flags)(-2147483648)) || internalUri.NotAny((InternalUri.Flags)(-2147483648)))
			{
				if (!this.IsUncOrDosPath)
				{
					if (this.m_String.Length == internalUri.m_String.Length)
					{
						fixed (string text2 = this.m_String)
						{
							char* ptr = text2;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (string text3 = internalUri.m_String)
							{
								char* ptr2 = text3;
								if (ptr2 != null)
								{
									ptr2 += RuntimeHelpers.OffsetToStringData / 2;
								}
								int num = this.m_String.Length - 1;
								while (num >= 0 && ptr[num] == ptr2[num])
								{
									num--;
								}
								if (num == -1)
								{
									return true;
								}
							}
						}
					}
				}
				else if (string.Compare(this.m_String, internalUri.m_String, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			this.EnsureUriInfo();
			internalUri.EnsureUriInfo();
			if (!this.UserDrivenParsing && !internalUri.UserDrivenParsing && this.Syntax.IsSimple && internalUri.Syntax.IsSimple)
			{
				if (this.InFact(InternalUri.Flags.CanonicalDnsHost) && internalUri.InFact(InternalUri.Flags.CanonicalDnsHost))
				{
					ushort num2 = this.m_Info.Offset.Host;
					ushort num3 = this.m_Info.Offset.Path;
					ushort num4 = internalUri.m_Info.Offset.Host;
					ushort path = internalUri.m_Info.Offset.Path;
					string @string = internalUri.m_String;
					if (num3 - num2 > path - num4)
					{
						num3 = num2 + path - num4;
					}
					while (num2 < num3)
					{
						if (this.m_String[(int)num2] != @string[(int)num4])
						{
							return false;
						}
						if (@string[(int)num4] == ':')
						{
							break;
						}
						num2 += 1;
						num4 += 1;
					}
					if (num2 < this.m_Info.Offset.Path && this.m_String[(int)num2] != ':')
					{
						return false;
					}
					if (num4 < path && @string[(int)num4] != ':')
					{
						return false;
					}
				}
				else
				{
					this.EnsureHostString(false);
					internalUri.EnsureHostString(false);
					if (!this.m_Info.Host.Equals(internalUri.m_Info.Host))
					{
						return false;
					}
				}
				if (this.Port != internalUri.Port)
				{
					return false;
				}
			}
			InternalUri.UriInfo info = this.m_Info;
			InternalUri.UriInfo info2 = internalUri.m_Info;
			if (info.MoreInfo == null)
			{
				info.MoreInfo = new InternalUri.MoreInfo();
			}
			if (info2.MoreInfo == null)
			{
				info2.MoreInfo = new InternalUri.MoreInfo();
			}
			string text4 = info.MoreInfo.RemoteUrl;
			if (text4 == null)
			{
				text4 = this.GetParts(UriComponents.HttpRequestUrl, UriFormat.SafeUnescaped);
				info.MoreInfo.RemoteUrl = text4;
			}
			string text5 = info2.MoreInfo.RemoteUrl;
			if (text5 == null)
			{
				text5 = internalUri.GetParts(UriComponents.HttpRequestUrl, UriFormat.SafeUnescaped);
				info2.MoreInfo.RemoteUrl = text5;
			}
			if (this.IsUncOrDosPath)
			{
				return string.Compare(info.MoreInfo.RemoteUrl, info2.MoreInfo.RemoteUrl, this.IsUncOrDosPath ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
			}
			if (text4.Length != text5.Length)
			{
				return false;
			}
			fixed (string text2 = text4)
			{
				char* ptr3 = text2;
				if (ptr3 != null)
				{
					ptr3 += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = text5)
				{
					char* ptr4 = text3;
					if (ptr4 != null)
					{
						ptr4 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr5 = ptr3 + text4.Length;
					char* ptr6 = ptr4 + text4.Length;
					while (ptr5 != ptr3)
					{
						if (*(--ptr5) != *(--ptr6))
						{
							return false;
						}
					}
					return true;
				}
			}
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0003BA10 File Offset: 0x00039C10
		public InternalUri MakeRelativeUri(InternalUri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (this.IsNotAbsoluteUri || uri.IsNotAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
			}
			if (this.Scheme == uri.Scheme && this.Host == uri.Host && this.Port == uri.Port)
			{
				string absolutePath = uri.AbsolutePath;
				string text = InternalUri.PathDifference(this.AbsolutePath, absolutePath, !this.IsUncOrDosPath);
				if (InternalUri.CheckForColonInFirstPathSegment(text) && (!uri.IsDosPath || !absolutePath.Equals(text, StringComparison.Ordinal)))
				{
					text = "./" + text;
				}
				text += uri.GetParts(UriComponents.Query | UriComponents.Fragment, UriFormat.UriEscaped);
				return new InternalUri(text, UriKind.Relative);
			}
			return uri;
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0003BADC File Offset: 0x00039CDC
		private static bool CheckForColonInFirstPathSegment(string uriString)
		{
			char[] array = new char[] { ':', '\\', '/', '?', '#' };
			int num = uriString.IndexOfAny(array);
			return num >= 0 && uriString[num] == ':';
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0003BB14 File Offset: 0x00039D14
		internal static string InternalEscapeString(string rawString, bool shouldUseLegacyV2Quirks)
		{
			if (rawString == null)
			{
				return string.Empty;
			}
			int num = 0;
			char[] array = InternalUriHelper.EscapeString(rawString, 0, rawString.Length, null, ref num, true, '?', '#', '%', shouldUseLegacyV2Quirks);
			if (array == null)
			{
				return rawString;
			}
			return new string(array, 0, num);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0003BB54 File Offset: 0x00039D54
		private unsafe static ParsingError ParseScheme(string uriString, ref InternalUri.Flags flags, ref InternalUriParser syntax)
		{
			int length = uriString.Length;
			if (length == 0)
			{
				return ParsingError.EmptyUriString;
			}
			if (length >= 65520)
			{
				return ParsingError.SizeLimit;
			}
			fixed (string text = uriString)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				ParsingError parsingError = ParsingError.None;
				ushort num = InternalUri.ParseSchemeCheckImplicitFile(ptr, (ushort)length, ref parsingError, ref flags, ref syntax);
				if (parsingError != ParsingError.None)
				{
					return parsingError;
				}
				flags |= (InternalUri.Flags)num;
			}
			return ParsingError.None;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0003BBA8 File Offset: 0x00039DA8
		internal UriFormatException ParseMinimal()
		{
			ParsingError parsingError = this.PrivateParseMinimal();
			if (parsingError == ParsingError.None)
			{
				return null;
			}
			this.m_Flags |= InternalUri.Flags.ErrorOrParsingRecursion;
			return InternalUri.GetException(parsingError);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0003BBDC File Offset: 0x00039DDC
		private unsafe ParsingError PrivateParseMinimal()
		{
			ushort num = (ushort)(this.m_Flags & InternalUri.Flags.IndexMask);
			ushort num2 = (ushort)this.m_String.Length;
			string text = null;
			this.m_Flags &= ~(InternalUri.Flags.SchemeNotCanonical | InternalUri.Flags.UserNotCanonical | InternalUri.Flags.HostNotCanonical | InternalUri.Flags.PortNotCanonical | InternalUri.Flags.PathNotCanonical | InternalUri.Flags.QueryNotCanonical | InternalUri.Flags.FragmentNotCanonical | InternalUri.Flags.E_UserNotCanonical | InternalUri.Flags.E_HostNotCanonical | InternalUri.Flags.E_PortNotCanonical | InternalUri.Flags.E_PathNotCanonical | InternalUri.Flags.E_QueryNotCanonical | InternalUri.Flags.E_FragmentNotCanonical | InternalUri.Flags.ShouldBeCompressed | InternalUri.Flags.FirstSlashAbsent | InternalUri.Flags.BackslashInPath | InternalUri.Flags.UserDrivenParsing);
			fixed (string text2 = ((this.m_iriParsing && (this.m_Flags & InternalUri.Flags.HasUnicode) != InternalUri.Flags.Zero && (this.m_Flags & InternalUri.Flags.HostUnicodeNormalized) == InternalUri.Flags.Zero) ? this.m_originalUnicodeString : this.m_String))
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				if (num2 > num && InternalUri.IsLWS(ptr[num2 - 1]))
				{
					num2 -= 1;
					while (num2 != num && InternalUri.IsLWS(ptr[(IntPtr)(num2 -= 1) * 2]))
					{
					}
					num2 += 1;
				}
				if (this.m_Syntax.IsAllSet(UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowDOSPath) && this.NotAny(InternalUri.Flags.ImplicitFile) && num + 1 < num2)
				{
					ushort num3 = num;
					char c;
					while (num3 < num2 && ((c = ptr[num3]) == '\\' || c == '/'))
					{
						num3 += 1;
					}
					if (this.m_Syntax.InFact(UriSyntaxFlags.FileLikeUri) || num3 - num <= 3)
					{
						if (num3 - num >= 2)
						{
							this.m_Flags |= InternalUri.Flags.AuthorityFound;
						}
						if (num3 + 1 < num2 && ((c = ptr[num3 + 1]) == ':' || c == '|') && InternalUri.IsAsciiLetter(ptr[num3]))
						{
							if (num3 + 2 >= num2 || ((c = ptr[num3 + 2]) != '\\' && c != '/'))
							{
								if (this.m_Syntax.InFact(UriSyntaxFlags.FileLikeUri))
								{
									return ParsingError.MustRootedPath;
								}
							}
							else
							{
								this.m_Flags |= InternalUri.Flags.DosPath;
								if (this.m_Syntax.InFact(UriSyntaxFlags.MustHaveAuthority))
								{
									this.m_Flags |= InternalUri.Flags.AuthorityFound;
								}
								if (num3 != num && num3 - num != 2)
								{
									num = num3 - 1;
								}
								else
								{
									num = num3;
								}
							}
						}
						else if (this.m_Syntax.InFact(UriSyntaxFlags.FileLikeUri) && num3 - num >= 2 && num3 - num != 3 && num3 < num2 && ptr[num3] != '?' && ptr[num3] != '#')
						{
							this.m_Flags |= InternalUri.Flags.UncPath;
							num = num3;
						}
					}
				}
				if ((this.m_Flags & (InternalUri.Flags.DosPath | InternalUri.Flags.UncPath)) == InternalUri.Flags.Zero)
				{
					if (num + 2 <= num2)
					{
						char c2 = ptr[num];
						char c3 = ptr[num + 1];
						if (this.m_Syntax.InFact(UriSyntaxFlags.MustHaveAuthority))
						{
							if ((c2 != '/' && c2 != '\\') || (c3 != '/' && c3 != '\\'))
							{
								return ParsingError.BadAuthority;
							}
							this.m_Flags |= InternalUri.Flags.AuthorityFound;
							num += 2;
						}
						else if (this.m_Syntax.InFact(UriSyntaxFlags.OptionalAuthority) && (this.InFact(InternalUri.Flags.AuthorityFound) || (c2 == '/' && c3 == '/')))
						{
							this.m_Flags |= InternalUri.Flags.AuthorityFound;
							num += 2;
						}
						else if (this.m_Syntax.NotAny(UriSyntaxFlags.MailToLikeUri))
						{
							if (this.m_iriParsing && (this.m_Flags & InternalUri.Flags.HasUnicode) != InternalUri.Flags.Zero && (this.m_Flags & InternalUri.Flags.HostUnicodeNormalized) == InternalUri.Flags.Zero)
							{
								this.m_String = this.m_String.Substring(0, (int)num);
							}
							this.m_Flags |= (InternalUri.Flags)num | InternalUri.Flags.HostTypeMask;
							return ParsingError.None;
						}
					}
					else
					{
						if (this.m_Syntax.InFact(UriSyntaxFlags.MustHaveAuthority))
						{
							return ParsingError.BadAuthority;
						}
						if (this.m_Syntax.NotAny(UriSyntaxFlags.MailToLikeUri))
						{
							if (this.m_iriParsing && (this.m_Flags & InternalUri.Flags.HasUnicode) != InternalUri.Flags.Zero && (this.m_Flags & InternalUri.Flags.HostUnicodeNormalized) == InternalUri.Flags.Zero)
							{
								this.m_String = this.m_String.Substring(0, (int)num);
							}
							this.m_Flags |= (InternalUri.Flags)num | InternalUri.Flags.HostTypeMask;
							return ParsingError.None;
						}
					}
				}
				if (this.InFact(InternalUri.Flags.DosPath))
				{
					this.m_Flags |= (((this.m_Flags & InternalUri.Flags.AuthorityFound) != InternalUri.Flags.Zero) ? InternalUri.Flags.BasicHostType : InternalUri.Flags.HostTypeMask);
					this.m_Flags |= (InternalUri.Flags)num;
					return ParsingError.None;
				}
				ParsingError parsingError = ParsingError.None;
				num = this.CheckAuthorityHelper(ptr, num, num2, ref parsingError, ref this.m_Flags, this.m_Syntax, ref text);
				if (parsingError != ParsingError.None)
				{
					return parsingError;
				}
				if (num < num2 && ptr[num] == '\\' && this.NotAny(InternalUri.Flags.ImplicitFile) && this.m_Syntax.NotAny(UriSyntaxFlags.AllowDOSPath))
				{
					return ParsingError.BadAuthorityTerminator;
				}
				this.m_Flags |= (InternalUri.Flags)num;
			}
			if (InternalUri.s_IdnScope != UriIdnScope.None || this.m_iriParsing)
			{
				this.PrivateParseMinimalIri(text, num);
			}
			return ParsingError.None;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0003C09C File Offset: 0x0003A29C
		private void PrivateParseMinimalIri(string newHost, ushort idx)
		{
			if (newHost != null)
			{
				this.m_String = newHost;
			}
			if ((!this.m_iriParsing && this.AllowIdn && ((this.m_Flags & InternalUri.Flags.IdnHost) != InternalUri.Flags.Zero || (this.m_Flags & InternalUri.Flags.UnicodeHost) != InternalUri.Flags.Zero)) || (this.m_iriParsing && (this.m_Flags & InternalUri.Flags.HasUnicode) == InternalUri.Flags.Zero && this.AllowIdn && (this.m_Flags & InternalUri.Flags.IdnHost) != InternalUri.Flags.Zero))
			{
				this.m_Flags &= ~(InternalUri.Flags.SchemeNotCanonical | InternalUri.Flags.UserNotCanonical | InternalUri.Flags.HostNotCanonical | InternalUri.Flags.PortNotCanonical | InternalUri.Flags.PathNotCanonical | InternalUri.Flags.QueryNotCanonical | InternalUri.Flags.FragmentNotCanonical | InternalUri.Flags.E_UserNotCanonical | InternalUri.Flags.E_HostNotCanonical | InternalUri.Flags.E_PortNotCanonical | InternalUri.Flags.E_PathNotCanonical | InternalUri.Flags.E_QueryNotCanonical | InternalUri.Flags.E_FragmentNotCanonical | InternalUri.Flags.ShouldBeCompressed | InternalUri.Flags.FirstSlashAbsent | InternalUri.Flags.BackslashInPath);
				this.m_Flags |= (InternalUri.Flags)((long)this.m_String.Length);
				this.m_String += this.m_originalUnicodeString.Substring((int)idx, this.m_originalUnicodeString.Length - (int)idx);
			}
			if (this.m_iriParsing && (this.m_Flags & InternalUri.Flags.HasUnicode) != InternalUri.Flags.Zero)
			{
				this.m_Flags |= InternalUri.Flags.UseOrigUncdStrOffset;
			}
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0003C1A4 File Offset: 0x0003A3A4
		private unsafe void CreateUriInfo(InternalUri.Flags cF)
		{
			InternalUri.UriInfo uriInfo = new InternalUri.UriInfo();
			uriInfo.Offset.End = (ushort)this.m_String.Length;
			if (!this.UserDrivenParsing)
			{
				bool flag = false;
				ushort num;
				if ((cF & InternalUri.Flags.ImplicitFile) != InternalUri.Flags.Zero)
				{
					num = 0;
					while (InternalUri.IsLWS(this.m_String[(int)num]))
					{
						num += 1;
						InternalUri.UriInfo uriInfo2 = uriInfo;
						uriInfo2.Offset.Scheme = uriInfo2.Offset.Scheme + 1;
					}
					if (InternalUri.StaticInFact(cF, InternalUri.Flags.UncPath))
					{
						for (num += 2; num < (ushort)(cF & InternalUri.Flags.IndexMask); num += 1)
						{
							if (this.m_String[(int)num] != '/' && this.m_String[(int)num] != '\\')
							{
								break;
							}
						}
					}
				}
				else
				{
					num = (ushort)this.m_Syntax.SchemeName.Length;
					for (;;)
					{
						string @string = this.m_String;
						ushort num2 = num;
						num = num2 + 1;
						if (@string[(int)num2] == ':')
						{
							break;
						}
						InternalUri.UriInfo uriInfo3 = uriInfo;
						uriInfo3.Offset.Scheme = uriInfo3.Offset.Scheme + 1;
					}
					if ((cF & InternalUri.Flags.AuthorityFound) != InternalUri.Flags.Zero)
					{
						if (this.m_String[(int)num] == '\\' || this.m_String[(int)(num + 1)] == '\\')
						{
							flag = true;
						}
						num += 2;
						if ((cF & (InternalUri.Flags.DosPath | InternalUri.Flags.UncPath)) != InternalUri.Flags.Zero)
						{
							while (num < (ushort)(cF & InternalUri.Flags.IndexMask) && (this.m_String[(int)num] == '/' || this.m_String[(int)num] == '\\'))
							{
								flag = true;
								num += 1;
							}
						}
					}
				}
				if (this.m_Syntax.DefaultPort != -1)
				{
					uriInfo.Offset.PortValue = (ushort)this.m_Syntax.DefaultPort;
				}
				if ((cF & InternalUri.Flags.HostTypeMask) == InternalUri.Flags.HostTypeMask || InternalUri.StaticInFact(cF, InternalUri.Flags.DosPath))
				{
					uriInfo.Offset.User = (ushort)(cF & InternalUri.Flags.IndexMask);
					uriInfo.Offset.Host = uriInfo.Offset.User;
					uriInfo.Offset.Path = uriInfo.Offset.User;
					cF &= ~(InternalUri.Flags.SchemeNotCanonical | InternalUri.Flags.UserNotCanonical | InternalUri.Flags.HostNotCanonical | InternalUri.Flags.PortNotCanonical | InternalUri.Flags.PathNotCanonical | InternalUri.Flags.QueryNotCanonical | InternalUri.Flags.FragmentNotCanonical | InternalUri.Flags.E_UserNotCanonical | InternalUri.Flags.E_HostNotCanonical | InternalUri.Flags.E_PortNotCanonical | InternalUri.Flags.E_PathNotCanonical | InternalUri.Flags.E_QueryNotCanonical | InternalUri.Flags.E_FragmentNotCanonical | InternalUri.Flags.ShouldBeCompressed | InternalUri.Flags.FirstSlashAbsent | InternalUri.Flags.BackslashInPath);
					if (flag)
					{
						cF |= InternalUri.Flags.SchemeNotCanonical;
					}
				}
				else
				{
					uriInfo.Offset.User = num;
					if (this.HostType == InternalUri.Flags.BasicHostType)
					{
						uriInfo.Offset.Host = num;
						uriInfo.Offset.Path = (ushort)(cF & InternalUri.Flags.IndexMask);
						cF &= ~(InternalUri.Flags.SchemeNotCanonical | InternalUri.Flags.UserNotCanonical | InternalUri.Flags.HostNotCanonical | InternalUri.Flags.PortNotCanonical | InternalUri.Flags.PathNotCanonical | InternalUri.Flags.QueryNotCanonical | InternalUri.Flags.FragmentNotCanonical | InternalUri.Flags.E_UserNotCanonical | InternalUri.Flags.E_HostNotCanonical | InternalUri.Flags.E_PortNotCanonical | InternalUri.Flags.E_PathNotCanonical | InternalUri.Flags.E_QueryNotCanonical | InternalUri.Flags.E_FragmentNotCanonical | InternalUri.Flags.ShouldBeCompressed | InternalUri.Flags.FirstSlashAbsent | InternalUri.Flags.BackslashInPath);
					}
					else
					{
						if ((cF & InternalUri.Flags.HasUserInfo) != InternalUri.Flags.Zero)
						{
							while (this.m_String[(int)num] != '@')
							{
								num += 1;
							}
							num += 1;
							uriInfo.Offset.Host = num;
						}
						else
						{
							uriInfo.Offset.Host = num;
						}
						num = (ushort)(cF & InternalUri.Flags.IndexMask);
						cF &= ~(InternalUri.Flags.SchemeNotCanonical | InternalUri.Flags.UserNotCanonical | InternalUri.Flags.HostNotCanonical | InternalUri.Flags.PortNotCanonical | InternalUri.Flags.PathNotCanonical | InternalUri.Flags.QueryNotCanonical | InternalUri.Flags.FragmentNotCanonical | InternalUri.Flags.E_UserNotCanonical | InternalUri.Flags.E_HostNotCanonical | InternalUri.Flags.E_PortNotCanonical | InternalUri.Flags.E_PathNotCanonical | InternalUri.Flags.E_QueryNotCanonical | InternalUri.Flags.E_FragmentNotCanonical | InternalUri.Flags.ShouldBeCompressed | InternalUri.Flags.FirstSlashAbsent | InternalUri.Flags.BackslashInPath);
						if (flag)
						{
							cF |= InternalUri.Flags.SchemeNotCanonical;
						}
						uriInfo.Offset.Path = num;
						bool flag2 = false;
						bool flag3 = (cF & InternalUri.Flags.UseOrigUncdStrOffset) > InternalUri.Flags.Zero;
						cF &= ~InternalUri.Flags.UseOrigUncdStrOffset;
						if (flag3)
						{
							uriInfo.Offset.End = (ushort)this.m_originalUnicodeString.Length;
						}
						if (num < uriInfo.Offset.End)
						{
							fixed (string text = (flag3 ? this.m_originalUnicodeString : this.m_String))
							{
								char* ptr = text;
								if (ptr != null)
								{
									ptr += RuntimeHelpers.OffsetToStringData / 2;
								}
								if (ptr[num] == ':')
								{
									int num3 = 0;
									if ((num += 1) < uriInfo.Offset.End)
									{
										num3 = (int)((ushort)(ptr[num] - '0'));
										if (num3 != 65535 && num3 != 15 && num3 != 65523)
										{
											flag2 = true;
											if (num3 == 0)
											{
												cF |= InternalUri.Flags.PortNotCanonical | InternalUri.Flags.E_PortNotCanonical;
											}
											for (num += 1; num < uriInfo.Offset.End; num += 1)
											{
												ushort num4 = (ushort)(ptr[num] - '0');
												if (num4 == 65535 || num4 == 15 || num4 == 65523)
												{
													break;
												}
												num3 = num3 * 10 + (int)num4;
											}
										}
									}
									if (flag2 && uriInfo.Offset.PortValue != (ushort)num3)
									{
										uriInfo.Offset.PortValue = (ushort)num3;
										cF |= InternalUri.Flags.NotDefaultPort;
									}
									else
									{
										cF |= InternalUri.Flags.PortNotCanonical | InternalUri.Flags.E_PortNotCanonical;
									}
									uriInfo.Offset.Path = num;
								}
							}
						}
					}
				}
			}
			cF |= InternalUri.Flags.MinimalUriInfoSet;
			uriInfo.DnsSafeHost = this.m_DnsSafeHost;
			string string2 = this.m_String;
			lock (string2)
			{
				if ((this.m_Flags & InternalUri.Flags.MinimalUriInfoSet) == InternalUri.Flags.Zero)
				{
					this.m_Info = uriInfo;
					this.m_Flags = (this.m_Flags & ~(InternalUri.Flags.SchemeNotCanonical | InternalUri.Flags.UserNotCanonical | InternalUri.Flags.HostNotCanonical | InternalUri.Flags.PortNotCanonical | InternalUri.Flags.PathNotCanonical | InternalUri.Flags.QueryNotCanonical | InternalUri.Flags.FragmentNotCanonical | InternalUri.Flags.E_UserNotCanonical | InternalUri.Flags.E_HostNotCanonical | InternalUri.Flags.E_PortNotCanonical | InternalUri.Flags.E_PathNotCanonical | InternalUri.Flags.E_QueryNotCanonical | InternalUri.Flags.E_FragmentNotCanonical | InternalUri.Flags.ShouldBeCompressed | InternalUri.Flags.FirstSlashAbsent | InternalUri.Flags.BackslashInPath)) | cF;
				}
			}
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0003C634 File Offset: 0x0003A834
		private unsafe void CreateHostString()
		{
			InternalUri.UriInfo uriInfo;
			if (!this.m_Syntax.IsSimple)
			{
				uriInfo = this.m_Info;
				lock (uriInfo)
				{
					if (this.NotAny(InternalUri.Flags.ErrorOrParsingRecursion))
					{
						this.m_Flags |= InternalUri.Flags.ErrorOrParsingRecursion;
						this.GetHostViaCustomSyntax();
						this.m_Flags &= ~InternalUri.Flags.ErrorOrParsingRecursion;
						return;
					}
				}
			}
			InternalUri.Flags flags = this.m_Flags;
			string text = InternalUri.CreateHostStringHelper(this.m_String, this.m_Info.Offset.Host, this.m_Info.Offset.Path, this.ShouldUseLegacyV2Quirks, ref flags, ref this.m_Info.ScopeId);
			if (text.Length != 0)
			{
				if (this.HostType == InternalUri.Flags.BasicHostType)
				{
					ushort num = 0;
					InternalUri.Check check;
					fixed (string text2 = text)
					{
						char* ptr = text2;
						if (ptr != null)
						{
							ptr += RuntimeHelpers.OffsetToStringData / 2;
						}
						check = this.CheckCanonical(ptr, ref num, (ushort)text.Length, char.MaxValue);
					}
					if ((check & InternalUri.Check.DisplayCanonical) == InternalUri.Check.None && (this.NotAny(InternalUri.Flags.ImplicitFile) || (check & InternalUri.Check.ReservedFound) != InternalUri.Check.None))
					{
						flags |= InternalUri.Flags.HostNotCanonical;
					}
					if (this.InFact(InternalUri.Flags.ImplicitFile) && (check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.ReservedFound)) != InternalUri.Check.None)
					{
						check &= ~InternalUri.Check.EscapedCanonical;
					}
					if ((check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.BackslashInPath)) != InternalUri.Check.EscapedCanonical)
					{
						flags |= InternalUri.Flags.E_HostNotCanonical;
						if (this.NotAny(InternalUri.Flags.UserEscaped))
						{
							int num2 = 0;
							char[] array = InternalUriHelper.EscapeString(text, 0, text.Length, null, ref num2, true, '?', '#', this.IsImplicitFile ? char.MaxValue : '%', this.ShouldUseLegacyV2Quirks);
							if (array != null)
							{
								text = new string(array, 0, num2);
							}
						}
					}
				}
				else if (this.NotAny(InternalUri.Flags.CanonicalDnsHost))
				{
					if (this.m_Info.ScopeId != null)
					{
						flags |= InternalUri.Flags.HostNotCanonical | InternalUri.Flags.E_HostNotCanonical;
					}
					else
					{
						for (int i = 0; i < text.Length; i++)
						{
							if ((int)this.m_Info.Offset.Host + i >= (int)this.m_Info.Offset.End || text[i] != this.m_String[(int)this.m_Info.Offset.Host + i])
							{
								flags |= InternalUri.Flags.HostNotCanonical | InternalUri.Flags.E_HostNotCanonical;
								break;
							}
						}
					}
				}
			}
			this.m_Info.Host = text;
			uriInfo = this.m_Info;
			lock (uriInfo)
			{
				this.m_Flags |= flags;
			}
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0003C8D4 File Offset: 0x0003AAD4
		private static string CreateHostStringHelper(string str, ushort idx, ushort end, bool shouldUseLegacyV2Quirks, ref InternalUri.Flags flags, ref string scopeId)
		{
			bool flag = false;
			InternalUri.Flags flags2 = flags & InternalUri.Flags.HostTypeMask;
			string text;
			if (flags2 <= InternalUri.Flags.DnsHostType)
			{
				if (flags2 == InternalUri.Flags.IPv6HostType)
				{
					text = IPv6AddressHelper.ParseCanonicalName(str, (int)idx, shouldUseLegacyV2Quirks, ref flag, ref scopeId);
					goto IL_00C7;
				}
				if (flags2 == InternalUri.Flags.IPv4HostType)
				{
					text = IPv4AddressHelper.ParseCanonicalName(str, (int)idx, (int)end, ref flag);
					goto IL_00C7;
				}
				if (flags2 == InternalUri.Flags.DnsHostType)
				{
					text = DomainNameHelper.ParseCanonicalName(str, (int)idx, (int)end, ref flag);
					goto IL_00C7;
				}
			}
			else
			{
				if (flags2 == InternalUri.Flags.UncHostType)
				{
					text = UncNameHelper.ParseCanonicalName(str, (int)idx, (int)end, ref flag);
					goto IL_00C7;
				}
				if (flags2 != InternalUri.Flags.BasicHostType)
				{
					if (flags2 == InternalUri.Flags.HostTypeMask)
					{
						text = string.Empty;
						goto IL_00C7;
					}
				}
				else
				{
					if (InternalUri.StaticInFact(flags, InternalUri.Flags.DosPath))
					{
						text = string.Empty;
					}
					else
					{
						text = str.Substring((int)idx, (int)(end - idx));
					}
					if (text.Length == 0)
					{
						flag = true;
						goto IL_00C7;
					}
					goto IL_00C7;
				}
			}
			throw InternalUri.GetException(ParsingError.BadHostName);
			IL_00C7:
			if (flag)
			{
				flags |= InternalUri.Flags.LoopbackHost;
			}
			return text;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0003C9BC File Offset: 0x0003ABBC
		private unsafe void GetHostViaCustomSyntax()
		{
			if (this.m_Info.Host != null)
			{
				return;
			}
			string text = this.m_Syntax.InternalGetComponents(this, UriComponents.Host, UriFormat.UriEscaped);
			if (this.m_Info.Host == null)
			{
				if (text.Length >= 65520)
				{
					throw InternalUri.GetException(ParsingError.SizeLimit);
				}
				ParsingError parsingError = ParsingError.None;
				InternalUri.Flags flags = this.m_Flags & ~InternalUri.Flags.HostTypeMask;
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					string text3 = null;
					if (this.CheckAuthorityHelper(ptr, 0, (ushort)text.Length, ref parsingError, ref flags, this.m_Syntax, ref text3) != (ushort)text.Length)
					{
						flags &= ~InternalUri.Flags.HostTypeMask;
						flags |= InternalUri.Flags.HostTypeMask;
					}
				}
				if (parsingError != ParsingError.None || (flags & InternalUri.Flags.HostTypeMask) == InternalUri.Flags.HostTypeMask)
				{
					this.m_Flags = (this.m_Flags & ~InternalUri.Flags.HostTypeMask) | InternalUri.Flags.BasicHostType;
				}
				else
				{
					text = InternalUri.CreateHostStringHelper(text, 0, (ushort)text.Length, this.ShouldUseLegacyV2Quirks, ref flags, ref this.m_Info.ScopeId);
					for (int i = 0; i < text.Length; i++)
					{
						if ((int)this.m_Info.Offset.Host + i >= (int)this.m_Info.Offset.End || text[i] != this.m_String[(int)this.m_Info.Offset.Host + i])
						{
							this.m_Flags |= InternalUri.Flags.HostNotCanonical | InternalUri.Flags.E_HostNotCanonical;
							break;
						}
					}
					this.m_Flags = (this.m_Flags & ~InternalUri.Flags.HostTypeMask) | (flags & InternalUri.Flags.HostTypeMask);
				}
			}
			string text4 = this.m_Syntax.InternalGetComponents(this, UriComponents.StrongPort, UriFormat.UriEscaped);
			int num = 0;
			if (text4 == null || text4.Length == 0)
			{
				this.m_Flags &= ~InternalUri.Flags.NotDefaultPort;
				this.m_Flags |= InternalUri.Flags.PortNotCanonical | InternalUri.Flags.E_PortNotCanonical;
				this.m_Info.Offset.PortValue = 0;
			}
			else
			{
				for (int j = 0; j < text4.Length; j++)
				{
					int num2 = (int)(text4[j] - '0');
					if (num2 < 0 || num2 > 9 || (num = num * 10 + num2) > 65535)
					{
						throw new UriFormatException(SR.GetString("net_uri_PortOutOfRange", this.m_Syntax.GetType().FullName, text4));
					}
				}
				if (num != (int)this.m_Info.Offset.PortValue)
				{
					if (num == this.m_Syntax.DefaultPort)
					{
						this.m_Flags &= ~InternalUri.Flags.NotDefaultPort;
					}
					else
					{
						this.m_Flags |= InternalUri.Flags.NotDefaultPort;
					}
					this.m_Flags |= InternalUri.Flags.PortNotCanonical | InternalUri.Flags.E_PortNotCanonical;
					this.m_Info.Offset.PortValue = (ushort)num;
				}
			}
			this.m_Info.Host = text;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0003CC8F File Offset: 0x0003AE8F
		internal string GetParts(UriComponents uriParts, UriFormat formatAs)
		{
			return this.GetComponents(uriParts, formatAs);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		private string GetEscapedParts(UriComponents uriParts)
		{
			ushort num = (ushort)(((ushort)this.m_Flags & 16256) >> 6);
			if (this.InFact(InternalUri.Flags.SchemeNotCanonical))
			{
				num |= 1;
			}
			if ((uriParts & UriComponents.Path) != (UriComponents)0)
			{
				if (this.InFact(InternalUri.Flags.ShouldBeCompressed | InternalUri.Flags.FirstSlashAbsent | InternalUri.Flags.BackslashInPath))
				{
					num |= 16;
				}
				else if (this.IsDosPath && this.m_String[(int)(this.m_Info.Offset.Path + this.SecuredPathIndex - 1)] == '|')
				{
					num |= 16;
				}
			}
			if (((ushort)uriParts & num) == 0)
			{
				string uriPartsFromUserString = this.GetUriPartsFromUserString(uriParts);
				if (uriPartsFromUserString != null)
				{
					return uriPartsFromUserString;
				}
			}
			return this.ReCreateParts(uriParts, num, UriFormat.UriEscaped);
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0003CD38 File Offset: 0x0003AF38
		private string GetUnescapedParts(UriComponents uriParts, UriFormat formatAs)
		{
			ushort num = (ushort)this.m_Flags & 127;
			if ((uriParts & UriComponents.Path) != (UriComponents)0)
			{
				if ((this.m_Flags & (InternalUri.Flags.ShouldBeCompressed | InternalUri.Flags.FirstSlashAbsent | InternalUri.Flags.BackslashInPath)) != InternalUri.Flags.Zero)
				{
					num |= 16;
				}
				else if (this.IsDosPath && this.m_String[(int)(this.m_Info.Offset.Path + this.SecuredPathIndex - 1)] == '|')
				{
					num |= 16;
				}
			}
			if (((ushort)uriParts & num) == 0)
			{
				string uriPartsFromUserString = this.GetUriPartsFromUserString(uriParts);
				if (uriPartsFromUserString != null)
				{
					return uriPartsFromUserString;
				}
			}
			return this.ReCreateParts(uriParts, num, formatAs);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0003CDC0 File Offset: 0x0003AFC0
		private unsafe string ReCreateParts(UriComponents parts, ushort nonCanonical, UriFormat formatAs)
		{
			this.EnsureHostString(false);
			string text = (((parts & UriComponents.Host) == (UriComponents)0) ? string.Empty : this.m_Info.Host);
			int num = (int)((this.m_Info.Offset.End - this.m_Info.Offset.User) * ((formatAs == UriFormat.UriEscaped) ? 12 : 1));
			char[] array = new char[text.Length + num + this.m_Syntax.SchemeName.Length + 3 + 1];
			num = 0;
			if ((parts & UriComponents.Scheme) != (UriComponents)0)
			{
				this.m_Syntax.SchemeName.CopyTo(0, array, num, this.m_Syntax.SchemeName.Length);
				num += this.m_Syntax.SchemeName.Length;
				if (parts != UriComponents.Scheme)
				{
					array[num++] = ':';
					if (this.InFact(InternalUri.Flags.AuthorityFound))
					{
						array[num++] = '/';
						array[num++] = '/';
					}
				}
			}
			if ((parts & UriComponents.UserInfo) != (UriComponents)0 && this.InFact(InternalUri.Flags.HasUserInfo))
			{
				if ((nonCanonical & 2) != 0)
				{
					switch (formatAs)
					{
					case UriFormat.UriEscaped:
						if (this.NotAny(InternalUri.Flags.UserEscaped))
						{
							array = InternalUriHelper.EscapeString(this.m_String, (int)this.m_Info.Offset.User, (int)this.m_Info.Offset.Host, array, ref num, true, '?', '#', '%', this.ShouldUseLegacyV2Quirks);
						}
						else
						{
							this.InFact(InternalUri.Flags.E_UserNotCanonical);
							this.m_String.CopyTo((int)this.m_Info.Offset.User, array, num, (int)(this.m_Info.Offset.Host - this.m_Info.Offset.User));
							num += (int)(this.m_Info.Offset.Host - this.m_Info.Offset.User);
						}
						break;
					case UriFormat.Unescaped:
						array = InternalUriHelper.UnescapeString(this.m_String, (int)this.m_Info.Offset.User, (int)this.m_Info.Offset.Host, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, this.m_Syntax, false);
						break;
					case UriFormat.SafeUnescaped:
						array = InternalUriHelper.UnescapeString(this.m_String, (int)this.m_Info.Offset.User, (int)(this.m_Info.Offset.Host - 1), array, ref num, '@', '/', '\\', this.InFact(InternalUri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape, this.m_Syntax, false);
						array[num++] = '@';
						break;
					default:
						array = InternalUriHelper.UnescapeString(this.m_String, (int)this.m_Info.Offset.User, (int)this.m_Info.Offset.Host, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, false);
						break;
					}
				}
				else
				{
					InternalUriHelper.UnescapeString(this.m_String, (int)this.m_Info.Offset.User, (int)this.m_Info.Offset.Host, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, false);
				}
				if (parts == UriComponents.UserInfo)
				{
					num--;
				}
			}
			if ((parts & UriComponents.Host) != (UriComponents)0 && text.Length != 0)
			{
				UnescapeMode unescapeMode;
				if (formatAs != UriFormat.UriEscaped && this.HostType == InternalUri.Flags.BasicHostType && (nonCanonical & 4) != 0)
				{
					unescapeMode = ((formatAs == UriFormat.Unescaped) ? (UnescapeMode.Unescape | UnescapeMode.UnescapeAll) : (this.InFact(InternalUri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape));
				}
				else
				{
					unescapeMode = UnescapeMode.CopyOnly;
				}
				if ((parts & UriComponents.NormalizedHost) != (UriComponents)0)
				{
					fixed (string text2 = text)
					{
						char* ptr = text2;
						if (ptr != null)
						{
							ptr += RuntimeHelpers.OffsetToStringData / 2;
						}
						bool flag = false;
						bool flag2 = false;
						try
						{
							text = DomainNameHelper.UnicodeEquivalent(ptr, 0, text.Length, ref flag, ref flag2);
						}
						catch (UriFormatException)
						{
						}
					}
				}
				array = InternalUriHelper.UnescapeString(text, 0, text.Length, array, ref num, '/', '?', '#', unescapeMode, this.m_Syntax, false);
				if ((parts & UriComponents.SerializationInfoString) != (UriComponents)0 && this.HostType == InternalUri.Flags.IPv6HostType && this.m_Info.ScopeId != null)
				{
					this.m_Info.ScopeId.CopyTo(0, array, num - 1, this.m_Info.ScopeId.Length);
					num += this.m_Info.ScopeId.Length;
					array[num - 1] = ']';
				}
			}
			if ((parts & UriComponents.Port) != (UriComponents)0)
			{
				if ((nonCanonical & 8) == 0)
				{
					if (this.InFact(InternalUri.Flags.NotDefaultPort))
					{
						ushort num2 = this.m_Info.Offset.Path;
						while (this.m_String[(int)(num2 -= 1)] != ':')
						{
						}
						this.m_String.CopyTo((int)num2, array, num, (int)(this.m_Info.Offset.Path - num2));
						num += (int)(this.m_Info.Offset.Path - num2);
					}
					else if ((parts & UriComponents.StrongPort) != (UriComponents)0 && this.m_Syntax.DefaultPort != -1)
					{
						array[num++] = ':';
						text = this.m_Info.Offset.PortValue.ToString(CultureInfo.InvariantCulture);
						text.CopyTo(0, array, num, text.Length);
						num += text.Length;
					}
				}
				else if (this.InFact(InternalUri.Flags.NotDefaultPort) || ((parts & UriComponents.StrongPort) != (UriComponents)0 && this.m_Syntax.DefaultPort != -1))
				{
					array[num++] = ':';
					text = this.m_Info.Offset.PortValue.ToString(CultureInfo.InvariantCulture);
					text.CopyTo(0, array, num, text.Length);
					num += text.Length;
				}
			}
			if ((parts & UriComponents.Path) != (UriComponents)0)
			{
				array = this.GetCanonicalPath(array, ref num, formatAs);
				if (parts == UriComponents.Path)
				{
					ushort num3;
					if (this.InFact(InternalUri.Flags.AuthorityFound) && num != 0 && array[0] == '/')
					{
						num3 = 1;
						num--;
					}
					else
					{
						num3 = 0;
					}
					if (num != 0)
					{
						return new string(array, (int)num3, num);
					}
					return string.Empty;
				}
			}
			if ((parts & UriComponents.Query) != (UriComponents)0 && this.m_Info.Offset.Query < this.m_Info.Offset.Fragment)
			{
				ushort num3 = this.m_Info.Offset.Query + 1;
				if (parts != UriComponents.Query)
				{
					array[num++] = '?';
				}
				if ((nonCanonical & 32) != 0)
				{
					if (formatAs != UriFormat.UriEscaped)
					{
						if (formatAs != UriFormat.Unescaped)
						{
							if (formatAs != (UriFormat)32767)
							{
								array = InternalUriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, '#', char.MaxValue, char.MaxValue, this.InFact(InternalUri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape, this.m_Syntax, true);
							}
							else
							{
								array = InternalUriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, '#', char.MaxValue, char.MaxValue, (this.InFact(InternalUri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape) | UnescapeMode.V1ToStringFlag, this.m_Syntax, true);
							}
						}
						else
						{
							array = InternalUriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, '#', char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, this.m_Syntax, true);
						}
					}
					else if (this.NotAny(InternalUri.Flags.UserEscaped))
					{
						array = InternalUriHelper.EscapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, true, '#', char.MaxValue, '%', this.ShouldUseLegacyV2Quirks);
					}
					else
					{
						InternalUriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, true);
					}
				}
				else
				{
					InternalUriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.Fragment, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, true);
				}
			}
			if ((parts & UriComponents.Fragment) != (UriComponents)0 && this.m_Info.Offset.Fragment < this.m_Info.Offset.End)
			{
				ushort num3 = this.m_Info.Offset.Fragment + 1;
				if (parts != UriComponents.Fragment)
				{
					array[num++] = '#';
				}
				if ((nonCanonical & 64) != 0)
				{
					if (formatAs != UriFormat.UriEscaped)
					{
						if (formatAs != UriFormat.Unescaped)
						{
							if (formatAs != (UriFormat)32767)
							{
								array = InternalUriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, '#', char.MaxValue, char.MaxValue, this.InFact(InternalUri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape, this.m_Syntax, false);
							}
							else
							{
								array = InternalUriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, '#', char.MaxValue, char.MaxValue, (this.InFact(InternalUri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape) | UnescapeMode.V1ToStringFlag, this.m_Syntax, false);
							}
						}
						else
						{
							array = InternalUriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, '#', char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, this.m_Syntax, false);
						}
					}
					else if (this.NotAny(InternalUri.Flags.UserEscaped))
					{
						array = InternalUriHelper.EscapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, true, this.ShouldUseLegacyV2Quirks ? '#' : char.MaxValue, char.MaxValue, '%', this.ShouldUseLegacyV2Quirks);
					}
					else
					{
						InternalUriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, false);
					}
				}
				else
				{
					InternalUriHelper.UnescapeString(this.m_String, (int)num3, (int)this.m_Info.Offset.End, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.CopyOnly, this.m_Syntax, false);
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0003D78C File Offset: 0x0003B98C
		private string GetUriPartsFromUserString(UriComponents uriParts)
		{
			UriComponents uriComponents = uriParts & ~UriComponents.KeepDelimiter;
			if (uriComponents <= UriComponents.Fragment)
			{
				if (uriComponents <= UriComponents.Path)
				{
					switch (uriComponents)
					{
					case UriComponents.Scheme:
						if (uriParts != UriComponents.Scheme)
						{
							return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.User - this.m_Info.Offset.Scheme));
						}
						return this.m_Syntax.SchemeName;
					case UriComponents.UserInfo:
					{
						if (this.NotAny(InternalUri.Flags.HasUserInfo))
						{
							return string.Empty;
						}
						ushort num;
						if (uriParts == UriComponents.UserInfo)
						{
							num = this.m_Info.Offset.Host - 1;
						}
						else
						{
							num = this.m_Info.Offset.Host;
						}
						if (this.m_Info.Offset.User >= num)
						{
							return string.Empty;
						}
						return this.m_String.Substring((int)this.m_Info.Offset.User, (int)(num - this.m_Info.Offset.User));
					}
					case UriComponents.Scheme | UriComponents.UserInfo:
						goto IL_0992;
					case UriComponents.Host:
					{
						ushort num2 = this.m_Info.Offset.Path;
						if (this.InFact(InternalUri.Flags.PortNotCanonical | InternalUri.Flags.NotDefaultPort))
						{
							while (this.m_String[(int)(num2 -= 1)] != ':')
							{
							}
						}
						if (num2 - this.m_Info.Offset.Host != 0)
						{
							return this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(num2 - this.m_Info.Offset.Host));
						}
						return string.Empty;
					}
					default:
						switch (uriComponents)
						{
						case UriComponents.SchemeAndServer:
							if (!this.InFact(InternalUri.Flags.HasUserInfo))
							{
								return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.Scheme));
							}
							return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.User - this.m_Info.Offset.Scheme)) + this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.Host));
						case UriComponents.UserInfo | UriComponents.Host | UriComponents.Port:
							break;
						case UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port:
							return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.Scheme));
						case UriComponents.Path:
						{
							ushort num;
							if (uriParts == UriComponents.Path && this.InFact(InternalUri.Flags.AuthorityFound) && this.m_Info.Offset.End > this.m_Info.Offset.Path && this.m_String[(int)this.m_Info.Offset.Path] == '/')
							{
								num = this.m_Info.Offset.Path + 1;
							}
							else
							{
								num = this.m_Info.Offset.Path;
							}
							if (num >= this.m_Info.Offset.Query)
							{
								return string.Empty;
							}
							return this.m_String.Substring((int)num, (int)(this.m_Info.Offset.Query - num));
						}
						default:
							goto IL_0992;
						}
						break;
					}
				}
				else if (uriComponents != UriComponents.Query)
				{
					if (uriComponents == UriComponents.PathAndQuery)
					{
						return this.m_String.Substring((int)this.m_Info.Offset.Path, (int)(this.m_Info.Offset.Fragment - this.m_Info.Offset.Path));
					}
					switch (uriComponents)
					{
					case UriComponents.HttpRequestUrl:
						if (this.InFact(InternalUri.Flags.HasUserInfo))
						{
							return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.User - this.m_Info.Offset.Scheme)) + this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(this.m_Info.Offset.Fragment - this.m_Info.Offset.Host));
						}
						if (this.m_Info.Offset.Scheme == 0 && (int)this.m_Info.Offset.Fragment == this.m_String.Length)
						{
							return this.m_String;
						}
						return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.Fragment - this.m_Info.Offset.Scheme));
					case UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query:
						goto IL_0992;
					case UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query:
						if (this.m_Info.Offset.Scheme == 0 && (int)this.m_Info.Offset.Fragment == this.m_String.Length)
						{
							return this.m_String;
						}
						return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.Fragment - this.m_Info.Offset.Scheme));
					case UriComponents.Fragment:
					{
						ushort num;
						if (uriParts == UriComponents.Fragment)
						{
							num = this.m_Info.Offset.Fragment + 1;
						}
						else
						{
							num = this.m_Info.Offset.Fragment;
						}
						if (num >= this.m_Info.Offset.End)
						{
							return string.Empty;
						}
						return this.m_String.Substring((int)num, (int)(this.m_Info.Offset.End - num));
					}
					default:
						goto IL_0992;
					}
				}
				else
				{
					ushort num;
					if (uriParts == UriComponents.Query)
					{
						num = this.m_Info.Offset.Query + 1;
					}
					else
					{
						num = this.m_Info.Offset.Query;
					}
					if (num >= this.m_Info.Offset.Fragment)
					{
						return string.Empty;
					}
					return this.m_String.Substring((int)num, (int)(this.m_Info.Offset.Fragment - num));
				}
			}
			else if (uriComponents <= (UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query | UriComponents.Fragment))
			{
				if (uriComponents == (UriComponents.Path | UriComponents.Query | UriComponents.Fragment))
				{
					return this.m_String.Substring((int)this.m_Info.Offset.Path, (int)(this.m_Info.Offset.End - this.m_Info.Offset.Path));
				}
				if (uriComponents != (UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query | UriComponents.Fragment))
				{
					goto IL_0992;
				}
				if (this.InFact(InternalUri.Flags.HasUserInfo))
				{
					return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.User - this.m_Info.Offset.Scheme)) + this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(this.m_Info.Offset.End - this.m_Info.Offset.Host));
				}
				if (this.m_Info.Offset.Scheme == 0 && (int)this.m_Info.Offset.End == this.m_String.Length)
				{
					return this.m_String;
				}
				return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.End - this.m_Info.Offset.Scheme));
			}
			else if (uriComponents != UriComponents.AbsoluteUri)
			{
				if (uriComponents != UriComponents.HostAndPort)
				{
					if (uriComponents != UriComponents.StrongAuthority)
					{
						goto IL_0992;
					}
				}
				else if (this.InFact(InternalUri.Flags.HasUserInfo))
				{
					if (this.InFact(InternalUri.Flags.NotDefaultPort) || this.m_Syntax.DefaultPort == -1)
					{
						return this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.Host));
					}
					return this.m_String.Substring((int)this.m_Info.Offset.Host, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.Host)) + ":" + this.m_Info.Offset.PortValue.ToString(CultureInfo.InvariantCulture);
				}
				if (!this.InFact(InternalUri.Flags.NotDefaultPort) && this.m_Syntax.DefaultPort != -1)
				{
					return this.m_String.Substring((int)this.m_Info.Offset.User, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.User)) + ":" + this.m_Info.Offset.PortValue.ToString(CultureInfo.InvariantCulture);
				}
			}
			else
			{
				if (this.m_Info.Offset.Scheme == 0 && (int)this.m_Info.Offset.End == this.m_String.Length)
				{
					return this.m_String;
				}
				return this.m_String.Substring((int)this.m_Info.Offset.Scheme, (int)(this.m_Info.Offset.End - this.m_Info.Offset.Scheme));
			}
			if (this.m_Info.Offset.Path - this.m_Info.Offset.User != 0)
			{
				return this.m_String.Substring((int)this.m_Info.Offset.User, (int)(this.m_Info.Offset.Path - this.m_Info.Offset.User));
			}
			return string.Empty;
			IL_0992:
			return null;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0003E12C File Offset: 0x0003C32C
		private unsafe void ParseRemaining()
		{
			this.EnsureUriInfo();
			InternalUri.Flags flags = InternalUri.Flags.Zero;
			if (!this.UserDrivenParsing)
			{
				bool flag = this.m_iriParsing && (this.m_Flags & InternalUri.Flags.HasUnicode) != InternalUri.Flags.Zero && (this.m_Flags & InternalUri.Flags.RestUnicodeNormalized) == InternalUri.Flags.Zero;
				ushort num = this.m_Info.Offset.Scheme;
				ushort num2 = (ushort)this.m_String.Length;
				UriSyntaxFlags flags2 = this.m_Syntax.Flags;
				InternalUri.Check check;
				fixed (string text = this.m_String)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					if (num2 > num && InternalUri.IsLWS(ptr[num2 - 1]))
					{
						num2 -= 1;
						while (num2 != num && InternalUri.IsLWS(ptr[(IntPtr)(num2 -= 1) * 2]))
						{
						}
						num2 += 1;
					}
					if (this.IsImplicitFile)
					{
						flags |= InternalUri.Flags.SchemeNotCanonical;
					}
					else
					{
						ushort num3 = 0;
						ushort num4 = (ushort)this.m_Syntax.SchemeName.Length;
						while (num3 < num4)
						{
							if (this.m_Syntax.SchemeName[(int)num3] != ptr[num + num3])
							{
								flags |= InternalUri.Flags.SchemeNotCanonical;
							}
							num3 += 1;
						}
						if ((this.m_Flags & InternalUri.Flags.AuthorityFound) != InternalUri.Flags.Zero && (num + num3 + 3 >= num2 || ptr[num + num3 + 1] != '/' || ptr[num + num3 + 2] != '/'))
						{
							flags |= InternalUri.Flags.SchemeNotCanonical;
						}
					}
					if ((this.m_Flags & InternalUri.Flags.HasUserInfo) != InternalUri.Flags.Zero)
					{
						num = this.m_Info.Offset.User;
						check = this.CheckCanonical(ptr, ref num, this.m_Info.Offset.Host, '@');
						if ((check & InternalUri.Check.DisplayCanonical) == InternalUri.Check.None)
						{
							flags |= InternalUri.Flags.UserNotCanonical;
						}
						if ((check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.BackslashInPath)) != InternalUri.Check.EscapedCanonical)
						{
							flags |= InternalUri.Flags.E_UserNotCanonical;
						}
						if (this.m_iriParsing && (check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.DisplayCanonical | InternalUri.Check.BackslashInPath | InternalUri.Check.NotIriCanonical | InternalUri.Check.FoundNonAscii)) == (InternalUri.Check.DisplayCanonical | InternalUri.Check.FoundNonAscii))
						{
							flags |= InternalUri.Flags.UserIriCanonical;
						}
					}
				}
				num = this.m_Info.Offset.Path;
				ushort num5 = this.m_Info.Offset.Path;
				if (flag)
				{
					if (this.IsDosPath)
					{
						if (this.IsImplicitFile)
						{
							this.m_String = string.Empty;
						}
						else
						{
							this.m_String = this.m_Syntax.SchemeName + InternalUri.SchemeDelimiter;
						}
					}
					this.m_Info.Offset.Path = (ushort)this.m_String.Length;
					num = this.m_Info.Offset.Path;
					ushort num6 = num5;
					if (this.IsImplicitFile || (flags2 & (UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment)) == UriSyntaxFlags.None)
					{
						this.FindEndOfComponent(this.m_originalUnicodeString, ref num5, (ushort)this.m_originalUnicodeString.Length, char.MaxValue);
					}
					else
					{
						this.FindEndOfComponent(this.m_originalUnicodeString, ref num5, (ushort)this.m_originalUnicodeString.Length, this.m_Syntax.InFact(UriSyntaxFlags.MayHaveQuery) ? '?' : (this.m_Syntax.InFact(UriSyntaxFlags.MayHaveFragment) ? '#' : '\ufffe'));
					}
					string text2 = this.EscapeUnescapeIri(this.m_originalUnicodeString, (int)num6, (int)num5, UriComponents.Path);
					try
					{
						if (this.ShouldUseLegacyV2Quirks)
						{
							this.m_String += text2.Normalize(NormalizationForm.FormC);
						}
						else
						{
							this.m_String += text2;
						}
					}
					catch (ArgumentException)
					{
						throw InternalUri.GetException(ParsingError.BadFormat);
					}
					if (!InternalServicePointManager.AllowAllUriEncodingExpansion && this.m_String.Length > 65535)
					{
						throw InternalUri.GetException(ParsingError.SizeLimit);
					}
					num2 = (ushort)this.m_String.Length;
				}
				fixed (string text = this.m_String)
				{
					char* ptr2 = text;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					if (this.IsImplicitFile || (flags2 & (UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment)) == UriSyntaxFlags.None)
					{
						check = this.CheckCanonical(ptr2, ref num, num2, char.MaxValue);
					}
					else
					{
						check = this.CheckCanonical(ptr2, ref num, num2, ((flags2 & UriSyntaxFlags.MayHaveQuery) != UriSyntaxFlags.None) ? '?' : (this.m_Syntax.InFact(UriSyntaxFlags.MayHaveFragment) ? '#' : '\ufffe'));
					}
					if ((this.m_Flags & InternalUri.Flags.AuthorityFound) != InternalUri.Flags.Zero && (flags2 & UriSyntaxFlags.PathIsRooted) != UriSyntaxFlags.None && (this.m_Info.Offset.Path == num2 || (ptr2[this.m_Info.Offset.Path] != '/' && ptr2[this.m_Info.Offset.Path] != '\\')))
					{
						flags |= InternalUri.Flags.FirstSlashAbsent;
					}
				}
				bool flag2 = false;
				if (this.IsDosPath || ((this.m_Flags & InternalUri.Flags.AuthorityFound) != InternalUri.Flags.Zero && ((flags2 & (UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath)) != UriSyntaxFlags.None || this.m_Syntax.InFact(UriSyntaxFlags.UnEscapeDotsAndSlashes))))
				{
					if ((check & InternalUri.Check.DotSlashEscaped) != InternalUri.Check.None && this.m_Syntax.InFact(UriSyntaxFlags.UnEscapeDotsAndSlashes))
					{
						flags |= InternalUri.Flags.PathNotCanonical | InternalUri.Flags.E_PathNotCanonical;
						flag2 = true;
					}
					if ((flags2 & UriSyntaxFlags.ConvertPathSlashes) != UriSyntaxFlags.None && (check & InternalUri.Check.BackslashInPath) != InternalUri.Check.None)
					{
						flags |= InternalUri.Flags.PathNotCanonical | InternalUri.Flags.E_PathNotCanonical;
						flag2 = true;
					}
					if ((flags2 & UriSyntaxFlags.CompressPath) != UriSyntaxFlags.None && ((flags & InternalUri.Flags.E_PathNotCanonical) != InternalUri.Flags.Zero || (check & InternalUri.Check.DotSlashAttn) != InternalUri.Check.None))
					{
						flags |= InternalUri.Flags.ShouldBeCompressed;
					}
					if ((check & InternalUri.Check.BackslashInPath) != InternalUri.Check.None)
					{
						flags |= InternalUri.Flags.BackslashInPath;
					}
				}
				else if ((check & InternalUri.Check.BackslashInPath) != InternalUri.Check.None)
				{
					flags |= InternalUri.Flags.E_PathNotCanonical;
					flag2 = true;
				}
				if ((check & InternalUri.Check.DisplayCanonical) == InternalUri.Check.None && ((this.m_Flags & InternalUri.Flags.ImplicitFile) == InternalUri.Flags.Zero || (this.m_Flags & InternalUri.Flags.UserEscaped) != InternalUri.Flags.Zero || (check & InternalUri.Check.ReservedFound) != InternalUri.Check.None))
				{
					flags |= InternalUri.Flags.PathNotCanonical;
					flag2 = true;
				}
				if ((this.m_Flags & InternalUri.Flags.ImplicitFile) != InternalUri.Flags.Zero && (check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.ReservedFound)) != InternalUri.Check.None)
				{
					check &= ~InternalUri.Check.EscapedCanonical;
				}
				if ((check & InternalUri.Check.EscapedCanonical) == InternalUri.Check.None)
				{
					flags |= InternalUri.Flags.E_PathNotCanonical;
				}
				if (this.m_iriParsing && (!flag2 & ((check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.DisplayCanonical | InternalUri.Check.NotIriCanonical | InternalUri.Check.FoundNonAscii)) == (InternalUri.Check.DisplayCanonical | InternalUri.Check.FoundNonAscii))))
				{
					flags |= InternalUri.Flags.PathIriCanonical;
				}
				if (flag)
				{
					ushort num7 = num5;
					if ((int)num5 < this.m_originalUnicodeString.Length && this.m_originalUnicodeString[(int)num5] == '?')
					{
						num5 += 1;
						this.FindEndOfComponent(this.m_originalUnicodeString, ref num5, (ushort)this.m_originalUnicodeString.Length, ((flags2 & UriSyntaxFlags.MayHaveFragment) != UriSyntaxFlags.None) ? '#' : '\ufffe');
						string text3 = this.EscapeUnescapeIri(this.m_originalUnicodeString, (int)num7, (int)num5, UriComponents.Query);
						try
						{
							if (this.ShouldUseLegacyV2Quirks)
							{
								this.m_String += text3.Normalize(NormalizationForm.FormC);
							}
							else
							{
								this.m_String += text3;
							}
						}
						catch (ArgumentException)
						{
							throw InternalUri.GetException(ParsingError.BadFormat);
						}
						if (!InternalServicePointManager.AllowAllUriEncodingExpansion && this.m_String.Length > 65535)
						{
							throw InternalUri.GetException(ParsingError.SizeLimit);
						}
						num2 = (ushort)this.m_String.Length;
					}
				}
				this.m_Info.Offset.Query = num;
				fixed (string text = this.m_String)
				{
					char* ptr3 = text;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					if (num < num2 && ptr3[num] == '?')
					{
						num += 1;
						check = this.CheckCanonical(ptr3, ref num, num2, ((flags2 & UriSyntaxFlags.MayHaveFragment) != UriSyntaxFlags.None) ? '#' : '\ufffe');
						if ((check & InternalUri.Check.DisplayCanonical) == InternalUri.Check.None)
						{
							flags |= InternalUri.Flags.QueryNotCanonical;
						}
						if ((check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.BackslashInPath)) != InternalUri.Check.EscapedCanonical)
						{
							flags |= InternalUri.Flags.E_QueryNotCanonical;
						}
						if (this.m_iriParsing && (check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.DisplayCanonical | InternalUri.Check.BackslashInPath | InternalUri.Check.NotIriCanonical | InternalUri.Check.FoundNonAscii)) == (InternalUri.Check.DisplayCanonical | InternalUri.Check.FoundNonAscii))
						{
							flags |= InternalUri.Flags.QueryIriCanonical;
						}
					}
				}
				if (flag)
				{
					ushort num8 = num5;
					if ((int)num5 < this.m_originalUnicodeString.Length && this.m_originalUnicodeString[(int)num5] == '#')
					{
						num5 += 1;
						this.FindEndOfComponent(this.m_originalUnicodeString, ref num5, (ushort)this.m_originalUnicodeString.Length, '\ufffe');
						string text4 = this.EscapeUnescapeIri(this.m_originalUnicodeString, (int)num8, (int)num5, UriComponents.Fragment);
						try
						{
							if (this.ShouldUseLegacyV2Quirks)
							{
								this.m_String += text4.Normalize(NormalizationForm.FormC);
							}
							else
							{
								this.m_String += text4;
							}
						}
						catch (ArgumentException)
						{
							throw InternalUri.GetException(ParsingError.BadFormat);
						}
						if (!InternalServicePointManager.AllowAllUriEncodingExpansion && this.m_String.Length > 65535)
						{
							throw InternalUri.GetException(ParsingError.SizeLimit);
						}
						num2 = (ushort)this.m_String.Length;
					}
				}
				this.m_Info.Offset.Fragment = num;
				fixed (string text = this.m_String)
				{
					char* ptr4 = text;
					if (ptr4 != null)
					{
						ptr4 += RuntimeHelpers.OffsetToStringData / 2;
					}
					if (num < num2 && ptr4[num] == '#')
					{
						num += 1;
						check = this.CheckCanonical(ptr4, ref num, num2, '\ufffe');
						if ((check & InternalUri.Check.DisplayCanonical) == InternalUri.Check.None)
						{
							flags |= InternalUri.Flags.FragmentNotCanonical;
						}
						if ((check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.BackslashInPath)) != InternalUri.Check.EscapedCanonical)
						{
							flags |= InternalUri.Flags.E_FragmentNotCanonical;
						}
						if (this.m_iriParsing && (check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.DisplayCanonical | InternalUri.Check.BackslashInPath | InternalUri.Check.NotIriCanonical | InternalUri.Check.FoundNonAscii)) == (InternalUri.Check.DisplayCanonical | InternalUri.Check.FoundNonAscii))
						{
							flags |= InternalUri.Flags.FragmentIriCanonical;
						}
					}
				}
				this.m_Info.Offset.End = num;
			}
			flags |= (InternalUri.Flags)int.MinValue;
			InternalUri.UriInfo info = this.m_Info;
			lock (info)
			{
				this.m_Flags |= flags;
			}
			this.m_Flags |= InternalUri.Flags.RestUnicodeNormalized;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0003EA44 File Offset: 0x0003CC44
		private unsafe static ushort ParseSchemeCheckImplicitFile(char* uriString, ushort length, ref ParsingError err, ref InternalUri.Flags flags, ref InternalUriParser syntax)
		{
			ushort num = 0;
			while (num < length && InternalUri.IsLWS(uriString[num]))
			{
				num += 1;
			}
			ushort num2 = num;
			while (num2 < length && uriString[num2] != ':')
			{
				num2 += 1;
			}
			if (IntPtr.Size == 4 && num2 != length && num2 >= num + 2 && InternalUri.CheckKnownSchemes((long*)(uriString + num), num2 - num, ref syntax))
			{
				return num2 + 1;
			}
			if (num + 2 >= length || num2 == num)
			{
				err = ParsingError.BadFormat;
				return 0;
			}
			char c;
			if ((c = uriString[num + 1]) == ':' || c == '|')
			{
				if (!InternalUri.IsAsciiLetter(uriString[num]))
				{
					if (c == ':')
					{
						err = ParsingError.BadScheme;
					}
					else
					{
						err = ParsingError.BadFormat;
					}
					return 0;
				}
				if ((c = uriString[num + 2]) == '\\' || c == '/')
				{
					flags |= InternalUri.Flags.AuthorityFound | InternalUri.Flags.DosPath | InternalUri.Flags.ImplicitFile;
					syntax = InternalUriParser.FileUri;
					return num;
				}
				err = ParsingError.MustRootedPath;
				return 0;
			}
			else if ((c = uriString[num]) == '/' || c == '\\')
			{
				if ((c = uriString[num + 1]) == '\\' || c == '/')
				{
					flags |= InternalUri.Flags.AuthorityFound | InternalUri.Flags.UncPath | InternalUri.Flags.ImplicitFile;
					syntax = InternalUriParser.FileUri;
					num += 2;
					while (num < length && ((c = uriString[num]) == '/' || c == '\\'))
					{
						num += 1;
					}
					return num;
				}
				err = ParsingError.BadFormat;
				return 0;
			}
			else
			{
				if (num2 == length)
				{
					err = ParsingError.BadFormat;
					return 0;
				}
				if (num2 - num > 1024)
				{
					err = ParsingError.SchemeLimit;
					return 0;
				}
				char* ptr;
				checked
				{
					ptr = stackalloc char[unchecked((UIntPtr)(num2 - num)) * 2];
					length = 0;
				}
				while (num < num2)
				{
					ref short ptr2 = ref *(short*)ptr;
					ushort num3 = length;
					length = num3 + 1;
					*((ref ptr2) + (IntPtr)num3 * 2) = (short)uriString[num];
					num += 1;
				}
				err = InternalUri.CheckSchemeSyntax(ptr, length, ref syntax);
				if (err != ParsingError.None)
				{
					return 0;
				}
				return num2 + 1;
			}
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0003EBD8 File Offset: 0x0003CDD8
		private unsafe static bool CheckKnownSchemes(long* lptr, ushort nChars, ref InternalUriParser syntax)
		{
			if (nChars != 2)
			{
				long num = *lptr | 9007336695791648L;
				if (num <= 29273878621519975L)
				{
					if (num <= 16326042577993847L)
					{
						if (num != 12948347151515758L)
						{
							if (num != 16326029693157478L)
							{
								if (num == 16326042577993847L)
								{
									if (nChars == 3)
									{
										syntax = InternalUriParser.WssUri;
										return true;
									}
								}
							}
							else if (nChars == 3)
							{
								syntax = InternalUriParser.FtpUri;
								return true;
							}
						}
						else
						{
							if (nChars == 8 && (lptr[1] | 9007336695791648L) == 28429453690994800L)
							{
								syntax = InternalUriParser.NetPipeUri;
								return true;
							}
							if (nChars == 7 && (lptr[1] | 9007336695791648L) == 16326029692043380L)
							{
								syntax = InternalUriParser.NetTcpUri;
								return true;
							}
						}
					}
					else if (num != 28147948650299509L)
					{
						if (num != 28429436511125606L)
						{
							if (num == 29273878621519975L)
							{
								if (nChars == 6 && (*(int*)(lptr + 1) | 2097184) == 7471205)
								{
									syntax = InternalUriParser.GopherUri;
									return true;
								}
							}
						}
						else if (nChars == 4)
						{
							syntax = InternalUriParser.FileUri;
							return true;
						}
					}
					else if (nChars == 4)
					{
						syntax = InternalUriParser.UuidUri;
						return true;
					}
				}
				else if (num <= 31525614009974892L)
				{
					if (num != 30399748462674029L)
					{
						if (num != 30962711301259380L)
						{
							if (num == 31525614009974892L)
							{
								if (nChars == 4)
								{
									syntax = InternalUriParser.LdapUri;
									return true;
								}
							}
						}
						else if (nChars == 6 && (*(int*)(lptr + 1) | 2097184) == 7602277)
						{
							syntax = InternalUriParser.TelnetUri;
							return true;
						}
					}
					else if (nChars == 6 && (*(int*)(lptr + 1) | 2097184) == 7274612)
					{
						syntax = InternalUriParser.MailToUri;
						return true;
					}
				}
				else if (num != 31525695615008878L)
				{
					if (num != 31525695615402088L)
					{
						if (num == 32370133429452910L)
						{
							if (nChars == 4)
							{
								syntax = InternalUriParser.NewsUri;
								return true;
							}
						}
					}
					else
					{
						if (nChars == 4)
						{
							syntax = InternalUriParser.HttpUri;
							return true;
						}
						if (nChars == 5 && (*(ushort*)(lptr + 1) | 32) == 115)
						{
							syntax = InternalUriParser.HttpsUri;
							return true;
						}
					}
				}
				else if (nChars == 4)
				{
					syntax = InternalUriParser.NntpUri;
					return true;
				}
				return false;
			}
			if (((int)(*lptr) | 2097184) == 7536759)
			{
				syntax = InternalUriParser.WsUri;
				return true;
			}
			return false;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0003EE44 File Offset: 0x0003D044
		private unsafe static ParsingError CheckSchemeSyntax(char* ptr, ushort length, ref InternalUriParser syntax)
		{
			char c = *ptr;
			if (c < 'a' || c > 'z')
			{
				if (c < 'A' || c > 'Z')
				{
					return ParsingError.BadScheme;
				}
				*ptr = c | ' ';
			}
			for (ushort num = 1; num < length; num += 1)
			{
				char c2 = ptr[num];
				if (c2 < 'a' || c2 > 'z')
				{
					if (c2 >= 'A' && c2 <= 'Z')
					{
						ptr[num] = c2 | ' ';
					}
					else if ((c2 < '0' || c2 > '9') && c2 != '+' && c2 != '-' && c2 != '.')
					{
						return ParsingError.BadScheme;
					}
				}
			}
			string text = new string(ptr, 0, (int)length);
			syntax = InternalUriParser.FindOrFetchAsUnknownV1Syntax(text);
			return ParsingError.None;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0003EED8 File Offset: 0x0003D0D8
		private unsafe ushort CheckAuthorityHelper(char* pString, ushort idx, ushort length, ref ParsingError err, ref InternalUri.Flags flags, InternalUriParser syntax, ref string newHost)
		{
			int num = (int)length;
			int num2 = (int)idx;
			ushort num3 = idx;
			newHost = null;
			bool flag = false;
			bool flag2 = InternalUri.IriParsingStatic(syntax);
			bool flag3 = (flags & InternalUri.Flags.HasUnicode) > InternalUri.Flags.Zero;
			bool flag4 = (flags & InternalUri.Flags.HostUnicodeNormalized) == InternalUri.Flags.Zero;
			UriSyntaxFlags flags2 = syntax.Flags;
			if (flag3 && flag2 && flag4)
			{
				newHost = this.m_originalUnicodeString.Substring(0, num2);
			}
			char c;
			if (idx == length || ((c = pString[idx]) == '/' || (c == '\\' && InternalUri.StaticIsFile(syntax))) || c == '#' || c == '?')
			{
				if (syntax.InFact(UriSyntaxFlags.AllowEmptyHost))
				{
					flags &= ~InternalUri.Flags.UncPath;
					if (InternalUri.StaticInFact(flags, InternalUri.Flags.ImplicitFile))
					{
						err = ParsingError.BadHostName;
					}
					else
					{
						flags |= InternalUri.Flags.BasicHostType;
					}
				}
				else
				{
					err = ParsingError.BadHostName;
				}
				if (flag3 && flag2 && flag4)
				{
					flags |= InternalUri.Flags.HostUnicodeNormalized;
				}
				return idx;
			}
			string text = null;
			if ((flags2 & UriSyntaxFlags.MayHaveUserInfo) != UriSyntaxFlags.None)
			{
				while ((int)num3 < num)
				{
					if ((int)num3 == num - 1 || pString[num3] == '?' || pString[num3] == '#' || pString[num3] == '\\' || pString[num3] == '/')
					{
						num3 = idx;
						break;
					}
					if (pString[num3] == '@')
					{
						flags |= InternalUri.Flags.HasUserInfo;
						if (flag2 || InternalUri.s_IdnScope != UriIdnScope.None)
						{
							if (flag2 && flag3 && flag4)
							{
								text = IriHelper.EscapeUnescapeIri(pString, num2, (int)(num3 + 1), UriComponents.UserInfo);
								try
								{
									if (syntax.ShouldUseLegacyV2Quirks)
									{
										text = text.Normalize(NormalizationForm.FormC);
									}
								}
								catch (ArgumentException)
								{
									err = ParsingError.BadFormat;
									return idx;
								}
								newHost += text;
								if (!InternalServicePointManager.AllowAllUriEncodingExpansion && newHost.Length > 65535)
								{
									err = ParsingError.SizeLimit;
									return idx;
								}
							}
							else
							{
								text = new string(pString, num2, (int)num3 - num2 + 1);
							}
						}
						num3 += 1;
						c = pString[num3];
						break;
					}
					num3 += 1;
				}
			}
			bool flag5 = (flags2 & UriSyntaxFlags.SimpleUserSyntax) == UriSyntaxFlags.None;
			if (c == '[' && syntax.InFact(UriSyntaxFlags.AllowIPv6Host) && IPv6AddressHelper.IsValid(pString, (int)(num3 + 1), ref num))
			{
				flags |= InternalUri.Flags.IPv6HostType;
				if (!InternalUri.s_ConfigInitialized)
				{
					InternalUri.InitializeUriConfig();
					this.m_iriParsing = InternalUri.IriParsingStatic(syntax);
				}
				if (flag3 && flag2 && flag4)
				{
					newHost += new string(pString, (int)num3, num - (int)num3);
					flags |= InternalUri.Flags.HostUnicodeNormalized;
					flag = true;
				}
			}
			else if (c <= '9' && c >= '0' && syntax.InFact(UriSyntaxFlags.AllowIPv4Host) && IPv4AddressHelper.IsValid(pString, (int)num3, ref num, false, InternalUri.StaticNotAny(flags, InternalUri.Flags.ImplicitFile), syntax.InFact(UriSyntaxFlags.V1_UnknownUri)))
			{
				flags |= InternalUri.Flags.IPv4HostType;
				if (flag3 && flag2 && flag4)
				{
					newHost += new string(pString, (int)num3, num - (int)num3);
					flags |= InternalUri.Flags.HostUnicodeNormalized;
					flag = true;
				}
			}
			else if ((flags2 & UriSyntaxFlags.AllowDnsHost) != UriSyntaxFlags.None && !flag2 && DomainNameHelper.IsValid(pString, num3, ref num, ref flag5, InternalUri.StaticNotAny(flags, InternalUri.Flags.ImplicitFile)))
			{
				flags |= InternalUri.Flags.DnsHostType;
				if (!flag5)
				{
					flags |= InternalUri.Flags.CanonicalDnsHost;
				}
				if (InternalUri.s_IdnScope != UriIdnScope.None)
				{
					if (InternalUri.s_IdnScope == UriIdnScope.AllExceptIntranet && this.IsIntranet(new string(pString, 0, num)))
					{
						flags |= InternalUri.Flags.IntranetUri;
					}
					if (this.AllowIdnStatic(syntax, flags))
					{
						bool flag6 = true;
						bool flag7 = false;
						string text2 = DomainNameHelper.UnicodeEquivalent(pString, (int)num3, num, ref flag6, ref flag7);
						if (flag7)
						{
							if (InternalUri.StaticNotAny(flags, InternalUri.Flags.HasUnicode))
							{
								this.m_originalUnicodeString = this.m_String;
							}
							flags |= InternalUri.Flags.IdnHost;
							newHost = this.m_originalUnicodeString.Substring(0, num2) + text + text2;
							flags |= InternalUri.Flags.CanonicalDnsHost;
							this.m_DnsSafeHost = new string(pString, (int)num3, num - (int)num3);
							flag = true;
						}
						flags |= InternalUri.Flags.HostUnicodeNormalized;
					}
				}
			}
			else if ((flags2 & UriSyntaxFlags.AllowDnsHost) != UriSyntaxFlags.None && ((syntax.InFact(UriSyntaxFlags.AllowIriParsing) && flag4) || syntax.InFact(UriSyntaxFlags.AllowIdn)) && DomainNameHelper.IsValidByIri(pString, num3, ref num, ref flag5, InternalUri.StaticNotAny(flags, InternalUri.Flags.ImplicitFile)))
			{
				this.CheckAuthorityHelperHandleDnsIri(pString, num3, num, num2, flag2, flag3, syntax, text, ref flags, ref flag, ref newHost, ref err);
			}
			else if ((flags2 & UriSyntaxFlags.AllowUncHost) != UriSyntaxFlags.None && UncNameHelper.IsValid(pString, num3, ref num, InternalUri.StaticNotAny(flags, InternalUri.Flags.ImplicitFile)) && num - (int)num3 <= 256)
			{
				flags |= InternalUri.Flags.UncHostType;
				if (flag3 && flag2 && flag4)
				{
					newHost += new string(pString, (int)num3, num - (int)num3);
					flags |= InternalUri.Flags.HostUnicodeNormalized;
					flag = true;
				}
			}
			if (num < (int)length && pString[num] == '\\' && (flags & InternalUri.Flags.HostTypeMask) != InternalUri.Flags.Zero && !InternalUri.StaticIsFile(syntax))
			{
				if (syntax.InFact(UriSyntaxFlags.V1_UnknownUri))
				{
					err = ParsingError.BadHostName;
					flags |= InternalUri.Flags.HostTypeMask;
					return (ushort)num;
				}
				flags &= ~InternalUri.Flags.HostTypeMask;
			}
			else if (num < (int)length && pString[num] == ':')
			{
				if (syntax.InFact(UriSyntaxFlags.MayHavePort))
				{
					int num4 = 0;
					int num5 = num;
					idx = (ushort)(num + 1);
					while (idx < length)
					{
						ushort num6 = (ushort)(pString[idx] - '0');
						if (num6 >= 0 && num6 <= 9)
						{
							if ((num4 = num4 * 10 + (int)num6) > 65535)
							{
								break;
							}
							idx += 1;
						}
						else
						{
							if (num6 == 65535 || num6 == 15 || num6 == 65523)
							{
								break;
							}
							if (syntax.InFact(UriSyntaxFlags.AllowAnyOtherHost) && syntax.NotAny(UriSyntaxFlags.V1_UnknownUri))
							{
								flags &= ~InternalUri.Flags.HostTypeMask;
								break;
							}
							err = ParsingError.BadPort;
							return idx;
						}
					}
					if (num4 > 65535)
					{
						if (!syntax.InFact(UriSyntaxFlags.AllowAnyOtherHost))
						{
							err = ParsingError.BadPort;
							return idx;
						}
						flags &= ~InternalUri.Flags.HostTypeMask;
					}
					if (flag2 && flag3 && flag)
					{
						newHost += new string(pString, num5, (int)idx - num5);
					}
				}
				else
				{
					flags &= ~InternalUri.Flags.HostTypeMask;
				}
			}
			if ((flags & InternalUri.Flags.HostTypeMask) == InternalUri.Flags.Zero)
			{
				flags &= ~InternalUri.Flags.HasUserInfo;
				if (syntax.InFact(UriSyntaxFlags.AllowAnyOtherHost))
				{
					flags |= InternalUri.Flags.BasicHostType;
					num = (int)idx;
					while (num < (int)length && pString[num] != '/' && pString[num] != '?' && pString[num] != '#')
					{
						num++;
					}
					this.CheckAuthorityHelperHandleAnyHostIri(pString, num2, num, flag2, flag3, syntax, ref flags, ref newHost, ref err);
				}
				else if (syntax.InFact(UriSyntaxFlags.V1_UnknownUri))
				{
					bool flag8 = false;
					int num7 = (int)idx;
					num = (int)idx;
					while (num < (int)length && (!flag8 || (pString[num] != '/' && pString[num] != '?' && pString[num] != '#')))
					{
						if (num >= (int)(idx + 2) || pString[num] != '.')
						{
							err = ParsingError.BadHostName;
							flags |= InternalUri.Flags.HostTypeMask;
							return idx;
						}
						flag8 = true;
						num++;
					}
					flags |= InternalUri.Flags.BasicHostType;
					if (flag2 && flag3 && InternalUri.StaticNotAny(flags, InternalUri.Flags.HostUnicodeNormalized))
					{
						string text3 = new string(pString, num7, num - num7);
						try
						{
							newHost += text3.Normalize(NormalizationForm.FormC);
						}
						catch (ArgumentException)
						{
							err = ParsingError.BadFormat;
							return idx;
						}
						flags |= InternalUri.Flags.HostUnicodeNormalized;
					}
				}
				else if (syntax.InFact(UriSyntaxFlags.MustHaveAuthority) || (syntax.InFact(UriSyntaxFlags.MailToLikeUri) && !syntax.ShouldUseLegacyV2Quirks))
				{
					err = ParsingError.BadHostName;
					flags |= InternalUri.Flags.HostTypeMask;
					return idx;
				}
			}
			return (ushort)num;
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0003F6EC File Offset: 0x0003D8EC
		private unsafe void CheckAuthorityHelperHandleDnsIri(char* pString, ushort start, int end, int startInput, bool iriParsing, bool hasUnicode, InternalUriParser syntax, string userInfoString, ref InternalUri.Flags flags, ref bool justNormalized, ref string newHost, ref ParsingError err)
		{
			flags |= InternalUri.Flags.DnsHostType;
			if (InternalUri.s_IdnScope == UriIdnScope.AllExceptIntranet && this.IsIntranet(new string(pString, 0, end)))
			{
				flags |= InternalUri.Flags.IntranetUri;
			}
			if (this.AllowIdnStatic(syntax, flags))
			{
				bool flag = true;
				bool flag2 = false;
				string text = DomainNameHelper.IdnEquivalent(pString, (int)start, end, ref flag, ref flag2);
				string text2 = DomainNameHelper.UnicodeEquivalent(text, pString, (int)start, end);
				if (!flag)
				{
					flags |= InternalUri.Flags.UnicodeHost;
				}
				if (flag2)
				{
					flags |= InternalUri.Flags.IdnHost;
				}
				if (flag && flag2 && InternalUri.StaticNotAny(flags, InternalUri.Flags.HasUnicode))
				{
					this.m_originalUnicodeString = this.m_String;
					newHost = this.m_originalUnicodeString.Substring(0, startInput) + (InternalUri.StaticInFact(flags, InternalUri.Flags.HasUserInfo) ? userInfoString : null);
					justNormalized = true;
				}
				else if (!iriParsing && (InternalUri.StaticInFact(flags, InternalUri.Flags.UnicodeHost) || InternalUri.StaticInFact(flags, InternalUri.Flags.IdnHost)))
				{
					this.m_originalUnicodeString = this.m_String;
					newHost = this.m_originalUnicodeString.Substring(0, startInput) + (InternalUri.StaticInFact(flags, InternalUri.Flags.HasUserInfo) ? userInfoString : null);
					justNormalized = true;
				}
				if (!flag || flag2)
				{
					this.m_DnsSafeHost = text;
					newHost += text2;
					justNormalized = true;
				}
				else if (flag && !flag2 && iriParsing && hasUnicode)
				{
					newHost += text2;
					justNormalized = true;
				}
			}
			else if (hasUnicode)
			{
				string text3 = InternalUri.StripBidiControlCharacter(pString, (int)start, end - (int)start);
				try
				{
					newHost += ((text3 != null) ? text3.Normalize(NormalizationForm.FormC) : null);
				}
				catch (ArgumentException)
				{
					err = ParsingError.BadHostName;
				}
				justNormalized = true;
			}
			flags |= InternalUri.Flags.HostUnicodeNormalized;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0003F8D8 File Offset: 0x0003DAD8
		private unsafe void CheckAuthorityHelperHandleAnyHostIri(char* pString, int startInput, int end, bool iriParsing, bool hasUnicode, InternalUriParser syntax, ref InternalUri.Flags flags, ref string newHost, ref ParsingError err)
		{
			if (InternalUri.StaticNotAny(flags, InternalUri.Flags.HostUnicodeNormalized) && (this.AllowIdnStatic(syntax, flags) || (iriParsing && hasUnicode)))
			{
				string text = new string(pString, startInput, end - startInput);
				if (this.AllowIdnStatic(syntax, flags))
				{
					bool flag = true;
					bool flag2 = false;
					string text2 = DomainNameHelper.UnicodeEquivalent(pString, startInput, end, ref flag, ref flag2);
					if (((flag && flag2) || !flag) && (!iriParsing || !hasUnicode))
					{
						this.m_originalUnicodeString = this.m_String;
						newHost = this.m_originalUnicodeString.Substring(0, startInput);
						flags |= InternalUri.Flags.HasUnicode;
					}
					if (flag2 || !flag)
					{
						newHost += text2;
						string text3 = null;
						this.m_DnsSafeHost = DomainNameHelper.IdnEquivalent(pString, startInput, end, ref flag, ref text3);
						if (flag2)
						{
							flags |= InternalUri.Flags.IdnHost;
						}
						if (!flag)
						{
							flags |= InternalUri.Flags.UnicodeHost;
						}
					}
					else if (iriParsing && hasUnicode)
					{
						newHost += text;
					}
				}
				else
				{
					try
					{
						newHost += text.Normalize(NormalizationForm.FormC);
					}
					catch (ArgumentException)
					{
						err = ParsingError.BadHostName;
					}
				}
				flags |= InternalUri.Flags.HostUnicodeNormalized;
			}
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0003FA1C File Offset: 0x0003DC1C
		private unsafe void FindEndOfComponent(string input, ref ushort idx, ushort end, char delim)
		{
			fixed (string text = input)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.FindEndOfComponent(ptr, ref idx, end, delim);
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0003FA48 File Offset: 0x0003DC48
		private unsafe void FindEndOfComponent(char* str, ref ushort idx, ushort end, char delim)
		{
			ushort num;
			for (num = idx; num < end; num += 1)
			{
				char c = str[num];
				if (c == delim || (delim == '?' && c == '#' && this.m_Syntax != null && this.m_Syntax.InFact(UriSyntaxFlags.MayHaveFragment)))
				{
					break;
				}
			}
			idx = num;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0003FA9C File Offset: 0x0003DC9C
		private unsafe InternalUri.Check CheckCanonical(char* str, ref ushort idx, ushort end, char delim)
		{
			InternalUri.Check check = InternalUri.Check.None;
			bool flag = false;
			bool flag2 = false;
			ushort num;
			for (num = idx; num < end; num += 1)
			{
				char c = str[num];
				if (c <= '\u001f' || (c >= '\u007f' && c <= '\u009f'))
				{
					flag = true;
					flag2 = true;
					check |= InternalUri.Check.ReservedFound;
				}
				else if (c > 'z' && c != '~')
				{
					if (this.m_iriParsing)
					{
						bool flag3 = false;
						check |= InternalUri.Check.FoundNonAscii;
						if (char.IsHighSurrogate(c))
						{
							if (num + 1 < end)
							{
								bool flag4 = false;
								flag3 = IriHelper.CheckIriUnicodeRange(c, str[num + 1], ref flag4, true);
							}
						}
						else
						{
							flag3 = IriHelper.CheckIriUnicodeRange(c, true);
						}
						if (!flag3)
						{
							check |= InternalUri.Check.NotIriCanonical;
						}
					}
					if (!flag)
					{
						flag = true;
					}
				}
				else
				{
					if (c == delim || (delim == '?' && c == '#' && this.m_Syntax != null && this.m_Syntax.InFact(UriSyntaxFlags.MayHaveFragment)))
					{
						break;
					}
					if (c == '?')
					{
						if (this.IsImplicitFile || (this.m_Syntax != null && !this.m_Syntax.InFact(UriSyntaxFlags.MayHaveQuery) && delim != '\ufffe'))
						{
							check |= InternalUri.Check.ReservedFound;
							flag2 = true;
							flag = true;
						}
					}
					else if (c == '#')
					{
						flag = true;
						if (this.IsImplicitFile || (this.m_Syntax != null && !this.m_Syntax.InFact(UriSyntaxFlags.MayHaveFragment)))
						{
							check |= InternalUri.Check.ReservedFound;
							flag2 = true;
						}
					}
					else if (c == '/' || c == '\\')
					{
						if ((check & InternalUri.Check.BackslashInPath) == InternalUri.Check.None && c == '\\')
						{
							check |= InternalUri.Check.BackslashInPath;
						}
						if ((check & InternalUri.Check.DotSlashAttn) == InternalUri.Check.None && num + 1 != end && (str[num + 1] == '/' || str[num + 1] == '\\'))
						{
							check |= InternalUri.Check.DotSlashAttn;
						}
					}
					else if (c == '.')
					{
						if (((check & InternalUri.Check.DotSlashAttn) == InternalUri.Check.None && num + 1 == end) || str[num + 1] == '.' || str[num + 1] == '/' || str[num + 1] == '\\' || str[num + 1] == '?' || str[num + 1] == '#')
						{
							check |= InternalUri.Check.DotSlashAttn;
						}
					}
					else if (!flag && ((c <= '"' && c != '!') || (c >= '[' && c <= '^') || c == '>' || c == '<' || c == '`'))
					{
						flag = true;
					}
					else if (c == '%')
					{
						if (!flag2)
						{
							flag2 = true;
						}
						if (num + 2 < end && (c = InternalUriHelper.EscapedAscii(str[num + 1], str[num + 2])) != '\uffff')
						{
							if (c == '.' || c == '/' || c == '\\')
							{
								check |= InternalUri.Check.DotSlashEscaped;
							}
							num += 2;
						}
						else if (!flag)
						{
							flag = true;
						}
					}
				}
			}
			if (flag2)
			{
				if (!flag)
				{
					check |= InternalUri.Check.EscapedCanonical;
				}
			}
			else
			{
				check |= InternalUri.Check.DisplayCanonical;
				if (!flag)
				{
					check |= InternalUri.Check.EscapedCanonical;
				}
			}
			idx = num;
			return check;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0003FD58 File Offset: 0x0003DF58
		private unsafe char[] GetCanonicalPath(char[] dest, ref int pos, UriFormat formatAs)
		{
			if (this.InFact(InternalUri.Flags.FirstSlashAbsent))
			{
				char[] array = dest;
				int num = pos;
				pos = num + 1;
				array[num] = 47;
			}
			if (this.m_Info.Offset.Path == this.m_Info.Offset.Query)
			{
				return dest;
			}
			int num2 = pos;
			int securedPathIndex = (int)this.SecuredPathIndex;
			if (formatAs == UriFormat.UriEscaped)
			{
				if (this.InFact(InternalUri.Flags.ShouldBeCompressed))
				{
					this.m_String.CopyTo((int)this.m_Info.Offset.Path, dest, num2, (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path));
					num2 += (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path);
					if (this.m_Syntax.InFact(UriSyntaxFlags.UnEscapeDotsAndSlashes) && this.InFact(InternalUri.Flags.PathNotCanonical) && !this.IsImplicitFile)
					{
						char[] array2;
						char* ptr;
						if ((array2 = dest) == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						InternalUri.UnescapeOnly(ptr, pos, ref num2, '.', '/', this.m_Syntax.InFact(UriSyntaxFlags.ConvertPathSlashes) ? '\\' : char.MaxValue);
						array2 = null;
					}
				}
				else if (this.InFact(InternalUri.Flags.E_PathNotCanonical) && this.NotAny(InternalUri.Flags.UserEscaped))
				{
					string text = this.m_String;
					if (securedPathIndex != 0 && text[securedPathIndex + (int)this.m_Info.Offset.Path - 1] == '|')
					{
						text = text.Remove(securedPathIndex + (int)this.m_Info.Offset.Path - 1, 1);
						text = text.Insert(securedPathIndex + (int)this.m_Info.Offset.Path - 1, ":");
					}
					dest = InternalUriHelper.EscapeString(text, (int)this.m_Info.Offset.Path, (int)this.m_Info.Offset.Query, dest, ref num2, true, '?', '#', this.IsImplicitFile ? char.MaxValue : '%', this.ShouldUseLegacyV2Quirks);
				}
				else
				{
					this.m_String.CopyTo((int)this.m_Info.Offset.Path, dest, num2, (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path));
					num2 += (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path);
				}
			}
			else
			{
				this.m_String.CopyTo((int)this.m_Info.Offset.Path, dest, num2, (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path));
				num2 += (int)(this.m_Info.Offset.Query - this.m_Info.Offset.Path);
				if (this.InFact(InternalUri.Flags.ShouldBeCompressed) && this.m_Syntax.InFact(UriSyntaxFlags.UnEscapeDotsAndSlashes) && this.InFact(InternalUri.Flags.PathNotCanonical) && !this.IsImplicitFile)
				{
					char[] array2;
					char* ptr2;
					if ((array2 = dest) == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					InternalUri.UnescapeOnly(ptr2, pos, ref num2, '.', '/', this.m_Syntax.InFact(UriSyntaxFlags.ConvertPathSlashes) ? '\\' : char.MaxValue);
					array2 = null;
				}
			}
			if (securedPathIndex != 0 && dest[securedPathIndex + pos - 1] == '|')
			{
				dest[securedPathIndex + pos - 1] = ':';
			}
			if (this.InFact(InternalUri.Flags.ShouldBeCompressed))
			{
				dest = InternalUri.Compress(dest, (ushort)(pos + securedPathIndex), ref num2, this.m_Syntax);
				if (dest[pos] == '\\')
				{
					dest[pos] = '/';
				}
				if (formatAs == UriFormat.UriEscaped && this.NotAny(InternalUri.Flags.UserEscaped) && this.InFact(InternalUri.Flags.E_PathNotCanonical))
				{
					dest = InternalUriHelper.EscapeString(new string(dest, pos, num2 - pos), 0, num2 - pos, dest, ref pos, true, '?', '#', this.IsImplicitFile ? char.MaxValue : '%', this.ShouldUseLegacyV2Quirks);
					num2 = pos;
				}
			}
			else if (this.m_Syntax.InFact(UriSyntaxFlags.ConvertPathSlashes) && this.InFact(InternalUri.Flags.BackslashInPath))
			{
				for (int i = pos; i < num2; i++)
				{
					if (dest[i] == '\\')
					{
						dest[i] = '/';
					}
				}
			}
			if (formatAs != UriFormat.UriEscaped && this.InFact(InternalUri.Flags.PathNotCanonical))
			{
				UnescapeMode unescapeMode;
				if (this.InFact(InternalUri.Flags.PathNotCanonical))
				{
					if (formatAs != UriFormat.Unescaped)
					{
						if (formatAs == (UriFormat)32767)
						{
							unescapeMode = (this.InFact(InternalUri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape) | UnescapeMode.V1ToStringFlag;
							if (this.IsImplicitFile)
							{
								unescapeMode &= ~UnescapeMode.Unescape;
							}
						}
						else
						{
							unescapeMode = (this.InFact(InternalUri.Flags.UserEscaped) ? UnescapeMode.Unescape : UnescapeMode.EscapeUnescape);
							if (this.IsImplicitFile)
							{
								unescapeMode &= ~UnescapeMode.Unescape;
							}
						}
					}
					else
					{
						unescapeMode = (this.IsImplicitFile ? UnescapeMode.CopyOnly : (UnescapeMode.Unescape | UnescapeMode.UnescapeAll));
					}
				}
				else
				{
					unescapeMode = UnescapeMode.CopyOnly;
				}
				char[] array3 = new char[dest.Length];
				Buffer.BlockCopy(dest, 0, array3, 0, num2 << 1);
				char[] array2;
				char* ptr3;
				if ((array2 = array3) == null || array2.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &array2[0];
				}
				dest = InternalUriHelper.UnescapeString(ptr3, pos, num2, dest, ref pos, '?', '#', char.MaxValue, unescapeMode, this.m_Syntax, false);
				array2 = null;
			}
			else
			{
				pos = num2;
			}
			return dest;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00040298 File Offset: 0x0003E498
		private unsafe static void UnescapeOnly(char* pch, int start, ref int end, char ch1, char ch2, char ch3)
		{
			if (end - start < 3)
			{
				return;
			}
			char* ptr = pch + end - 2;
			pch += start;
			char* ptr2 = null;
			while (pch < ptr)
			{
				if (*(pch++) == '%')
				{
					char c = InternalUriHelper.EscapedAscii(*(pch++), *(pch++));
					if (c == ch1 || c == ch2 || c == ch3)
					{
						ptr2 = pch - 2;
						*(ptr2 - 1) = c;
						while (pch < ptr)
						{
							if ((*(ptr2++) = *(pch++)) == '%')
							{
								c = InternalUriHelper.EscapedAscii(*(ptr2++) = *(pch++), *(ptr2++) = *(pch++));
								if (c == ch1 || c == ch2 || c == ch3)
								{
									ptr2 -= 2;
									*(ptr2 - 1) = c;
								}
							}
						}
						break;
					}
				}
			}
			ptr += 2;
			if (ptr2 == null)
			{
				return;
			}
			if (pch == ptr)
			{
				end -= (int)((long)(pch - ptr2));
				return;
			}
			*(ptr2++) = *(pch++);
			if (pch == ptr)
			{
				end -= (int)((long)(pch - ptr2));
				return;
			}
			*(ptr2++) = *(pch++);
			end -= (int)((long)(pch - ptr2));
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000403B4 File Offset: 0x0003E5B4
		private static char[] Compress(char[] dest, ushort start, ref int destLength, InternalUriParser syntax)
		{
			ushort num = 0;
			ushort num2 = 0;
			ushort num3 = 0;
			ushort num4 = 0;
			ushort num5 = (ushort)destLength - 1;
			start -= 1;
			while (num5 != start)
			{
				char c = dest[(int)num5];
				if (c == '\\' && syntax.InFact(UriSyntaxFlags.ConvertPathSlashes))
				{
					c = (dest[(int)num5] = '/');
				}
				if (c == '/')
				{
					num += 1;
				}
				else
				{
					if (num > 1)
					{
						num2 = num5 + 1;
					}
					num = 0;
				}
				if (c == '.')
				{
					num3 += 1;
				}
				else
				{
					if (num3 != 0)
					{
						bool flag = syntax.NotAny(UriSyntaxFlags.CanonicalizeAsFilePath) && (num3 > 2 || c != '/' || num5 == start);
						if (!flag && c == '/')
						{
							if ((num2 == num5 + num3 + 1 || (num2 == 0 && (int)(num5 + num3 + 1) == destLength)) && (syntax.ShouldUseLegacyV2Quirks || num3 <= 2))
							{
								num2 = num5 + 1 + num3 + ((num2 > 0) ? 1 : 0);
								Buffer.BlockCopy(dest, (int)num2 << 1, dest, (int)(num5 + 1) << 1, destLength - (int)num2 << 1);
								destLength -= (int)(num2 - num5 - 1);
								num2 = num5;
								if (num3 == 2)
								{
									num4 += 1;
								}
								num3 = 0;
								goto IL_0193;
							}
						}
						else if (syntax.ShouldUseLegacyV2Quirks && !flag && num4 == 0 && (num2 == num5 + num3 + 1 || (num2 == 0 && (int)(num5 + num3 + 1) == destLength)))
						{
							num3 = num5 + 1 + num3;
							Buffer.BlockCopy(dest, (int)num3 << 1, dest, (int)(num5 + 1) << 1, destLength - (int)num3 << 1);
							destLength -= (int)(num3 - num5 - 1);
							num2 = 0;
							num3 = 0;
							goto IL_0193;
						}
						num3 = 0;
					}
					if (c == '/')
					{
						if (num4 != 0)
						{
							num4 -= 1;
							num2 += 1;
							Buffer.BlockCopy(dest, (int)num2 << 1, dest, (int)(num5 + 1) << 1, destLength - (int)num2 << 1);
							destLength -= (int)(num2 - num5 - 1);
						}
						num2 = num5;
					}
				}
				IL_0193:
				num5 -= 1;
			}
			start += 1;
			if ((ushort)destLength > start && syntax.InFact(UriSyntaxFlags.CanonicalizeAsFilePath) && num <= 1)
			{
				if (num4 != 0 && dest[(int)start] != '/')
				{
					num2 += 1;
					Buffer.BlockCopy(dest, (int)num2 << 1, dest, (int)start << 1, destLength - (int)num2 << 1);
					destLength -= (int)num2;
				}
				else if (num3 != 0 && (num2 == num3 + 1 || (num2 == 0 && (int)(num3 + 1) == destLength)))
				{
					num3 += ((num2 > 0) ? 1 : 0);
					Buffer.BlockCopy(dest, (int)num3 << 1, dest, (int)start << 1, destLength - (int)num3 << 1);
					destLength -= (int)num3;
				}
			}
			return dest;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000405DF File Offset: 0x0003E7DF
		internal static int CalculateCaseInsensitiveHashCode(string text)
		{
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(text);
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000405EC File Offset: 0x0003E7EC
		private static string CombineUri(InternalUri basePart, string relativePart, UriFormat uriFormat)
		{
			char c = relativePart[0];
			if (basePart.IsDosPath && (c == '/' || c == '\\') && (relativePart.Length == 1 || (relativePart[1] != '/' && relativePart[1] != '\\')))
			{
				int num = basePart.OriginalString.IndexOf(':');
				if (basePart.IsImplicitFile)
				{
					return basePart.OriginalString.Substring(0, num + 1) + relativePart;
				}
				num = basePart.OriginalString.IndexOf(':', num + 1);
				return basePart.OriginalString.Substring(0, num + 1) + relativePart;
			}
			else if (InternalUri.StaticIsFile(basePart.Syntax) && (c == '\\' || c == '/'))
			{
				if (relativePart.Length >= 2 && (relativePart[1] == '\\' || relativePart[1] == '/'))
				{
					if (!basePart.IsImplicitFile)
					{
						return "file:" + relativePart;
					}
					return relativePart;
				}
				else
				{
					if (!basePart.IsUnc)
					{
						return "file://" + relativePart;
					}
					string text = basePart.GetParts(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.Unescaped);
					for (int i = 1; i < text.Length; i++)
					{
						if (text[i] == '/')
						{
							text = text.Substring(0, i);
							break;
						}
					}
					if (basePart.IsImplicitFile)
					{
						return "\\\\" + basePart.GetParts(UriComponents.Host, UriFormat.Unescaped) + text + relativePart;
					}
					return "file://" + basePart.GetParts(UriComponents.Host, uriFormat) + text + relativePart;
				}
			}
			else
			{
				bool flag = basePart.Syntax.InFact(UriSyntaxFlags.ConvertPathSlashes);
				string text2;
				if (c != '/' && (c != '\\' || !flag))
				{
					text2 = basePart.GetParts(UriComponents.Path | UriComponents.KeepDelimiter, basePart.IsImplicitFile ? UriFormat.Unescaped : uriFormat);
					int j = text2.Length;
					char[] array = new char[j + relativePart.Length];
					if (j > 0)
					{
						text2.CopyTo(0, array, 0, j);
						while (j > 0)
						{
							if (array[--j] == '/')
							{
								j++;
								break;
							}
						}
					}
					relativePart.CopyTo(0, array, j, relativePart.Length);
					c = (basePart.Syntax.InFact(UriSyntaxFlags.MayHaveQuery) ? '?' : char.MaxValue);
					char c2 = ((!basePart.IsImplicitFile && basePart.Syntax.InFact(UriSyntaxFlags.MayHaveFragment)) ? '#' : char.MaxValue);
					string text3 = string.Empty;
					if (c != '\uffff' || c2 != '\uffff')
					{
						int num2 = 0;
						while (num2 < relativePart.Length && array[j + num2] != c && array[j + num2] != c2)
						{
							num2++;
						}
						if (num2 == 0)
						{
							text3 = relativePart;
						}
						else if (num2 < relativePart.Length)
						{
							text3 = relativePart.Substring(num2);
						}
						j += num2;
					}
					else
					{
						j += relativePart.Length;
					}
					if (basePart.HostType == InternalUri.Flags.IPv6HostType)
					{
						if (basePart.IsImplicitFile)
						{
							text2 = "\\\\[" + basePart.DnsSafeHost + "]";
						}
						else
						{
							text2 = string.Concat(new string[]
							{
								basePart.GetParts(UriComponents.Scheme | UriComponents.UserInfo, uriFormat),
								"[",
								basePart.DnsSafeHost,
								"]",
								basePart.GetParts(UriComponents.Port | UriComponents.KeepDelimiter, uriFormat)
							});
						}
					}
					else if (basePart.IsImplicitFile)
					{
						if (basePart.IsDosPath)
						{
							array = InternalUri.Compress(array, 3, ref j, basePart.Syntax);
							return new string(array, 1, j - 1) + text3;
						}
						text2 = "\\\\" + basePart.GetParts(UriComponents.Host, UriFormat.Unescaped);
					}
					else
					{
						text2 = basePart.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port, uriFormat);
					}
					array = InternalUri.Compress(array, basePart.SecuredPathIndex, ref j, basePart.Syntax);
					return text2 + new string(array, 0, j) + text3;
				}
				if (relativePart.Length >= 2 && relativePart[1] == '/')
				{
					return basePart.Scheme + ":" + relativePart;
				}
				if (basePart.HostType == InternalUri.Flags.IPv6HostType)
				{
					text2 = string.Concat(new string[]
					{
						basePart.GetParts(UriComponents.Scheme | UriComponents.UserInfo, uriFormat),
						"[",
						basePart.DnsSafeHost,
						"]",
						basePart.GetParts(UriComponents.Port | UriComponents.KeepDelimiter, uriFormat)
					});
				}
				else
				{
					text2 = basePart.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port, uriFormat);
				}
				if (flag && c == '\\')
				{
					relativePart = "/" + relativePart.Substring(1);
				}
				return text2 + relativePart;
			}
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00040A2C File Offset: 0x0003EC2C
		private static string PathDifference(string path1, string path2, bool compareCase)
		{
			int num = -1;
			int i = 0;
			while (i < path1.Length && i < path2.Length && (path1[i] == path2[i] || (!compareCase && char.ToLower(path1[i], CultureInfo.InvariantCulture) == char.ToLower(path2[i], CultureInfo.InvariantCulture))))
			{
				if (path1[i] == '/')
				{
					num = i;
				}
				i++;
			}
			if (i == 0)
			{
				return path2;
			}
			if (i == path1.Length && i == path2.Length)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (i < path1.Length)
			{
				if (path1[i] == '/')
				{
					stringBuilder.Append("../");
				}
				i++;
			}
			if (stringBuilder.Length == 0 && path2.Length - 1 == num)
			{
				return "./";
			}
			return stringBuilder.ToString() + path2.Substring(num + 1);
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x00040B0F File Offset: 0x0003ED0F
		internal bool HasAuthority
		{
			get
			{
				return this.InFact(InternalUri.Flags.AuthorityFound);
			}
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00040B1D File Offset: 0x0003ED1D
		private static bool IsLWS(char ch)
		{
			return ch <= ' ' && (ch == ' ' || ch == '\n' || ch == '\r' || ch == '\t');
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00040B3C File Offset: 0x0003ED3C
		private static bool IsAsciiLetter(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z');
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00040B59 File Offset: 0x0003ED59
		internal static bool IsAsciiLetterOrDigit(char character)
		{
			return InternalUri.IsAsciiLetter(character) || (character >= '0' && character <= '9');
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00040B74 File Offset: 0x0003ED74
		internal static bool IsBidiControlCharacter(char ch)
		{
			return ch == '\u200e' || ch == '\u200f' || ch == '\u202a' || ch == '\u202b' || ch == '\u202c' || ch == '\u202d' || ch == '\u202e';
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00040BB0 File Offset: 0x0003EDB0
		internal unsafe static string StripBidiControlCharacter(char* strToClean, int start, int length)
		{
			if (length <= 0)
			{
				return "";
			}
			char[] array = new char[length];
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = strToClean[start + i];
				if (c < '\u200e' || c > '\u202e' || !InternalUri.IsBidiControlCharacter(c))
				{
					array[num++] = c;
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00040C10 File Offset: 0x0003EE10
		[Obsolete("The method has been deprecated. Please use MakeRelativeUri(Uri uri). https://go.microsoft.com/fwlink/?linkid=14202")]
		public string MakeRelative(InternalUri toUri)
		{
			if (toUri == null)
			{
				throw new ArgumentNullException("toUri");
			}
			if (this.IsNotAbsoluteUri || toUri.IsNotAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
			}
			if (this.Scheme == toUri.Scheme && this.Host == toUri.Host && this.Port == toUri.Port)
			{
				return InternalUri.PathDifference(this.AbsolutePath, toUri.AbsolutePath, !this.IsUncOrDosPath);
			}
			return toUri.ToString();
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0000336E File Offset: 0x0000156E
		[Obsolete("The method has been deprecated. It is not used by the system. https://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void Parse()
		{
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0000336E File Offset: 0x0000156E
		[Obsolete("The method has been deprecated. It is not used by the system. https://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void Canonicalize()
		{
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0000336E File Offset: 0x0000156E
		[Obsolete("The method has been deprecated. It is not used by the system. https://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void Escape()
		{
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00040CA0 File Offset: 0x0003EEA0
		[Obsolete("The method has been deprecated. Please use GetComponents() or static UnescapeDataString() to unescape a Uri component or a string. https://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual string Unescape(string path)
		{
			char[] array = new char[path.Length];
			int num = 0;
			array = InternalUriHelper.UnescapeString(path, 0, path.Length, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, null, false);
			return new string(array, 0, num);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00040CE8 File Offset: 0x0003EEE8
		[Obsolete("The method has been deprecated. Please use GetComponents() or static EscapeUriString() to escape a Uri component or a string. https://go.microsoft.com/fwlink/?linkid=14202")]
		protected static string EscapeString(string str, bool shouldUseLegacyV2Quirks)
		{
			if (str == null)
			{
				return string.Empty;
			}
			int num = 0;
			char[] array = InternalUriHelper.EscapeString(str, 0, str.Length, null, ref num, true, '?', '#', '%', shouldUseLegacyV2Quirks);
			if (array == null)
			{
				return str;
			}
			return new string(array, 0, num);
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00040D26 File Offset: 0x0003EF26
		[Obsolete("The method has been deprecated. It is not used by the system. https://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void CheckSecurity()
		{
			this.Scheme == "telnet";
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00040D39 File Offset: 0x0003EF39
		[Obsolete("The method has been deprecated. It is not used by the system. https://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual bool IsReservedCharacter(char character)
		{
			return character == ';' || character == '/' || character == ':' || character == '@' || character == '&' || character == '=' || character == '+' || character == '$' || character == ',';
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00040D6C File Offset: 0x0003EF6C
		[Obsolete("The method has been deprecated. It is not used by the system. https://go.microsoft.com/fwlink/?linkid=14202")]
		protected static bool IsExcludedCharacter(char character)
		{
			return character <= ' ' || character >= '\u007f' || character == '<' || character == '>' || character == '#' || character == '%' || character == '"' || character == '{' || character == '}' || character == '|' || character == '\\' || character == '^' || character == '[' || character == ']' || character == '`';
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00040DC8 File Offset: 0x0003EFC8
		[Obsolete("The method has been deprecated. It is not used by the system. https://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual bool IsBadFileSystemCharacter(char character)
		{
			return character < ' ' || character == ';' || character == '/' || character == '?' || character == ':' || character == '&' || character == '=' || character == ',' || character == '*' || character == '<' || character == '>' || character == '"' || character == '|' || character == '\\' || character == '^';
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00040E24 File Offset: 0x0003F024
		private void CreateThis(string uri, bool dontEscape, UriKind uriKind)
		{
			if (uriKind < UriKind.RelativeOrAbsolute || uriKind > UriKind.Relative)
			{
				throw new ArgumentException(SR.GetString("net_uri_InvalidUriKind", uriKind));
			}
			this.m_String = ((uri == null) ? string.Empty : uri);
			if (dontEscape)
			{
				this.m_Flags |= InternalUri.Flags.UserEscaped;
			}
			ParsingError parsingError = InternalUri.ParseScheme(this.m_String, ref this.m_Flags, ref this.m_Syntax);
			UriFormatException ex;
			this.InitializeUri(parsingError, uriKind, out ex);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00040EA0 File Offset: 0x0003F0A0
		private void InitializeUri(ParsingError err, UriKind uriKind, out UriFormatException e)
		{
			if (err == ParsingError.None)
			{
				if (this.IsImplicitFile)
				{
					if (this.NotAny(InternalUri.Flags.DosPath) && uriKind != UriKind.Absolute && (uriKind == UriKind.Relative || (this.m_String.Length >= 2 && (this.m_String[0] != '\\' || this.m_String[1] != '\\'))))
					{
						this.m_Syntax = null;
						this.m_Flags &= InternalUri.Flags.UserEscaped;
						e = null;
						return;
					}
					if (uriKind == UriKind.Relative && this.InFact(InternalUri.Flags.DosPath))
					{
						this.m_Syntax = null;
						this.m_Flags &= InternalUri.Flags.UserEscaped;
						e = null;
						return;
					}
				}
			}
			else if (err > ParsingError.EmptyUriString)
			{
				this.m_String = null;
				e = InternalUri.GetException(err);
				return;
			}
			bool flag = false;
			if (!InternalUri.s_ConfigInitialized && this.CheckForConfigLoad(this.m_String))
			{
				InternalUri.InitializeUriConfig();
			}
			this.m_iriParsing = this.m_Syntax == null || this.m_Syntax.InFact(UriSyntaxFlags.AllowIriParsing);
			if (this.m_iriParsing && (this.CheckForUnicode(this.m_String) || this.CheckForEscapedUnreserved(this.m_String)))
			{
				this.m_Flags |= InternalUri.Flags.HasUnicode;
				flag = true;
				this.m_originalUnicodeString = this.m_String;
			}
			if (this.m_Syntax != null)
			{
				if (this.m_Syntax.IsSimple)
				{
					if ((err = this.PrivateParseMinimal()) != ParsingError.None)
					{
						if (uriKind != UriKind.Absolute && err <= ParsingError.EmptyUriString)
						{
							this.m_Syntax = null;
							e = null;
							this.m_Flags &= InternalUri.Flags.UserEscaped;
						}
						else
						{
							e = InternalUri.GetException(err);
						}
					}
					else if (uriKind == UriKind.Relative)
					{
						e = InternalUri.GetException(ParsingError.CannotCreateRelative);
					}
					else
					{
						e = null;
					}
					if (!this.m_iriParsing || !flag || (uriKind != UriKind.Absolute && err != ParsingError.None))
					{
						return;
					}
					try
					{
						this.EnsureParseRemaining();
						return;
					}
					catch (UriFormatException ex)
					{
						if (InternalServicePointManager.AllowAllUriEncodingExpansion)
						{
							throw;
						}
						e = ex;
						return;
					}
				}
				this.m_Syntax = this.m_Syntax.InternalOnNewUri();
				this.m_Flags |= InternalUri.Flags.UserDrivenParsing;
				this.m_Syntax.InternalValidate(this, out e);
				if (e != null)
				{
					if (uriKind != UriKind.Absolute && err != ParsingError.None && err <= ParsingError.EmptyUriString)
					{
						this.m_Syntax = null;
						e = null;
						this.m_Flags &= InternalUri.Flags.UserEscaped;
						return;
					}
					return;
				}
				else
				{
					if (err != ParsingError.None || this.InFact(InternalUri.Flags.ErrorOrParsingRecursion))
					{
						this.SetUserDrivenParsing();
					}
					else if (uriKind == UriKind.Relative)
					{
						e = InternalUri.GetException(ParsingError.CannotCreateRelative);
					}
					if (!this.m_iriParsing || !flag)
					{
						return;
					}
					try
					{
						this.EnsureParseRemaining();
						return;
					}
					catch (UriFormatException ex2)
					{
						if (InternalServicePointManager.AllowAllUriEncodingExpansion)
						{
							throw;
						}
						e = ex2;
						return;
					}
				}
			}
			if (err != ParsingError.None && uriKind != UriKind.Absolute && err <= ParsingError.EmptyUriString)
			{
				e = null;
				this.m_Flags &= InternalUri.Flags.UserEscaped | InternalUri.Flags.HasUnicode;
				if (!this.m_iriParsing || !flag)
				{
					return;
				}
				this.m_String = this.EscapeUnescapeIri(this.m_originalUnicodeString, 0, this.m_originalUnicodeString.Length, (UriComponents)0);
				try
				{
					if (this.ShouldUseLegacyV2Quirks)
					{
						this.m_String = this.m_String.Normalize(NormalizationForm.FormC);
					}
					return;
				}
				catch (ArgumentException)
				{
					e = InternalUri.GetException(ParsingError.BadFormat);
					return;
				}
			}
			this.m_String = null;
			e = InternalUri.GetException(err);
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000411EC File Offset: 0x0003F3EC
		private unsafe bool CheckForConfigLoad(string data)
		{
			bool flag = false;
			int length = data.Length;
			fixed (string text = data)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				for (int i = 0; i < length; i++)
				{
					if (ptr[i] > '\u007f' || ptr[i] == '%' || (ptr[i] == 'x' && i + 3 < length && ptr[i + 1] == 'n' && ptr[i + 2] == '-' && ptr[i + 3] == '-'))
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00041280 File Offset: 0x0003F480
		private bool CheckForUnicode(string data)
		{
			bool flag = false;
			char[] array = new char[data.Length];
			int num = 0;
			array = InternalUriHelper.UnescapeString(data, 0, data.Length, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.Unescape | UnescapeMode.UnescapeAll, null, false);
			for (int i = 0; i < num; i++)
			{
				if (array[i] > '\u007f')
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000412DC File Offset: 0x0003F4DC
		private unsafe bool CheckForEscapedUnreserved(string data)
		{
			fixed (string text = data)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				for (int i = 0; i < data.Length - 2; i++)
				{
					if (ptr[i] == '%' && InternalUri.IsHexDigit(ptr[i + 1]) && InternalUri.IsHexDigit(ptr[i + 2]) && ptr[i + 1] >= '0' && ptr[i + 1] <= '7')
					{
						char c = InternalUriHelper.EscapedAscii(ptr[i + 1], ptr[i + 2]);
						if (c != '\uffff' && InternalUriHelper.Is3986Unreserved(c))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00041380 File Offset: 0x0003F580
		public static bool TryCreate(string uriString, UriKind uriKind, out InternalUri result)
		{
			if (uriString == null)
			{
				result = null;
				return false;
			}
			UriFormatException ex = null;
			result = InternalUri.CreateHelper(uriString, false, uriKind, ref ex);
			return ex == null && result != null;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000413B0 File Offset: 0x0003F5B0
		public static bool TryCreate(InternalUri baseUri, string relativeUri, out InternalUri result)
		{
			InternalUri internalUri;
			if (!InternalUri.TryCreate(relativeUri, UriKind.RelativeOrAbsolute, out internalUri))
			{
				result = null;
				return false;
			}
			if (!internalUri.IsAbsoluteUri)
			{
				return InternalUri.TryCreate(baseUri, internalUri, out result);
			}
			result = internalUri;
			return true;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000413E4 File Offset: 0x0003F5E4
		public static bool TryCreate(InternalUri baseUri, InternalUri relativeUri, out InternalUri result)
		{
			result = null;
			if (baseUri == null || relativeUri == null)
			{
				return false;
			}
			if (baseUri.IsNotAbsoluteUri)
			{
				return false;
			}
			string text = null;
			bool flag;
			UriFormatException ex;
			if (baseUri.Syntax.IsSimple)
			{
				flag = relativeUri.UserEscaped;
				result = InternalUri.ResolveHelper(baseUri, relativeUri, ref text, ref flag, out ex);
			}
			else
			{
				flag = false;
				text = baseUri.Syntax.InternalResolve(baseUri, relativeUri, out ex);
			}
			if (ex != null)
			{
				return false;
			}
			if (result == null)
			{
				result = InternalUri.CreateHelper(text, flag, UriKind.Absolute, ref ex);
			}
			return ex == null && result != null && result.IsAbsoluteUri;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0004146C File Offset: 0x0003F66C
		public string GetComponents(UriComponents components, UriFormat format)
		{
			if ((components & UriComponents.SerializationInfoString) != (UriComponents)0 && components != UriComponents.SerializationInfoString)
			{
				throw new ArgumentOutOfRangeException("components", components, SR.GetString("net_uri_NotJustSerialization"));
			}
			if ((format & (UriFormat)(-4)) != (UriFormat)0)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			if (this.IsNotAbsoluteUri)
			{
				if (components == UriComponents.SerializationInfoString)
				{
					return this.GetRelativeSerializationString(format);
				}
				throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
			}
			else
			{
				if (this.Syntax.IsSimple)
				{
					return this.GetComponentsHelper(components, format);
				}
				return this.Syntax.InternalGetComponents(this, components, format);
			}
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00041504 File Offset: 0x0003F704
		public static int Compare(InternalUri uri1, InternalUri uri2, UriComponents partsToCompare, UriFormat compareFormat, StringComparison comparisonType)
		{
			if (uri1 == null)
			{
				if (uri2 == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (uri2 == null)
				{
					return 1;
				}
				if (uri1.IsAbsoluteUri && uri2.IsAbsoluteUri)
				{
					return string.Compare(uri1.GetParts(partsToCompare, compareFormat), uri2.GetParts(partsToCompare, compareFormat), comparisonType);
				}
				if (uri1.IsAbsoluteUri)
				{
					return 1;
				}
				if (!uri2.IsAbsoluteUri)
				{
					return string.Compare(uri1.OriginalString, uri2.OriginalString, comparisonType);
				}
				return -1;
			}
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00041575 File Offset: 0x0003F775
		public bool IsWellFormedOriginalString()
		{
			if (this.IsNotAbsoluteUri || this.Syntax.IsSimple)
			{
				return this.InternalIsWellFormedOriginalString();
			}
			return this.Syntax.InternalIsWellFormedOriginalString(this);
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000415A0 File Offset: 0x0003F7A0
		public static bool IsWellFormedUriString(string uriString, UriKind uriKind)
		{
			Uri uri;
			return Uri.TryCreate(uriString, uriKind, out uri) && uri.IsWellFormedOriginalString();
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000415C0 File Offset: 0x0003F7C0
		internal unsafe bool InternalIsWellFormedOriginalString()
		{
			if (this.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_UserDrivenParsing", base.GetType().FullName));
			}
			fixed (string @string = this.m_String)
			{
				char* ptr = @string;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				ushort num = 0;
				if (!this.IsAbsoluteUri)
				{
					return (this.ShouldUseLegacyV2Quirks || !InternalUri.CheckForColonInFirstPathSegment(this.m_String)) && (this.CheckCanonical(ptr, ref num, (ushort)this.m_String.Length, '\ufffe') & (InternalUri.Check.EscapedCanonical | InternalUri.Check.BackslashInPath)) == InternalUri.Check.EscapedCanonical;
				}
				if (this.IsImplicitFile)
				{
					return false;
				}
				this.EnsureParseRemaining();
				InternalUri.Flags flags = this.m_Flags & (InternalUri.Flags.E_UserNotCanonical | InternalUri.Flags.E_HostNotCanonical | InternalUri.Flags.E_PortNotCanonical | InternalUri.Flags.E_PathNotCanonical | InternalUri.Flags.E_QueryNotCanonical | InternalUri.Flags.E_FragmentNotCanonical | InternalUri.Flags.UserIriCanonical | InternalUri.Flags.PathIriCanonical | InternalUri.Flags.QueryIriCanonical | InternalUri.Flags.FragmentIriCanonical);
				if ((flags & InternalUri.Flags.E_CannotDisplayCanonical & (InternalUri.Flags.E_UserNotCanonical | InternalUri.Flags.E_PathNotCanonical | InternalUri.Flags.E_QueryNotCanonical | InternalUri.Flags.E_FragmentNotCanonical)) != InternalUri.Flags.Zero && (!this.m_iriParsing || (this.m_iriParsing && ((flags & InternalUri.Flags.E_UserNotCanonical) == InternalUri.Flags.Zero || (flags & InternalUri.Flags.UserIriCanonical) == InternalUri.Flags.Zero) && ((flags & InternalUri.Flags.E_PathNotCanonical) == InternalUri.Flags.Zero || (flags & InternalUri.Flags.PathIriCanonical) == InternalUri.Flags.Zero) && ((flags & InternalUri.Flags.E_QueryNotCanonical) == InternalUri.Flags.Zero || (flags & InternalUri.Flags.QueryIriCanonical) == InternalUri.Flags.Zero) && ((flags & InternalUri.Flags.E_FragmentNotCanonical) == InternalUri.Flags.Zero || (flags & InternalUri.Flags.FragmentIriCanonical) == InternalUri.Flags.Zero))))
				{
					return false;
				}
				if (this.InFact(InternalUri.Flags.AuthorityFound))
				{
					num = (ushort)((int)this.m_Info.Offset.Scheme + this.m_Syntax.SchemeName.Length + 2);
					if (num >= this.m_Info.Offset.User || this.m_String[(int)(num - 1)] == '\\' || this.m_String[(int)num] == '\\')
					{
						return false;
					}
					if (this.InFact(InternalUri.Flags.DosPath | InternalUri.Flags.UncPath) && (num += 1) < this.m_Info.Offset.User && (this.m_String[(int)num] == '/' || this.m_String[(int)num] == '\\'))
					{
						return false;
					}
				}
				if (this.InFact(InternalUri.Flags.FirstSlashAbsent) && this.m_Info.Offset.Query > this.m_Info.Offset.Path)
				{
					return false;
				}
				if (this.InFact(InternalUri.Flags.BackslashInPath))
				{
					return false;
				}
				if (this.IsDosPath && this.m_String[(int)(this.m_Info.Offset.Path + this.SecuredPathIndex - 1)] == '|')
				{
					return false;
				}
				if ((this.m_Flags & InternalUri.Flags.CanonicalDnsHost) == InternalUri.Flags.Zero && this.HostType != InternalUri.Flags.IPv6HostType)
				{
					num = this.m_Info.Offset.User;
					InternalUri.Check check = this.CheckCanonical(ptr, ref num, this.m_Info.Offset.Path, '/');
					if ((check & (InternalUri.Check.EscapedCanonical | InternalUri.Check.BackslashInPath | InternalUri.Check.ReservedFound)) != InternalUri.Check.EscapedCanonical && (!this.m_iriParsing || (this.m_iriParsing && (check & (InternalUri.Check.DisplayCanonical | InternalUri.Check.NotIriCanonical | InternalUri.Check.FoundNonAscii)) != (InternalUri.Check.DisplayCanonical | InternalUri.Check.FoundNonAscii))))
					{
						return false;
					}
				}
				if ((this.m_Flags & (InternalUri.Flags.SchemeNotCanonical | InternalUri.Flags.AuthorityFound)) == (InternalUri.Flags.SchemeNotCanonical | InternalUri.Flags.AuthorityFound))
				{
					num = (ushort)this.m_Syntax.SchemeName.Length;
					IntPtr intPtr;
					ushort num2;
					do
					{
						intPtr = ptr;
						num2 = num;
						num = num2 + 1;
					}
					while (*(intPtr + (IntPtr)num2 * 2) != 58);
					if ((int)(num + 1) >= this.m_String.Length || ptr[num] != '/' || ptr[num + 1] != '/')
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000418E4 File Offset: 0x0003FAE4
		public unsafe static string UnescapeDataString(string stringToUnescape)
		{
			if (stringToUnescape == null)
			{
				throw new ArgumentNullException("stringToUnescape");
			}
			if (stringToUnescape.Length == 0)
			{
				return string.Empty;
			}
			char* ptr = stringToUnescape;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 0;
			while (num < stringToUnescape.Length && ptr[num] != '%')
			{
				num++;
			}
			if (num == stringToUnescape.Length)
			{
				return stringToUnescape;
			}
			UnescapeMode unescapeMode = UnescapeMode.Unescape | UnescapeMode.UnescapeAll;
			num = 0;
			char[] array = new char[stringToUnescape.Length];
			array = InternalUriHelper.UnescapeString(stringToUnescape, 0, stringToUnescape.Length, array, ref num, char.MaxValue, char.MaxValue, char.MaxValue, unescapeMode, null, false);
			return new string(array, 0, num);
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00041984 File Offset: 0x0003FB84
		public static string EscapeUriString(string stringToEscape, bool shouldUseLegacyV2Quirks)
		{
			if (stringToEscape == null)
			{
				throw new ArgumentNullException("stringToEscape");
			}
			if (stringToEscape.Length == 0)
			{
				return string.Empty;
			}
			int num = 0;
			char[] array = InternalUriHelper.EscapeString(stringToEscape, 0, stringToEscape.Length, null, ref num, true, char.MaxValue, char.MaxValue, char.MaxValue, shouldUseLegacyV2Quirks);
			if (array == null)
			{
				return stringToEscape;
			}
			return new string(array, 0, num);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000419E0 File Offset: 0x0003FBE0
		public static string EscapeDataString(string stringToEscape, bool shouldUseLegacyV2Quirks)
		{
			if (stringToEscape == null)
			{
				throw new ArgumentNullException("stringToEscape");
			}
			if (stringToEscape.Length == 0)
			{
				return string.Empty;
			}
			int num = 0;
			char[] array = InternalUriHelper.EscapeString(stringToEscape, 0, stringToEscape.Length, null, ref num, false, char.MaxValue, char.MaxValue, char.MaxValue, shouldUseLegacyV2Quirks);
			if (array == null)
			{
				return stringToEscape;
			}
			return new string(array, 0, num);
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00041A3C File Offset: 0x0003FC3C
		internal unsafe string EscapeUnescapeIri(string input, int start, int end, UriComponents component)
		{
			char* ptr = input;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return IriHelper.EscapeUnescapeIri(ptr, start, end, component);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00041A63 File Offset: 0x0003FC63
		private InternalUri(InternalUri.Flags flags, InternalUriParser uriParser, string uri)
		{
			this.m_Flags = flags;
			this.m_Syntax = uriParser;
			this.m_String = uri;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00041A80 File Offset: 0x0003FC80
		internal static InternalUri CreateHelper(string uriString, bool dontEscape, UriKind uriKind, ref UriFormatException e)
		{
			if (uriKind < UriKind.RelativeOrAbsolute || uriKind > UriKind.Relative)
			{
				throw new ArgumentException(SR.GetString("net_uri_InvalidUriKind", uriKind));
			}
			InternalUriParser internalUriParser = null;
			InternalUri.Flags flags = InternalUri.Flags.Zero;
			ParsingError parsingError = InternalUri.ParseScheme(uriString, ref flags, ref internalUriParser);
			if (dontEscape)
			{
				flags |= InternalUri.Flags.UserEscaped;
			}
			if (parsingError == ParsingError.None)
			{
				InternalUri internalUri = new InternalUri(flags, internalUriParser, uriString);
				InternalUri internalUri2;
				try
				{
					internalUri.InitializeUri(parsingError, uriKind, out e);
					if (e == null)
					{
						internalUri2 = internalUri;
					}
					else
					{
						internalUri2 = null;
					}
				}
				catch (UriFormatException ex)
				{
					e = ex;
					internalUri2 = null;
				}
				return internalUri2;
			}
			if (uriKind != UriKind.Absolute && parsingError <= ParsingError.EmptyUriString)
			{
				return new InternalUri(flags & InternalUri.Flags.UserEscaped, null, uriString);
			}
			return null;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00041B24 File Offset: 0x0003FD24
		internal static InternalUri ResolveHelper(InternalUri baseUri, InternalUri relativeUri, ref string newUriString, ref bool userEscaped, out UriFormatException e)
		{
			e = null;
			string text = string.Empty;
			if (relativeUri != null)
			{
				if (relativeUri.IsAbsoluteUri)
				{
					return relativeUri;
				}
				text = relativeUri.OriginalString;
				userEscaped = relativeUri.UserEscaped;
			}
			else
			{
				text = string.Empty;
			}
			if (text.Length > 0 && (InternalUri.IsLWS(text[0]) || InternalUri.IsLWS(text[text.Length - 1])))
			{
				text = text.Trim(InternalUri._WSchars);
			}
			if (text.Length == 0)
			{
				newUriString = baseUri.GetParts(UriComponents.AbsoluteUri, baseUri.UserEscaped ? UriFormat.UriEscaped : UriFormat.SafeUnescaped);
				return null;
			}
			if (text[0] == '#' && !baseUri.IsImplicitFile && baseUri.Syntax.InFact(UriSyntaxFlags.MayHaveFragment))
			{
				newUriString = baseUri.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query, UriFormat.UriEscaped) + text;
				return null;
			}
			if (text[0] == '?' && !baseUri.IsImplicitFile && baseUri.Syntax.InFact(UriSyntaxFlags.MayHaveQuery))
			{
				newUriString = baseUri.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped) + text;
				return null;
			}
			if (text.Length >= 3 && (text[1] == ':' || text[1] == '|') && InternalUri.IsAsciiLetter(text[0]) && (text[2] == '\\' || text[2] == '/'))
			{
				if (baseUri.IsImplicitFile)
				{
					newUriString = text;
					return null;
				}
				if (baseUri.Syntax.InFact(UriSyntaxFlags.AllowDOSPath))
				{
					string text2;
					if (baseUri.InFact(InternalUri.Flags.AuthorityFound))
					{
						text2 = (baseUri.Syntax.InFact(UriSyntaxFlags.PathIsRooted) ? ":///" : "://");
					}
					else
					{
						text2 = (baseUri.Syntax.InFact(UriSyntaxFlags.PathIsRooted) ? ":/" : ":");
					}
					newUriString = baseUri.Scheme + text2 + text;
					return null;
				}
			}
			ParsingError combinedString = InternalUri.GetCombinedString(baseUri, text, userEscaped, ref newUriString);
			if (combinedString != ParsingError.None)
			{
				e = InternalUri.GetException(combinedString);
				return null;
			}
			if (newUriString == baseUri.m_String)
			{
				return baseUri;
			}
			return null;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00041D14 File Offset: 0x0003FF14
		private string GetRelativeSerializationString(UriFormat format)
		{
			if (format == UriFormat.UriEscaped)
			{
				if (this.m_String.Length == 0)
				{
					return string.Empty;
				}
				int num = 0;
				char[] array = InternalUriHelper.EscapeString(this.m_String, 0, this.m_String.Length, null, ref num, true, char.MaxValue, char.MaxValue, '%', this.ShouldUseLegacyV2Quirks);
				if (array == null)
				{
					return this.m_String;
				}
				return new string(array, 0, num);
			}
			else
			{
				if (format == UriFormat.Unescaped)
				{
					return InternalUri.UnescapeDataString(this.m_String);
				}
				if (format != UriFormat.SafeUnescaped)
				{
					throw new ArgumentOutOfRangeException("format");
				}
				if (this.m_String.Length == 0)
				{
					return string.Empty;
				}
				char[] array2 = new char[this.m_String.Length];
				int num2 = 0;
				array2 = InternalUriHelper.UnescapeString(this.m_String, 0, this.m_String.Length, array2, ref num2, char.MaxValue, char.MaxValue, char.MaxValue, UnescapeMode.EscapeUnescape, null, false);
				return new string(array2, 0, num2);
			}
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00041DF8 File Offset: 0x0003FFF8
		internal string GetComponentsHelper(UriComponents uriComponents, UriFormat uriFormat)
		{
			if (uriComponents == UriComponents.Scheme)
			{
				return this.m_Syntax.SchemeName;
			}
			if ((uriComponents & UriComponents.SerializationInfoString) != (UriComponents)0)
			{
				uriComponents |= UriComponents.AbsoluteUri;
			}
			this.EnsureParseRemaining();
			if ((uriComponents & UriComponents.NormalizedHost) != (UriComponents)0)
			{
				uriComponents |= UriComponents.Host;
			}
			if ((uriComponents & UriComponents.Host) != (UriComponents)0)
			{
				this.EnsureHostString(true);
			}
			if (uriComponents == UriComponents.Port || uriComponents == UriComponents.StrongPort)
			{
				if ((this.m_Flags & InternalUri.Flags.NotDefaultPort) != InternalUri.Flags.Zero || (uriComponents == UriComponents.StrongPort && this.m_Syntax.DefaultPort != -1))
				{
					return this.m_Info.Offset.PortValue.ToString(CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
			else
			{
				if ((uriComponents & UriComponents.StrongPort) != (UriComponents)0)
				{
					uriComponents |= UriComponents.Port;
				}
				if (uriComponents == UriComponents.Host && (uriFormat == UriFormat.UriEscaped || (this.m_Flags & (InternalUri.Flags.HostNotCanonical | InternalUri.Flags.E_HostNotCanonical)) == InternalUri.Flags.Zero))
				{
					this.EnsureHostString(false);
					return this.m_Info.Host;
				}
				if (uriFormat == UriFormat.UriEscaped)
				{
					return this.GetEscapedParts(uriComponents);
				}
				if (uriFormat - UriFormat.Unescaped > 1 && uriFormat != (UriFormat)32767)
				{
					throw new ArgumentOutOfRangeException("uriFormat");
				}
				return this.GetUnescapedParts(uriComponents, uriFormat);
			}
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00041EFD File Offset: 0x000400FD
		public bool IsBaseOf(InternalUri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (!this.IsAbsoluteUri)
			{
				return false;
			}
			if (this.Syntax.IsSimple)
			{
				return this.IsBaseOfHelper(uri);
			}
			return this.Syntax.InternalIsBaseOf(this, uri);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00041F3C File Offset: 0x0004013C
		internal unsafe bool IsBaseOfHelper(InternalUri uriLink)
		{
			if (!this.IsAbsoluteUri || this.UserDrivenParsing)
			{
				return false;
			}
			if (!uriLink.IsAbsoluteUri)
			{
				string text = null;
				bool flag = false;
				UriFormatException ex;
				uriLink = InternalUri.ResolveHelper(this, uriLink, ref text, ref flag, out ex);
				if (ex != null)
				{
					return false;
				}
				if (uriLink == null)
				{
					uriLink = InternalUri.CreateHelper(text, flag, UriKind.Absolute, ref ex);
				}
				if (ex != null)
				{
					return false;
				}
			}
			if (this.Syntax.SchemeName != uriLink.Syntax.SchemeName)
			{
				return false;
			}
			string parts = this.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query, UriFormat.SafeUnescaped);
			string parts2 = uriLink.GetParts(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query, UriFormat.SafeUnescaped);
			fixed (string text2 = parts)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = parts2)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					return InternalUriHelper.TestForSubPath(ptr, (ushort)parts.Length, ptr2, (ushort)parts2.Length, this.IsUncOrDosPath || uriLink.IsUncOrDosPath);
				}
			}
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00042018 File Offset: 0x00040218
		private void CreateThisFromUri(InternalUri otherUri)
		{
			this.m_Info = null;
			this.m_Flags = otherUri.m_Flags;
			if (this.InFact(InternalUri.Flags.MinimalUriInfoSet))
			{
				this.m_Flags &= ~(InternalUri.Flags.SchemeNotCanonical | InternalUri.Flags.UserNotCanonical | InternalUri.Flags.HostNotCanonical | InternalUri.Flags.PortNotCanonical | InternalUri.Flags.PathNotCanonical | InternalUri.Flags.QueryNotCanonical | InternalUri.Flags.FragmentNotCanonical | InternalUri.Flags.E_UserNotCanonical | InternalUri.Flags.E_HostNotCanonical | InternalUri.Flags.E_PortNotCanonical | InternalUri.Flags.E_PathNotCanonical | InternalUri.Flags.E_QueryNotCanonical | InternalUri.Flags.E_FragmentNotCanonical | InternalUri.Flags.ShouldBeCompressed | InternalUri.Flags.FirstSlashAbsent | InternalUri.Flags.BackslashInPath | InternalUri.Flags.MinimalUriInfoSet | InternalUri.Flags.AllUriInfoSet);
				int num = (int)otherUri.m_Info.Offset.Path;
				if (this.InFact(InternalUri.Flags.NotDefaultPort))
				{
					while (otherUri.m_String[num] != ':' && num > (int)otherUri.m_Info.Offset.Host)
					{
						num--;
					}
					if (otherUri.m_String[num] != ':')
					{
						num = (int)otherUri.m_Info.Offset.Path;
					}
				}
				this.m_Flags |= (InternalUri.Flags)((long)num);
			}
			this.m_Syntax = otherUri.m_Syntax;
			this.m_String = otherUri.m_String;
			this.m_iriParsing = otherUri.m_iriParsing;
			if (otherUri.OriginalStringSwitched)
			{
				this.m_originalUnicodeString = otherUri.m_originalUnicodeString;
			}
			if (otherUri.AllowIdn && (otherUri.InFact(InternalUri.Flags.IdnHost) || otherUri.InFact(InternalUri.Flags.UnicodeHost)))
			{
				this.m_DnsSafeHost = otherUri.m_DnsSafeHost;
			}
		}

		// Token: 0x04000890 RID: 2192
		public static readonly string UriSchemeFile = InternalUriParser.FileUri.SchemeName;

		// Token: 0x04000891 RID: 2193
		public static readonly string UriSchemeFtp = InternalUriParser.FtpUri.SchemeName;

		// Token: 0x04000892 RID: 2194
		public static readonly string UriSchemeGopher = InternalUriParser.GopherUri.SchemeName;

		// Token: 0x04000893 RID: 2195
		public static readonly string UriSchemeHttp = InternalUriParser.HttpUri.SchemeName;

		// Token: 0x04000894 RID: 2196
		public static readonly string UriSchemeHttps = InternalUriParser.HttpsUri.SchemeName;

		// Token: 0x04000895 RID: 2197
		internal static readonly string UriSchemeWs = InternalUriParser.WsUri.SchemeName;

		// Token: 0x04000896 RID: 2198
		internal static readonly string UriSchemeWss = InternalUriParser.WssUri.SchemeName;

		// Token: 0x04000897 RID: 2199
		public static readonly string UriSchemeMailto = InternalUriParser.MailToUri.SchemeName;

		// Token: 0x04000898 RID: 2200
		public static readonly string UriSchemeNews = InternalUriParser.NewsUri.SchemeName;

		// Token: 0x04000899 RID: 2201
		public static readonly string UriSchemeNntp = InternalUriParser.NntpUri.SchemeName;

		// Token: 0x0400089A RID: 2202
		public static readonly string UriSchemeNetTcp = InternalUriParser.NetTcpUri.SchemeName;

		// Token: 0x0400089B RID: 2203
		public static readonly string UriSchemeNetPipe = InternalUriParser.NetPipeUri.SchemeName;

		// Token: 0x0400089C RID: 2204
		public static readonly string SchemeDelimiter = "://";

		// Token: 0x0400089D RID: 2205
		private const UriComponents UriComponentsNormalizedHost = UriComponents.NormalizedHost;

		// Token: 0x0400089E RID: 2206
		private const int c_Max16BitUtf8SequenceLength = 12;

		// Token: 0x0400089F RID: 2207
		internal const int c_MaxUriBufferSize = 65520;

		// Token: 0x040008A0 RID: 2208
		private const int c_MaxUriSchemeName = 1024;

		// Token: 0x040008A1 RID: 2209
		private string m_String;

		// Token: 0x040008A2 RID: 2210
		private string m_originalUnicodeString;

		// Token: 0x040008A3 RID: 2211
		private InternalUriParser m_Syntax;

		// Token: 0x040008A4 RID: 2212
		private string m_DnsSafeHost;

		// Token: 0x040008A5 RID: 2213
		private InternalUri.Flags m_Flags;

		// Token: 0x040008A6 RID: 2214
		private InternalUri.UriInfo m_Info;

		// Token: 0x040008A7 RID: 2215
		private bool m_iriParsing;

		// Token: 0x040008A8 RID: 2216
		private static volatile IInternetSecurityManager s_ManagerRef = null;

		// Token: 0x040008A9 RID: 2217
		private static object s_IntranetLock = new object();

		// Token: 0x040008AA RID: 2218
		private static volatile bool s_ConfigInitialized;

		// Token: 0x040008AB RID: 2219
		private static volatile bool s_ConfigInitializing;

		// Token: 0x040008AC RID: 2220
		private static volatile UriIdnScope s_IdnScope = UriIdnScope.None;

		// Token: 0x040008AD RID: 2221
		private const bool s_IriParsing = true;

		// Token: 0x040008AE RID: 2222
		private static object s_initLock;

		// Token: 0x040008AF RID: 2223
		private const UriFormat V1ToStringUnescape = (UriFormat)32767;

		// Token: 0x040008B0 RID: 2224
		internal const char c_DummyChar = '\uffff';

		// Token: 0x040008B1 RID: 2225
		internal const char c_EOL = '\ufffe';

		// Token: 0x040008B2 RID: 2226
		internal static readonly char[] HexLowerChars = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f'
		};

		// Token: 0x040008B3 RID: 2227
		private static readonly char[] _WSchars = new char[] { ' ', '\n', '\r', '\t' };

		// Token: 0x020002BC RID: 700
		[Flags]
		private enum Flags : ulong
		{
			// Token: 0x040008B5 RID: 2229
			Zero = 0UL,
			// Token: 0x040008B6 RID: 2230
			SchemeNotCanonical = 1UL,
			// Token: 0x040008B7 RID: 2231
			UserNotCanonical = 2UL,
			// Token: 0x040008B8 RID: 2232
			HostNotCanonical = 4UL,
			// Token: 0x040008B9 RID: 2233
			PortNotCanonical = 8UL,
			// Token: 0x040008BA RID: 2234
			PathNotCanonical = 16UL,
			// Token: 0x040008BB RID: 2235
			QueryNotCanonical = 32UL,
			// Token: 0x040008BC RID: 2236
			FragmentNotCanonical = 64UL,
			// Token: 0x040008BD RID: 2237
			CannotDisplayCanonical = 127UL,
			// Token: 0x040008BE RID: 2238
			E_UserNotCanonical = 128UL,
			// Token: 0x040008BF RID: 2239
			E_HostNotCanonical = 256UL,
			// Token: 0x040008C0 RID: 2240
			E_PortNotCanonical = 512UL,
			// Token: 0x040008C1 RID: 2241
			E_PathNotCanonical = 1024UL,
			// Token: 0x040008C2 RID: 2242
			E_QueryNotCanonical = 2048UL,
			// Token: 0x040008C3 RID: 2243
			E_FragmentNotCanonical = 4096UL,
			// Token: 0x040008C4 RID: 2244
			E_CannotDisplayCanonical = 8064UL,
			// Token: 0x040008C5 RID: 2245
			ShouldBeCompressed = 8192UL,
			// Token: 0x040008C6 RID: 2246
			FirstSlashAbsent = 16384UL,
			// Token: 0x040008C7 RID: 2247
			BackslashInPath = 32768UL,
			// Token: 0x040008C8 RID: 2248
			IndexMask = 65535UL,
			// Token: 0x040008C9 RID: 2249
			HostTypeMask = 458752UL,
			// Token: 0x040008CA RID: 2250
			HostNotParsed = 0UL,
			// Token: 0x040008CB RID: 2251
			IPv6HostType = 65536UL,
			// Token: 0x040008CC RID: 2252
			IPv4HostType = 131072UL,
			// Token: 0x040008CD RID: 2253
			DnsHostType = 196608UL,
			// Token: 0x040008CE RID: 2254
			UncHostType = 262144UL,
			// Token: 0x040008CF RID: 2255
			BasicHostType = 327680UL,
			// Token: 0x040008D0 RID: 2256
			UnusedHostType = 393216UL,
			// Token: 0x040008D1 RID: 2257
			UnknownHostType = 458752UL,
			// Token: 0x040008D2 RID: 2258
			UserEscaped = 524288UL,
			// Token: 0x040008D3 RID: 2259
			AuthorityFound = 1048576UL,
			// Token: 0x040008D4 RID: 2260
			HasUserInfo = 2097152UL,
			// Token: 0x040008D5 RID: 2261
			LoopbackHost = 4194304UL,
			// Token: 0x040008D6 RID: 2262
			NotDefaultPort = 8388608UL,
			// Token: 0x040008D7 RID: 2263
			UserDrivenParsing = 16777216UL,
			// Token: 0x040008D8 RID: 2264
			CanonicalDnsHost = 33554432UL,
			// Token: 0x040008D9 RID: 2265
			ErrorOrParsingRecursion = 67108864UL,
			// Token: 0x040008DA RID: 2266
			DosPath = 134217728UL,
			// Token: 0x040008DB RID: 2267
			UncPath = 268435456UL,
			// Token: 0x040008DC RID: 2268
			ImplicitFile = 536870912UL,
			// Token: 0x040008DD RID: 2269
			MinimalUriInfoSet = 1073741824UL,
			// Token: 0x040008DE RID: 2270
			AllUriInfoSet = 2147483648UL,
			// Token: 0x040008DF RID: 2271
			IdnHost = 4294967296UL,
			// Token: 0x040008E0 RID: 2272
			HasUnicode = 8589934592UL,
			// Token: 0x040008E1 RID: 2273
			HostUnicodeNormalized = 17179869184UL,
			// Token: 0x040008E2 RID: 2274
			RestUnicodeNormalized = 34359738368UL,
			// Token: 0x040008E3 RID: 2275
			UnicodeHost = 68719476736UL,
			// Token: 0x040008E4 RID: 2276
			IntranetUri = 137438953472UL,
			// Token: 0x040008E5 RID: 2277
			UseOrigUncdStrOffset = 274877906944UL,
			// Token: 0x040008E6 RID: 2278
			UserIriCanonical = 549755813888UL,
			// Token: 0x040008E7 RID: 2279
			PathIriCanonical = 1099511627776UL,
			// Token: 0x040008E8 RID: 2280
			QueryIriCanonical = 2199023255552UL,
			// Token: 0x040008E9 RID: 2281
			FragmentIriCanonical = 4398046511104UL,
			// Token: 0x040008EA RID: 2282
			IriCanonical = 8246337208320UL
		}

		// Token: 0x020002BD RID: 701
		private class UriInfo
		{
			// Token: 0x040008EB RID: 2283
			public string Host;

			// Token: 0x040008EC RID: 2284
			public string ScopeId;

			// Token: 0x040008ED RID: 2285
			public string String;

			// Token: 0x040008EE RID: 2286
			public InternalUri.Offset Offset;

			// Token: 0x040008EF RID: 2287
			public string DnsSafeHost;

			// Token: 0x040008F0 RID: 2288
			public InternalUri.MoreInfo MoreInfo;
		}

		// Token: 0x020002BE RID: 702
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct Offset
		{
			// Token: 0x040008F1 RID: 2289
			public ushort Scheme;

			// Token: 0x040008F2 RID: 2290
			public ushort User;

			// Token: 0x040008F3 RID: 2291
			public ushort Host;

			// Token: 0x040008F4 RID: 2292
			public ushort PortValue;

			// Token: 0x040008F5 RID: 2293
			public ushort Path;

			// Token: 0x040008F6 RID: 2294
			public ushort Query;

			// Token: 0x040008F7 RID: 2295
			public ushort Fragment;

			// Token: 0x040008F8 RID: 2296
			public ushort End;
		}

		// Token: 0x020002BF RID: 703
		private class MoreInfo
		{
			// Token: 0x040008F9 RID: 2297
			public string Path;

			// Token: 0x040008FA RID: 2298
			public string Query;

			// Token: 0x040008FB RID: 2299
			public string Fragment;

			// Token: 0x040008FC RID: 2300
			public string AbsoluteUri;

			// Token: 0x040008FD RID: 2301
			public int Hash;

			// Token: 0x040008FE RID: 2302
			public string RemoteUrl;
		}

		// Token: 0x020002C0 RID: 704
		[Flags]
		private enum Check
		{
			// Token: 0x04000900 RID: 2304
			None = 0,
			// Token: 0x04000901 RID: 2305
			EscapedCanonical = 1,
			// Token: 0x04000902 RID: 2306
			DisplayCanonical = 2,
			// Token: 0x04000903 RID: 2307
			DotSlashAttn = 4,
			// Token: 0x04000904 RID: 2308
			DotSlashEscaped = 128,
			// Token: 0x04000905 RID: 2309
			BackslashInPath = 16,
			// Token: 0x04000906 RID: 2310
			ReservedFound = 32,
			// Token: 0x04000907 RID: 2311
			NotIriCanonical = 64,
			// Token: 0x04000908 RID: 2312
			FoundNonAscii = 8
		}
	}
}
