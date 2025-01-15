using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000063 RID: 99
	internal sealed class XmlDeclarationToken : XmlToken
	{
		// Token: 0x0600022E RID: 558 RVA: 0x0000EC16 File Offset: 0x0000CE16
		public XmlDeclarationToken(TextPosition position)
			: base(XmlTokenType.Declaration, position)
		{
			this._version = string.Empty;
			this._encoding = null;
			this._standalone = false;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000EC39 File Offset: 0x0000CE39
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000EC41 File Offset: 0x0000CE41
		public string Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000EC4A File Offset: 0x0000CE4A
		public bool IsEncodingMissing
		{
			get
			{
				return this._encoding == null;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000EC55 File Offset: 0x0000CE55
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000EC66 File Offset: 0x0000CE66
		public string Encoding
		{
			get
			{
				return this._encoding ?? string.Empty;
			}
			set
			{
				this._encoding = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000EC6F File Offset: 0x0000CE6F
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000EC77 File Offset: 0x0000CE77
		public bool Standalone
		{
			get
			{
				return this._standalone;
			}
			set
			{
				this._standalone = value;
			}
		}

		// Token: 0x04000222 RID: 546
		private string _version;

		// Token: 0x04000223 RID: 547
		private string _encoding;

		// Token: 0x04000224 RID: 548
		private bool _standalone;
	}
}
