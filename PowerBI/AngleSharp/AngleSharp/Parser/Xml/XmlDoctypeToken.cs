using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000064 RID: 100
	internal sealed class XmlDoctypeToken : XmlToken
	{
		// Token: 0x06000236 RID: 566 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public XmlDoctypeToken(TextPosition position)
			: base(XmlTokenType.Doctype, position)
		{
			this._name = null;
			this._publicIdentifier = null;
			this._systemIdentifier = null;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000EC9F File Offset: 0x0000CE9F
		public bool IsNameMissing
		{
			get
			{
				return this._name == null;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000ECAA File Offset: 0x0000CEAA
		public bool IsPublicIdentifierMissing
		{
			get
			{
				return this._publicIdentifier == null;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000ECB5 File Offset: 0x0000CEB5
		public bool IsSystemIdentifierMissing
		{
			get
			{
				return this._systemIdentifier == null;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000ECD1 File Offset: 0x0000CED1
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000ECDA File Offset: 0x0000CEDA
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000ECEB File Offset: 0x0000CEEB
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000ED05 File Offset: 0x0000CF05
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000ED0E File Offset: 0x0000CF0E
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000ED16 File Offset: 0x0000CF16
		public string InternalSubset { get; set; }

		// Token: 0x04000225 RID: 549
		private string _name;

		// Token: 0x04000226 RID: 550
		private string _publicIdentifier;

		// Token: 0x04000227 RID: 551
		private string _systemIdentifier;
	}
}
