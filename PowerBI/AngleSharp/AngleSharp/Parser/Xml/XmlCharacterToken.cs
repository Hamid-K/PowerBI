using System;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000061 RID: 97
	internal sealed class XmlCharacterToken : XmlToken
	{
		// Token: 0x06000227 RID: 551 RVA: 0x0000EBB3 File Offset: 0x0000CDB3
		public XmlCharacterToken(TextPosition position)
			: this(position, string.Empty)
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000EBC1 File Offset: 0x0000CDC1
		public XmlCharacterToken(TextPosition position, string data)
			: base(XmlTokenType.Character, position)
		{
			this._data = data;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000EBD2 File Offset: 0x0000CDD2
		public override bool IsIgnorable
		{
			get
			{
				return this._data.StripLeadingTrailingSpaces().Length == 0;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000EBE7 File Offset: 0x0000CDE7
		public string Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x04000220 RID: 544
		private readonly string _data;
	}
}
