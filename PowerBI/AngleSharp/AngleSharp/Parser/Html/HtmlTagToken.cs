using System;
using System.Collections.Generic;

namespace AngleSharp.Parser.Html
{
	// Token: 0x02000075 RID: 117
	public sealed class HtmlTagToken : HtmlToken
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00017024 File Offset: 0x00015224
		public HtmlTagToken(HtmlTokenType type, TextPosition position)
			: this(type, position, string.Empty)
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00017033 File Offset: 0x00015233
		public HtmlTagToken(HtmlTokenType type, TextPosition position, string name)
			: base(type, position, name)
		{
			this._attributes = new List<KeyValuePair<string, string>>();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00017049 File Offset: 0x00015249
		public static HtmlTagToken Open(string name)
		{
			return new HtmlTagToken(HtmlTokenType.StartTag, TextPosition.Empty, name);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00017057 File Offset: 0x00015257
		public static HtmlTagToken Close(string name)
		{
			return new HtmlTagToken(HtmlTokenType.EndTag, TextPosition.Empty, name);
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00017065 File Offset: 0x00015265
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0001706D File Offset: 0x0001526D
		public bool IsSelfClosing
		{
			get
			{
				return this._selfClosing;
			}
			set
			{
				this._selfClosing = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00017076 File Offset: 0x00015276
		public List<KeyValuePair<string, string>> Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001707E File Offset: 0x0001527E
		public void AddAttribute(string name)
		{
			this._attributes.Add(new KeyValuePair<string, string>(name, string.Empty));
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00017096 File Offset: 0x00015296
		public void AddAttribute(string name, string value)
		{
			this._attributes.Add(new KeyValuePair<string, string>(name, value));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000170AC File Offset: 0x000152AC
		public void SetAttributeValue(string value)
		{
			this._attributes[this._attributes.Count - 1] = new KeyValuePair<string, string>(this._attributes[this._attributes.Count - 1].Key, value);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000170F8 File Offset: 0x000152F8
		public string GetAttribute(string name)
		{
			for (int num = 0; num != this._attributes.Count; num++)
			{
				if (this._attributes[num].Key == name)
				{
					return this._attributes[num].Value;
				}
			}
			return string.Empty;
		}

		// Token: 0x040002CD RID: 717
		private readonly List<KeyValuePair<string, string>> _attributes;

		// Token: 0x040002CE RID: 718
		private bool _selfClosing;
	}
}
