using System;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Html
{
	// Token: 0x02000074 RID: 116
	public sealed class HtmlDoctypeToken : HtmlToken
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00016921 File Offset: 0x00014B21
		public HtmlDoctypeToken(bool quirksForced, TextPosition position)
			: base(HtmlTokenType.Doctype, position, null)
		{
			this._publicIdentifier = null;
			this._systemIdentifier = null;
			this._quirks = quirksForced;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00016941 File Offset: 0x00014B41
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00016949 File Offset: 0x00014B49
		public bool IsQuirksForced
		{
			get
			{
				return this._quirks;
			}
			set
			{
				this._quirks = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00016952 File Offset: 0x00014B52
		public bool IsPublicIdentifierMissing
		{
			get
			{
				return this._publicIdentifier == null;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0001695D File Offset: 0x00014B5D
		public bool IsSystemIdentifierMissing
		{
			get
			{
				return this._systemIdentifier == null;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00016968 File Offset: 0x00014B68
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00016979 File Offset: 0x00014B79
		public string PublicIdentifier
		{
			get
			{
				return this._publicIdentifier ?? string.Empty;
			}
			set
			{
				this._publicIdentifier = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00016982 File Offset: 0x00014B82
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00016993 File Offset: 0x00014B93
		public string SystemIdentifier
		{
			get
			{
				return this._systemIdentifier ?? string.Empty;
			}
			set
			{
				this._systemIdentifier = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0001699C File Offset: 0x00014B9C
		public bool IsLimitedQuirks
		{
			get
			{
				return this.PublicIdentifier.StartsWith("-//W3C//DTD XHTML 1.0 Frameset//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD XHTML 1.0 Transitional//", StringComparison.OrdinalIgnoreCase) || this.SystemIdentifier.StartsWith("-//W3C//DTD HTML 4.01 Frameset//", StringComparison.OrdinalIgnoreCase) || this.SystemIdentifier.StartsWith("-//W3C//DTD HTML 4.01 Transitional//", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00016A00 File Offset: 0x00014C00
		public bool IsFullQuirks
		{
			get
			{
				return this.IsQuirksForced || !base.Name.Is("html") || this.PublicIdentifier.StartsWith("+//Silmaril//dtd html Pro v0r11 19970101//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//AdvaSoft Ltd//DTD HTML 3.0 asWedit + extensions//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//AS//DTD HTML 3.0 asWedit + extensions//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0 Level 1//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0 Level 2//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0 Strict Level 1//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0 Strict Level 2//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0 Strict//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.1E//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 3.0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 3.2 Final//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 3.2//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML 3//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML Level 0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML Level 1//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML Level 2//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML Level 3//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML Strict Level 0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML Strict Level 1//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML Strict Level 2//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML Strict Level 3//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML Strict//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//IETF//DTD HTML//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Metrius//DTD Metrius Presentational//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 HTML Strict//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 HTML//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 Tables//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 HTML Strict//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 HTML//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 Tables//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Netscape Comm. Corp.//DTD HTML//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Netscape Comm. Corp.//DTD Strict HTML//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//O'Reilly and Associates//DTD HTML 2.0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//O'Reilly and Associates//DTD HTML Extended 1.0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//O'Reilly and Associates//DTD HTML Extended Relaxed 1.0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//SoftQuad Software//DTD HoTMetaL PRO 6.0::19990601::extensions to HTML 4.0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//SoftQuad//DTD HoTMetaL PRO 4.0::19971010::extensions to HTML 4.0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Spyglass//DTD HTML 2.0 Extended//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//SQ//DTD HTML 2.0 HoTMetaL + extensions//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Sun Microsystems Corp.//DTD HotJava HTML//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//Sun Microsystems Corp.//DTD HotJava Strict HTML//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD HTML 3 1995-03-24//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD HTML 3.2 Draft//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD HTML 3.2 Final//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD HTML 3.2//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD HTML 3.2S Draft//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD HTML 4.0 Frameset//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD HTML 4.0 Transitional//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD HTML Experimental 19960712//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD HTML Experimental 970421//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3C//DTD W3 HTML//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//W3O//DTD W3 HTML 3.0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.Isi("-//W3O//DTD W3 HTML Strict 3.0//EN//") || this.PublicIdentifier.StartsWith("-//WebTechs//DTD Mozilla HTML 2.0//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.StartsWith("-//WebTechs//DTD Mozilla HTML//", StringComparison.OrdinalIgnoreCase) || this.PublicIdentifier.Isi("-/W3C/DTD HTML 4.0 Transitional/EN") || this.PublicIdentifier.Isi("HTML") || this.SystemIdentifier.Equals("http://www.ibm.com/data/dtd/v11/ibmxhtml1-transitional.dtd", StringComparison.OrdinalIgnoreCase) || (this.IsSystemIdentifierMissing && this.PublicIdentifier.StartsWith("-//W3C//DTD HTML 4.01 Frameset//", StringComparison.OrdinalIgnoreCase)) || (this.IsSystemIdentifierMissing && this.PublicIdentifier.StartsWith("-//W3C//DTD HTML 4.01 Transitional//", StringComparison.OrdinalIgnoreCase));
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00016F3C File Offset: 0x0001513C
		public bool IsValid
		{
			get
			{
				if (!base.Name.Is("html"))
				{
					return false;
				}
				if (this.IsPublicIdentifierMissing)
				{
					return this.IsSystemIdentifierMissing || this.SystemIdentifier.Is("about:legacy-compat");
				}
				if (this.PublicIdentifier.Is("-//W3C//DTD HTML 4.0//EN"))
				{
					return this.IsSystemIdentifierMissing || this.SystemIdentifier.Is("http://www.w3.org/TR/REC-html40/strict.dtd");
				}
				if (this.PublicIdentifier.Is("-//W3C//DTD HTML 4.01//EN"))
				{
					return this.IsSystemIdentifierMissing || this.SystemIdentifier.Is("http://www.w3.org/TR/html4/strict.dtd");
				}
				if (this.PublicIdentifier.Is("-//W3C//DTD XHTML 1.0 Strict//EN"))
				{
					return this.SystemIdentifier.Is("http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd");
				}
				return this.PublicIdentifier.Is("-//W3C//DTD XHTML 1.1//EN") && this.SystemIdentifier.Is("http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd");
			}
		}

		// Token: 0x040002CA RID: 714
		private bool _quirks;

		// Token: 0x040002CB RID: 715
		private string _publicIdentifier;

		// Token: 0x040002CC RID: 716
		private string _systemIdentifier;
	}
}
